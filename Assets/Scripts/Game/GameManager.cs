using System;
using TapToDefuse.UI;
using UnityEngine;

namespace TapToDefuse.Game
{
    public class GameManager : MonoBehaviour
    {
        public event Action OnFinishGame;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<GameManager>();
                return _instance;
            }
        }
        private static GameManager _instance;

        [SerializeField] private EndScreenView endScreenView;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            
            endScreenView.gameObject.SetActive(false);
        }

        public void GameOver()
        {
            OnFinishGame?.Invoke();
            endScreenView.gameObject.SetActive(true);
            endScreenView.ShowView();
        }
    }
}
