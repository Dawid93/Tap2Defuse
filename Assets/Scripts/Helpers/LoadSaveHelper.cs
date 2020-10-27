using UnityEngine;

namespace TapToDefuse.Helpers
{
    public static class LoadSaveHelper
    {
        private const string HighScoreKey = "HScore";

        public static void SaveNewHighScore(int score)
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
        }

        public static int LoadHighScore(int score)
        {
            return PlayerPrefs.GetInt(HighScoreKey, 0);
        }
    }
}