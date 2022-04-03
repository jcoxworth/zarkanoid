using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneButtonInput : MonoBehaviour
{
    public static bool buttonUp = false;
    public static bool buttonDown = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void NextLevel()
    {
        if (GameLoop._access.currentGameState == GameLoop.GameState.postgame)
        {
            GameLoop._access.NextLevel();
        }
    }
    // Update is called once per frame
    void Update()
    {
        buttonDown = (Input.GetMouseButtonDown(0));
        buttonUp = (Input.GetMouseButtonUp(0));
        if (buttonUp)
        {
           //If we are launching the ball from pregame, it will start the game
                if (GameLoop._access.currentGameState == GameLoop.GameState.pregame)
                    GameLoop._access.StartGame();
            
        }
    }
}
