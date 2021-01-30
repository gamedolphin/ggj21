using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidPath : MonoBehaviour
{
    const float wayPointGizmoRadius = 0.25f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < transform.childCount; i++)
        {
            int j = GetNextIndex(i);
            Gizmos.DrawSphere(GetWayPoint(i), wayPointGizmoRadius);
            Gizmos.DrawLine(GetWayPoint(i), GetWayPoint(j));
        }
    }
    
    public int GetNextIndex(int currentIndex)
    {
        if (currentIndex + 1 == transform.childCount) return 0;
        else return currentIndex + 1;
    }

    public Vector3 GetWayPoint(int index)
    {
        return transform.GetChild(index).position;
    }

}
