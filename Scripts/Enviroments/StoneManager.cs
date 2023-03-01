using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneManager : MonoBehaviour
{

    #region SerializedFields    
    [SerializeField] GameObject attentionParticle;

    [SerializeField] LayerMask groundLayer;


    #endregion
    #region  Private Fields

    private CameraShake cameraShake;

    private RaycastHit hit;

    private GameObject attentionEffect;

    private GameObject child;
    #endregion

    private void Start()
    {
        cameraShake = FindObjectOfType<CameraShake>();
        attentionEffect = Instantiate(attentionParticle);
        attentionEffect.SetActive(false);
        child = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        DetectDropPoint();
    }

    private void DetectDropPoint()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            Vector3 detectPoint = hit.point;
            attentionEffect.SetActive(true);
            attentionEffect.transform.position = detectPoint + new Vector3(0,1,0);

        }
        Debug.DrawRay(transform.position, Vector3.down * Mathf.Infinity, Color.white);
    }

    private void OnCollisionEnter(Collision other)
    {
        cameraShake.StoneDropShaker();
        Destroy(attentionEffect);
        child.SetActive(false);
        Destroy(GetComponent<StoneManager>());
        Destroy(gameObject,2f);

    }
}
