using System;
using Infastructure;
using MainPlayer;
using TMPro;
using UnityEngine;

namespace Levels
{
    public class ChangeLevelUI : MonoBehaviour
    {
        [SerializeField] private GameObject _restartPanel;
        [SerializeField] private GameObject _nextLevelPanel;
        [SerializeField] private TextMeshProUGUI _winTimeText;

        private bool _isTimerStarted;
        private float _timer;

        #region MONO

        private void OnEnable()
        {
            Finish.IsFinished += OpenNextLevelPanel;
            PlayerHealth.IsRestarted += OpenRestartPanel;
            Restart.IsRestarted += OpenRestartPanel;
            GameSettings.OnLevelChanged += ClosePanels;
            Start.IsStarted += StartTimer;
        }

        private void OnDisable()
        {
            Finish.IsFinished -= OpenNextLevelPanel;
            Restart.IsRestarted -= OpenRestartPanel;
            PlayerHealth.IsRestarted -= OpenRestartPanel;
            GameSettings.OnLevelChanged -= ClosePanels;
            Start.IsStarted -= StartTimer;
        }

        private void Update()
        {
            if (_isTimerStarted)
                _timer += Time.deltaTime;
        }

        #endregion
        
        private void OpenRestartPanel()
        {
            _restartPanel.SetActive(true);
            _isTimerStarted = false;
        }

        private void OpenNextLevelPanel()
        {
            _nextLevelPanel.SetActive(true);
            _isTimerStarted = false;
            _winTimeText.text = $"{_timer:F2}";
        }

        private void StartTimer()
        {
            _isTimerStarted = true;
        }
        
        private void ClosePanels()
        {
            _isTimerStarted = false;
            _timer = 0f;
            
            _restartPanel.SetActive(false);
            _nextLevelPanel.SetActive(false);
        }
    }
}