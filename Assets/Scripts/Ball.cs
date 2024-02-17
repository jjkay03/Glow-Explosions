using UnityEngine;

public class Ball : MonoBehaviour {
    
    /* -------------------------------- Variables ------------------------------- */
    public GameObject ballExplosion;
    public GameObject yellowBallExplosion;
    public GameObject redBallExplosion;
    public GameObject gameEdingExplosion;

    public Material yellowBallMaterial;
    public Material redBallMaterial;

    public AudioSource audioSrc;
    public AudioClip bounceSfx;
    public bool isDestroyed = false;

    private int lives = 4;


    /* --------------------------------- Methods -------------------------------- */
    void OnCollisionEnter(Collision collision) {
        // Check if ball collides with floor
        if (collision.gameObject.tag == "Floor") {
            
            // Bounce sound effect
            //audioSrc.clip = bounceSfx;
            //audioSrc.Play();

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
                GameManager.GAME_STATUS = false;
                PlayParticleAndDestroy(gameEdingExplosion);
                Debug.Log("Game ending ball hit the floor!");
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

    void PlayParticleAndDestroy(GameObject particlePrefab) {
        if (particlePrefab != null)
        {
            // Instantiate the particle system
            GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);

            // Get the particle system component
            ParticleSystem particleSystem = particle.GetComponent<ParticleSystem>();

            // Get the duration of the particle system
            float particleDuration = particleSystem.main.duration;

            // Destroy the particle object after the duration of the particle system
            Destroy(particle, particleDuration);
        }
    }

    public void CalculateScore() {
        if (lives > 2) { GameManager.SCORE += 1; }
        else if (lives == 2) { GameManager.SCORE += 2; }
        else if (lives <= 1) { GameManager.SCORE += 4; }
    }

    public void DestroyBall() {
        isDestroyed = true;

        // Destroy the object this script is attached to
        Destroy(gameObject);

        // Explosion particles
        if (lives > 2) { PlayParticleAndDestroy(ballExplosion); }
        else if (lives == 2) { PlayParticleAndDestroy(yellowBallExplosion); }
        else if (lives <= 1) { PlayParticleAndDestroy(redBallExplosion); }
    }

}
