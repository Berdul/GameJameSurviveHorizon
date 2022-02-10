using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public GameObject mobPrefab;
    public float range = 10.0f;

    private int enemyAllyBalance = 0; // Positive -> more allies than enemies, Negative -> more enemies than allies, 0 -> balanced

    private void Start() {
        StartCoroutine(SpawnMobs());        
    }

    IEnumerator SpawnMobs() {
        //while(true) {
            Vector2 point;
            if (NavMeshHelper.RandomPointInNavMesh(PlayerManager.instance.player.transform.position, range, out point)) {
                Debug.DrawRay(point, Vector3.up, Color.blue, 0.01f);
                GameObject mob = Instantiate(mobPrefab, point, Quaternion.identity);
                bool nextMobShouldBeEnemy = NextMobShouldBeEnemy();
                mob.GetComponent<MobBehavior>().isEnemy = nextMobShouldBeEnemy;
                mob.GetComponent<MobBehavior>().setColor();
                if (nextMobShouldBeEnemy) {
                    enemyAllyBalance--;
                } else {
                    enemyAllyBalance++;
                }
            }
            yield return new WaitForSeconds(3f);
        //}
    }

    private bool NextMobShouldBeEnemy() {
        return enemyAllyBalance < 0 ? false : true;
    }
}
