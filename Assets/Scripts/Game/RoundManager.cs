using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class RoundManager : MonoBehaviour
{
    public int numberOfZombies = 10;
    public List<GameObject> typesOfZombies = new List<GameObject>();

    public float spawnPointRadius = 1f;

    ZombieManager zombieManager;
    int currentNumberZombie;
    int numberOfZombieTypes;


    public GridGraph navGraph;

    // Start is called before the first frame update

    void Start()
    {
        navGraph = AstarPath.active.data.gridGraph;

        numberOfZombieTypes = typesOfZombies.Count;

        zombieManager = GetComponent<ZombieManager>();
        for(int i = 0; i < numberOfZombies; i++)
        {

            addZombie();
        }    

    }

    // Update is called once per frame
    void Update()
    {
        //currentNumberZombie = zombieManager.NumberOfEnemiesTotal;

        //while(currentNumberZombie < numberOfZombies)
        //{
         //   addZombie();
        //}
    }
    void addZombie()
    {
        int rand = Random.Range(0, numberOfZombieTypes - 1);
        GameObject zombie = Instantiate(typesOfZombies[rand], RandomPoint(), Quaternion.identity);
    }
    Vector2 RandomPoint()
    {
        float xW = (navGraph.width * navGraph.nodeSize) / 2;
        float yD = (navGraph.depth * navGraph.nodeSize) / 2;
        
        Vector3 pratiBimbh = navGraph.center;

        Vector2 point = new((Random.Range((pratiBimbh.x+spawnPointRadius), xW )* NegtPos()), Random.Range(pratiBimbh.y+spawnPointRadius, yD)*NegtPos());
        return point;
    }
    float NegtPos() //???? ???? ??? ???? ???? ??????
    {
        return Random.Range(0f, 1f)*2 - 1;
    }


}
