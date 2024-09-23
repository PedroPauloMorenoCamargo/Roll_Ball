using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart_Button : MonoBehaviour{
    public void Restart(){
        SceneManager.LoadScene(1); 
    }

    public void Menu()
    {
        SceneManager.LoadScene(0); 
    }
}
