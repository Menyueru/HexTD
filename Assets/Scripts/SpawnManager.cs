using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace HexTD
{
    [Serializable]
    public class WaveMob
    {
        public GameObject MobPrefab;
        public float offsetSpawn;
    }

    [Serializable]
    public class Wave
    {
        public List<WaveMob> mobs;
        public int reward=0;
    }

    public class SpawnManager : MonoBehaviour
    {
        private float spawnStart = 0;
        private bool spawning=false;

        public int currentWaveNumber=0;
        public List<Wave> waves;
        public Text WaveText;
        public Wave CurrentWave;

        private BoardHandle board;

        private int mobsSpawned = 0;

        public void StartSpawn()
        {
            if (!spawning)
            {
                mobsSpawned = 0;
                spawning = true;
                spawnStart = Time.time;
                var repeats = currentWaveNumber / waves.Count;
                var lastWave = waves.Last();
                for (var i = 0; i < repeats; i++)
                {
                    CurrentWave.mobs.AddRange(lastWave.mobs);
                    CurrentWave.reward += lastWave.reward;
                }
                var nextWave = waves[currentWaveNumber % waves.Count];
                CurrentWave.mobs.AddRange(nextWave.mobs);
                CurrentWave.reward += nextWave.reward;
            }
        }

        // Use this for initialization
        void Start()
        {
            board = GameObject.Find("Board").GetComponent<BoardHandle>();
        }

        // Update is called once per frame
        void Update()
        {
            WaveText.text = (currentWaveNumber+1).ToString();
            if (spawning)
            {
                var timeInterval = Time.time - spawnStart;
                var mobs = CurrentWave.mobs.Where(x => timeInterval >= x.offsetSpawn).ToList();

                foreach (var mob in mobs)
                {
                    mobsSpawned++;
                    Instantiate(mob.MobPrefab,transform);
                    CurrentWave.mobs.Remove(mob);
                }
                if (CurrentWave.mobs.Count==0 && GameObject.FindGameObjectWithTag("Mob")==null)
                {
                    board.currency += CurrentWave.reward;
                    spawning = false;
                    currentWaveNumber++;
                }
            }
        }
    }
}