using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    private class WaveUnit
    {
        private GameObject enemyPrefab;

        private int noEnemies;

        public WaveUnit(GameObject enemyPrefab, int noEnemies)
        {
            this.enemyPrefab = enemyPrefab;
            this.noEnemies = noEnemies;
        }
    }

    [System.Serializable]
    private class Wave
    {
        private ArrayList<WaveUnit> units;

        private int rate;

        private string name;

        public Wave(ArrayList<WaveUnit> units, int rate, string name)
        {
            this.units = units;
            this.rate = rate;
            this.name = name;
        }
    }

    private enum spawnStates { SPAWNING, WAITING, COUNTING };

    [SerializeField] spawnStates state = spawnStates.COUNTING;

    private ArrayList<Wave> waves;

    private int nextWave = 0;

    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] float waveCountdown;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if(waveCountdown <= 0f)
        {
            if(state != spawnStates.SPAWNING)
            {
                
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }
}
