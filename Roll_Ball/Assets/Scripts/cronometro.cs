using UnityEngine;
using TMPro; 

public class CronometroDisplay : MonoBehaviour{
    public TMP_Text cronometroText;  


    void Update(){
        // Update the elapsed time every frame
        GameManager.Instance.time += Time.deltaTime;

        // Format the time into minutes and seconds
        int minutos = Mathf.FloorToInt(GameManager.Instance.time / 60F);
        int segundos = Mathf.FloorToInt(GameManager.Instance.time % 60F);
        int milissegundos = Mathf.FloorToInt((GameManager.Instance.time * 100F) % 100F);

        // Display the timer in mm:ss:ms format
        cronometroText.text = string.Format("{0:00}:{1:00}:{2:00}", minutos, segundos, milissegundos);
    }
}
