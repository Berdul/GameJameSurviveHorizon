using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobBehavior : MonoBehaviour
{
    private static Color ALLY_COLOR = new Color(0, 48f/255f, 73/255f, 1);
    private static Color ENEMY_COLOR = new Color(214/255f, 40/255f, 40/255f, 1);

    public bool isEnemy;

    private int healthPoints;
    private int maxHealthPoints;
    private int armor;
    private int strenght;

    private Transform player;

    public NavMeshAgent navMeshAgent;
    public float moveSpeed;
    public float sigthDistance;


    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.player.transform;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Search for a mob of the opposite team in range to attack.
        // If found, go attack him.
        // If not found, wander.
        // If wandering, is enemy and see player, attack player.

        Collider2D[] objectsInSight = Physics2D.OverlapCircleAll(transform.position, sigthDistance);
        Debug.Log("objectis in sight : " + objectsInSight.Length);
        var mobsInSight = new List<Collider2D>();
        var playerDetected = false;

        foreach (var objDetected in objectsInSight) {
            if (objDetected.CompareTag("mob") && objDetected.GetComponent<MobBehavior>().isEnemy != isEnemy) {
                mobsInSight.Add(objDetected);
            } else if (objDetected.CompareTag("Player")) {
                playerDetected = true;
            }
        }
        
        Vector2? target = null;
        float? targetDistance = null;
        if (mobsInSight.Count > 0) { // Mobs detected -> Attack mobs
            foreach(var mob in mobsInSight) {
                if (target == null && targetDistance == null) {
                    target = mob.gameObject.transform.position;
                    targetDistance = Vector2.Distance(mob.transform.position, transform.position);
                } else {
                    var tmpDistance = Vector2.Distance(mob.transform.position, transform.position);
                    if (tmpDistance < targetDistance) {
                        target = mob.gameObject.transform.position;
                        targetDistance = tmpDistance;
                        Debug.Log("Set target to MOB at pos " + target);
                    }
                }
                navMeshAgent.speed = moveSpeed;
            }
        } else if (playerDetected && !isEnemy) { // Ally, no mobs detected, player detected -> Attack player
            target = player.position;
            navMeshAgent.speed = moveSpeed;
            Debug.Log("Set target to PLAYER");
        } else { // Nothing to attack -> wander
            Vector2 wanderDestination;
            if (NavMeshHelper.RandomPointInNavMesh(transform.position, 10f, out wanderDestination)) {
                target = wanderDestination;
                navMeshAgent.speed = moveSpeed / 2;
                Debug.Log("WANDERING");
            }
        }
        navMeshAgent.destination = (Vector3) target;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sigthDistance);
    }

    public void setColor() {
        if (isEnemy) {
            GetComponent<SpriteRenderer>().color = ENEMY_COLOR;
        } else {
            GetComponent<SpriteRenderer>().color = ALLY_COLOR;
        }
    }

    private bool isPlayerInRange(float range) {
        return Vector3.Distance(player.position, transform.position) < range;
    }
}
