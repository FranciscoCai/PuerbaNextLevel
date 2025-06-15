using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterInputLogic : MonoBehaviour
{
    public StartedInput _input; // Asigna este campo en el inspector si es necesario
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float lookSensitivity = 2f;
    public Transform cameraTransform; // Asigna la cámara en el inspector

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float cameraPitch = 0f;

    [SerializeField] private float projectileVelocity = 20f;
    [SerializeField] private float shootableObjectVelocity = 5f;

    [SerializeField] private ProjectileWell projectileWell;

    private Vector3 RespawnPosition;
    private Quaternion RespawnRotation;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        if (_input == null)
            _input = GetComponent<StartedInput>();
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        // Oculta y bloquea el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SetSpawnPoint(transform.position, transform.rotation);
    }
    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener<int>("Respawn", Respawn);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener<int>("Respawn", Respawn);
    }
    public void Respawn(int num)
    {
        transform.position = RespawnPosition;
        transform.rotation = RespawnRotation;
        Physics.SyncTransforms(); 
    }
    private void FixedUpdate()
    {
        isGrounded = controller.isGrounded;

        // Si está en el suelo y está cayendo, resetea la velocidad vertical
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // Movimiento horizontal
        Vector3 move = new Vector3(_input.move.x, 0, _input.move.z);
        Vector3 moveWorld = transform.TransformDirection(move) * moveSpeed;

        // Salto
        if (_input.jump && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravedad SIEMPRE
        velocity.y += gravity * Time.deltaTime;

        // Movimiento total (horizontal + vertical)
        Vector3 totalMove = moveWorld * Time.deltaTime;
        totalMove.y = velocity.y * Time.deltaTime;
        controller.Move(totalMove);

        // Rotación de cámara y jugador
        RotateView();
        ShootLeft();
        ShootRight();
        ChangeShotableObject();
    }

    private void RotateView()
    {
        Vector2 look = _input.look * lookSensitivity;

        // Rotar el jugador en el eje Y (yaw)
        transform.Rotate(Vector3.up * look.x);

        // Rotar la camara en el eje X (pitch)
        cameraPitch -= look.y;
        cameraPitch = Mathf.Clamp(cameraPitch, -55f, 55f);
        if (cameraTransform != null)
            cameraTransform.localEulerAngles = new Vector3(cameraPitch, 0f, 0f);
    }
    private void ShootLeft()
    {
        if(_input.shootLeft)
        {
            _input.shootLeft = false; // Reset the shoot input to prevent multiple shots in one frame
            if (projectileWell != null)
            {
                projectileWell.SpawnAProyectile(projectileVelocity);
            }
        }
    }
    private void ShootRight()
    {
        if (_input.shootRight)
        {
            _input.shootRight = false; // Reset the shoot input to prevent multiple shots in one frame
            if (ObjectManager.instance != null)
            {
                var objectList = ObjectManager.instance.ReturnObjectList(); 
                if (objectList.Count > 0)
                {
                    bool isSpawn = projectileWell.SpawnAObject(shootableObjectVelocity,ObjectManager.instance.GetShootableObject());
                    if (!isSpawn)
                    {
                        return;
                    }
                    ObjectManager.instance.RemoveObject(ObjectManager.instance.GetShootableObject());
                    ObjectManager.instance.ChangeObjectIndex(0);
                }
            }
        }
    }
    private void ChangeShotableObject()
    {
        if(_input.scroll != 0)
        {
            ObjectManager.instance.ChangeObjectIndex(_input.scroll > 0 ? 1 : -1);
            _input.scroll = 0; // Reset the scroll input to prevent multiple changes in one frame
        }
    }
    public void SetSpawnPoint(Vector3 position, Quaternion rotation)
    {
        RespawnPosition = position;
        RespawnRotation = rotation;
    }
}
