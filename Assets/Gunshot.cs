using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunshot : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    void Update()
    {
        if (!isActiveAndEnabled)
            return;
        GetComponent<Rigidbody2D>().velocity = Vector3.up * Time.deltaTime * 500f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(transform.gameObject);
    }
}
