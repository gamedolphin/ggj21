using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidPath : MonoBehaviour
{
    public List<Vector3> positions = new List<Vector3>();

    public float radius;

    private void Awake()
    {
        var transforms = new List<Transform>();
        GetComponentsInChildren(true, transforms);

        foreach(var t in transforms)
        {
            positions.Add(t.position);
        }
    }
}
