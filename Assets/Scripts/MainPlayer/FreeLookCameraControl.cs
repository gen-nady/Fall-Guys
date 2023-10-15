using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MainPlayer
{
    public class FreeLookCameraControl : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private CinemachineFreeLook _camera;
        [SerializeField] private Image _controlRotateArea;
        private const string MOUSE_X = "Mouse X";
        private const string MOUSE_Y = "Mouse Y";
        
        public void OnDrag(PointerEventData eventData)
        {
#if UNITY_ANDROID && !UNITY_2023
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_controlRotateArea.rectTransform,
                    eventData.position, eventData.enterEventCamera, out var posOut))
            {
                _camera.m_XAxis.m_InputAxisName = MOUSE_X;
                _camera.m_YAxis.m_InputAxisName = MOUSE_Y;
            }
#endif
        }

        public void OnPointerDown(PointerEventData eventData)
        {
#if UNITY_ANDROID && !UNITY_2023
            OnDrag(eventData);
#endif
        }

        public void OnPointerUp(PointerEventData eventData)
        {
#if UNITY_ANDROID && !UNITY_2023
            _camera.m_XAxis.m_InputAxisName = null;
            _camera.m_YAxis.m_InputAxisName = null;
            _camera.m_XAxis.m_InputAxisValue = 0;
            _camera.m_YAxis.m_InputAxisValue = 0;
#endif
        }
    }
}
