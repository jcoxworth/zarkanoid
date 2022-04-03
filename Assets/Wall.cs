using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public enum WallPosition { right, left, top, bottom}
    public WallPosition position = WallPosition.right;
    // Start is called before the first frame update
    void Start()
    {
        switch (position)
        {
            case WallPosition.right:
                transform.position = new Vector2(Screen.width, transform.position.y);
                break;
            case WallPosition.left:
                transform.position = new Vector2(Screen.width * -1, transform.position.y);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
