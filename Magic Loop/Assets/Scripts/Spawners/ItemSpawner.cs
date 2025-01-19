using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemsToSpawn;
    private bool itemSpawned = false;

    void Start()
    {
        foreach (GameObject item in itemsToSpawn)
        {
            if (!itemSpawned)
            {
                if (Random.value <= item.GetComponent<Item>().spawnChance)
                {
                    GameObject spawnedItem = Instantiate(item, transform.position, Quaternion.identity, transform);
                    itemSpawned = true;
                }
            }
        }
    }
}
