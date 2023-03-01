using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    #region Private Fields
    private float sideWay;
    private float sideMove;
    private Vector3 firstPos;
    private Vector3 lastPos;
    #endregion

    #region Properties
    public float SideWay{get{return sideMove;}}
    #endregion
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                firstPos = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                lastPos = touch.position;
            }

            sideWay = lastPos.x-firstPos.x;
             sideMove = Mathf.Lerp(sideMove,Mathf.Sign(sideWay),Time.deltaTime*10);
 
            if(touch.phase == TouchPhase.Ended){
                firstPos =Vector3.zero;
                lastPos = Vector3.zero;
               
            }            

        }
        else
         sideMove=Mathf.Lerp(sideMove,0,Time.deltaTime*15);

         
    }
}
