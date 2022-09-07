using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2;
    private GameObject player;
    private Rigidbody enemyRb;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //  We need to move enmy in the direction of the player;
        // Get player's postion
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed );

        // if enemy position, is less than -10, destroy enemy.
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
