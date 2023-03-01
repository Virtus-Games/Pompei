using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorParticle : MonoBehaviour
{
    public GameObject warning;
    private GameObject myWarning;
    // Start is called before the first frame update
    void Start()
    {
        myWarning = Instantiate(warning, new Vector3(this.gameObject.transform.position.x, 0, MeteorController.zPos - (Mathf.Sqrt(MeteorController.yPos/5)* MeteorController.speed)), Quaternion.identity);
        myWarning.transform.Rotate(new Vector3(90, 0, 0), Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = this.gameObject.GetComponent<Rigidbody>().velocity;
        // Debug.Log(this.gameObject.transform.position);
        // Debug.Log(this.gameObject.transform.position + new Vector3(vel.x, vel.y, vel.z));
        //! sets fire on back of rock 
        this.gameObject.transform.LookAt(this.gameObject.transform.position - new Vector3(vel.x, vel.y, vel.z));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Destroy(this.gameObject);
            Destroy(myWarning);
        }
    }
}
