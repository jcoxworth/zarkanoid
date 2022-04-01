using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Vector2 ballLaunchOffset = new Vector2(0f,1.2f);
    // Start is called before the first frame update
    void Start()
    {
        ballLaunchOffset = new Vector2(0f, 1.2f);
    }
    private void Update()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        MovePaddle();
    }
    private void LaunchBall()
    {
            
    }
    private void MovePaddle()
    {
        float xMovement = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        xMovement = Mathf.Clamp(xMovement, -3f, 3f);
        transform.position = new Vector2(xMovement, transform.position.y);

    }
}
