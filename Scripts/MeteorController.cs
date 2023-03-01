using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    [Tooltip("Prefab Of Meteor")]
    public GameObject meteor;
    [Tooltip("Fall Speed of Meteor")]
    public static float speed;
    public static float yPos;
    public static float zPos;
    public float max_x_and_y;
    private Transform createPos;
    // Start is called before the first frame update
    void Start()
    {
        speed = 20;
        yPos = 40;
        zPos = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            var x = Random.Range(0, max_x_and_y);
            var m = Random.Range(0, 100);
            if (m % 2 == 0)
            {
                x = -x;
            }
            var myObject = Instantiate(meteor, new Vector3(x, yPos, zPos), Quaternion.identity);
            myObject.GetComponent<Rigidbody>().AddForce(Vector3.back * speed, ForceMode.Impulse);
            Destroy(myObject, 3);
        }
    }
}
