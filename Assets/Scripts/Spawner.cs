using UnityEngine;

public class Spawner : MonoBehaviour {
    
    /* -------------------------------- Variables ------------------------------- */
    public GameObject ballPrefab;
    
    public static float spawnInterval = 2f;
    public static int multipleBallAttackCooldown = 10;

    /* --------------------------------- Methods -------------------------------- */    
    public void StartSpawner() {
        ConstentSpawnBall();
    }

    void ConstentSpawnBall() {
        if (GameManager.GAME_STATUS) {
            
            // Spawn random ball
            SpawnRandomBall(true);

            // Multiple balls attack
            AttackMultipleBalls();

            // Cancel the existing InvokeRepeating
            CancelInvoke("ConstentSpawnBall");

            // Decrease the spawn interval over time
            if (Random.Range(0, 15) == 0 && spawnInterval > 0.8f) {
                spawnInterval *= 0.95f;
                Debug.Log($"[SPAWNER] Spawn interval: {spawnInterval}");
            }  

            // Start a new InvokeRepeating with the updated interval
            InvokeRepeating("ConstentSpawnBall", spawnInterval, spawnInterval);
        }
    }

    void SpawnRandomBall(bool hasAngle=false) {
        // Choose a random position on the spawner
        Vector3 randomPosition = new Vector3(
        Random.Range(transform.position.x - transform.localScale.x / 2f, transform.position.x + transform.localScale.x / 2f),
        transform.position.y,
        transform.position.z
        );

        // Spawn a new ball at the random postion
        GameObject newBall = Instantiate(ballPrefab, randomPosition, Quaternion.identity);

        // If hasAngle is true, shoot the ball in a random angle in the x-axis
        if (hasAngle) {
            // Access the Rigidbody component directly
            Rigidbody newBallRigidbody = newBall.GetComponent<Rigidbody>();

            if (newBallRigidbody != null) {
                // Calculate a random angle in radians
                float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

                // Set a constant speed for the ball
                float speed = 10f;

                // Calculate the direction based on the angle
                Vector3 direction = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0f);

                // Apply force to shoot the ball
                newBallRigidbody.velocity = direction * speed;
            } else {
                Debug.LogError("Rigidbody component not found on ballPrefab. Make sure to add a Rigidbody component.");
            }
        }
    }   

    void SpawnMultipleBalls(bool hasAngle=false) {
        Debug.Log($"[SPAWNER] Multiple ball attack");
        for (int i = 0; i < Random.Range(2, 4); i++) {
            SpawnRandomBall(hasAngle);
        }
    }

    void AttackMultipleBalls() {
        // Multiple ball attack
        multipleBallAttackCooldown -= 1;
        if (GameManager.DIFICULTY >= 5) {
            if (Random.Range(0, 3) == 0) { 
                SpawnMultipleBalls(true && multipleBallAttackCooldown <= 0);
                multipleBallAttackCooldown = 1;
            }
        } else if (GameManager.DIFICULTY >= 4) {
            if (Random.Range(0, 4) == 0 && multipleBallAttackCooldown <= 0) { 
                SpawnMultipleBalls(true);
                multipleBallAttackCooldown = 2;
            }
        } else if (GameManager.DIFICULTY >= 3) {
            if (Random.Range(0, 4) == 0 && multipleBallAttackCooldown <= 0) { 
                SpawnMultipleBalls();
                multipleBallAttackCooldown = 2;
            }
        } else if (GameManager.DIFICULTY > 0) {
            if (Random.Range(0, 4) == 0 && multipleBallAttackCooldown <= 0) { 
                SpawnMultipleBalls();
                multipleBallAttackCooldown = 2;
            }
        }
    }
}
