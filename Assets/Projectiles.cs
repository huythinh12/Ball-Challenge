using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    Rigidbody bullet;
    public float speed;
    GameObject[] enemy;
    public Vector3 lookDir; 
    // Start is called before the first frame update
    void Start()
    {
        bullet = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {

                bullet.AddForce(lookDir * speed);
        if (transform.position.z > 20 || transform.position.z < -20)
        {
            Destroy(gameObject);
        }
        else if (transform.position.x > 20 || transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }
}
