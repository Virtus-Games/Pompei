using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    #region Serialized Fields
    [SerializeField] PlayerAttackData playerAttackData;
    #endregion
    #region  Private Fields


    #endregion


    public void AddForceAtObject(Vector3 dir, GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(dir * playerAttackData.forceAmount, ForceMode.Impulse);
    }
}
