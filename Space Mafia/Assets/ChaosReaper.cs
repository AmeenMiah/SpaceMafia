using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaosReaper : MonoBehaviour
{
    public float health = 50;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Animator anim;
    public HealthBar healthBar;
    public float maxHealth = 50;
    public bool rageMode = false;
    public Material GhostMat;
    public float GhostTime = 0f;
    public AudioManager Audio;
    public GameObject Behind;
    public GameManager GM;
    public AudioSource bgmusic;
    public GameObject TheOrb;
    public GameObject TheOrb2;
    public GameObject TheOrb3;
    public GameObject TheOrb4;
    public GameObject TheOrb5;
    public GameObject TheOrb6;
    public GameObject TheOrb7;
    public GameObject TheOrb8;
    public GameObject projectile;




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
        healthBar.gameObject.SetActive(true);
        gameObject.tag = "Obstacle";
        GhostMat.color = new Color(1, 1, 1, 1);
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        bgmusic.Pause();
    }

    private void Update()
    {
        GhostTime += Time.deltaTime;
        //Check for sight and attack range
        healthBar.SetHealth(health);
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //if (!playerInSightRange && !playerInAttackRange) Patroling();
        //if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        if (health <= maxHealth / 2 && rageMode == false)
        {
            FindObjectOfType<AudioManager>().Play("Roar");
            rageMode = true;
            Rampage();
        }
        if (GhostTime > 5)
        {
            GhostTime = 0f;
            StartCoroutine(GhostChange());
        }
        if (health <= 0f)
        {
            healthBar.gameObject.SetActive(false);
            Die();
        }
    }

    IEnumerator GhostChange()
    {
        GhostMat.color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(2f);
        GhostMat.color = new Color(1, 1, 1, 1);
        if (rageMode)
        {
            Audio.Play("DigitalScratch");
            gameObject.tag = "Untagged";
            RampageState();
            yield return new WaitForSeconds(0.3f);
            gameObject.tag = "Obstacle";
            yield return new WaitForSeconds(1f);
            Rigidbody rb = Instantiate(projectile, TheOrb.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb.GetComponent<CosmicBall>().enabled = true;
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            Rigidbody rb2 = Instantiate(projectile, TheOrb2.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb2.GetComponent<CosmicBall>().enabled = true;
            rb2.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb2.AddForce(transform.up * 8f, ForceMode.Impulse);

            Rigidbody rb3 = Instantiate(projectile, TheOrb3.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb3.GetComponent<CosmicBall>().enabled = true;
            rb3.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb3.AddForce(transform.up * 8f, ForceMode.Impulse);

            Rigidbody rb4 = Instantiate(projectile, TheOrb4.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb4.GetComponent<CosmicBall>().enabled = true;
            rb4.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb4.AddForce(transform.up * 8f, ForceMode.Impulse);

            Rigidbody rb5 = Instantiate(projectile, TheOrb5.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb5.GetComponent<CosmicBall>().enabled = true;
            rb5.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb5.AddForce(transform.up * 8f, ForceMode.Impulse);

            Rigidbody rb6 = Instantiate(projectile, TheOrb6.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb6.GetComponent<CosmicBall>().enabled = true;
            rb6.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb6.AddForce(transform.up * 8f, ForceMode.Impulse);

            Rigidbody rb7 = Instantiate(projectile, TheOrb7.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb7.GetComponent<CosmicBall>().enabled = true;
            rb7.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb7.AddForce(transform.up * 8f, ForceMode.Impulse);

            Rigidbody rb8 = Instantiate(projectile, TheOrb8.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb8.GetComponent<CosmicBall>().enabled = true;
            rb8.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb8.AddForce(transform.up * 8f, ForceMode.Impulse);

        }

    }

    void RampageState()
    {
        //Vector3 GhostTP = new Vector3 (player.position.x, player.position.y, player.position.z);
        gameObject.transform.position = Behind.gameObject.transform.position;
    }

    void Rampage()
    {

        //anim.Play("RampageModeAnim");
        agent.speed = agent.speed + 10;
        timeBetweenAttacks = 1;
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
        //anim.Play("RinoPause");
        Vector3 PlayerY0 = new Vector3(player.position.x, 0, player.position.z);
        transform.LookAt(PlayerY0);
        agent.SetDestination(transform.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move

        if (!alreadyAttacked)
        {

            Vector3 PlayerY0 = new Vector3(player.position.x, 0, player.position.z);
            agent.SetDestination(player.transform.position);
            transform.LookAt(PlayerY0);

            if (rageMode == false)
            {
                Invoke("NonRageAttack", 3f);
            }
    
            /* Rigidbody rb = Instantiate(projectile, TheOrb.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb.GetComponent<CosmicBall>().enabled = true;
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            Rigidbody rb2 = Instantiate(projectile, TheOrb2.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb2.GetComponent<CosmicBall>().enabled = true;
            rb2.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb2.AddForce(transform.up * 8f, ForceMode.Impulse);

            Rigidbody rb3 = Instantiate(projectile, TheOrb3.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb3.GetComponent<CosmicBall>().enabled = true;
            rb3.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb3.AddForce(transform.up * 8f, ForceMode.Impulse);

            Rigidbody rb4 = Instantiate(projectile, TheOrb4.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb4.GetComponent<CosmicBall>().enabled = true;
            rb4.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb4.AddForce(transform.up * 8f, ForceMode.Impulse);
            
            Rigidbody rb5 = Instantiate(projectile, TheOrb5.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb5.GetComponent<CosmicBall>().enabled = true;
            rb5.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb5.AddForce(transform.up * 8f, ForceMode.Impulse);

            Rigidbody rb6 = Instantiate(projectile, TheOrb6.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb6.GetComponent<CosmicBall>().enabled = true;
            rb6.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb6.AddForce(transform.up * 8f, ForceMode.Impulse);

            Rigidbody rb7 = Instantiate(projectile, TheOrb7.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb7.GetComponent<CosmicBall>().enabled = true;
            rb7.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb7.AddForce(transform.up * 8f, ForceMode.Impulse);

            Rigidbody rb8 = Instantiate(projectile, TheOrb8.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb8.GetComponent<CosmicBall>().enabled = true;
            rb8.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb8.AddForce(transform.up * 8f, ForceMode.Impulse);

            */
            //Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            //rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            //rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void NonRageAttack()
    {

        Rigidbody rb = Instantiate(projectile, TheOrb.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

        rb.GetComponent<CosmicBall>().enabled = true;
        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        rb.AddForce(transform.up * 8f, ForceMode.Impulse);

        Rigidbody rb2 = Instantiate(projectile, TheOrb2.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

        rb2.GetComponent<CosmicBall>().enabled = true;
        rb2.AddForce(transform.forward * 32f, ForceMode.Impulse);
        rb2.AddForce(transform.up * 8f, ForceMode.Impulse);

        Rigidbody rb3 = Instantiate(projectile, TheOrb3.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

        rb3.GetComponent<CosmicBall>().enabled = true;
        rb3.AddForce(transform.forward * 32f, ForceMode.Impulse);
        rb3.AddForce(transform.up * 8f, ForceMode.Impulse);

        Rigidbody rb4 = Instantiate(projectile, TheOrb4.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

        rb4.GetComponent<CosmicBall>().enabled = true;
        rb4.AddForce(transform.forward * 32f, ForceMode.Impulse);
        rb4.AddForce(transform.up * 8f, ForceMode.Impulse);
    }

    private void ResetAttack()
    {

        Vector3 PlayerY0 = new Vector3(player.position.x, 0, player.position.z);
        transform.LookAt(PlayerY0);
        //anim.Play("RinoPause");
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
        if (GM != null)
        {
            GM.FinalBossComplete();
        }
        WaveSpawner.Score += 300;
        bgmusic.UnPause();
        Destroy(gameObject);

    }
}
