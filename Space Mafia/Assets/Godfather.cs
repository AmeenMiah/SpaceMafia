using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Godfather : MonoBehaviour
{
    public float health = 50f;
    public float maxHealth = 50;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public GameObject projectile;
    public HealthBar healthBar;
    public bool rageMode = false;
    public GameObject TheOrb;
    public GameObject RageOrb1;
    public GameObject RageOrb2;
    public GameObject RageOrb3;
    public Animator Anim;
    public bool RageAttackOn = false;
    int TeleportNum = 1;
    int TeleportNumTemp = 1;
    public GameManager GM;
    public AudioSource bgmusic;

    public GameObject TeleportLoc1;
    public GameObject TeleportLoc2;
    public GameObject TeleportLoc3;
    public GameObject TeleportLoc4;
    public GameObject TeleportLoc5;
    public GameObject TeleportParticle;
    public float TeleportTime;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player 2").transform;
        agent = GetComponent<NavMeshAgent>();
        if (WaveSpawner.WaveNum > 49)
        {
            maxHealth = maxHealth + (WaveSpawner.WaveNum * 10);
        }
    }
    private void Start()
    {
        TeleportNum = TeleportNumTemp;
        healthBar.gameObject.SetActive(true);
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        if (bgmusic != null)
        {
            bgmusic.Pause();
        }
    }
    private void Update()
    {
        healthBar.SetHealth(health);
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        TeleportTime += Time.deltaTime;

        if (!playerInSightRange && !playerInAttackRange && !RageAttackOn) Patroling();
        if (playerInSightRange && !playerInAttackRange && !RageAttackOn) ChasePlayer();
        if (playerInAttackRange && playerInSightRange && !RageAttackOn) AttackPlayer();

        if (health <= maxHealth / 2 && rageMode == false)
        {
            FindObjectOfType<AudioManager>().Play("Roar");
            rageMode = true;
            
        }

        if (health <= 0f)
        {
            Debug.Log("Death");
            if (GM != null)
            {
                GM.Level4Complete();
            }
            healthBar.gameObject.SetActive(false);
            Die();
        }

        if (TeleportTime > 15)
        {
            TeleportTime = 0;
            StartCoroutine(RandomNum());
        }
    }

    IEnumerator RandomNum()
    {
        
        while (TeleportNumTemp == TeleportNum)
        {
            TeleportNumTemp = Random.Range(1, 5);
        }
        TeleportNum = TeleportNumTemp;
        yield return new WaitForSeconds(2f);
        switch (TeleportNum)
        {
            case 1:
                GameObject TPEffect = Instantiate(TeleportParticle, gameObject.transform.position, Quaternion.LookRotation(gameObject.transform.rotation.eulerAngles));
                yield return new WaitForSeconds(0.2f);
                gameObject.transform.position = TeleportLoc1.transform.position;
                Destroy(TPEffect, 2f);
                break;
            case 2:
                GameObject TPE2ffect = Instantiate(TeleportParticle, gameObject.transform.position, Quaternion.LookRotation(gameObject.transform.rotation.eulerAngles));
                yield return new WaitForSeconds(0.2f);
                gameObject.transform.position = TeleportLoc2.transform.position;
                Destroy(TPE2ffect, 2f);
                break;
            case 3:
                GameObject TP3Effect = Instantiate(TeleportParticle, gameObject.transform.position, Quaternion.LookRotation(gameObject.transform.rotation.eulerAngles));
                yield return new WaitForSeconds(0.2f);
                gameObject.transform.position = TeleportLoc3.transform.position;
                Destroy(TP3Effect, 2f);
                break;
            case 4:
                GameObject TP4Effect = Instantiate(TeleportParticle, gameObject.transform.position, Quaternion.LookRotation(gameObject.transform.rotation.eulerAngles));
                yield return new WaitForSeconds(0.2f);
                gameObject.transform.position = TeleportLoc4.transform.position;
                Destroy(TP4Effect, 2f);
                break;
            case 5:
                GameObject TP5Effect = Instantiate(TeleportParticle, gameObject.transform.position, Quaternion.LookRotation(gameObject.transform.rotation.eulerAngles));
                yield return new WaitForSeconds(0.2f);
                gameObject.transform.position = TeleportLoc5.transform.position;
                Destroy(TP5Effect, 2f);
                break;
            default:
                break;
        }
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        Vector3 PlayerY0 = new Vector3(player.position.x, 0, player.position.z);
        transform.LookAt(PlayerY0);

        if (!alreadyAttacked)
        {
            switch (rageMode)
            {
                case true:
                    Anim.Play("RageAttack");
                    RageAttackOn = true;
                    StartCoroutine(RageAttack());
                    alreadyAttacked = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks+3);
                    break;
                case false:
                    Anim.Play("Attack");
                    Rigidbody rb = Instantiate(projectile, TheOrb.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

                    rb.GetComponent<CosmicBall>().enabled = true;
                    rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                    rb.AddForce(transform.up * 8f, ForceMode.Impulse);
                    ///End of attack code

                    alreadyAttacked = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                    break;
                default:
                    Rigidbody rb4 = Instantiate(projectile, TheOrb.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

                    rb4.AddForce(transform.forward * 32f, ForceMode.Impulse);
                    rb4.AddForce(transform.up * 8f, ForceMode.Impulse);
                    ///End of attack code

                    alreadyAttacked = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks); ;
                    break;
            }
            
        }
    }

    IEnumerator RageAttack()
    {
        Rigidbody rb1 = Instantiate(projectile, RageOrb1.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb1.useGravity = false;
        Rigidbody rb2 = Instantiate(projectile, RageOrb2.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb2.useGravity = false;
        Rigidbody rb3 = Instantiate(projectile, RageOrb3.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb3.useGravity = false;

        yield return new WaitForSeconds(0.5f);
        if (rb1 != null)
        {
            rb1.useGravity = true;
            rb1.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb1.AddForce(transform.up * 8f, ForceMode.Impulse);
        }
        yield return new WaitForSeconds(0.1f);
        if (rb1 != null)
            rb1.GetComponent<CosmicBall>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        if (rb2 != null)
        {
            rb2.useGravity = true;
            rb2.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb2.AddForce(transform.up * 8f, ForceMode.Impulse);
        }
        yield return new WaitForSeconds(0.1f);
        if (rb2 != null)
        {
            rb2.GetComponent<CosmicBall>().enabled = true;
        }
        yield return new WaitForSeconds(0.5f);
        if (rb3 != null)
        {
            rb3.useGravity = true;
            rb3.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb3.AddForce(transform.up * 8f, ForceMode.Impulse);
        }
        yield return new WaitForSeconds(0.1f);
        if (rb3 != null)
            rb3.GetComponent<CosmicBall>().enabled = true;
        RageAttackOn = false;
        ///End of attack code


    }


    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float amount)
    {

        switch (rageMode)
        {
            case true:
                health -= amount;
                break;
            case false:
                health -= amount * 1.5f;
                break;
            default:
                health -= amount;
                break;
        }



        
    }

    void Die()
    {
        if (bgmusic != null)
        {
            bgmusic.UnPause();
        }    
        WaveSpawner.Score += 400;
        Debug.Log("Death1");
        Destroy(gameObject);
    }

}
