using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    private List<Boid> boids = new List<Boid>();

    [SerializeField]
    private Boid boidPrefab = null;

    [SerializeField]
    private int initialBoidCount = 10;

    [SerializeField]
    private float borderBuffer;

    private void Start()
    {
        Vector3 dims = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        var width = dims.x;
        var height = dims.y;
        for (int i = 0; i < initialBoidCount; i++)
        {
            var pos = new Vector2(Random.Range(-width, width), Random.Range(-height, height));
            var world = new Rect(-width + borderBuffer, -height + borderBuffer,
                                 width * 2 - borderBuffer, height * 2 - borderBuffer);
            CreateBoid(pos, world);
        }
    }

    private void CreateBoid(Vector2 pos, Rect world)
    {
        var boid = Instantiate(boidPrefab);
        boid.transform.SetParent(transform, true);

        boid.transform.position = pos;
        boid.WorldRect = world;

        boids.Add(boid);
    }

    private void Update()
    {
        foreach (var boid in boids)
        {
            boid.UpdateBoid(boids);
        }
    }
}
