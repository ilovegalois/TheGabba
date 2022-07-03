using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;
using UnityEngine.UI;


public class ZombieController : MonoBehaviour
{
    [Tooltip("Meth the vampire posses")]
    public int methPossion = 5;
    [Tooltip("Cash Prefab")]
    public GameObject cashPrefab;

    [Tooltip("Speed of Player")]
    public float speed = 20f;
    [Tooltip("Node distance between rounded path")]
    public float nextWayPointDistance = 3f;

    [Tooltip("Time of each path cal cycle")]
    public float pathTimeDistance = 2f; //Path recalculation time
    [Tooltip("Radius to next potential patrol direction")] 
    public float patrolRadius = 1f;
    [Tooltip("Sixth sense Range of Zombie")]
    public float sightRange = 1f;
    [Tooltip("Only Actor layer to avoid collision")]
    public LayerMask actorLayer;


    [Tooltip("Which path node currently onto")]
    int currentWayPoint = 0;
    [Tooltip("If End of the path reached stop")]
    bool reachedEndofPath = false;

    [Tooltip("Time passed since last path refresh")]
    private float currentTimeDistance;
    [Tooltip("Navigation Graph details")]
    GridGraph navGraph;

    [Tooltip("Calculated Path")]
    Path path;

    [Tooltip("Seeker script")]
    Seeker seeker;
    [Tooltip("RigidBody of Zombie itself")]
    Rigidbody2D rb;

    [Tooltip("Animation")]
    private Animator animator;

    [Tooltip("Checks if player in SightRange")]
    bool playerInSightRange;

    [Tooltip("Player Positon")]
    Transform Player;
    
    [Tooltip("Health and healthBar")]
    Health zombieHealth;
    public Image healthBar;

    [Tooltip("Zombie manager to Register and Unregister the zombie in local identification")]
    ZombieManager zombieManager;

    // Start is called before the first frame update
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        zombieManager = GameObject.FindObjectOfType<ZombieManager>();

        navGraph = AstarPath.active.data.gridGraph;
    }
    void Start()
    {
        zombieHealth = GetComponent<Health>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        zombieHealth.OnDie += OnDeath;

        zombieManager.RegisterEnemy(this);

        playerInSightRange = false;
        currentTimeDistance = Time.time;

    }
    private void Update()
    {
        if (currentTimeDistance + pathTimeDistance <= Time.time)
        {
                currentTimeDistance = Time.time;
                if (playerInSightRange)
                {
                    Chasing();
                }
                else
                {
                    Patrolling();
                }
        }
    }

    void FixedUpdate()
    {
        
        PathPipeline();
        AnimationController();
        HealthBar();
    }

    void HealthBar()
    {
        healthBar.fillAmount = zombieHealth.CurrentHealth / zombieHealth.MaxHealth;
    }

    void UpdatePath(Vector2 playerPosition)
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, playerPosition, OnPathComplete);

    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }
    void PathPipeline()
    {
        if (path == null)
        {
            return;
        }
        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndofPath = true;
            return;
        }
        else
        {
            reachedEndofPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;

        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }
    }

    void AnimationController()
    {
        animator.SetFloat("m_Hor", rb.velocity.x);
        animator.SetFloat("m_Vert", rb.velocity.y);
    }

    // Update is called once per frame

    void Patrolling()
    {
        Vector2 patrolPoint = RandomPoint();
        UpdatePath(patrolPoint);
        bool playerWithinSphere = Physics2D.OverlapCircle(rb.position, sightRange, actorLayer);
        RaycastHit2D rayCastHit = Physics2D.Raycast(rb.position, ((Vector2)Player.position - rb.position).normalized, sightRange*2);
        if(rayCastHit.collider != null)
        {
            GameObject gob = rayCastHit.collider.gameObject;
            if (gob.GetComponent<Actor>() != null)
           {
                if(gob.GetComponent<Actor>().Affiliation == 0) playerInSightRange = true;
            }
        }
    }

    void Chasing()
    {
        UpdatePath(Player.position);
        bool playerWithinSphere = Physics2D.OverlapCircle(rb.position, sightRange, actorLayer);
        
        //Start Patrolling Again if lost the target
        if(!playerWithinSphere) playerInSightRange = false;
    }

    Vector2 RandomPoint()
    {
        float xW = (navGraph.width * navGraph.nodeSize)/2;
        float yD = (navGraph.depth * navGraph.nodeSize) / 2;
        Vector2 point = new(Random.Range(-xW, xW), Random.Range(-yD, yD));
        return point;
    }

    void OnDeath()
    {
        zombieManager.UnregisterEnemy(this);
        RoundManager.roundMeth += methPossion;
        Instantiate(cashPrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);

        Destroy(this.gameObject);
    }
}
