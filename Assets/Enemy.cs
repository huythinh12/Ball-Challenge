using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    Rigidbody enemyRb;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        if(transform.name.StartsWith("EnemyBig"))
        {
            StartCoroutine(Illution());
        }
    }
    IEnumerator Illution()
    {
        yield return new WaitForSeconds(2);
        Instantiate(gameObject, new Vector3((transform.position.x + 3), transform.position.y, (transform.position.z + 3)), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            Vector3 lookDir = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDir * speed);
        }
        if(transform.position.y < -10)
        {
            SpawnManager.countEnemyDeath++;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.StartsWith("ForceBig"))
        {

            Vector3 awayFromPlayer = (transform.position - other.transform.position);

            enemyRb.AddForce(awayFromPlayer * 5.0f, ForceMode.Impulse);
        }
    }
}
