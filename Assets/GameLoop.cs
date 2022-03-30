using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public enum GameState { pregame, playing, postgame}
    public int PlayerLives = 3;
    public int PlayerScore = 0;
    public int PlayerLevel = 1;

    public int RemainingBricks = 1;
    public bool Debug = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void NewGame()
    {
        PlayerLives = 3;
        PlayerScore = 0;
        PlayerLevel = 1;

        //REmaining bricks will be set somewhere else, but let's set here for now 
        RemainingBricks = 10;
    }
    // Update is called once per frame
    void Update()
    {
        if (Debug)
        {
            DebugFunctions();
            return;
        }
    }
    public void AddScore(int _score)
    {
        PlayerScore += _score;
    }
    public void DestroyBrick()
    {
        RemainingBricks -= 1;
    }
    public void LoseLife()
    {
        PlayerLives -= 1;
    }
    public void DebugFunctions()
    {
        //test Making a new Game
        if (Input.GetKeyDown(KeyCode.N))
            NewGame();
        //test Scoring system
        if (Input.GetKeyDown(KeyCode.S))
            AddScore(100 + Random.Range(0, 50));

        //test Bricks system
        if (Input.GetKeyDown(KeyCode.B))
            DestroyBrick();

        //Test Lives system
        if (Input.GetKeyDown(KeyCode.L))
            LoseLife();


    }
}
