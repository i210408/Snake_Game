using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SnakeMovement : MonoBehaviour
{
    public Transform snakeHead;
    public Vector2 direction = Vector2.right;
    public float speed;
    public Transform Food;
    public Collider2D grid;
    public Bounds bound;
    private int score;
    private int highScore;
    public GameplayUI gameplayUIReference;
    public List<GameObject> tailSegments = new List<GameObject>();
    public GameObject tailSegmentPrefab;
    public AudioManager audioManagerReference;
    public int currentLevel;
    public LayerMask obstacleLayer;
    public GameObject gameplayScreen;

    private void Awake()
    {
        speed = PlayerPrefs.GetFloat("snakeSpeed", 0.2f);
    }

    void Start()
    {
        score = 0;
        highScore = PlayerPrefs.GetInt("highScore" + currentLevel.ToString(), 0);
        gameplayUIReference.DisplayScore(score);
        gameplayUIReference.DisplayHighScore(highScore);
        bound = grid.bounds;
        Time.fixedDeltaTime = speed;
        snakeHead.rotation = Quaternion.Euler(0f, 0f, 90f);

        // Create and initialize 3 tail segments
        for (int i = 0; i < 3; i++)
        {
            GameObject tailSegment = Instantiate(tailSegmentPrefab, snakeHead.position - (Vector3)(direction * (i + 1)), Quaternion.identity);
            tailSegment.GetComponent<Renderer>().material.color = Color.green;
            tailSegments.Add(tailSegment);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            direction = Vector2.up;
            snakeHead.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction = Vector2.down;
            snakeHead.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = Vector2.left;
            snakeHead.rotation = Quaternion.Euler(0f, 0f, 270f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
            snakeHead.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
    }

    private void FixedUpdate()
    {
        // Move the tail segments
        for (int i = tailSegments.Count - 1; i > 0; i--)
        {
            tailSegments[i].transform.position = tailSegments[i - 1].transform.position;
        }

        // Move the first tail segment to the snake head's position
        if (tailSegments.Count > 0)
            tailSegments[0].transform.position = snakeHead.position;

        // Move the snake head
        snakeHead.position = new Vector2(snakeHead.position.x + direction.x, snakeHead.position.y + direction.y);
    }

    // Rest of the code remains the same...
}