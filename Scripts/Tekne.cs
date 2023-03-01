using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tekne : Singleton<Tekne>
{
    public float speed;
    public float roteSpeed;
    public Transform target;

    private bool isPlay;


    void Update()
    {
        if(isPlay){
        transform.position = Vector3.Slerp(transform.position, target.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, roteSpeed * Time.deltaTime);

        }
    }

    public void Play(){
        isPlay = true;
    }
}
