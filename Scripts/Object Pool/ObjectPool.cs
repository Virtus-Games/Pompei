using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    //SERIALIZED FIELDS
    [SerializeField] int objCount;

    //PUBLIC FILEDS
    public List<GameObject> poolObjects;

    //SCRIPTS REFERENCES

    //PRIVATE FILEDS
    private GameObject bulletPrefab;

    private void Start()
    {

        for (int i = 0; i < objCount; i++)
        {
            GameObject projectTile = Instantiate(bulletPrefab);
            projectTile.SetActive(false);
            projectTile.transform.SetParent(transform);
            projectTile.transform.localPosition = Vector3.zero;
            poolObjects.Add(projectTile);
        }
    }



}
