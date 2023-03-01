using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSpawner : MonoBehaviour
{
    #region Serialized Field

    [SerializeField] GameObject lavaPrefab;
    [SerializeField] int maxSpawn;
    #endregion

    #region  Private Fields
    private int currentSpawn;

    #endregion




    private void OnTriggerExit(Collider other)
    {
        if (currentSpawn == maxSpawn)
            return;
        Instantiate(lavaPrefab, transform.position, transform.rotation);
        currentSpawn++;
    }


}
