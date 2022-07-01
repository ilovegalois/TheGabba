using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public List<ZombieController> Enemies { get; private set; }
    public int NumberOfEnemiesTotal { get; private set; }
    public int NumberOfEnemiesRemaining => Enemies.Count;

    void Awake()
    {
        Enemies = new List<ZombieController>();
    }

    public void RegisterEnemy(ZombieController enemy)
    {
        Enemies.Add(enemy);

        NumberOfEnemiesTotal++;
    }

    public void UnregisterEnemy(ZombieController enemyKilled)
    {
        int enemiesRemainingNotification = NumberOfEnemiesRemaining - 1;

        //This one needs to be worked give notificatin when eleminates an enemy but may be not needed.

        //EnemyKillEvent evt = Events.EnemyKillEvent;
        //evt.Enemy = enemyKilled.gameObject;
        //evt.RemainingEnemyCount = enemiesRemainingNotification;
        //EventManager.Broadcast(evt);

        // removes the enemy from the list, so that we can keep track of how many are left on the map
        Enemies.Remove(enemyKilled);
    }
}