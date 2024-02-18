using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour {
    
    /* -------------------------------- Variables ------------------------------- */
    public Camera mainCamera;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI scoreText;
    
    public static bool GAME_STATUS = false;
    public static int SCORE = 0;
    public static int DIFICULTY = 0;

    Color mainBlueColor = new Color(0xBB / 255.0f, 0xFF / 255.0f, 0xED / 255.0f, 1.0f);
    Color scoreColor = new Color(22f / 255f, 22f / 255f, 22f / 255f);

    bool titleScreen = true;
    bool gameEnded = false;
    int oldScore = 0;

    /* --------------------------------- Methods -------------------------------- */
    // Game start
    void Start() {
        // Enable title when game start
        titleText.enabled = true;
    }
    
    // Update while game is running
    void Update() {
        // Update score
        UpdateScore();

        // Update difficulty
        UpdateDifficulty();

        // Start game for the first time
        if (titleScreen && Input.GetMouseButtonDown(0)) {
            ResetGame();
            FirstStartGame();  
        }

        // Check for end-game conditions
        if (!GAME_STATUS && !gameEnded && !titleScreen) {
            gameEnded = true;
            EndGame();
        }

        // Check for game restart
        if (!GAME_STATUS && Input.GetMouseButtonDown(0)) {
            ResetGame();
        }
    }

    void UpdateScore() {
        // Check if score changed
        if (SCORE != oldScore) {
            // Update score text
            scoreText.text = SCORE.ToString();

            // Score animation
            scoreText.color = mainBlueColor;
            Invoke("ResetScoreText", 0.15f);

            // Update old score for detection for score changing
            oldScore = SCORE;
        }
    }

    void ResetScoreText() {
        scoreText.color = scoreColor;
    }

    void UpdateDifficulty() {
        int oldDificulty = DIFICULTY;

        // Change dificulty based on score
        if (SCORE >= 1000) { DIFICULTY = 5; }
        else if (SCORE >= 500) { DIFICULTY = 4; }
        else if (SCORE >= 250) { DIFICULTY = 3; }
        else if (SCORE >= 50) { DIFICULTY = 2; }
        else if (SCORE >= 15) { DIFICULTY = 1; }

        // Log dificulty changing
        if (oldDificulty != DIFICULTY) {
            Debug.Log("[DIFICULTY] Changed diff: " + DIFICULTY);
        }
    }

    void FirstStartGame() {
        titleText.enabled = false;
        scoreText.enabled = true;
        titleScreen = false;
        Spawner[] spawners = FindObjectsOfType<Spawner>();
        foreach (Spawner spawner in spawners) {
            spawner.StartSpawner();
        }
    }

    void ResetGame() {
        Debug.Log("GAME START");

        // Reset score and game status
        GAME_STATUS = true;
        SCORE = 0;
        DIFICULTY = 0;
        gameEnded = false;
        Spawner.spawnInterval = 2f;
        Spawner.multipleBallAttackCooldown = 10;

        // Change camera background to light blue
        mainCamera.backgroundColor = mainBlueColor;
    }

    void EndGame(){
        Debug.Log("GAME OVER");

        // Change score color to red
        scoreText.color = Color.red;

        // Change camera background to red
        mainCamera.backgroundColor = Color.red;

        // Destroy all balls
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls) {
            Ball ballScript = ball.GetComponent<Ball>();
            if (ballScript != null) {
                ballScript.DestroyBall();
            }
        }
    }
}
