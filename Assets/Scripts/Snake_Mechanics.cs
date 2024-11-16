using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_Mechanics : MonoBehaviour
{
    public Transform Snake_head;
    public Vector2 direction = Vector2.right;
    public float speed = 0.1f;
    public Transform Food;
    public Collider2D grid;
    public Bounds bound;


    List<Transform> list = new List<Transform>();
    public Transform snake_segment;
    // Start is called before the first frame update
    void Start()
    {
        bound = grid.bounds;
        Time.fixedDeltaTime = speed;
        list.Add(Snake_head);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {

            direction = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
        }
    }
    private void FixedUpdate()
    {
        for(int i = list.Count - 1; i > 0; i--)
        {
            list[i].position = list[i - 1].position;
        }

        Snake_head.position = new Vector2(Snake_head.position.x + direction.x, Snake_head.position.y + direction.y);
       /*
        (Snake_head.position.x + direction.x, 
            Snake_head.position.y + direction.y); */
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Obstacle")
        {
            Snake_head.position = Vector2.zero;
            for(int i = 1; i < list.Count; i++)
            {
                Destroy(list[i].gameObject);
            }
            list.Clear();
            list.Add(Snake_head);
        }
        else if (collision.tag == "Food")
        {
            randomizeFood();
            grow();
            
        }
    }

    void randomizeFood()
    {
        float x = Random.Range(bound.min.x,bound.max.x);
        float y = Random.Range(bound.min.y, bound.max.y);
        x = Mathf.Round(x);
        y = Mathf.Round(y);
        Food.position = new Vector2(x,y);
    }
    void grow()
    {
        Transform temp = Instantiate(snake_segment);
        temp.position = list[list.Count-1].position;
        list.Add(temp);
    }
}
