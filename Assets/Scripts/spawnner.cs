using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class spawnner : MonoBehaviour
{
    public GameObject Woodpeaker;
    public GameObject PidgeonRight;
    public GameObject PidgeonLeft;
    [FormerlySerializedAs("_spawnPoints")] public Transform[] SpawnPoints;

    public Transform[] SpawnPointsPidgeons;

    private const float MinDelay = 0.01f;
    private const float MaxDelay = 0.7f;
    
    private const float MinDelayPidgeons = 3f;
    private const float MaxDelayPidgeons = 5f;


    private void Start()
    {
        StartCoroutine(SpawnWoodpeakers());
        StartCoroutine(SpawnPidgeons());
    }

    private IEnumerator SpawnWoodpeakers()
    {
        while (true)
        {
            var delay = Random.Range(MinDelay, MaxDelay);
            yield return new WaitForSeconds(delay);

            var spawnIndex = Random.Range(0, SpawnPoints.Length);
            var spawnPoint = SpawnPoints[spawnIndex];
            
            Instantiate(Woodpeaker, spawnPoint.position, spawnPoint.rotation);
        }
    }

    private IEnumerator SpawnPidgeons()
    {
        while (true)
        {
            var delay = Random.Range(MinDelayPidgeons, MaxDelayPidgeons);
            yield return new WaitForSeconds(delay);

            var spawnIndex = Random.Range(0, SpawnPointsPidgeons.Length);
            var spawnPoint = SpawnPointsPidgeons[spawnIndex];

            Instantiate(PidgeonRight, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
