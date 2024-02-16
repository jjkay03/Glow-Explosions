using UnityEngine;

public class Spawner : MonoBehaviour {
    
    /* -------------------------------- Variables ------------------------------- */
    public GameObject ballPrefab;
    
    public bool spawnBalls = true;
    private float spawnInterval = 2f;


    /* --------------------------------- Methods -------------------------------- */
    void Start() {
        if (spawnBalls) {
            // Spawn the first ball
            SpawnBall();
        }   
    }
    
    void SpawnBall() {
        if (GameManager.GAME_STATUS) {
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
            if (Random.Range(0, 10) == 0 && spawnInterval > 1f) {
                    spawnInterval *= 0.95f;
                    Debug.Log($"Spawn interval changed: {spawnInterval}");
            }  

            // Start a new InvokeRepeating with the updated interval
            InvokeRepeating("SpawnBall", spawnInterval, spawnInterval);
        }
    }
}
