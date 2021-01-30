using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidPath : MonoBehaviour
{
    public List<Vector3> positions = new List<Vector3>();

    public float radius;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            positions.Add(child.position);
        }
    }

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

    private int GetNextIndex(int currentIndex)
    {
        if (currentIndex + 1 == transform.childCount) return 0;
        else return currentIndex + 1;
    }

    private Vector3 GetWayPoint(int index)
    {
        return transform.GetChild(index).position;
    }
}
