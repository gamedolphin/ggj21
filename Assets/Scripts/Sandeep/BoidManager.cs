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

    [SerializeField]
    private Rect worldContainer;

    private void Start()
    {
        var badBoid = Random.Range(0, initialBoidCount);
        for (int i = 0; i < initialBoidCount; i++)
        {
            var pos = new Vector2(Random.Range(worldContainer.xMin, worldContainer.xMax),
                                  Random.Range(worldContainer.yMin, worldContainer.yMax));
            CreateBoid(pos, worldContainer, i==badBoid);
        }

    }

    private void CreateBoid(Vector2 pos, Rect world, bool isBad)
    {
        var boid = Instantiate(boidPrefab);
        boid.transform.SetParent(transform, true);

        boid.transform.position = pos;
        boid.WorldRect = world;
        boid.IsBad = isBad;
        var indx = boids.Count;
        boid.Initialize((b) => {
            boids.Remove(b);
        });

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
