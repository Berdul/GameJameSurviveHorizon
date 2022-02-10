using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobBehavior : MonoBehaviour
{
    
    private bool isEnemy;

    private int healthPoints;
    private int maxHealthPoints;
    private int armor;
    private int strenght;

    private Transform player;

    public NavMeshAgent navMeshAgent;
    public float moveSpeed;


    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.speed = moveSpeed;
        navMeshAgent.destination = player.position;
    }
}
