using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class JoyStickController : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float verticalSpeed = 20f;
    public float turnSpeed = 12f;

    public int lives = 3;
    public int power = 0;

    public Image[] heartImg;
    public Text DBjump;
    public GameObject dieText;
    public GameObject victoryText;

    public AudioSource victoryAudio;
    public VariableJoystick joystickInput;

    bool isGrounded;
    bool canJump = true;
    bool isFalling = false;
    public bool doJump = false;

    Animator animator;
    Rigidbody playerRb;

    Vector3 playerMovement;
    Vector3 desiredForward;
    Vector3 Rotation;
    public Vector3 checkPoint;

    Quaternion playerRotation = Quaternion.identity;

    private void Start()
    {
        Time.timeScale = 1;
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        checkPoint = gameObject.transform.position;
    }

    private void Update()
    {
        move();
    }


    private void move()
    {
        float h = joystickInput.Horizontal;
        float v = joystickInput.Vertical;

        bool hasHorizontalInput = !Mathf.Approximately(h, 0f);
        bool hasVerticalInput = !Mathf.Approximately(v, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        playerMovement = new Vector3(h, 0f, v);
        playerMovement.Normalize();
        playerMovement *= forwardSpeed;

        if (CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded && canJump)
        {
            Debug.Log("First Jump");
            doJump = true;
            //animator.SetTrigger("doJump");
            playerRb.AddForce(Vector3.up * verticalSpeed, ForceMode.Impulse);
            isGrounded = false;
        }
        else if (CrossPlatformInputManager.GetButtonDown("Jump") && canJump && power > 0)
        {
            Debug.Log("Second Jump");
            doJump = true;
            //animator.SetTrigger("doJump");
            playerRb.AddForce(Vector3.up * verticalSpeed, ForceMode.Impulse);
            canJump = false;
            power -= 1;
            DisplayPower(power);
        }
        else if (!isGrounded)
        {
            Debug.Log("doJump false is working");
            isFalling = true;
            doJump = false;
            //animator.SetBool("doJump", false);

        }

        //if(Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        //{
        //    Debug.Log("Jump");
        //    animator.SetTrigger("doJump");
        //    playerRb.AddForce(Vector3.up * verticalSpeed, ForceMode.Impulse);
        //    isGrounded = false;
        //}
        //else if (!isGrounded)
        //{
        //    isFalling = true;
        //    Debug.Log("falling");
        //}

        playerRb.velocity = playerMovement;
        desiredForward = Vector3.RotateTowards(transform.forward, playerMovement, turnSpeed * Time.deltaTime, 0f);
        playerRotation = Quaternion.LookRotation(desiredForward);

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isFalling", isFalling);
        animator.SetBool("doJump", doJump);

        if (gameObject.transform.position.y < -12 && lives > 0)
        {
            Die();
        }
        else if (lives <= 0)
        {
            dieText.SetActive(true);
            Time.timeScale = 0;
        }

        if (lives <= 0 && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
            Time.timeScale = 1;
        }
    }

    private void OnAnimatorMove()
    {
        playerRb.MovePosition(playerRb.position + playerMovement * animator.deltaPosition.magnitude);
        playerRb.MoveRotation(playerRotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("MovingGround"))
        {
            isGrounded = true;
            isFalling = false;
            canJump = true;
            Debug.Log("Player is on the Ground");
        }

        if (collision.gameObject.CompareTag("MovingGround"))
        {
            Debug.Log("move with the island");
            gameObject.transform.SetParent(collision.transform);
        }


    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && collision.gameObject.CompareTag("MovingGround"))
        {
            isGrounded = true;
            isFalling = false;
            canJump = true;
            Debug.Log("Player is staying on the Ground");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Crystal"))
        {
            Debug.Log("Collide with crystal");
            power++;
            DisplayPower(power);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Reward"))
        {
            Debug.Log("Victory!");
            Victory();
        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("MovingGround"))
    //    {
    //        isGrounded = false;
    //        Debug.Log("Player is not on the ground.");
    //    }

    //    if (collision.gameObject.CompareTag("MovingGround"))
    //    {
    //        Debug.Log("Unattach the player");
    //        transform.SetParent(null);
    //    }
    //}

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("MovingGround"))
        {
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                playerRb.AddForce(Vector3.up * verticalSpeed, ForceMode.Impulse);
            }
            isGrounded = false;
            Debug.Log("Player is not touching with the ground");
        }

        if (collision.gameObject.CompareTag("MovingGround"))
        {
            Debug.Log("Unattach the player");
            transform.SetParent(null);
        }
    }

    private void Die()
    {
        lives--;
        Destroy(heartImg[lives], 0.4f);
        playerRb.Sleep();
        gameObject.transform.position = checkPoint;
        playerRb.WakeUp();
    }

    private void Victory()
    {
        victoryText.SetActive(true);
        victoryAudio.Play();
        Time.timeScale = 0;
    }

    //IEnumerator Wait()
    //{


    //    yield return new WaitForSeconds(3);
    //    dieText.SetActive(false);
    //    SceneManager.LoadScene(1);
    //}

    private void DisplayPower(float p)
    {
        Debug.Log(p + " of powers are left");
        DBjump.text = "DB Jump x" + p;
    }
}
