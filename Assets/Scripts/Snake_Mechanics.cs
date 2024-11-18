using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Snake_Mechanics : MonoBehaviour
{
    public Transform Snake_head;
    public Vector2 direction = Vector2.right;
    public float speed;
    public Transform Food;
    public Collider2D grid;
    public Bounds bound;
    private int score;
    private int highScore;
    public GameplayUI gameplayUIReference;
    List<Transform> list = new List<Transform>();
    public Transform snake_segment;
    public AudioManager audioManagerReference;
    public int currentLevel;
    public LayerMask obstacleLayer;
    public GameObject gameplayScreen;

    private void Awake()
    {
        speed = PlayerPrefs.GetFloat("snakeSpeed", 0.2f);
    }
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        highScore = PlayerPrefs.GetInt("highScore" + currentLevel.ToString(), 0);
        gameplayUIReference.DisplayScore(score);
        gameplayUIReference.DisplayHighScore(highScore);
        bound = grid.bounds;
        Time.fixedDeltaTime = speed;
        list.Add(Snake_head);
        grow();
        grow();
        grow();
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

            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("highScore"+currentLevel.ToString(), highScore);
            }
            audioManagerReference.PlayGameOverSoundEffect();
            gameplayUIReference.DisplayGameOverScore(score);
            gameplayUIReference.DisplayGameOverScreen();
            gameplayScreen.SetActive(false);
        }
        else if (collision.tag == "Food")
        {
            randomizeFood();
            grow();
            score++;
            gameplayUIReference.DisplayScore(score);
            audioManagerReference.PlayEatSoundEffect();
        }
        else if (collision.tag == "Portal1")
        {
            Transform portal = GameObject.FindGameObjectWithTag("Portal2").GetComponent<Transform>();
            Snake_head.position = new Vector2(portal.position.x + direction.x, portal.position.y + direction.y);
            audioManagerReference.PlayTeleportSoundEffect();
        }
        else if (collision.tag == "Portal2")
        {
            Transform portal = GameObject.FindGameObjectWithTag("Portal1").GetComponent<Transform>();
            Snake_head.position = new Vector2(portal.position.x + direction.x, portal.position.y + direction.y);
            audioManagerReference.PlayTeleportSoundEffect(); 
        }
    }

    void randomizeFood()
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
        Food.position = new Vector2(x,y);   
    }
    void grow()
    {
        Transform temp = Instantiate(snake_segment);
        temp.position = list[list.Count-1].position;
        list.Add(temp);
    }
    public void UpdateSnakeSpeed(float speed)
    {
        this.speed = speed;
        Time.fixedDeltaTime = speed;
        PlayerPrefs.SetFloat("snakeSpeed", speed);
    } 
}
