using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ItemsAndInventory : MonoBehaviour
{


    public static List<Tuple<Sprite, InvTestingScript>> items = new List<Tuple<Sprite, InvTestingScript>>();
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Item" && Input.GetKey("e"))
        {   
            Debug.Log("item added");
            items.Add(new Tuple<Sprite, InvTestingScript>(other.gameObject.GetComponent<SpriteRenderer>().sprite, other.gameObject.GetComponent<InvTestingScript>()));
            Destroy(other.gameObject);
        }
    }
}
