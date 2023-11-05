using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterController controller;
    public Transform cam;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public static float speed = 6f;
    public static float sprintSpeed = 12f;
    public float gravity = -9.81f;

    //Jumping Variables
    public float jumpHeight = 3;
    public bool isJumping = false;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    
    //Dodge Variables
    public bool isDodging = false;
    public float DogdeCooldown = 5;
    private float ActCooldown;
    public float PushAmt = 3;
    public int DodgeSprintConsumation = 500;

    public int sprintTime;
    public int maxSprintTime = 1000;
    public bool sprintEnable = true;
    public bool isSprinting = false;
    public SprintBar sprintBar;
    public Animator Anim;
    public HealthBar healthBar;
    public PlayerCollision PlayerColl;
    public bool HealthRegened = false;
    public bool Moving = false;
    private float TimetoSprint;
    private float SprintRegenTime1;
    private float SprintRegenTime2;

    public AudioManager Audio;
    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        
        Clock.TimeStopped = false;
        sprintTime = maxSprintTime;
        sprintBar.SetMaxSprint(maxSprintTime);
        Cursor.lockState = CursorLockMode.Locked;

    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        
        if (Clock.TimeStopped)
            return;
        float horziontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horziontal, 0f, vertical).normalized;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        sprintBar.SetSprint(sprintTime);

        if (isSprinting == false && sprintTime < maxSprintTime && direction.magnitude < 0.1f)
        {
            SprintRegenTime1 += Time.deltaTime;
            if(SprintRegenTime1 > 0.1)
            {
                SprintRegenTime1 = 0;
                sprintTime += 30;
                //Mathf.Lerp(sprintTime, sprintTime+60, 1f);
            }
            

        }
        else
        {
            SprintRegenTime1 = 0;
        }
        if (isSprinting == false && Moving == false && PlayerColl.playerCurrentHealth < (PlayerColl.maxHealth+1) && HealthRegened == false && PlayerColl.TakenDamage == false)
        {
            HealthRegened = true;
            //Debug.Log(PlayerColl.playerCurrentHealth);
            StartCoroutine(HealthRegen());
        }

     



        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            Moving = true;


            if (Input.GetButton("Vertical") && Input.GetButton("Sprint") && sprintEnable == true)
            {
                isSprinting = true;
            }
            else
            {
                isSprinting = false;
            }

            if (isSprinting == true)
            {

                controller.Move(moveDir.normalized * sprintSpeed * Time.deltaTime);
                TimetoSprint += Time.deltaTime;
                if (TimetoSprint > 0.1)
                {
                    TimetoSprint = 0;
                    sprintTime -= 10;
                }
            }
            else
            {
                TimetoSprint = 0;
            }

            bool Roll = Input.GetButtonDown("Roll");

            if (ActCooldown <=0 && sprintTime > DodgeSprintConsumation)
            {
                if (Roll)
                {
                    StartCoroutine(Dodge());
                }
            }
            else
            {
                
                ActCooldown -= Time.deltaTime;
            }

            
            if (sprintTime < 10)
            {
                sprintEnable = false;
                isSprinting = false;
                if (sprintTime < 0)
                {
                    sprintTime = 0;
                }
            }
            else if (sprintTime > 100)
            {
                sprintEnable = true;
            }

            if (isSprinting == true && sprintEnable == false)
            {
                isSprinting = false;
            }
            if (isSprinting == false && sprintTime < maxSprintTime)
            {
                SprintRegenTime2 += Time.deltaTime;
                if (SprintRegenTime2 > 0.1)
                {
                    sprintTime += 15;
                    SprintRegenTime2 = 0;
                }
            }
            else
            {
                SprintRegenTime2 = 0;
            }


        }
        else
        {
            Moving = false;
        }

        if(isGrounded && velocity.y <0)
        {
            velocity.y = -2f;
            isJumping = false;

        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJumping = true;
            
        }

   
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    IEnumerator Dodge()
    {
        isDodging = true;
        ActCooldown = DogdeCooldown;
        sprintTime -= DodgeSprintConsumation;
        Anim.Play("Dodge");
        Debug.Log("It Works");
        yield return new WaitForSeconds(2f);
        isDodging = false;

    }

    IEnumerator HealthRegen()
    {
        yield return new WaitForSeconds(0.2f);
        HealthRegened = false;
        if (isSprinting == false && Moving == false && PlayerColl.playerCurrentHealth < (PlayerColl.maxHealth + 1) && PlayerColl.TakenDamage == false)
        {
            PlayerColl.playerCurrentHealth += 1;
            healthBar.SetHealth(PlayerColl.playerCurrentHealth);
        }
            
    }
}



