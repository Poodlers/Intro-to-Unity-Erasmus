using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyApplyDamage : MonoBehaviour
{

    public int enemyDamage = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(enemyDamage);
        }
    }

}
