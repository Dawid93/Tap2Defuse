using System;
using TapToDefuse.Helpers;
using TapToDefuse.ObjectPool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TapToDefuse.Game
{
    public class BombSpawner : MonoBehaviour
    {
        [SerializeField] private RectTransform spawnParent;
        [SerializeField] private SpawnSettings spawnSettings;
        [SerializeField] private int pointsToUpdateSettings = 5;

        private float _currentMinTimeToExplode;
        private float _currentMaxTimeToExplode;
        private float _currentTimeToNextSpawn;
        

        private float _currentTime = 0;

        private void Start()
        {
            UpdateSettings(0);
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _currentTimeToNextSpawn)
            {
                _currentTime = 0;
                SpawnBomb();
            }
                
        }

        private void SpawnBomb()
        {
            float spawnChance = Random.Range(0f, 1f);
            
            if (spawnChance > .85f)
                ObjectPooler.Instance.GetFromPool(PoolTagHelper.AvoidBombTag, new Vector3(500, 200, 0),
                Quaternion.identity, spawnParent);
            else
            {
                
            }
        }

        private void UpdateSettings(float points)
        {
            points = Mathf.Clamp(points, 0, spawnSettings.FinalPointsSettings);
            
            _currentMinTimeToExplode = spawnSettings.MinTimeToExplodeOverThePoints.Evaluate(points);
            _currentMaxTimeToExplode = spawnSettings.MaxTimeToExplodeOverThePoints.Evaluate(points);
            _currentTimeToNextSpawn = spawnSettings.TimeSpawnOverThePoints.Evaluate(points);
        }
    }
}
