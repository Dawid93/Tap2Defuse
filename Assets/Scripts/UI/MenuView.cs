using System;
using TapToDefuse.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TapToDefuse.UI
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI highScore;

        private void Start()
        {
            highScore.SetText($"{LoadSaveHelper.LoadHighScore()}");
        }

        public void StartGame()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
