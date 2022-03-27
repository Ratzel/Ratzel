using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorBlock : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    Text text_num;

    [SerializeField]
    Image img_background;

    int index = 0;
    int x = 0;
    int y = 0;

    private Action<int, int> swapFunc = null;   

    public void Init(int i_, int j_, int index_, Action<int, int> swapFunc_)
    {
        index = index_;
        swapFunc = swapFunc_;
        UpdatePos(i_, j_);
        
        string str_num = "";
        

        if (index == 0)
        {
            str_num = "";
            img_background.gameObject.SetActive(false);
        }
        else
        {
            str_num = index.ToString();
            img_background.gameObject.SetActive(true);
        }
        

        text_num.text = str_num;
        
    }

    public void UpdatePos(int i_ , int j_)
    {
        x = i_;
        y = j_;


        transform.localPosition = new Vector2(i_ * 200, -(j_ * 200));
    }

    public void MovePos(int i_, int j_)
    {
        x = i_;
        y = j_;
        iTween.MoveTo(gameObject,
         iTween.Hash(
             "x", i_ * 200,
             "y", j_ * -200,
             "isLocal", true,
             "time", 0.2f,
             "easeType", iTween.EaseType.easeOutExpo
         )
     ); ;
    }

    public bool IsEmpty()
    {
        return index == 0;
    }

    //void OnMouseDown()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Debug.Log("Check Mouse Down");
    //        swapFunc?.Invoke(x, y);
    //    }
    //}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            swapFunc?.Invoke(x, y);
        }
        //else if (eventData.button == PointerEventData.InputButton.Middle)
        //{
        //    Debug.Log("Mouse Click Button : Middle");
        //}
        //else if (eventData.button == PointerEventData.InputButton.Right)
        //{
        //    Debug.Log("Mouse Click Button : Right");
        //}
        //Debug.Log("Mouse Position : " + eventData.position); Debug.Log("Mouse Click Count : " + eventData.clickCount);
    }

}
