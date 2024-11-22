// 21i-0408
// 21i-0425

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
    public Transform Portal1;
    public Transform Portal2;
    public Collider2D grid;
    public Bounds bound;
    public LayerMask obstacleLayer;
    public float delay;

    private void Start()
    {
        bound = grid.bounds;
        InvokeRepeating("RandomizePortals", 0f, delay);
    }

    private void RandomizePortals()
    {
        float x;
        float y;
        while(true)
        {

            x = Random.Range(bound.min.x,bound.max.x);
            y = Random.Range(bound.min.y, bound.max.y);
            x = Mathf.Round(x);
            y = Mathf.Round(y);

            Vector2 newPosition = new Vector2(x, y);
            Collider2D blocked = Physics2D.OverlapPoint(newPosition, obstacleLayer);
            if (blocked == null)
            {
                break;
            }
        }
        Portal1.position = new Vector2(x,y);

        while(true)
        {

            x = Random.Range(bound.min.x,bound.max.x);
            y = Random.Range(bound.min.y, bound.max.y);
            x = Mathf.Round(x);
            y = Mathf.Round(y);

            Vector2 newPosition = new Vector2(x, y);
            Collider2D blocked = Physics2D.OverlapPoint(newPosition, obstacleLayer);
            if (blocked == null)
            {
                break;
            }
        }
        Portal2.position = new Vector2(x,y);   
    }
}
