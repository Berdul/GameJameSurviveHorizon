using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public static class NavMeshHelper {
    
    public static bool RandomPointInNavMesh(Vector2 center, float range, out Vector2 result) {
        for (int i = 0; i < 50; i++) {
            Vector3 randomPoint = center + Random.insideUnitCircle * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
                result = hit.position;
                return true;
            }
        }
        result = Vector2.zero;
        return false;
    }

}
