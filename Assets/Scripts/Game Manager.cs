using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    /* -------------------------------- Variables ------------------------------- */
    public static bool GAME_STATUS = true;
    public static int BALLS_DESTROYED = 0;

    bool gameEnded = false;

    /* --------------------------------- Methods -------------------------------- */
    void Update() {
        // Check for end-game conditions
        if (!GAME_STATUS && !gameEnded) {
            gameEnded = true;
            EndGame();
        }
    }

    void EndGame(){
        Debug.Log("GAME OVER");

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
