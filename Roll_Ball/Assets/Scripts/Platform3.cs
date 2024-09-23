using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform3 : MonoBehaviour{
    public float speed = 5.0f;
    public float movementDistance = 20.0f;

    public float waitTime = 1.0f;
    private Vector3 startPosition;
    private Vector3 targetPosition;

    private bool isWaiting = false;

    private bool movingForward = true;
    
    void Start(){
        startPosition = transform.position;
        targetPosition = new Vector3(startPosition.x+movementDistance, startPosition.y, startPosition.z);
    }


    void Update(){
        if (!isWaiting){
            if (movingForward){
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

                if (transform.position == targetPosition){
                    movingForward = false;
                    StartCoroutine(WaitBeforeMoving());
                }
            }
            else{
                transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);

                if (transform.position == startPosition){
                    movingForward = true;
                    StartCoroutine(WaitBeforeMoving());
                }
            }
        }
    }

    private IEnumerator WaitBeforeMoving(){
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
    }
}
