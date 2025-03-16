using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform _trailTransform;
    private bool _isAiming;
    private Camera _camera;
    [SerializeField] private GameObject _airJordan;
    private PlayerControls _playerControls;
    [SerializeField] private Transform _bulletOrigin;
    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();


        if (_photonView.IsMine)
        {
            _playerControls = new PlayerControls();
            _playerControls.KeyboardContols.Aim.started += OnAimChanged;
            _playerControls.KeyboardContols.Aim.canceled += OnAimChanged;
            _playerControls.KeyboardContols.Fire.started += OnShoot;
        }


    }
    private void Start()
    {
        _camera = Camera.main;
    }
    private void OnEnable()
    {
        _playerControls.Enable();
    }
    private void OnDisable()
    {
        _playerControls?.Disable();
    }

    //Это Ванюша если что.
    private void LateUpdate()
    {
        Aim();
    }

    private void OnAimChanged(InputAction.CallbackContext context)
    {
        _isAiming = context.ReadValueAsButton();
        _trailTransform.gameObject.SetActive(_isAiming);
    }
    private void OnShoot(InputAction.CallbackContext context)
    {
        Shoot();
    }
    private void Aim()
    {
        if (_isAiming)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Vector3 mousePos = hitInfo.point;
                Vector3 direction = mousePos - transform.position;
                direction = new Vector3(direction.x, 0, direction.z);
                _trailTransform.forward = direction;
            }
            RaycastHit[] raycastHits  =   Physics.RaycastAll(_trailTransform.position + _trailTransform.forward,  _trailTransform.forward, 8);

            foreach(RaycastHit hit in raycastHits)
            {
                if (hit.collider.TryGetComponent(out TargetMark targetMark))
                {
                    targetMark.isTarget = true;
                }
            }
        }
    }
    private void Shoot()
    {
        if (_isAiming)
        {
            GameObject bullet = PhotonNetwork.Instantiate("Bullet", _bulletOrigin.position, Quaternion.identity);
            Bullet script = bullet.GetComponent<Bullet>();
            script.SetDirection(_trailTransform.forward, gameObject);
        }
    }
}