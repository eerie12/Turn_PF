using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler
{
    //joystickの設定

    private Image bgImag;
    public Image joystickImg;
    public Vector3 inputVector;
    

    public Vector2 pos;

    public float fix_x;
    public float fix_y;


    void Start()
    {

        bgImag = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();

    }

    #region joystick処理

    public virtual void OnDrag(PointerEventData ped)
    {

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImag.rectTransform,ped.position,ped.pressEventCamera,out pos))
        {
         
            pos.x = (pos.x / bgImag.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImag.rectTransform.sizeDelta.y);

            inputVector = new Vector3(pos.x+ fix_x, 0,pos.y+fix_y);
            inputVector = (inputVector.magnitude >1.0f) ? inputVector.normalized:inputVector;


            joystickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImag.rectTransform.sizeDelta.x/3), inputVector.z * (bgImag.rectTransform.sizeDelta.y/3));


        }
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;

    }

    public float Horizontal()
    {
            return inputVector.x;
    }

    public float Vertical()
    {
            return inputVector.z;
    }

    #endregion
}
