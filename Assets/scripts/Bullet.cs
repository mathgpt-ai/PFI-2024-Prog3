using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 1000;
    Rigidbody rb;
    [SerializeField] float lifeSpan = 2;

    [SerializeField] int bulletDamage;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed*Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "ennemy")
        {
            //a faire le ennemy script.
        }

        // si je veux mettre des effets but im lazy for now

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Invoke(nameof(DisableBullet), lifeSpan);
    }
    private void DisableBullet()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(DisableBullet));
    }
}
