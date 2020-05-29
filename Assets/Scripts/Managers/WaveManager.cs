using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //Helper class representing spawn points.
    [System.Serializable]
    private class SpawnPoint
    {
        public WaveUnit.enemyTypes type = WaveUnit.enemyTypes.ANYTHING;

        public Transform position;

        public SpawnPoint(WaveUnit.enemyTypes type, Transform position)
        {
            this.type = type;
            this.position = position;
        }
    }

    //Helper class representing enemy units.
    [System.Serializable]
    private class WaveUnit
    {
        public enum enemyTypes { AIR, GROUND, ANYTHING };

        public enemyTypes type = enemyTypes.ANYTHING;

        public GameObject enemyPrefab;

        public int noEnemies;

        public WaveUnit(enemyTypes type, GameObject enemyPrefab, int noEnemies)
        {
            this.type = type;
            this.enemyPrefab = enemyPrefab;
            this.noEnemies = noEnemies;
        }
    }

    //Helper class representing waves.
    [System.Serializable]
    private class Wave
    {
        public string name;

        public List<WaveUnit> units;

        public int rate;

        public Wave(List<WaveUnit> units, int rate, string name)
        {
            this.units = units;
            this.rate = rate;
            this.name = name;
        }
    }

    private enum spawnStates { SPAWNING, WAITING, COUNTING };

    [SerializeField] spawnStates state = spawnStates.COUNTING;

    private Transform enemyContainer;

    [SerializeField] List<SpawnPoint> spawnPoints;
    [SerializeField] List<Wave> waves;

    private int nextWave = 0;

    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] float infiniteMultiplier = 20f;
    private float waveCountdown;

    [SerializeField] bool infinite = true;
    private bool canContinue = false;

    void Start()
    {
        enemyContainer = transform.Find("EnemyContainer").transform;
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if(state == spawnStates.WAITING)
        {
            if(enemyContainer.childCount <= 0f)
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if(waveCountdown <= 0f)
        {
            if(state != spawnStates.SPAWNING)
            {
                StartCoroutine(SpawnWaveBehaviour(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    //Gets called when when wave finishes and takes care of the proceeding actions.
    private void WaveCompleted()
    {
        state = spawnStates.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Count - 1)
        {
            if(infinite)
            {
                foreach(WaveUnit unit in waves[nextWave].units)
                {
                    if(Mathf.RoundToInt(unit.noEnemies * (100 / infiniteMultiplier)) > 1)
                    {
                        unit.noEnemies += Mathf.RoundToInt(unit.noEnemies * (infiniteMultiplier / 100));
                    }
                    else
                    {
                        unit.noEnemies += 1;
                    }
                }
            }
            else
            {
                //this is where the end of the game is i guess
                print("you got to the end of non-infinity, what now?");
            }
        }
        else
        {
            nextWave++;
        }
    }

    //Takes care of spawning a specific wave.
    IEnumerator SpawnWaveBehaviour(Wave wave)
    {
        state = spawnStates.SPAWNING;

        foreach(WaveUnit unit in wave.units)
        {
            StartCoroutine(SpawnUnit(unit, wave.rate));
            
            yield return new WaitUntil(() => canContinue);

            canContinue = false;
        }

        state = spawnStates.WAITING;

        yield break;
    }

    //Gets called by the wave spawn behaviour. It takes care of spawning one specific unit.
    IEnumerator SpawnUnit(WaveUnit unit, int rate)
    {
        NetworkController networkController = FindObjectOfType<NetworkController>();

        List<SpawnPoint> availableSpawnPoint = new List<SpawnPoint>();

        if (unit.type == WaveUnit.enemyTypes.ANYTHING)
        {
            availableSpawnPoint = spawnPoints;
        }
        else
        {
            foreach (SpawnPoint point in spawnPoints)
            {
                if (point.type == unit.type || point.type == WaveUnit.enemyTypes.ANYTHING)
                {
                    availableSpawnPoint.Add(point);
                }
            }
        }

        for(int i = 0; i < unit.noEnemies; i++)
        {
            SpawnPoint spawnPoint = availableSpawnPoint[Random.Range(0, availableSpawnPoint.Count)];
            Vector3 spawnPos = new Vector3(spawnPoint.position.position.x + Random.Range(4f, 6f), spawnPoint.position.position.y, spawnPoint.position.position.z + Random.Range(4f, 6f));
            
            networkController.NetworkSpawn(unit.enemyPrefab.name, spawnPos, Quaternion.identity, Vector3.zero);

            yield return new WaitForSeconds(1f / rate);
        }

        canContinue = true;
    }
}