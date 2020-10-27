using System;
using TapToDefuse.Game;
using TMPro;
using UnityEngine;

namespace TapToDefuse.UI
{
    public class TimerControl : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmpPro;

        private bool _updateTimer;
        private float _gameTime;
        
        private void Awake()
        {
            GameManager.Instance.OnFinishGame += HandleGameOver;
        }

        private void Start()
        {
            _updateTimer = true;
        }

        private void HandleGameOver()
        {
            _updateTimer = false;
            GameStats.Instance.SetGameTime(_gameTime);
        }

        private void Update()
        {
            if(!_updateTimer) return;

            _gameTime = Time.time;
            tmpPro.SetText($"Time: {_gameTime:F2}");
        }
    }
}
