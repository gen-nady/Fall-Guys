using UnityEngine;

namespace MainPlayer.Controllers
{
    public class KeyboardController : IInputController
    {
        private const string HORIZONTAL = "Horizontal";
        private const string VERTICAL = "Vertical";
        private const KeyCode SPACE = KeyCode.Space;

        public Vector3 GetAxis() => new Vector3(Input.GetAxis(HORIZONTAL), 0f, Input.GetAxis(VERTICAL));
        
        public bool GetJump() => Input.GetKeyDown(SPACE);
    }
}