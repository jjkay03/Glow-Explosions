using UnityEngine;

public class Cursor : MonoBehaviour {
    
    /* -------------------------------- Variables ------------------------------- */    
    private Ball collidedBall;


    /* --------------------------------- Methods -------------------------------- */
    // Make the cursor object track the actual cursor
    void Update() {
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Set the distance from the camera to the object
        mousePosition.z += 10f;

        // Convert the screen coordinates to world coordinates
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Set the position of the cursor object
        transform.position = worldPosition;
    }

    // Check if cursor colides with ball
    void OnTriggerEnter(Collider other) {
        // Check if the collider belongs to a GameObject tagged as Ball
        if (other.CompareTag("Ball") && !other.GetComponent<Ball>().isDestroyed) {
            // Save a reference to the collided ball
            collidedBall = other.GetComponent<Ball>();

            // Destroy the ball
            collidedBall.DestroyBall();

            if (GameManager.GAME_STATUS) {
                // Increment the balls destroyed counter
                collidedBall.CalculateScore();
            }
        }
    }
}
