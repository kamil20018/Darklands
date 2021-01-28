using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseInventory : MonoBehaviour
{
    public void CloseThis(){
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
