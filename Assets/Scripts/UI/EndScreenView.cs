using TapToDefuse.Game;
using TapToDefuse.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TapToDefuse.UI
{
    public class EndScreenView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI points;
        [SerializeField] private TextMeshProUGUI time;
        [SerializeField] private TextMeshProUGUI highScore;

        private bool _showHighScore;
        
        public void ShowView()
        {
            int currentScore = GameStats.Instance.Points;
            _showHighScore = currentScore > LoadSaveHelper.LoadHighScore();
            if(_showHighScore)
                LoadSaveHelper.SaveNewHighScore(currentScore);
            highScore.gameObject.SetActive(_showHighScore);

            points.SetText($"Points: {currentScore}");
            time.SetText($"Time: {GameStats.Instance.Time}");
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}
