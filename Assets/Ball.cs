using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public enum BallState { idle, inPlay}
    public BallState currentBallState = BallState.idle;

    public Vector2 velocityVector;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void InitializeBall()
    {
        currentBallState = BallState.idle;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
