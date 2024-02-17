using UnityEngine;

public class GameManager : MonoBehaviour {
    
    /* -------------------------------- Variables ------------------------------- */
    public Camera mainCamera;

    public static bool GAME_STATUS = true;
    public static int SCORE = 0;

    Color startingBgColor = new Color(0xBB / 255.0f, 0xFF / 255.0f, 0xED / 255.0f, 1.0f);
    bool gameEnded = false;

    /* --------------------------------- Methods -------------------------------- */
    // Game start
    void Start() {
        ResetGame();
    }
    
    // Update while game is running
    void Update() {
        // Check for end-game conditions
        if (!GAME_STATUS && !gameEnded) {
            gameEnded = true;
            EndGame();
        }
    }

    void ResetGame() {
        Debug.Log("GAME START");

        // Change camera background to light blue
        mainCamera.backgroundColor = startingBgColor;

        // Reset score and game status
        GAME_STATUS = true;
        SCORE = 0;
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
