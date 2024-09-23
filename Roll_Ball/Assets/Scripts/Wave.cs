using UnityEngine;

public class Wave : MonoBehaviour{
    //Array de Retângulos
    public GameObject[,] rectangles; 

    // Velocidade da Onda
    public float waveSpeed = 1f; 
    // Altura da Onda
    public float waveHeight = 1f; 
    // Offset
    public float waveOffset = 0.5f;  

    void Start(){
        // Inicializa o array de retângulos
        rectangles = new GameObject[4,16];
        for (int i = 0; i < 4; i++){
            for (int j = 0; j < 16; j++){
               // Preenche o array com os retângulos filhos do objeto
                rectangles[i, j] = transform.GetChild(i * 16 + j).gameObject;
            }
        }
    }

    void Update(){
        // Atualiza a posição dos retângulos para simular uma onda
        for (int i = 0; i < 4; i++){
            for (int j = 0; j < 16; j++){
                float wave = Mathf.Sin(Time.time * waveSpeed + j * waveOffset);
                Vector3 originalPosition = rectangles[i, j].transform.position;
                rectangles[i, j].transform.position = new Vector3(originalPosition.x, wave * waveHeight, originalPosition.z);
            }
        }
    }
}
