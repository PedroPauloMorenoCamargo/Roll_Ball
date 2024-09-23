using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour{
    public Transform player; 
    public float speed = 20f; 
    public float waitTime = 1f; 
    private float rotationSpeed;
    
    private int playerI, playerJ; 

    public int maxI = 5, maxJ = 5; 

    public bool preferHorizontal = true;

    public bool comecou = false;

    public int planeSize = 2; 

    private int i, j; 
    private int gridStartx;
    private int gridStartz;

    private Quaternion targetRotation;

    public AudioSource BossMove; 
    public AudioClip bossMoveClip;

    public AudioSource Boss_Music; 
    public AudioClip boss_Music_Clip;
    void Start(){
        
        //Inicializa os Audios
        BossMove = gameObject.AddComponent<AudioSource>();
        BossMove.clip = bossMoveClip;
        BossMove.playOnAwake = false;

        Boss_Music = gameObject.AddComponent<AudioSource>();
        Boss_Music.clip = boss_Music_Clip;
        Boss_Music.playOnAwake = false;

        i = 0;
        j = 0;
        //Pega os valores (0,0) da grid considerando o topo esquerdo como origem
        gridStartx = Mathf.RoundToInt(transform.position.x-planeSize/2);
        gridStartz = Mathf.RoundToInt(transform.position.z-planeSize/2);	
        targetRotation = transform.rotation;

        //Calcula a velocidade de rotação baseado na movimentação do inimigo
        rotationSpeed = 90f*speed/planeSize;
        StartCoroutine(MoveEnemy());

    }

   IEnumerator MoveEnemy(){
        while (true){
            if (comecou){
                // Calcula a posição do player na grid
                playerI = Mathf.RoundToInt(Mathf.Abs(player.position.x - gridStartx) / planeSize);
                playerJ = Mathf.RoundToInt(Mathf.Abs(player.position.z - gridStartz) / planeSize);

                // Toca a música do Boss
                if (Boss_Music.isPlaying == false){
                    Boss_Music.Play();
                }
                // Decide se o inimigo deve se mover horizontalmente ou verticalmente
                if (ShouldMoveHorizontally()){
                    //Move Pra direita
                    if (i < playerI && i < maxI){
                        i++;
                        SetTargetRotation(-90, 'z');
                    }
                    //Move pra esquerda
                    else if (i > playerI && i > 0){
                        i--;
                        SetTargetRotation(90, 'z'); 
                    }
                    //Audio de movimento
                    if (BossMove.isPlaying == false){
                        BossMove.Play();
                    }
                    yield return MoveToPosition(new Vector3(gridStartx + i * planeSize + planeSize / 2, transform.position.y, transform.position.z));
                }
                else
                {   
                    //Move pra baixo
                    if (j < playerJ && j < maxJ){
                        j++;
                        SetTargetRotation(-90, 'x'); 
                    }
                    //Move pra cima
                    else if (j > 0){
                        j--;
                        SetTargetRotation(90, 'x'); 
                    }
                    if (BossMove.isPlaying == false){
                        BossMove.Play();
                    }
                    yield return MoveToPosition(new Vector3(transform.position.x, transform.position.y, gridStartz - j * planeSize + planeSize / 2));
                }

                yield return new WaitForSeconds(waitTime);
            }
            else{
                yield return new WaitForSeconds(waitTime);
            }
    }
    }

    // Decide se o inimigo deve se mover horizontalmente ou verticalmente
    bool ShouldMoveHorizontally(){
        //Prioriza o movimento horizontal
        if (preferHorizontal)
        {
            return Mathf.Abs(playerI - i) > Mathf.Abs(playerJ - j); 
        }
        else
        {
            return Mathf.Abs(playerI - i) <= Mathf.Abs(playerJ - j); 
        }
    }

    // Rotaciona o inimigo para a direção desejada
    void SetTargetRotation(float angle, char axis){
        if (axis == 'z'){
            targetRotation = Quaternion.Euler(0, 0, angle) * transform.rotation;
        }
        else if (axis == 'x'){
            targetRotation = Quaternion.Euler(angle, 0, 0) * transform.rotation;
        }
    }
    IEnumerator MoveToPosition(Vector3 targetPosition){
        // Move o inimigo até a posição desejada
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f || Quaternion.Angle(transform.rotation, targetRotation) > 0.01f){
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }
}
