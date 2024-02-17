using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {
    
    /* -------------------------------- Variables ------------------------------- */
    [SerializeField] TextMeshProUGUI scoreText;

    /* --------------------------------- Methods -------------------------------- */
    void Update() {
        scoreText.text = GameManager.SCORE.ToString();
    }
}
