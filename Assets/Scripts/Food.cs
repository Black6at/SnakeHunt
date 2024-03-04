using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Food : MonoBehaviour
{
    private int xBound = 19;
    private int yBound = 10;

    public Snake sn;
    private List<Vector2> emptySpace = new List<Vector2>();
    public int[] score = new int[20];

    public Sprite[] sprites;
    public int random;

    //Special Food Bomb -> Kill player, Clock -> SlowDown Time, Coins-> Money to buy skins, Fuel -> Speed, Crown -> Invincible for a given time

    void Start()
    {
        RandomFoodPosition();
    }

    private void CalculateEmptySpace()
    {
        emptySpace.Clear();

        for(int x = -xBound; x <= xBound; x++)
        {
            for(int y = -yBound; y <= yBound; y++)
            {
                emptySpace.Add(new Vector2(x, y));
            }
        }

        foreach(GameObject segment in sn.segments)
        {
            int x = Mathf.RoundToInt(segment.transform.position.x);
            int y = Mathf.RoundToInt(segment.transform.position.y);
            Vector2 pos = new Vector2(x,y);
            emptySpace.Remove(pos);
        }
    }

    public void RandomFoodPosition()
    {
       random = Random.Range(0, sprites.Length);
       GetComponent<SpriteRenderer>().sprite = sprites[random];
       CalculateEmptySpace();
       Vector2 newRandomPosition = emptySpace[Random.Range(0, emptySpace.Count)];
       transform.position = newRandomPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameManager>().IncreaseScore(score[random]);
        RandomFoodPosition();
    }
}
