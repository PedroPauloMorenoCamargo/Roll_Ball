using UnityEngine;

public class Cilindro_Obstaculo : MonoBehaviour
{
    public float spinSpeed = 20f; 
    private Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; 
    }

    void FixedUpdate(){
        Quaternion deltaRotation = Quaternion.Euler(0f, spinSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
