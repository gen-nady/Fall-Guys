using System;
using System.Collections.Generic;
using Levels;
using MainPlayer;
using UnityEngine;
using Application = UnityEngine.Application;

namespace Infastructure
{
    public class GameSettings : MonoBehaviour
    {
        public static event Action OnLevelChanged;
        public static event Action<Vector3> OnSetPosition;

        [SerializeField] private List<LevelInfo> _levels; 
        [SerializeField] private int _targetFrameRate;

        private LevelsLoader _levelsLoader;
        //В эту переменную можно реализовать подгрузку с сохранений любого уровня. На данный момент по дефолту 0
        private int _saveLevel = 0;

        #region MONO

        private void Awake()
        {
            Application.targetFrameRate = _targetFrameRate;

            _levelsLoader = new LevelsLoader(_levels);
            var level = _levelsLoader.ChangeLevel(_saveLevel);
            OnSetPosition?.Invoke(level.StartPosition);
            OnLevelChanged?.Invoke();
#if !UNITY_ANDROID || UNITY_2023
            HideCursor();
#endif
        }

        private void OnEnable()
        {
            Finish.IsFinished += VisibleCursor;
            Restart.IsRestarted += VisibleCursor;
            PlayerHealth.IsRestarted += VisibleCursor;
        }

        private void OnDisable()
        {
            Finish.IsFinished -= VisibleCursor;
            Restart.IsRestarted -= VisibleCursor;
            PlayerHealth.IsRestarted -= VisibleCursor;
        }

        #endregion
        
        private void VisibleCursor()
        {
            Cursor.visible = true;
            Cursor.lockState =  CursorLockMode.None;
        }
        
        private void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState =  CursorLockMode.Locked;
        }
        
        public void RestartLevel()
        {
            var level = _levelsLoader.RestartLevel();
            OnLevelChanged?.Invoke();
            OnSetPosition?.Invoke(level.StartPosition);
            HideCursor();
        }
        
        public void NextLevel()
        {
            var level = _levelsLoader.ChangeLevel(++_saveLevel);
            OnLevelChanged?.Invoke();
            OnSetPosition?.Invoke(level.StartPosition);
            HideCursor();
        }
    }
}