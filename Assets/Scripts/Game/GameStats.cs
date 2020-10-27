using System;
using UnityEngine;

namespace TapToDefuse.Game
{
    public class GameStats : MonoBehaviour
    {
        public event Action<int> OnBombDefuseChange;
        public event Action<int> OnPointsChange;

        public static GameStats Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<GameStats>();
                return _instance;
            }
        }
        private static GameStats _instance;

        public int DefusedBombs
        {
            get => _defusedBombs;
            set
            {
                _defusedBombs = value;
                Points = _defusedBombs;
                OnBombDefuseChange?.Invoke(_defusedBombs);
            }
        }
        public int Points
        {
            get => _points;
            set
            {
                _points = value * userPointsMultiplier;
                OnPointsChange?.Invoke(_points);
            }
        }
        public float Time { get; private set; }

        [SerializeField] private int userPointsMultiplier = 100;

        private int _defusedBombs;
        private int _points;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            
            _points = 0;
            _defusedBombs = 0;
        }

        public void SetGameTime(float time)
        {
            Time = time;
        }
    }
}