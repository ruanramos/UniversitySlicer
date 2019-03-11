using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class spawnner : MonoBehaviour
{
    public static bool SpawnnerActive;
    
    public GameObject Woodpeaker;
    [FormerlySerializedAs("PidgeonRight")] public GameObject Pidgeon;
    public GameObject Unilol;
    [FormerlySerializedAs("_spawnPoints")] public Transform[] SpawnPoints;

    public Transform[] SpawnPointsPidgeons;

    private const float MinDelay = 0.005f;
    private const float MaxDelay = 0.3f;
    
    private const float MinDelayPidgeons = 2f;
    private const float MaxDelayPidgeons = 4f;
    
    private const float MinDelayUnilol = 10f;
    private const float MaxDelayUnilol = 25f;


    private void Start()
    {
        SpawnnerActive = true;
        StartCoroutine(SpawnWoodpeakers());
        StartCoroutine(SpawnPidgeons());
        StartCoroutine(SpawnUnilol());
    }

    private void OnEnable()
    {
        SpawnnerActive = true;
        StartCoroutine(SpawnWoodpeakers());
        StartCoroutine(SpawnPidgeons());
        StartCoroutine(SpawnUnilol());
    }

    private IEnumerator SpawnWoodpeakers()
    {
        while (SpawnnerActive)
        {
            var delay = Random.Range(MinDelay, MaxDelay);
            yield return new WaitForSeconds(delay);

            var spawnIndex = Random.Range(0, SpawnPoints.Length);
            var spawnPoint = SpawnPoints[spawnIndex];
            
            var go = Instantiate(Woodpeaker, spawnPoint.position, spawnPoint.rotation);
            Destroy(go, 6f);
        }
    }

    private IEnumerator SpawnPidgeons()
    {
        while (SpawnnerActive)
        {
            var delay = Random.Range(MinDelayPidgeons, MaxDelayPidgeons);
            yield return new WaitForSeconds(delay);

            var spawnIndex = Random.Range(0, SpawnPointsPidgeons.Length);
            var spawnPoint = SpawnPointsPidgeons[spawnIndex];

            var go = Instantiate(Pidgeon, spawnPoint.position, spawnPoint.rotation);
            Destroy(go, 9f);
        }
    }

    private IEnumerator SpawnUnilol()
    {
        while (SpawnnerActive)
        {
            var delay = Random.Range(MinDelayUnilol, MaxDelayUnilol);
            yield return new WaitForSeconds(delay);

            var spawnIndex = Random.Range(0, SpawnPoints.Length);
            var spawnPoint = SpawnPoints[spawnIndex];

            var go = Instantiate(Unilol, spawnPoint.position, spawnPoint.rotation);
            Destroy(go, 6f);
        }
    }
}
