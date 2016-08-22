using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float speed, damage, travelTime;
    Rigidbody rigidbody;
    Shooter shooter;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.forward * speed);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);

        Destroy(gameObject, travelTime);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Health enemyHP = collision.gameObject.GetComponent<Health>();

        if (enemyHP != null)
        {
            enemyHP.health -= damage;
        }
        Destroy(gameObject);
    }
}
