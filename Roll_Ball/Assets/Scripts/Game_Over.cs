using UnityEngine;
using UnityEngine.SceneManagement;
public class Game_Over : MonoBehaviour{

    void Update(){
        CheckIfBelowThreshold();
    }

    void CheckIfBelowThreshold(){
        //Checa se o personagem caiu do mapa
        if (transform.position.y < -50f){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
