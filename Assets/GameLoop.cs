using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public static GameLoop _access;
    public GameLoopUI gameLoopUI;
    public GameObject PlayerPaddle;
    public GameObject PlayerGun;
    public GameObject PlayerBackWall;

    public bool flashOn = false;
    public GameObject pu_Molten, pu_Wall, pu_Gun;
    public enum GameState { pregame, playing, postgame}
    public GameState currentGameState = GameState.pregame;
    public int PlayerLives = 3;
    public int PlayerScore = 0;
    public int PlayerLevel = 1;
    public int PlayerChain = 0;

    public Level[] levels;
    public GameObject mainBall;
    public List<GameObject> BallsInPlay = new List<GameObject>();
    public List<GameObject> RemainingBricks = new List<GameObject>();
    public bool _Debug = false;

    // Start is called before the first frame update
    void Awake()
    {
        gameLoopUI = GetComponent<GameLoopUI>();
        BallsInPlay.Clear();
        RemainingBricks.Clear();
        _access = this;
    }
    public void Start()
    {
        NewGame();
        flashOn = false;
        StartCoroutine(Flash());
    }
    public void NewGame()
    {
        PlayerLives = 3;
        PlayerScore = 0;
        PlayerChain = 0;

        //REmaining bricks will be set somewhere else, but let's set here for now 
        //RemainingBricks = 10;
        RemainingBricks.Clear();
        currentGameState = GameState.pregame;
        PlayerLevel = 0;
        LoadCurrentLevel();
        mainBall.GetComponent<Ball>().CatchBall();
        //UI Starting the game updates all the UI
        gameLoopUI.UpdateScoreUI(PlayerScore);
        gameLoopUI.UpdateLivesUI(PlayerLives);
        gameLoopUI.UpdateGameMessagesUI();
    }
    // Update is called once per frame
    void Update()
    {
        if (_Debug)
        {
            DebugFunctions();
            return;
        }
        
    }
   
    public void StartGame()
    {
        currentGameState = GameState.playing;


        //UI Starting the game updates all the UI
        gameLoopUI.UpdateScoreUI(PlayerScore);
        gameLoopUI.UpdateLivesUI(PlayerLives);
        gameLoopUI.UpdateGameMessagesUI();
    }
    public void WinLevel()
    {
        if (mainBall)
            mainBall.GetComponent<Ball>().CatchBall();
        currentGameState = GameState.postgame;
        //UI: Show the win message
        //UI: Score UI is handled by the AddScore() method, duh!
        gameLoopUI.UpdateGameMessagesUI();
    }
    public void NextLevel()
    {
        if (PlayerLives < 0)
        {
            NewGame();
        }
        else
        {
            int levelInt = levels.Length - 1;
            if (PlayerLevel < levelInt)
                PlayerLevel++;
            else
                PlayerLevel = 0; //let's just restart for now
            LoadCurrentLevel();
        }

        currentGameState = GameState.pregame;
        //
        gameLoopUI.UpdateScoreUI(PlayerScore);
        gameLoopUI.UpdateGameMessagesUI();
    }
    public void LoadCurrentLevel()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (i == PlayerLevel)
            {
                levels[i].LoadLevel();
                string levelName = levels[i].LevelName;
                gameLoopUI.UpdateLevelUI(PlayerLevel, levelName);
                //If the player still has this power up, let them use it in the next level!
                if (mainBall.GetComponent<Ball>().isMolten)
                    levels[i].SetBricksTrigger();
            }
            else
                levels[i].UnloadLevel();
        }
    }
    public void LoseGame()
    {
        currentGameState = GameState.postgame;
        //        
        gameLoopUI.UpdateGameMessagesUI();
    }
    public void UpdateBalls()
    {
        //If we have lost all of our balls
        if (BallsInPlay.Count < 1)
        {
            currentGameState = GameState.postgame;
        }
        else
        {
            mainBall = BallsInPlay[0];
        }
    }
    public void UpdateBricks()
    {
        //if all bricks are destroyed, we win
        if (RemainingBricks.Count < 1)
        {
            WinLevel();
            currentGameState = GameState.postgame;
        }
    }
    public void Chain()
    {
        PlayerChain++;
        gameLoopUI.UpdateChainScoreUI(PlayerChain);
    }
    public void KillChain()
    {
        PlayerChain = 0;
        //UI update that ui bro
        gameLoopUI.UpdateChainScoreUI(PlayerChain);
    }
    public void AddScore(int _score)
    {
        PlayerScore += _score * PlayerChain;
        //UI:Show the score after hitting a brick or anything else
    }
    public void AddBall(GameObject newBall)
    {
        if (BallsInPlay.Contains(newBall))
            return;
        BallsInPlay.Add(newBall);

        UpdateBalls();
    }
    public void RemoveBall(GameObject ballToRemove)
    {
        if (BallsInPlay.Contains(ballToRemove))
            BallsInPlay.Remove(ballToRemove);

        UpdateBalls();
    }
    public void AddBrick(GameObject newBrick)
    {
        if (RemainingBricks.Contains(newBrick))
            return;
        RemainingBricks.Add(newBrick);
    }

    public void DestroyBrick(GameObject brickToDestroy)
    {
        if (RemainingBricks.Contains(brickToDestroy))
            RemainingBricks.Remove(brickToDestroy);


        if (brickToDestroy.GetComponent<Brick>().holdsPowerup)
        {
            DropPowerup(brickToDestroy.transform.position);
        }
        //After we destroy, check if there are any left
        UpdateBricks();

        //if the ball is destroying bricks, it's not hung so reset the timer
        mainBall.GetComponent<Ball>().ResetHungBallTimer();
        //UI
        gameLoopUI.UpdateScoreUI(PlayerScore);

    }
    public void DropPowerup(Vector2 pos)
    {
        Debug.Log("-------------------DroppingPowerup");
        //Drop the powerup here
        int r = Random.Range(0, 3);
        GameObject droppedPowerup = pu_Gun; //we'll just set it to pu_gun as default
        if (r == 0) droppedPowerup = pu_Wall;
        else if (r == 1) droppedPowerup = pu_Molten;
        else droppedPowerup = pu_Gun;

        droppedPowerup.transform.position = pos;
        droppedPowerup.SetActive(true);
    }
    public void LoseLife()
    {
        if (mainBall)
            mainBall.GetComponent<Ball>().CatchBall();
        PlayerLives -= 1;
        Debug.Log("Lost a life");
        PlayerChain = 0;
        if (PlayerLives < 0)
            LoseGame();
        //Ui
        gameLoopUI.UpdateLivesUI(PlayerLives);
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
            DestroyBrick(RemainingBricks[Random.Range(0, RemainingBricks.Count)]);

        //Test Lives system
        if (Input.GetKeyDown(KeyCode.L))
            LoseLife();

        if (Input.GetKeyDown(KeyCode.P))
            DropPowerup(Vector2.up);

    }


    public enum PlayerPowerup { moltenBall, backWall, gun }
    public void Powerup(PlayerPowerup _obtainedPowerup)
    {
        Debug.Log("Got a Powerup");
        switch(_obtainedPowerup)
        {
            case PlayerPowerup.moltenBall:
                StartCoroutine(MoltenBall());
                break;
            case PlayerPowerup.backWall:
                StartCoroutine(BackWall());

                break;
            case PlayerPowerup.gun:
                StartCoroutine(Gun());
                break;
        }
    }
    private IEnumerator MoltenBall()
    {
        Debug.Log("Started Molten Ball powerup");
        levels[PlayerLevel].SetBricksTrigger();
        mainBall.GetComponent<Ball>().isMolten = true;
        yield return new WaitForSeconds(7.0f);
        levels[PlayerLevel].SetBricksNormal();
        mainBall.GetComponent<Ball>().isMolten = false;
        Debug.Log("Finihsed MoltenBall Powerufp");
    }
    private IEnumerator BackWall()
    {
        Debug.Log("Started BackWall powerup");
        //Make a back wall appear to make it impossible to lose, also make the ball bounce faster!
        PlayerBackWall.SetActive(true);
        mainBall.GetComponent<Ball>().hyperSpeed = true;
        yield return new WaitForSeconds(7.0f);
        PlayerBackWall.transform.GetComponent<Flasher>().StartFlashing();
        yield return new WaitForSeconds(3.0f);
        PlayerBackWall.transform.GetComponent<Flasher>().StopFlashing();
        mainBall.GetComponent<Ball>().hyperSpeed = false;
        PlayerBackWall.SetActive(false);
        Debug.Log("Finihsed BackWall Powerufp");
    }
    private IEnumerator Gun()
    {
        Debug.Log("Started gun powerup");
        PlayerGun.SetActive(true);
        yield return new WaitForSeconds(7.0f);
        PlayerGun.SetActive(false);

        Debug.Log("Finihsed gun Powerufp");
    }

    public IEnumerator Flash()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(0.1f);
            flashOn = false;
            yield return new WaitForSeconds(0.1f);
            flashOn = true;
        }
    }
}
