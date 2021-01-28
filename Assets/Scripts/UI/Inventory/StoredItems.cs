using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class StoredItems : MonoBehaviour
{
    public static Image[] buttonsToSwap = new Image[2];

    private void Awake() 
    {

    }

    private void OnEnable() 
    {   List<Tuple<Sprite, InvTestingScript>> items = ItemsAndInventory.items;
        int number = items.Count;
        if(number > 0){
            for(int x = 0; x < 36; x++)
            {
                if(number == 0) break;
                if(this.transform.GetChild(x).GetChild(0).GetComponent<Image>().sprite == null)
                {    
                    this.transform.GetChild(x).GetChild(0).GetComponent<Image>().sprite = items[0].Item1;
                    items.RemoveAt(0);
                    number -= 1;
                }
                
            }
        }
    }

    public static void Swap(Image image)
    {
        if(buttonsToSwap[0] == null){
            buttonsToSwap[0] = image;
        }
        else{
            buttonsToSwap[1] = image;            
            Sprite temp1 = buttonsToSwap[0].sprite;
            buttonsToSwap[0].sprite = buttonsToSwap[1].sprite;
            buttonsToSwap[1].sprite = temp1;
            buttonsToSwap = new Image[2];
            Color temp2 = new Color32(100, 0, 0, 0);
        }
    }
}
