using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    bool move;
    Vector2 mousePos;

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            move = true;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(move == true)
        {
            mousePos = Input.mousePosition;

            this.gameObject.transform.localPosition = new Vector2(mousePos.x, mousePos.y);
        }
    }
}
