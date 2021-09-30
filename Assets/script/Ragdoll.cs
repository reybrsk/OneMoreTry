using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Ragdoll : MonoBehaviour
{
    public Rigidbody[] rigidBodys;
    public float mass;

    public PhysicMaterial material;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < rigidBodys.Length; i++)
        {
            rigidBodys[i].GetComponent<Rigidbody>().isKinematic = true;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fall()
    {
        for (int i = 0; i < rigidBodys.Length; i++)
        {
            
            rigidBodys[i].GetComponent<Rigidbody>().isKinematic = false;
            rigidBodys[i].GetComponent<Rigidbody>().mass = mass;
            rigidBodys[i].GetComponent<Collider>().material = material;
        }
    }
}
