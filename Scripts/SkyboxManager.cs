using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    [SerializeField] float rotatipnSpeed;

private void Start() {
   // StartCoroutine(RotateSky());
}


    IEnumerator RotateSky()
    {
        while (true)
        {
            RenderSettings.skybox.SetFloat("_Rotation",Time.time*rotatipnSpeed);
            yield return null;

        }
    }


}
