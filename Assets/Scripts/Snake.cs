using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 moveDirection;
    private int dir = 0;
    public GameObject segmentPrefab;
    public List<GameObject> segments = new List<GameObject>();
    public Food food;
    private int r;
    private int s;

    void Start()
    {
        SetRandomDirection();
        segments.Add(gameObject);

    }

    void Update()
    {
        DirectionMovement();
    }

    private void FixedUpdate()
    {
        SegementMovement();
        HeadMovement();
    }

    private void HeadMovement()
    {
        transform.position = (Vector2)transform.position + moveDirection;
    }

    private void SegementMovement()
    {
        for(int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].transform.position = segments[i - 1].transform.position;
        }
    }
    
    private void DirectionMovement()
    {
        if (Input.GetAxisRaw("Vertical") == 1 && dir != 1)
        {
            moveDirection = Vector2.up;
            dir = 0;
        }
        else if (Input.GetAxisRaw("Vertical") == -1 && dir != 0)
        {
            moveDirection = Vector2.down;
            dir = 1;
        }
        else if (Input.GetAxisRaw("Horizontal") == 1 && dir != 3)
        {
            moveDirection = Vector2.right;
            dir = 2;
        }
        else if (Input.GetAxisRaw("Horizontal") == -1 && dir != 2)
        {
            moveDirection = Vector2.left;
            dir = 3;
        }
    }

    public void ExpandSnake()
    {
        r = food.random;
        s = food.score[r];
        for(int i = 0; i < s; i++){
            GameObject segment = Instantiate(segmentPrefab, segments[segments.Count - 1].transform.position, segmentPrefab.transform.rotation);
            segments.Add(segment);
        }
    }

    public void SetRandomDirection(){
         int random = Random.Range(0, 4);

        if(random == 0){
            moveDirection = Vector2.up;
        }
        else if(random == 1){
            moveDirection = Vector2.right;
        }
        else if(random == 2){
            moveDirection = Vector2.down;
        }
        else if(random == 3){
            moveDirection = Vector2.left;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Food")
        {
            ExpandSnake();
        }
        else
        {
            FindObjectOfType<GameManager>().RestartGame();
        }
    }
}
