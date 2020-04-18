using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    public Texture2D texture;
    public Vector2 center;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(texture, center, CursorMode.ForceSoftware);  
    }
}
