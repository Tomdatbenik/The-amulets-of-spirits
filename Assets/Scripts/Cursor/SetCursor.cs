using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    public Sprite cursorSprite;
    public Vector2 center;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorSprite.texture, center, CursorMode.Auto);   
    }
}
