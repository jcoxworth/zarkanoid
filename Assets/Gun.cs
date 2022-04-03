using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject gunshot;
    public List<GameObject> shots = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        MakePool();
    }

    // Update is called once per frame
    void Update()
    {
        if (OneButtonInput.buttonDown)
        {
            GameObject shot = GetShotFromPool();
            shot.transform.position = transform.position;
            shot.transform.rotation = transform.rotation;
            shot.SetActive(true);
        }
    }
    public GameObject GetShotFromPool()
    {
        for (int i =0; i < shots.Count; i++)
        {
            if (shots[i].activeInHierarchy == false)
                return shots[i];
            else
                continue;
        }

        // return the first shot in the list if they can't find anyhting
        return shots[0];

    }
    public void MakePool()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject newShot =Instantiate(gunshot, transform.position, transform.rotation);
            newShot.SetActive(false);
         //   newShot.hideFlags = HideFlags.HideInHierarchy;
            shots.Add(newShot);
        }
    }
}
