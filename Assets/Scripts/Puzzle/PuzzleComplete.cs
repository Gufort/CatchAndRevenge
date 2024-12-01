using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleComplete : MonoBehaviour
{
    int fullElement;
    public static int curElement;
    public GameObject puzzleGamePanel;
    public GameObject allPuzzle;
    public GameObject completedPuzzle;
    public GameObject completePanel;
    public static bool isPuzzleEnd = false;

    void Start()
    {
        isPuzzleEnd = false;
        curElement = 0;
        fullElement = allPuzzle.transform.childCount;
    }
    
    void Update()
    {     
        //if(MovingPuzzle.end==true) completePanel.SetActive(true);
        if (curElement == 16)
        {
            completePanel.SetActive(true);
            isPuzzleEnd = true;
        }
    }
    public static void AddElement()
    {
        curElement++;
    }
} 