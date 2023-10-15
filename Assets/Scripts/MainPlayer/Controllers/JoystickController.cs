using UnityEngine;
using UnityEngine.UI;
using Task = System.Threading.Tasks.Task;

namespace MainPlayer.Controllers
{
    public class JoystickController : IInputController
    {
        private readonly FloatingJoystick _joystick;
        private bool _isButtonJumpClicked;
        
        public JoystickController(FloatingJoystick joystick, Button jumpButton)
        {
            _joystick = joystick;
            _joystick.gameObject.SetActive(true);
            jumpButton.onClick.AddListener(OnClickedEvent);
            jumpButton.gameObject.SetActive(true);
        }

        public Vector3 GetAxis() => new Vector3(_joystick.Direction.x, 0f, _joystick.Direction.y);

        public bool GetJump() => _isButtonJumpClicked;

        private async void OnClickedEvent()
        {
            _isButtonJumpClicked = true;
            await Task.Delay((int)(Time.deltaTime * 1000));
            _isButtonJumpClicked = false;
        }
    }
}