using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    private class WaveUnit
    {
        public GameObject enemyPrefab;

        public int noEnemies;

        public WaveUnit(GameObject enemyPrefab, int noEnemies)
        {
            this.enemyPrefab = enemyPrefab;
            this.noEnemies = noEnemies;
        }
    }

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

    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] List<Wave> waves;

    private int nextWave = 0;

    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] float infiniteMultiplier = 20f;
    private float waveCountdown;

    [SerializeField] bool infinite = true;
    private bool canContinue = false;

    void Start()
    {
        enemyContainer = GameObject.Find("EnemyContainer").transform;
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

    IEnumerator SpawnUnit(WaveUnit unit, int rate)
    {
        for(int i = 0; i < unit.noEnemies; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            Vector3 spawnPos = new Vector3(spawnPoint.position.x + Random.Range(3f, 6f), spawnPoint.position.y, spawnPoint.position.z + Random.Range(3f, 6f));
            Instantiate(unit.enemyPrefab, spawnPos, Quaternion.identity, enemyContainer);
            
            yield return new WaitForSeconds(1f / rate);
        }

        canContinue = true;
    }
}