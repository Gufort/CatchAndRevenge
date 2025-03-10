using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovePuzzle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool move;
    Vector2 offset;
    public Image form; 
    bool finish = false;
    public static bool end = false;
    public GameObject CompletePanelActivate;

    void Start()
    {
        finish = false;
        end = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!finish)
        {
            move = true;
            offset = (Vector2)this.GetComponent<RectTransform>().position - (Vector2)Input.mousePosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        move = false;

        if (Vector2.Distance((Vector2)this.GetComponent<RectTransform>().position, (Vector2)form.GetComponent<RectTransform>().position) <= 10f && !finish)
        {
            this.GetComponent<RectTransform>().position = form.GetComponent<RectTransform>().position;
            finish = true;
            Debug.Log("Add puzzle element");
            PuzzleComplete.AddElement();
            if (PuzzleComplete.curElement == 16)
            {
                Debug.Log("Finish Puzzle");
                CompletePanelActivate.SetActive(true);
                end = true;
            }
        }
    }

    void Update()
    {
        if (move && !finish)
        {
            Vector2 cursorPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            this.GetComponent<RectTransform>().position = cursorPosition + offset;
        }

        if (finish)
        {
            move = false;
        }
    }
}