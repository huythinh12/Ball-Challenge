using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject powerUpIndicator, balloon;
    public GameObject bullet;
    public GameObject managerForce;
    public float speed = 5.0f;
    public bool hasPowerUp = false;
    public GameObject retrybutton, gameoverTitle;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private bool hasRocket = false;
    private bool isLand = true;
    private float powerUp = 15.0f;
    private bool hashJump =false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);

        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        balloon.transform.position = transform.position + new Vector3(0, +2.2f, 0);

        //chi cho phep nhay 1 lan
        if (hashJump && Input.GetKeyDown(KeyCode.Space) && isLand)
        {
            isLand = false;
            playerRb.AddForce(Vector3.up * 30, ForceMode.Impulse);
            managerForce.SetActive(false);

        }
        if (transform.position.y > 4.3f)
        {
            playerRb.AddForce(Vector3.down * 80, ForceMode.Impulse);
        }

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (gameObject != null)
        {
            retrybutton.SetActive(true);
            gameoverTitle.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            if (other.name.StartsWith("PowerRocket"))
            {
                hasRocket = true;
                StartCoroutine(SpawnBulletInTime());
            }
            else if (other.name.StartsWith("PowerJump"))
            {
                hashJump = true;
                balloon.gameObject.SetActive(true);
                playerRb.AddForce(Vector3.up * 30, ForceMode.Impulse);
            }
            else
            {
                powerUpIndicator.gameObject.SetActive(true);
            }
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }

     
    }
    IEnumerator SpawnBulletInTime()
    {
        while (hasRocket)
        {
            yield return new WaitForSeconds(0.5f);
            var gameObj = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < gameObj.Length; i++)
            {
                var objBullet = Instantiate(bullet, transform.position, bullet.transform.rotation);
                objBullet.GetComponent<Projectiles>().lookDir = (gameObj[i].transform.position - transform.position).normalized;
            }
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            enemyRb.AddForce(awayFromPlayer * powerUp, ForceMode.Impulse);
        }

        if (collision.gameObject.name.StartsWith("Island") && hashJump)
        {
            isLand = true;
            StartCoroutine(MakeForce());
        }

    }
    IEnumerator MakeForce()
    {
        managerForce.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        managerForce.SetActive(false);
    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        hasRocket = false;
        hashJump = false;
        powerUpIndicator.gameObject.SetActive(false);
        balloon.gameObject.SetActive(false);
        managerForce.SetActive(false);
    }
}
