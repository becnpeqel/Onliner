using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 2.4f;
    [SerializeField] private float _runSpeed = 5.0f;
    [SerializeField] private float _rotationSpeed = 6.0f;
    private float _speed;
    private Vector3 _velocity = new Vector3();
    private float _gravity = -9.81f;
    private Animator _annimator;

    private CharacterController _characterController;
    private Vector2 _movementVector;

    private PlayerControls _input;
    private bool _isRunning;

    private PhotonView _photonView;
    private CameraController _cameraController; 
    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        if (_photonView.IsMine)
        {
            _cameraController = FindObjectOfType<CameraController>();
            _cameraController.SetTarger(transform);
        }
        _annimator = GetComponent<Animator>();
        _input = new PlayerControls();
        _characterController = GetComponent<CharacterController>();
        _input.KeyboardContols.Movement.started += OnMoveInput;
        _input.KeyboardContols.Movement.canceled += OnMoveInput;
        _input.KeyboardContols.Movement.performed += OnMoveInput;
        _input.KeyboardContols.Run.started += OnRunInput;
        _input.KeyboardContols.Run.canceled += OnRunInput;
    }
    private void OnEnable()
    {
        _input.Enable();

    }
    private void OnDisable()
    {
        _input.Disable();
    }


    private void Update()
    {
        if (_photonView.IsMine)
        {
            Move();
            Rotate();
        }

    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        _movementVector = context.ReadValue<Vector2>();
    }
    private void OnRunInput(InputAction.CallbackContext context)
    {
        _isRunning = context.ReadValueAsButton();
    }
    private void Move()
    {

        _speed = _isRunning ? _runSpeed : _walkSpeed;

        Vector3 _desiredVelocity = new Vector3(_movementVector.x * _speed, _velocity.y + _gravity * Time.deltaTime, _movementVector.y * _speed) * Time.deltaTime;
        _velocity = Vector3.Lerp(_velocity, _desiredVelocity, Time.deltaTime * 6);
        float speedParametr = (_movementVector * _speed).magnitude;
        _annimator.SetFloat("Speed", speedParametr);
        _velocity.y = _characterController.isGrounded ? 0 : _velocity.y;
        _characterController.Move(_velocity);

    }
    private void Rotate()
    {
        if (_movementVector.magnitude == 0)
            return;

        Vector3 lookDirection = new Vector3(_velocity.x, 0, _velocity.z);
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        Quaternion rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        transform.rotation = rotation;
    }
}
