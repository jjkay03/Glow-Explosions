using UnityEngine;

public class Ball : MonoBehaviour
{
    public Material yellowBallMaterial;
    public Material redBallMaterial;
    int lives = 4;

    void OnMouseOver() {
        // Increment the balls destroyed counter
        Spawner.BALLS_DESTROYED++;

        // Debug the number of destroyed balls in the console
        Debug.Log($"Balls Destroyed: {Spawner.BALLS_DESTROYED}");

        // Destroy the object this script is attached to
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision) {
        // Check if ball collides with floor
        if (collision.gameObject.tag == "Floor") {
            
            // Decrement lives
            lives--;

            // Deal with lives change
            if (lives == 2) {
                // Change to yellow material
                ChangeMaterial(yellowBallMaterial);
            }
            else if (lives == 1) {
                // Change to red material
                ChangeMaterial(redBallMaterial);
            }
            else if (lives <= 0) {
                Spawner.GAME_STATUS = false;
            }
        }
    }

    void ChangeMaterial(Material newMaterial) {
        // Check if the object has a MeshRenderer component
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        // Change the material if a MeshRenderer component is found
        if (meshRenderer != null) {
            meshRenderer.material = newMaterial;
        }
        else {
            // Log a warning if no MeshRenderer component is found
            Debug.LogWarning("MeshRenderer component not found on the object.");
        }
    }
}
