using UnityEngine;

public class Ball : MonoBehaviour {
    
    /* -------------------------------- Variables ------------------------------- */
    public Camera mainCamera;
    public GameObject audioObject;
    public GameObject blueBallParticle;
    public GameObject yellowBallParticle;
    public GameObject redBallParticle;
    public GameObject gameEdingExplosion;

    public Material yellowBallMaterial;
    public Material redBallMaterial;

    //public AudioSource audioSource;
    public AudioClip audioClipBounce;
    public AudioClip audioClipDestroy;
    public bool isDestroyed = false;

    private int lives = 4;


    /* --------------------------------- Methods -------------------------------- */
    void Start() {
        mainCamera = Camera.main;
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
                GameManager.GAME_STATUS = false;
                PlayParticleAndDestroy(gameEdingExplosion);
                PlaySound(audioObject, audioClipDestroy, 0.5f, 0.5f, 0.5f);
                Debug.Log("Game ending ball hit the floor!");
            }
        }
        // Bounce particles and sound
        if (IsInCameraView()) {
            BallParticle();
            PlaySound(audioObject, audioClipBounce, 0.05f, 0.9f, 1.1f);
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
            GameObject particleObject = Instantiate(particlePrefab, transform.position, Quaternion.identity);

            // Get the particle system component
            ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();

            // Get the duration of the particle system
            float particleDuration = particleSystem.main.duration;

            // Destroy the particle object after the duration of the particle system
            Destroy(particleObject, particleDuration);
        }
    }

    void BallParticle() {
        if (lives > 2) { PlayParticleAndDestroy(blueBallParticle); }
        else if (lives == 2) { PlayParticleAndDestroy(yellowBallParticle); }
        else if (lives <= 1) { PlayParticleAndDestroy(redBallParticle); }
    }

    void PlaySound(GameObject audioPrefab, AudioClip audioClip, float volume, float pitch_1, float pitch_2) {
        if (audioPrefab != null) {
            // Create object
            GameObject audioObject = Instantiate(audioPrefab, transform.position, Quaternion.identity);

            // Get the AudioSource component from the instantiated audioObject
            AudioSource audioSource = audioObject.GetComponent<AudioSource>();

            // Check if an AudioSource component is found
            if (audioSource != null) {
                // Adjust pitch and play the one-shot sound
                audioSource.pitch = Random.Range(pitch_1, pitch_2);
                audioSource.PlayOneShot(audioClip, volume);
            }
            // Destroy audio object when done playing
            Destroy(audioObject, audioClip.length);
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

        // Explosion particles and sound
        BallParticle();
        PlaySound(audioObject, audioClipDestroy, 0.1f, 0.7f, 1.1f);
    }

    bool IsInCameraView() {
        // Get the object's bounds
        Bounds bounds = GetComponent<Renderer>().bounds;

        // Check if the bounds are within the camera's frustum
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(mainCamera), bounds);
    }
}
