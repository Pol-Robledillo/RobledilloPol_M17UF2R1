using UnityEngine;
[CreateAssetMenu(fileName = "Spawner.asset", menuName = "Spawners/Spawner")]
public class SpawnerData : ScriptableObject
{
    public GameObject whatToSpawn;
    public int maxGenerations, minGenerations;
}
