using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")] // Variables that concern movement
    public float movementSpeed = 50f;
    public float jumpForce = 20f;
    private float gravMultiplier = 1;
    public bool locked = false;
    public bool playerWalking;
    Rigidbody playerRb;

    public bool topdownView;

    [Header("Ground Check")] // Checks for things like ground, in order for jumping.
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    public float groundDrag;

    [Header("Wall Interactions")] // Checks if the player is on walls.
    public bool onWall;




    [Header("Player Stats")] // Player stats such as health and inventory
    public Image healthBar;
    public TextMeshProUGUI healthText;
    public float maxHealth;
    public float health = 10;
    private SkinsManager skinsManager;
    private Renderer renderThing;

    [Header("Enemy Interactions")] // Player stats concerning enemies
    public float damage;
    public float hitCooldown;
    private bool alreadyHit;

    [Header("Sound Settings")] // Player sounds
    public AudioClip attackingSFX;
    private AudioSource audioSource;
    public AudioClip hurt;
    public AudioClip collect;
    public AudioClip walking;

    float walkSoundCd = .5f;

    private GameManager gameManager;

    private void Awake() //References all components.
    {
        
        renderThing = GetComponent<Renderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
    }
    void Start() // references the components and sets the ground drag.
    {
        renderThing.material = skinsManager.playerSkin;
        maxHealth = health;
        playerRb.drag = groundDrag;
        UnlockPlayer();

        if (gameManager.deathScreen != null)
        {
            gameManager.deathScreen.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (grounded) // if the player is on the ground, add ground drag so the player doesn't slide.
        {
            playerRb.drag = groundDrag;
            gravMultiplier = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded || (Input.GetKeyDown(KeyCode.Space) && onWall)) // makes it so you can jump off of walls and on the ground.
        {
            Jump();
        }

        if (onWall)
        {
            playerRb.drag = groundDrag / 2;
        }
        if (!grounded || !onWall)
        {
            gravMultiplier += Mathf.Pow(Time.deltaTime, 1.4f) ;
            Physics.gravity = new Vector3(0, (-25 - gravMultiplier), 0);
        }
        else
        {
            Physics.gravity = new Vector3(0, -25, 0);
        }

        
        if (Input.GetMouseButtonDown(0))
        {
            if (!alreadyHit)
            {
                StartCoroutine(AttackSequence());
            }
        }


    }

    private void FixedUpdate()
    {
        if (!locked)
        { 
            PlayerMovements();

        }
        
        
    }

    private void PlayerMovements()
    {
        // Checks if the keys for the axis "Horizontal" are being inputted, and gives it a value between -1 and 1.
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        playerRb.AddForce(Vector3.right * movementSpeed * horizontal, ForceMode.Force);
        if(horizontal != 0)
        {
            playerWalking = true;
        }
        else
        {
            playerWalking = false;
        }
        if (topdownView)
        {
            playerRb.AddForce(Vector3.forward * movementSpeed * vertical, ForceMode.Force);
            if(vertical != 0)
            {
                playerWalking = true;
            }
            else
            {
                playerWalking = false;
            }
        }

        if (playerWalking && grounded)
        {
            walkSoundCd -= Time.deltaTime;

            if (walkSoundCd <= 0)
            {

                audioSource.PlayOneShot(walking);
                walkSoundCd = 0.5f;
            }

        }
        else
        {
            audioSource.Stop();
        } 


    }


    private void Jump()
    {
        playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);
        playerRb.drag /= 2;
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            
        }
    }

    public void LockPlayer()
    {
        playerRb.velocity = new Vector3(0, 0, 0);
        locked = true;
    }

    public void UnlockPlayer() { locked = false; }

    public IEnumerator AttackSequence()
    {
        // ATTACK CODE
        Debug.Log("Attacked");
        audioSource.PlayOneShot(attackingSFX);
        alreadyHit = true;
        yield return new WaitForSeconds(hitCooldown);

        alreadyHit = false;
    }

    public void Push()
    {

    }

    public void UpdateHealth(float addedHealth)
    {
        health += addedHealth;

        healthBar.fillAmount = health / 10;
        healthText.text = health + "/10";

        if (health <= 0)
        {
            if (gameManager.deathScreen != null)
            {
                gameManager.deathScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }

    }
}
