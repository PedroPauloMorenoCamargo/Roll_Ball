using UnityEngine;
using TMPro;
public class ReceiveTexts : MonoBehaviour{

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI timeText;
    void Start(){
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay(){
        if (scoreText != null && timeText != null){
            // Pega o score do GameManager e atualiza o texto do TMP
            scoreText.text = "Score: " + GameManager.Instance.score.ToString();

            // Pega os valores de minutos, segundos e milissegundos do GameManager e atualiza o texto do TMP
            int minutos = Mathf.FloorToInt(GameManager.Instance.time / 60F);
            int segundos = Mathf.FloorToInt(GameManager.Instance.time % 60F);
            int milissegundos = Mathf.FloorToInt((GameManager.Instance.time * 100F) % 100F);
            timeText.text = string.Format("Time: {0:00}:{1:00}:{2:00}", minutos, segundos, milissegundos);
        }

    }
}
