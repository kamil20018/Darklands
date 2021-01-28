using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class OnClickItem : MonoBehaviour
{
    public static bool holdingItem = false;
    private bool buttonClicked = false;
    private Vector3 initPos;
    private Image thisImage;

    void Start()
    {
        thisImage = gameObject.transform.GetChild(0).GetComponent<Image>();
        initPos = gameObject.transform.GetChild(0).transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(buttonClicked)
        {
            //gameObject.transform.GetChild(0).transform.position = Input.mousePosition + new Vector3(0, 0, -1);
            if(Input.GetMouseButton(0)){
                buttonClicked = false;
                gameObject.transform.GetChild(0).transform.localRotation = Quaternion.identity;
                gameObject.transform.GetChild(0).transform.localPosition = Vector3.zero;
                gameObject.transform.GetChild(0).transform.localScale = Vector3.one;
            }
        }
                
    }

    
    public void MouseOver()
    {
        //works like on trigger enter but for mouse, test ex below
        //Debug.Log("Mouse is over GameObject.");
    }

    public void Click()
    {
        StoredItems.Swap(thisImage);
        buttonClicked = true;
    }

}
