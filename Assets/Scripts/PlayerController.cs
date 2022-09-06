using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody playerRb;

    private GameObject focalPoint;

    public bool hasPowerUp;
    public float powerupStrength = 15;
    public GameObject powerupIndicator;

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
        // playerRb.AddForce(Vector3.forward * forwardInput * speed);
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);

        // Make the powerupIndicator follow the player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    /**
    Called when we collide with a colliderBody on another object with onTrigger
    */
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Powerup"))
        {
            hasPowerUp = true;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine("PowerupCountdownRoutine");
        }
    }

    /**
    *   Interface
    */
    IEnumerator PowerupCountdownRoutine()
    {
        // Createa new thread in the background
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        powerupIndicator.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerUp);

            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse );
        }
    }
}
