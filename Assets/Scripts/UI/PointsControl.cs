using System;
using TapToDefuse.Game;
using TMPro;
using UnityEngine;

namespace TapToDefuse.UI
{
    public class PointsControl : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmpPro;

        private void Awake()
        {
            GameStats.Instance.OnPointsChange += UpdatePoints;
        }

        private void UpdatePoints(int points)
        {
            tmpPro.SetText($"Points: {points}");
        }
    }
}