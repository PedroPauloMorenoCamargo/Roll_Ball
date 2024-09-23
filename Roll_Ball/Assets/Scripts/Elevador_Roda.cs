using UnityEngine;

public class Elevador_Roda : MonoBehaviour{
    public float spinSpeed = -20f; 

    void Update(){
        transform.Rotate(0f,spinSpeed * Time.deltaTime, 0f);
    }
}
