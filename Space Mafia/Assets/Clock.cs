using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Clock : MonoBehaviour
{
    public float health = 50;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Animator anim;
    public HealthBar healthBar;
    public float maxHealth = 50;
    public bool rageMode = false;
    public float GhostTime = 0f;
    public AudioManager Audio;
    public static bool TimeStopped;
    public GameObject Filter;
    public GameObject Behind;
    public GameManager GM;
    public GameObject HQDoor;
    public GameObject Door;
    public GameObject Trigger;
    public Text Objective;
    public Level3Objective L3O;
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
            health = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }
        bgmusic.Pause();
    }

    private void Start()
    {
        Filter.SetActive(false);
        if (HQDoor != null)
        {
            HQDoor.gameObject.SetActive(false);
        }
        healthBar.gameObject.SetActive(true);
        gameObject.tag = "Obstacle";
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
        if (GhostTime > 10)
        {
            GhostTime = 0f;
            StartCoroutine(GhostChange());
        }
    }

    IEnumerator GhostChange()
    {
        anim.Play("Time");
        yield return new WaitForSeconds(1f);
        Filter.SetActive(true);
        Audio.Play("TheWorld");
        TimeStopped = true;
        gameObject.transform.position = Behind.transform.position;
        agent.SetDestination(Behind.transform.position);
        //gameObject.transform.LookAt(Behind.transform.position);
        yield return new WaitForSeconds(3f);
        anim.Play("Def");
        Filter.SetActive(false);
        TimeStopped = false;
        /* if (rageMode)
        {
            Audio.Play("Scream");
            RampageState();

        }*/

    }

    void RampageState()
    {
        Vector3 GhostTP = new Vector3(player.position.x, player.position.y, player.position.z);
        gameObject.transform.position = GhostTP;
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

        if (TimeStopped)
            return;
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
  


        if (health <= 0f)
        {
            WaveSpawner.Score += 300;
            TimeStopped = false;
            Filter.SetActive(false);
            healthBar.gameObject.SetActive(false);
            if (HQDoor != null)
            {
                HQDoor.gameObject.SetActive(true);
            }
            if (Door != null)
            {
                Door.gameObject.SetActive(false);
            }
            Trigger.gameObject.SetActive(false);
            bgmusic.UnPause();
            if (L3O != null)
            {
                L3O.Key();
            }
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
