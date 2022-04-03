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

    // Update is called once per frame
    void Update()
    {
        buttonDown = (Input.GetMouseButtonDown(0));
        buttonUp = (Input.GetMouseButtonUp(0));
        if (buttonUp)
        {
            //Here, the player can look at the score before going to the next level
            if (GameLoop._access.currentGameState == GameLoop.GameState.postgame)
            {
                GameLoop._access.NextLevel();
            }
            else
            {
                //If we are launching the ball from pregame, it will start the game
                if (GameLoop._access.currentGameState == GameLoop.GameState.pregame)
                    GameLoop._access.StartGame();
            }
        }
    }
}
