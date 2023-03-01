using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetrifactionControl : MonoBehaviour
{
    private SkinnedMeshRenderer skin;
    private float s;

    private float a;

    float animSpeed = 0;


    private Material mat;

    private Animator m_anim;
    private Coroutine coroutine;

    [SerializeField] float petrifactionSpeed;



    private void Start()
    {
        skin = GetComponentInChildren<SkinnedMeshRenderer>();
        mat = skin.material;
        m_anim = GetComponent<Animator>();
        mat.SetFloat("_fill", -1);
        mat.SetFloat("_metalic", 0.9f);


    }

    public void StartPetrifaction()
    {
        if(gameObject.CompareTag("Player"))
        GameManager.Instance.UpdateGameState(GAMESTATE.DEFEAT);
        if (coroutine == null)
        {
            StartCoroutine(MakePetrifaction());
        }
    }


    IEnumerator MakePetrifaction()
    {

        while (true)
        {
            s += Time.deltaTime * petrifactionSpeed;
            mat.SetFloat("_fill", s);
            animSpeed = 1 - s;
            animSpeed = Mathf.Clamp(animSpeed, 0, 1);
            m_anim.SetFloat("speed", animSpeed);
            a = 1 - s * 1.3f;
            a = Mathf.Clamp(a, 0.2f, 0.9f);
            mat.SetFloat("_metalic", a);
            if (animSpeed == 0)
            {   
                if(gameObject.CompareTag("Player"))
                 GameManager.Instance.UpdateGameState(GAMESTATE.DEFEAT);
                 Destroy(gameObject);
                 break;
               
            }

            yield return null;

        }



    }
}
