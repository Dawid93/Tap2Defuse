using System;
using TapToDefuse.Helpers;
using TapToDefuse.ObjectPool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TapToDefuse.Game
{
    public class BombSpawner : MonoBehaviour
    {
        [Header("Difficulty settings")]
        [SerializeField] private bool useDifficultyBasedOnScore;
        [SerializeField] private SpawnSettings spawnSettings;
        [Header("For score based difficulty")]
        [SerializeField] private int pointsToUpdateSettings = 5;
        [Header("For time based difficulty")]
        [SerializeField] private int timeToUpdateSettings = 5;
        
        [Header("References")]
        [SerializeField] private RectTransform spawnParent;
        [SerializeField] private BombCellsCreator creator;

        private BombCell[] _cells;
        
        private float _currentMinTimeToExplode;
        private float _currentMaxTimeToExplode;
        private float _currentTimeToNextSpawn;
        
        private float _currentTime = 0;
        private float _currentTimeToUpdateSettings = 0;
        private float _totalGameTime;
        private bool _spawnBombs = false;
        private bool _canUpdateSettings;

        private void Awake()
        {
            if(useDifficultyBasedOnScore)
                GameStats.Instance.OnBombDefuseChange += HandleOnPointsChange;
            GameManager.Instance.OnFinishGame += HandleGameOver;
        }

        private void HandleGameOver()
        {
            _spawnBombs = false;
        }

        private void Start()
        {
            creator.PrepareBoard((bombCells) =>
            {
                _cells = bombCells;
                _spawnBombs = true;
            });
            UpdateSettings(0);
        }

        private void Update()
        {
            if(!_spawnBombs) return;
            
            _currentTime += Time.deltaTime;
            _currentTimeToUpdateSettings += Time.deltaTime;
            _totalGameTime += Time.deltaTime;
            
            if (_currentTime >= _currentTimeToNextSpawn)
            {
                _currentTime = 0;
                SpawnBomb();
            }

            if (!useDifficultyBasedOnScore && _currentTimeToUpdateSettings >= timeToUpdateSettings)
            {
                _currentTimeToUpdateSettings = 0;
                UpdateSettings(_totalGameTime);
            }
        }

        private void SpawnBomb()
        {
            float spawnChance = Random.Range(0f, 1f);
            TapBombSettings tbs = new TapBombSettings();
            
            BombCell targetCell = RandomCell();
            Vector3 pos = targetCell.RectTransform.localPosition;
            tbs.BombCell = targetCell;
            
            if (spawnChance > .85f)
                ObjectPooler.Instance.GetFromPool(PoolTagHelper.AvoidBombTag, pos,
                Quaternion.identity, spawnParent, tbs);
            else
            {
                tbs.TimeToExplode = Random.Range(_currentMinTimeToExplode, _currentMaxTimeToExplode);

                ObjectPooler.Instance.GetFromPool(PoolTagHelper.DefuseBombTag, pos, Quaternion.identity, spawnParent, tbs);
            }
        }

        private BombCell RandomCell()
        {
            BombCell randCell = null;
            while (randCell == null)
            {
                var tempCell = _cells[Random.Range(0, _cells.Length)];
                if (tempCell.IsAvailable)
                    randCell = tempCell;
            }

            return randCell;
        }

        private void HandleOnPointsChange(int points)
        {
            if(points % pointsToUpdateSettings != 0)
                return;
            UpdateSettings(points);
        }

        private void UpdateSettings(float points)
        {
            Debug.Log("Update");   
            points = Mathf.Clamp(points, 0, spawnSettings.FinalPointsSettings);
            
            _currentMinTimeToExplode = spawnSettings.MinTimeToExplodeOverThePoints.Evaluate(points);
            _currentMaxTimeToExplode = spawnSettings.MaxTimeToExplodeOverThePoints.Evaluate(points);
            _currentTimeToNextSpawn = spawnSettings.TimeSpawnOverThePoints.Evaluate(points);
        }
    }
}
