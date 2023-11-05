using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class RinoBoss : MonoBehaviour
{
    public float health = 50;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public GameObject projectile;
    public Animator anim;
    public HealthBar healthBar;
    public float maxHealth = 50;
    public bool rageMode = false;
    public GameManager GM;
    public AudioSource bgmusic;


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
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.gameObject.SetActive(true);
        bgmusic.Pause();
    }

    private void Update()
    {
        //Check for sight and attack range
        Debug.Log(health);
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
    }

    void Rampage()
    {
    
        anim.Play("RampageModeAnim");
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
        anim.Play("RinoPause");
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
            //Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            //rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            //rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }



    private void ResetAttack()
    {
        Vector3 PlayerY0 = new Vector3(player.position.x, 0, player.position.z);
        transform.LookAt(PlayerY0);
        anim.Play("RinoPause");
        alreadyAttacked = false;
    }

    public void TakeDamage(float amount)
    {

        switch (rageMode)
        {
                case true:
                    health -= amount / 2;
                    break;
                case false:
                    health -= amount;
                    break;
                default:
                    health -= amount;
                    break;
        }


       
        if (health <= 0f)
        {
            WaveSpawner.Score += 100;
            healthBar.gameObject.SetActive(false);
            Die();
        }
    }

    void Die()
    {
        bgmusic.UnPause();
        if (GM != null)
        {
            GM.Level1Complete();
        }
        Destroy(gameObject);
    }

}
