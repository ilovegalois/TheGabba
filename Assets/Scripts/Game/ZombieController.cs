using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class ZombieController : MonoBehaviour
{

    public Transform Player;
    public float speed = 20f;
    public float nextWayPointDistance = 30f;

    public float pathTimeDistance = 2f; //Path recalculation time
    public float patrolRadius = 1f;
    public float sightRange = 1f;
    public LayerMask actorLayer;


    int currentWayPoint = 0;
    bool reachedEndofPath = false;
    
    private float currentTimeDistance; 
    public GridGraph navGraph;

    Path path;

    Seeker seeker;
    Rigidbody2D rb;

    private Animator animator;

    bool playerInSightRange;
    // Start is called before the first frame update
    void Awake()
    {
        playerInSightRange = false;

        navGraph = AstarPath.active.data.gridGraph;
        currentTimeDistance = Time.time;

        //InvokeRepeating("UpdatePath", 0f, 0.5f);
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        
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

    [System.Obsolete]
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

    [System.Obsolete]
    Vector2 RandomPoint()
    {
        float xW = (navGraph.width * navGraph.nodeSize)/2;
        float yD = (navGraph.depth * navGraph.nodeSize) / 2;
        Vector2 point = new(Random.RandomRange(-xW / 2, xW / 2), Random.RandomRange(-yD / 2, yD / 2));
        Debug.Log(point);
        return point;
    }
}
