using UnityEngine;
using TMPro; // For TextMeshPro
using UnityEngine.SceneManagement;
public class Countdown: MonoBehaviour
{
    public TMP_Text countdownText;  // Reference to TextMeshPro component
    private float remainingTime = 60f;  // Set the countdown time to 60 seconds (1 minute)
    public bool startCountdown = false;  // Boolean to control when the countdown starts

    void Update()
    {
        // Check if the countdown should start and the time is greater than 0
        if (startCountdown && remainingTime > 0)
        {
            // Subtract the time that has passed since the last frame
            remainingTime -= Time.deltaTime;

            // Prevent time from going below 0
            remainingTime = Mathf.Max(remainingTime, 0);

            // Format the time as minutes and seconds
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);

            // Update the TextMeshPro text to display the countdown in mm:ss format
            countdownText.text = string.Format("Survive: {0:00}:{1:00}", minutes, seconds);
    

            if (remainingTime <= 0){
                SceneManager.LoadScene(3);
            }
        }
    }
}
