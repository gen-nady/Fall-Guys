using UnityEngine;

namespace MainPlayer.Controllers
{
    public interface IInputController
    {
        Vector3 GetAxis();
        bool GetJump();
    }
}