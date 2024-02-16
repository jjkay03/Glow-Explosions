using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    
    public static bool GAME_STATUS = true;
    public static int BALLS_DESTROYED = 0;
    public GameObject ballPrefab;
    public bool spawnBalls = true;
    private float spawnInterval = 2f;

    void Start() {
        if (spawnBalls) {
            // Spawn the first ball
            SpawnBall();
        }   
    }

    void SpawnBall() {
        // Choose a random position on the spawner
        Vector3 randomPosition = new Vector3(
            Random.Range(transform.position.x - transform.localScale.x / 2f, transform.position.x + transform.localScale.x / 2f),
            transform.position.y,
            transform.position.z
        );

        // Spawn a new ball at the random postion
        GameObject newBall = Instantiate(ballPrefab, randomPosition, Quaternion.identity);

        // Cancel the existing InvokeRepeating
        CancelInvoke("SpawnBall");

        // Decrease the spawn interval over time
        if (Random.Range(0, 10) == 0 && spawnInterval > 0.5f) {
                spawnInterval *= 0.9f;
                Debug.Log($"Spawn interval changed: {spawnInterval}");
        }  

        // Start a new InvokeRepeating with the updated interval
        InvokeRepeating("SpawnBall", spawnInterval, spawnInterval);
    }
}
