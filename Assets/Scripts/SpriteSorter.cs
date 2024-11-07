using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
   private int sorting_order_base = 0;
   private Renderer renderer;
   public float offset = 0;
   public bool IsStatic = false;

   private void Awaike(){
        renderer = GetComponent<Renderer>();
   }

    private void LateUpdate(){
        renderer.sortingOrder = (int)(sorting_order_base - transform.position.y + offset);
        if(IsStatic) Destroy(this);
    }
}
