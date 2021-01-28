using UnityEngine;

public class DebugBoidInput : MonoBehaviour
{
    [SerializeField]
    private Boid boid;

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        boid.SetTarget(new Vector2(mousePos.x,mousePos.y));
    }
}
