using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update

    public int health = 3;
    void Start()
    {

    }

    //coroutine to make sprite red for a second
    IEnumerator MakeSpriteRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy Health: " + health);
        //make sprite red
        StartCoroutine(MakeSpriteRed());
        if (health <= 0)
        {

            Destroy(gameObject);
        }
    }

}
