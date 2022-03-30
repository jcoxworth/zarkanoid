using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameLoopUI : MonoBehaviour
{
    public GameObject winScreen, loseScreen;
    public Text PlayerLives, PlayerScore, PlayerLevel;
    private GameLoop gameLoop;
    // Start is called before the first frame update
    void Start()
    {
        gameLoop = GetComponent<GameLoop>();
        if (!gameLoop)
            Debug.Log("Please add a gameloop component to this gameobject: GameLoopUI");
    }

    // Update is called once per frame
    void Update()
    {

        UpdateTextUI();
        UpdateGameMessagesUI();
    }
    private void UpdateTextUI()
    {
        if (!gameLoop)
            return;

        PlayerLives.text = gameLoop.PlayerLives.ToString();
        PlayerScore.text = gameLoop.PlayerScore.ToString() + " points";
        PlayerLevel.text = "Lvl: "+ gameLoop.PlayerLevel.ToString();
    }

    private void UpdateGameMessagesUI()
    {
        if (!gameLoop)
            return;


        winScreen.SetActive(gameLoop.RemainingBricks < 1);

        //Show the lose screen if the player lives are less than 1, and we are also not showing the win screen already (let's win before we lose)
        loseScreen.SetActive(gameLoop.PlayerLives < 1 && !winScreen.activeSelf);
    }
}
