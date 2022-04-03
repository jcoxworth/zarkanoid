using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public string LevelName = "Level 1: The Gauntlet";
    public Brick[] bricks;
    public int numberOfPowerups = 1;
    // Start is called before the first frame update
    void Start()
    {
        bricks = GetComponentsInChildren<Brick>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UnloadLevel()
    {

        //Unload level will be called by Gameloop at the start, any level that hasn't been reached will be unloaded.
        foreach (Brick b in bricks)
        {
            b.DeactivateBrick();
            //Debug.Log("deactivated brick " + b.name);
        }
    }
    public void LoadLevel()
    {
        if (bricks.Length < 1)
        {
            Debug.Log("didn't get any briks");
            return;
        }
        foreach(Brick b in bricks)
        {
            b.gameObject.SetActive(true);
            b.ActivateBrick();
            //you have to reset this or it will be a powerup block next time too
            b.holdsPowerup = false;
            //Debug.Log("loaded brick " + b.name);
        }

        for (int i = 0; i < numberOfPowerups; i++)
        {
            bricks[Random.Range(0, bricks.Length)].holdsPowerup = true;
        }
    }



    /// <summary>
    /// these methods enable the player to have a super powered ball that tears through bricks without bouncing off of them, for a limited time
    /// </summary>
    public void SetBricksTrigger()
    {
        if (bricks.Length < 1)
        {
            Debug.Log("didn't get any briks");
            return;
        }
        foreach (Brick b in bricks)
        {
            b.SetBricksTrigger();
            //Debug.Log("loaded brick " + b.name);
        }
    }
    public void SetBricksNormal()
    {
        if (bricks.Length < 1)
        {
            Debug.Log("didn't get any briks");
            return;
        }
        foreach (Brick b in bricks)
        {
            b.SetBricksNormal();
            //Debug.Log("loaded brick " + b.name);
        }
    }
}
