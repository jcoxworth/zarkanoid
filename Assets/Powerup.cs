using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public GameLoop.PlayerPowerup powerupType = GameLoop.PlayerPowerup.moltenBall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActiveAndEnabled)
            return;
        transform.position += Vector3.down * Time.deltaTime;
    }
    
    public void GivePowerUp()
    {
        GameLoop._access.Powerup(powerupType);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameLoop._access.PlayerPaddle)
        {
            GivePowerUp();
            gameObject.SetActive(false);
        }
    }
}
