using UnityEngine;

namespace TapToDefuse.Game
{
    [CreateAssetMenu(fileName = "SpawnSettings", menuName = "SpawnSettings", order = 0)]
    public class SpawnSettings : ScriptableObject
    {
        public int FinalPointsSettings => finalPointsSettings;
        public AnimationCurve TimeSpawnOverThePoints => timeSpawnOverThePoints;
        public AnimationCurve MinTimeToExplodeOverThePoints => minTimeToExplodeOverThePoints;
        public AnimationCurve MaxTimeToExplodeOverThePoints => maxTimeToExplodeOverThePoints;

        [SerializeField] private int finalPointsSettings = 100;
        [SerializeField] private AnimationCurve timeSpawnOverThePoints;
        [SerializeField] private AnimationCurve minTimeToExplodeOverThePoints;
        [SerializeField] private AnimationCurve maxTimeToExplodeOverThePoints;
    }
}