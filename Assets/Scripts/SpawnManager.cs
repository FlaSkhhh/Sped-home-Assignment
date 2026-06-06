using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameSettingsSO settings;     //scriptable object settings
    public GameObject[] prefab;
    public int poolSize = 15;

    Queue<GameObject> pool = new Queue<GameObject>();
    
    float spawnTimer;
    float distanceFromCamera = 4f;      //has to be same as hand z pos

    void Start()
    {
        //spawn and disable objects at start
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab[Random.Range(0,prefab.Length)], transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }


    void Update()
    {
        //timing from scriptable object
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            Spawn();
            spawnTimer = settings.spawnInterval;
        }
    }

    private void Spawn()
    {
       
        Vector3 spawnViewport = new Vector3(Random.Range(0.1f, 0.9f), 1.1f, distanceFromCamera);    //random x on screen and y just above screen

        if (Camera.main != null)
        {
            Vector3 spawnPos = Camera.main.ViewportToWorldPoint(spawnViewport);
            GetFromPool(spawnPos);
        }
    }

    GameObject GetFromPool(Vector3 position)
    {
        if (pool.Count == 0) return null; //null stop if spawning is much faster than reseting 

        GameObject obj = pool.Dequeue();
        obj.transform.position = position;
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        //reset the object back to pool
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    public void ResetSpawner()
    {
        spawnTimer = settings.spawnInterval;
        FallingItem[] activeItems = FindObjectsByType<FallingItem>(FindObjectsSortMode.None);
        foreach (var item in activeItems)
        {
            ReturnToPool(item.gameObject);
        }
    }
}