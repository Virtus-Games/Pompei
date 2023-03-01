using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    #region  Serialized Fields

    [Header("Stone Drop Shake")]
    [SerializeField] float stoneDropShakeAmplitude;
    [SerializeField] float stoneDropShakeSpeed;
    [SerializeField] float stoneDropShakeDuration;

    [Header("Volcano Explosion Shake")]
    [SerializeField] float volcanoExplosionShakeAmplitude;
    [SerializeField] float volcanoExplosionShakeSpeed;
    [SerializeField] float volcanoExplosionShakeDuration;

    #endregion

    #region Private Fields
    private Coroutine coroutine;
    private Transform tr;

    private float timer;

    #endregion

    private void Start()
    {
        tr = transform;
    }


    public void StoneDropShaker()
    {
        StartCoroutine(StoneDropShake());
    }

    public void VolcanoExplosionShaker()
    {
        StartCoroutine(VolcanoExplosionShake());
    }
    private IEnumerator StoneDropShake()
    {


        while (timer < stoneDropShakeDuration)
        {
            timer += Time.deltaTime;


            tr.position += new Vector3(Mathf.Sin(Time.time * stoneDropShakeSpeed), Mathf.Cos(Time.time * stoneDropShakeSpeed), 0) * stoneDropShakeAmplitude / 10;

            yield return null;
        }
        timer = 0;
    }

    private IEnumerator VolcanoExplosionShake()
    {


        while (timer < volcanoExplosionShakeDuration)
        {
            timer += Time.deltaTime;


            tr.position += new Vector3(Mathf.Sin(Time.time * volcanoExplosionShakeSpeed), Mathf.Cos(Time.time * volcanoExplosionShakeSpeed), 0) * volcanoExplosionShakeAmplitude / 10;

            yield return null;
        }
        timer = 0;
    }

}
