using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameLoopUI : MonoBehaviour
{
    public GameObject pregameScreen, winScreen, loseScreen;
    public Text PlayerScore, PlayerChain, PlayerLevel, LevelName;
    public PlayerLivesUI playerLivesUI;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void UpdateLevelUI(int _level, string _levelName)
    {
        //Change the weird computer number into a number player's understand
        PlayerLevel.text = "Lvl: " + (_level +1);
        LevelName.text = _levelName;
    }
    public void UpdateScoreUI(int _score)
    {
        PlayerScore.text = _score + " points";
    }
    public void UpdateChainScoreUI(int _chain)
    {
        if (_chain > 1)
            PlayerChain.text = _chain.ToString();
        else
            PlayerChain.text = "";
    }
    public void UpdateLivesUI(int _lives)
    {
        //PlayerLives.text = gameLoop.PlayerLives.ToString();
        if (playerLivesUI)
            playerLivesUI.UpdateLives(_lives);
        else
            Debug.Log("Please set playerLivesUI");


    }
    public void UpdateGameMessagesUI()
    {
        // Debug.Log("Updateing game messages");
        pregameScreen.SetActive(
             GameLoop._access.currentGameState == GameLoop.GameState.pregame);
        winScreen.SetActive(
            GameLoop._access.currentGameState == GameLoop.GameState.postgame 
            && GameLoop._access.RemainingBricks.Count < 1);
        //Show the lose screen if the player lives are less than 1, and we are also not showing the win screen already (let's win before we lose)
        loseScreen.SetActive(
            GameLoop._access.currentGameState == GameLoop.GameState.postgame 
            && GameLoop._access.PlayerLives < 1 && !winScreen.activeSelf);
        
    }
}
