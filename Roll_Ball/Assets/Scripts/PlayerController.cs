using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count;
    private bool isGrounded;
    private bool isJumping;
    
    public float jumpForce = 30.0f;
    public float speed = 0;
    public float collisionImpulseForce = 10.0f;
    
    public Boss enemy;
    public Countdown countdown;

    public TextMeshProUGUI countText;
    public TextMeshProUGUI lifeText;

    private int life = 3;
    public Transform respawnPoint;


    public AudioClip jumpClip;  
    public AudioClip coinClip;  
    public AudioClip musicClip;  
    public AudioClip deathClip; 

    public AudioClip alarmClip;

    public AudioSource jumpAudioSource;
    public AudioSource coinAudioSource;
    public AudioSource alarmeAudioSource;
    public AudioSource musicaAudioSource;
    public AudioSource deathAudioSource;

    private bool isPlaying = false;

    void Start() {
        GameManager.Instance.score = 0;
        GameManager.Instance.time = 0;
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.drag = 0;
        count = 0;
        SetCountText();
        UpdateLifeText();

        count = PlayerPrefs.GetInt("PlayerScore", 0);

        // Determina os AudioSource e os AudioClips
        jumpAudioSource = gameObject.AddComponent<AudioSource>();
        jumpAudioSource.clip = jumpClip; 
        jumpAudioSource.playOnAwake = false;


        coinAudioSource = gameObject.AddComponent<AudioSource>();
        coinAudioSource.clip = coinClip;
        coinAudioSource.playOnAwake = false;


        alarmeAudioSource = gameObject.AddComponent<AudioSource>();
        alarmeAudioSource.clip = alarmClip;
        alarmeAudioSource.playOnAwake = false;
        alarmeAudioSource.volume = 0.7f;


        musicaAudioSource = gameObject.AddComponent<AudioSource>();
        musicaAudioSource.clip = musicClip;
        musicaAudioSource.playOnAwake = true;
        musicaAudioSource.loop = true;
        musicaAudioSource.volume = 0.1f;
        musicaAudioSource.Play();



        deathAudioSource = gameObject.AddComponent<AudioSource>();
        deathAudioSource.clip = deathClip;
        deathAudioSource.playOnAwake = false;
    }

    void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void Update(){
        if ((Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.JoystickButton1)) && isGrounded){
            isJumping = true;

            if (jumpAudioSource != null && jumpClip != null){
                jumpAudioSource.Play();
            }
        }
    }

    private void FixedUpdate(){
        if (isJumping){
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = false;
        }

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        Vector3 currentVelocity = rb.velocity;

        if (currentVelocity.magnitude > speed + 10){
            rb.velocity = currentVelocity.normalized * speed;
        }
        else{
            rb.AddForce(movement * speed);
        }
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Pickup")){
            coinAudioSource.Play();
            other.gameObject.SetActive(false);
            GameManager.Instance.score += 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Cube")){
            deathAudioSource.Play();
            life = life - 1;
            UpdateLifeText();
            RespawnPlayer();

            if (life == 0){
                PlayerPrefs.SetInt("PlayerScore", count);
                SceneManager.LoadScene(2);
            }
        }
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Ground")){
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Arena")){
            
            if (!isPlaying){
                Debug.Log("Music stopped.");
                musicaAudioSource.mute = true;
                alarmeAudioSource.Play();
                isPlaying = true;
            }

            if (enemy != null){
                enemy.comecou = true;
            }

            countdown.startCountdown = true;
        }
    }

    private void OnCollisionExit(Collision collision){
        if (collision.gameObject.CompareTag("Ground")){
            isGrounded = false;
        }
    }

    void SetCountText(){
        countText.text = "Score: " + GameManager.Instance.score.ToString();
    }

    void UpdateLifeText(){
        lifeText.text = "Life: " + life;

        if (life == 3){
            lifeText.color = Color.green;
        }
        else if (life == 2){
            lifeText.color = Color.yellow;
        }
        else if (life == 1){
            lifeText.color = Color.red;
        }
    }

    void RespawnPlayer(){
        transform.position = new Vector3(respawnPoint.position.x, respawnPoint.position.y + 10, respawnPoint.position.z);
        rb.velocity = Vector3.zero;
    }
}
