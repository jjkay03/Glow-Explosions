using UnityEngine;

public class Spawner : MonoBehaviour {
    
    /* -------------------------------- Variables ------------------------------- */
    public GameObject ballPrefab;
    
    public bool spawnBalls = true;
    private float spawnInterval = 2f;
    private int multipleBallAttackCooldown = 5;

    /* --------------------------------- Methods -------------------------------- */
    void Start() {
        if (spawnBalls) {
            // Spawn the first ball
            ConstentSpawnBall();
        }   
    }
    
    void ConstentSpawnBall() {
        if (GameManager.GAME_STATUS) {
            
            // Spawn random ball
            SpawnRandomBall();

            // Multiple ball attack
            multipleBallAttackCooldown -= 1;
            if (Random.Range(0, 5) == 0 && multipleBallAttackCooldown <= 0) {
                Debug.Log($"[SPAWNER] Multiple ball attack");
                multipleBallAttackCooldown = 3;
                for (int i = 0; i < Random.Range(2, 4); i++) {
                    SpawnRandomBall();
                }
            }

            // Cancel the existing InvokeRepeating
            CancelInvoke("ConstentSpawnBall");

            // Decrease the spawn interval over time
            if (Random.Range(0, 10) == 0 && spawnInterval > 1f) {
                    spawnInterval *= 0.95f;
                    Debug.Log($"[SPAWNER] Spawn interval: {spawnInterval}");
            }  

            // Start a new InvokeRepeating with the updated interval
            InvokeRepeating("ConstentSpawnBall", spawnInterval, spawnInterval);
        }
    }

    void SpawnRandomBall() {
        // Choose a random position on the spawner
            Vector3 randomPosition = new Vector3(
                Random.Range(transform.position.x - transform.localScale.x / 2f, transform.position.x + transform.localScale.x / 2f),
                transform.position.y,
                transform.position.z
            );

            // Spawn a new ball at the random postion
            GameObject newBall = Instantiate(ballPrefab, randomPosition, Quaternion.identity);
    }
}
