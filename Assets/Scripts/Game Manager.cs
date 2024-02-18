using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    
    /* -------------------------------- Variables ------------------------------- */
    public Camera mainCamera;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI scoreText;
    
    public static bool GAME_STATUS = false;
    public static int SCORE = 0;
    public static int DIFICULTY = 0;

    Color startingBgColor = new Color(0xBB / 255.0f, 0xFF / 255.0f, 0xED / 255.0f, 1.0f);
    bool titleScreen = true;
    bool gameEnded = false;

    /* --------------------------------- Methods -------------------------------- */
    // Game start
    void Start() {
        // Enable title when game start
        titleText.enabled = true;
    }
    
    // Update while game is running
    void Update() {
        // Update score
        scoreText.text = SCORE.ToString();

        // Update difficulty
        UpdateDifficulty();

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
        mainCamera.backgroundColor = startingBgColor;
    }

    void EndGame(){
        Debug.Log("GAME OVER");

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
