using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public enum BallState { idle, inPlay}
    public BallState currentBallState = BallState.idle;
    public Vector2 velocityVector;
    private Rigidbody2D rb;

    public float speed = 10f;
    private float baseSpeed;
    public bool isMolten = false;
    public bool hyperSpeed = false;
    public float hungBallTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        InitializeBall();
    }
    public void InitializeBall()
    {
        rb = GetComponent<Rigidbody2D>();
        currentBallState = BallState.idle;
        GameLoop._access.AddBall(gameObject);

        //this timer will help keep the ball from bouncing around in an endless loop away from the player
        hungBallTimer = 0f;

        //we need to keep the base speed variable for making a faster ball powerup
        baseSpeed = speed;
    }
    public void CatchBall()
    {
        currentBallState = BallState.idle;
    }
    public void LaunchInput()
    {
        //let it be mouseup so that the player can hold down their finger on smartphone and release to launch
        if (OneButtonInput.buttonUp)
        {
            //If we are launching the ball from pregame, it will start the game
                if (GameLoop._access.currentGameState == GameLoop.GameState.playing)
                    LaunchBall();


            //anyway we will launch the ball, then we can have a powerup that catches the ball each time and lets the player launch it each time 
            //If we want to make that later....
        }
    }
    
    public void LaunchBall()
    {
        if (currentBallState != BallState.idle)
            return;
        currentBallState = BallState.inPlay;
        rb.isKinematic = false;
        velocityVector = new Vector2(0f, speed);
        rb.velocity = velocityVector;
    }
    public void BallIdle()
    {
        rb.isKinematic = true;
        float xMovement = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        xMovement = Mathf.Clamp(xMovement, -2f, 2f);
        transform.position = new Vector2(xMovement, -2.8f);
    }
    public void BallInPlay()
    {
        if (transform.position.y < -5)
            GameLoop._access.LoseLife();

        if (hyperSpeed)
            speed = baseSpeed * 2f;
        else
            speed = baseSpeed;

        hungBallTimer += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameLoop._access.PlayerPaddle)
        {
            float X_difference = (transform.position.x - GameLoop._access.PlayerPaddle.transform.position.x) / collision.collider.bounds.size.x;
           // Debug.Log("Hit Player Paddle. Difference = " + X_difference);
            //Now this will be like launching the ball
            velocityVector = new Vector2(X_difference * speed, speed);
            rb.velocity = velocityVector;

            //touching the paddle will reset the timer on the ball as well as in Gameloop, when the bricks are hit, it will reset the timer
            
            ResetHungBallTimer();

            //also kill the chain that the player is building up
            GameLoop._access.KillChain();
        }

        //This will help keep the ball from getting stuck/hung bouncing around above the player
        velocityVector = rb.velocity + (Vector2.down * hungBallTimer * Time.deltaTime);
        rb.velocity = velocityVector;
    }

    public void ResetHungBallTimer()
    {
        //touching the paddle will reset the timer on the ball as well as in Gameloop, when the bricks are hit, it will reset the timer
        hungBallTimer = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        LaunchInput();

        if (isMolten)
            GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        else
            GetComponent<SpriteRenderer>().color = Color.white;
    }
    private void FixedUpdate()
    {
        switch(currentBallState)
        {
            case BallState.idle:
                BallIdle();
                break;
            case BallState.inPlay:
                BallInPlay();
                break;
        }
    }
}
