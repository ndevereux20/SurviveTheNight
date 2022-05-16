using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject Zombie;
    public Transform[] spawnLocations;
    public GameObject player; 

    public int zombiesSpawned = 0; //total number of zombies spawned 
    public int initialSpawns = 3; //initial number of zombies to be spawnedd 
    public int maxZombies = 10; //the total number of zombies to be spawned 

    private float ticks = 0; // keeps track of how many frame updates
    public int spawnRate = 100; //How often the zombies should be spawned 

    private int lastSpawnPoint = 0; //keeps track of the spawn point the last zombie spawned on
    private int spawnPoint; // integer value that chooses what spawn location the zombie will spawn on 
    private int newSpawnPoint; // new spawn point if a zombie had just spawned on the spawnPoint

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];  //returns GameObject[]
        SpawnEnemy(initialSpawns);
    }

    // Update is called once per frame
    void Update()
    {
        ticks++;
        if (ticks % spawnRate == 0.0 && zombiesSpawned < maxZombies && PauseMenu.instance.gameIsPaused == false)
        {
            SpawnEnemy(1);
        }
       
        
    }

    void SpawnEnemy(int spawns)
    {
        for (int i = 0; i < spawns; i++)
        {
            // Picks a random spawn location 
            spawnPoint = Random.Range(0, spawnLocations.Length);
            // If a zombie just spawned on that point give it a new location and spawn it 
            if (lastSpawnPoint == spawnPoint)
            {
                newSpawnPoint = Random.Range(0, spawnLocations.Length);
                if ((player.transform.position - spawnLocations[newSpawnPoint].position).magnitude > 5)
                {
                    Instantiate(Zombie, spawnLocations[newSpawnPoint].position, Quaternion.identity);
                    zombiesSpawned += 1;
                    lastSpawnPoint = newSpawnPoint;
                }
            }
            // Else spawn it on the spawn location
            else{
                if ((player.transform.position - spawnLocations[spawnPoint].position).magnitude > 5)
                {
                    Instantiate(Zombie, spawnLocations[spawnPoint].position, Quaternion.identity);
                    zombiesSpawned += 1;
                    lastSpawnPoint = spawnPoint;
                }
            }
        }
    }
}
