using System;
using UnityEngine;

namespace TapToDefuse.Game
{
    public class GameStats : MonoBehaviour
    {
        public event Action<int> OnBombDefuseChange;
        public event Action<int> OnPointsChange;

        public int DefusedBombs
        {
            get => _defusedBombs;
            set
            {
                _defusedBombs = value;
                OnBombDefuseChange?.Invoke(_defusedBombs);
            }
        }
        public int Points
        {
            get => _points;
            set
            {
                _points = value;
                OnPointsChange?.Invoke(_points);
            }
        }

        private int _defusedBombs;
        private int _points;
        
        private void Start()
        {
            _points = 0;
            _defusedBombs = 0;
        }
    }
}