﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemys : MonoBehaviour
{
    public GameObject Enemy;
    public int quantity;
    public float spawnRate;
    public GameObject Path;
    public int pathSize;

    private float counter;
    private int enemyNumber;
    private float randomRate;
    private bool onetime;
    // Start is called before the first frame update
    void Start()
    {
        onetime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (onetime)
        {
            randomRate = Random.Range(spawnRate - 0.8f, spawnRate + 0.8f);
            onetime = false;
        }
        counter += Time.deltaTime;
        if (counter >= randomRate && enemyNumber<=quantity)
        {
            onetime = true;
            counter = 0;
            enemyNumber++;
            GameObject enemy = Instantiate(Enemy,transform.position,Quaternion.identity);
            enemy.GetComponent<EnemyFollowPath>().path = Path;
            enemy.GetComponent<EnemyFollowPath>().pathPoints.Capacity=pathSize;
        }
    }
}
