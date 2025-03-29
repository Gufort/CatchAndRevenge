using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInventoryState : MonoBehaviour
{
    public GameObject inventory;
    public GameObject Collider1;
    public GameObject Collider2;
    private bool IsTriggered;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsTriggered)
        {
            foreach (var x in inventory.GetComponentsInChildren<Transform>())
            {
                Transform[] AllChildren = x.GetComponentsInChildren<Transform>();
                if (AllChildren.Length > 0)
                {
                    if (AllChildren[0].CompareTag("Mushroom"))
                    {
                        IsTriggered = true;
                        Collider1.SetActive(false);
                        Collider2.SetActive(true);
                    }
                }
            }
        }
    }
}
