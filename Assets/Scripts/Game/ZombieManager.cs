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

        Enemies.Remove(enemyKilled);
    }
}