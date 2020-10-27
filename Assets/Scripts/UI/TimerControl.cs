using System;
using TMPro;
using UnityEngine;

namespace TapToDefuse.UI
{
    public class TimerControl : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmpPro;

        private void Update()
        {
            tmpPro.SetText($"Time: {Time.time:F2}");
        }
    }
}
