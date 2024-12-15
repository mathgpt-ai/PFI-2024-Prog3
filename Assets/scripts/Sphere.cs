using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{

    private GameObject go;
    Manage manage;
    private Rigidbody rb;
    

    void Start()
    {
        go = GameObject.Find("GameManager");
        manage=go.GetComponent<Manage>();
        rb = GetComponent<Rigidbody>();
      
    }

    void OnCollisionEnter(Collision collision)
    {
       
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;



            rb.isKinematic = true;

        manage.nodes.Add(gameObject);
        
       
        
    }


    
}


