using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLivesUI : MonoBehaviour
{
    public GameObject[] lives;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetThreeLives()
    {
        for (int i = 0; i < lives.Length; i++)
        {
            lives[i].SetActive(true);
        }
    }
    public void UpdateLives(int remainingLives)
    {
       for (int i = 0; i < lives.Length; i++)
        {
            if (i < remainingLives) { lives[i].SetActive(true); }
            else { lives[i].SetActive(false); }
        }
    }
}
