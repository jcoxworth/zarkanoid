using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flasher : MonoBehaviour
{
    public bool isFlashing = false;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void StartFlashing()
    {
        isFlashing = true;
    }
    public void StopFlashing()
    {
        isFlashing = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (isFlashing)
            spriteRenderer.enabled = GameLoop._access.flashOn;
        else
            spriteRenderer.enabled = true;
    }
}
