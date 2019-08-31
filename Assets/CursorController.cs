using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    public GameObject cursor;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    private void Update() {
        cursor.transform.position = Input.mousePosition;
    }
    void SetZoom(bool b)
    {
        if (b)
            cursor.transform.localScale = new Vector2(2, 2);
        else
            cursor.transform.localScale = new Vector2(1, 1);
    }
}
