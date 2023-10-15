using Infastructure;
using MainPlayer.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace MainPlayer
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("InputController")] 
        [SerializeField] private FloatingJoystick _joystick;
        [SerializeField] private Button _jumpButton;
      
        [Header("Moving")]
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Transform _transform;
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSmoothTime;
        private float _speedModifer = 1f;
        private float _currentAngle; 
        private float _currentAngleVelocity;
        
        [Header("Gravity")]
        [SerializeField] private float _gravity = 9.8f;
        [SerializeField] private float _gravityMultiplier = 2;
        [SerializeField] private float _groundedGravity = -0.5f;
        [SerializeField] private float _jumpHeight = 3f;
        private float _velocityY = 0f;

        private IInputController _inputController;
        
        #region MONO

        private void Awake()
        {
#if UNITY_ANDROID && !UNITY_2023
            _inputController = new JoystickController(_joystick, _jumpButton);
#else
            _inputController = new KeyboardController();
#endif
            _speedModifer = 1f;
        }

        private void OnEnable()
        {
            GameSettings.OnSetPosition += SetPosition;
        }

        private void OnDisable()
        {
            GameSettings.OnSetPosition -= SetPosition;
        }

        private void Update()
        {
            Move();
            Gravity();
            Jump();
        }

        #endregion
     
        private void Move()
        {
            var movement = _inputController.GetAxis();
            if (movement.magnitude >= 0.1f) 
            {     
                var targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
                _currentAngle = Mathf.SmoothDampAngle(_currentAngle, targetAngle, ref _currentAngleVelocity, _rotationSmoothTime);
                transform.rotation = Quaternion.Euler(0, _currentAngle, 0);
                var rotatedMovement = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                _characterController.Move(rotatedMovement * _speedModifer * _speed * Time.deltaTime);
            }
        }

        private void Jump()
        {
            if (_characterController.isGrounded && _inputController.GetJump())
                _velocityY = Mathf.Sqrt(_jumpHeight * 2f * _speedModifer * _gravity);
        }
        
        private void Gravity()
        {
            if (_characterController.isGrounded && _velocityY < 0f)
                _velocityY = _groundedGravity;
          
            _velocityY -= _gravity * _gravityMultiplier * Time.deltaTime;
            _characterController.Move(Vector3.up * _velocityY * Time.deltaTime);
        }

        public void SetSpeedModifer(float modifer)
        {
            _speedModifer = modifer;
        }
        
        private void SetPosition(Vector3 startPosition)
        {
            _characterController.enabled = false;
            _transform.position = startPosition;
            _characterController.enabled = true;
        }
    }
}