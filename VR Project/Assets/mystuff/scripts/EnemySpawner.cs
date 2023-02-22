using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float startingSpawnInterval;
    private float spawnInterval;
    private float counter;

    private void OnEnable()
    {
        counter = 0f;
        spawnInterval = startingSpawnInterval;
    }
    private void Update()
    {
        if (counter >= spawnInterval)
        {
            counter = 0f;
            spawnInterval -= Time.deltaTime;
            spawnInterval = Mathf.Clamp(spawnInterval, 1, startingSpawnInterval);
            Enemy enemy = GameManager.instance.ObjectPoolHandler.EnemyOP.GetPooledObject();
            enemy.transform.position = transform.position;
            enemy.gameObject.SetActive(true);
        }
        else
        {
            counter += Time.deltaTime;
        }    
    }
}
