using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OpenInventory : MonoBehaviour
{
    public GameObject inventoryScreen;

    public void OpenInv(){
        if(inventoryScreen != null){
            inventoryScreen.SetActive(true);
        }
    }
}
