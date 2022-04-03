using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite damage0, damage1, damage2, invincible;
    public enum BrickType { oneHit, twoHits, threeHits, invincible}
    public BrickType type = BrickType.oneHit;
    public int hits = 0;
    public GameObject brickExplosion;
    public bool holdsPowerup = false;
    
    public void ActivateBrick()
    {
        Debug.Log("Activating brick" + Time.time);
        hits = 0;
        spriteRenderer.sprite = damage0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        brickExplosion.SetActive(false);
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Collider2D>().isTrigger = false;
        spriteRenderer.enabled = true;
        //Register yourself with the gameLoop 
        GameLoop._access.AddBrick(gameObject);
    }
    public void DeactivateBrick()
    {
      //  Debug.Log("De activating brick" + Time.time);

        GetComponent<Collider2D>().enabled = false;
        spriteRenderer.enabled = false;
    }
    public void SetBricksTrigger()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    public void SetBricksNormal()
    {
        GetComponent<Collider2D>().isTrigger = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {       
        //Every bricktype will add to the chain score except invincible bricks
        if (type != BrickType.invincible)
        {
            GameLoop._access.Chain();
            hits++;
            CheckHealth();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameLoop._access.mainBall)
        {
            GameLoop._access.Chain();
            hits += 5; //add enough hits to destroy any brick
            CheckHealth();
        }
    }
    public void CheckHealth()
    {
        switch (type)
        {
            case BrickType.oneHit:
                if (hits > 0)
                {
                    GameLoop._access.AddScore(15);
                    SmashBrick();
                }
                break;
            case BrickType.twoHits:
                if (hits > 1)
                {
                    GameLoop._access.AddScore(40);
                    SmashBrick();
                }
                else
                {
                    GameLoop._access.AddScore(20);
                    spriteRenderer.sprite = damage2;
                }
                break;
            case BrickType.threeHits:
                if (hits > 2)
                {
                    GameLoop._access.AddScore(60);
                    SmashBrick();
                }
                if (hits == 2)
                {
                    GameLoop._access.AddScore(25);
                    spriteRenderer.sprite = damage2;
                }
                if (hits == 1)
                {
                    GameLoop._access.AddScore(10);
                    spriteRenderer.sprite = damage1;
                }
                break;
            case BrickType.invincible:
                if (hits > 4)
                {
                    GameLoop._access.AddScore(100);
                    SmashBrick();
                }
                break;
        }
    }
    public void SmashBrick()
    {
        brickExplosion.SetActive(true);
        DeactivateBrick();
        GameLoop._access.DestroyBrick(gameObject);

        // Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (holdsPowerup)
            spriteRenderer.color = Random.ColorHSV();

    }
}
