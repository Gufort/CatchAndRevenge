using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovePuzzle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public AudioClip soundClip;
    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!finish)
        {
            move = true;
            offset = (Vector2)this.GetComponent<RectTransform>().position - (Vector2)Input.mousePosition;

            transform.SetAsLastSibling();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        move = false;

        if (Vector2.Distance((Vector2)this.GetComponent<RectTransform>().position, (Vector2)form.GetComponent<RectTransform>().position) <= 25f && !finish)
        {
            this.GetComponent<RectTransform>().position = form.GetComponent<RectTransform>().position;
            finish = true;
            Debug.Log("Add puzzle element");
            PuzzleComplete.AddElement();
            if (audioSource != null && soundClip != null)
            {
                audioSource.clip = soundClip;
                audioSource.Play();
            }
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

            foreach (Transform child in transform.parent)
            {
                MovePuzzle childPuzzle = child.GetComponent<MovePuzzle>();
                if (child != transform && !childPuzzle.finish && Vector2.Distance(child.position, transform.position) < 50f)
                {
                    Vector2 direction = (child.position - transform.position).normalized;
                    child.position += (Vector3)(direction * 500f * Time.deltaTime);
                }
            }
        }

        if (finish)
        {
            move = false;
        }
    }
}