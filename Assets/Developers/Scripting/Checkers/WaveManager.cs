using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Enemy
    {
        public GameObject checker;
        public int spawnpoint;
        public float waitTime;
    }

    [System.Serializable]
    public class Wave
    {
        public List<Enemy> checkers;
        public float waitTime;
        public int moneyGainedFromWave;
    }

    [System.Serializable]
    public class Level
    {
        public List<Vector3> spawnpoints;
        public List<Wave> waves;
    }

    [SerializeField] public Level level;

    public bool hasSpawnedEverything = false;

    private DataManager _dataManager;
    private List<GameObject> newestChecker = new List<GameObject>();

    private void Start()
    {
        _dataManager = DataManager.GetInstance();
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        for (int i = 0; i < level.waves.Count; i++)
        {
            yield return new WaitForSeconds(level.waves[i].waitTime);

            _dataManager.Money += level.waves[i].moneyGainedFromWave;

            for (int j = 0; j < level.waves[i].checkers.Count; j++)
            {
                Enemy currentChecker = level.waves[i].checkers[j];
                yield return new WaitForSeconds(currentChecker.waitTime);
                newestChecker.Add(Instantiate(currentChecker.checker, level.spawnpoints[currentChecker.spawnpoint], Quaternion.identity));
            }
        }
        
        bool hasBeatenLevel = false;

        while (!hasBeatenLevel)
        {
            bool completeCheck = true;

            for (int i = 0; i < newestChecker.Count; i++)
            {
                if(newestChecker[i] != null)
                {
                    completeCheck = false;
                    break;
                }
            }

            hasBeatenLevel = completeCheck;

            yield return new WaitForSeconds(1f);
        }

        yield return null;

        hasSpawnedEverything = true;
    }
}
