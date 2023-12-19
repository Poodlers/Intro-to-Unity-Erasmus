using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private UiInstanceController uiInstanceController;
    private bool isVulnerable = true;

    public int health = 3;
    void Start()
    {
        uiInstanceController = GameObject.Find("HeartContainer").GetComponent<UiInstanceController>();


    }
    public void TakeDamage(int damage)
    {
        if (!isVulnerable)
        {
            return;
        }
        health -= damage;
        Debug.Log("Player Health: " + health);
        uiInstanceController.DestroyInstance();
        StartCoroutine(Invulnerable());
        if (health <= 0)
        {

            Destroy(gameObject);
        }
    }

    private void ToggleSprite()
    {
        GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
    }

    IEnumerator Invulnerable()
    {
        isVulnerable = false;
        // flash sprite
        InvokeRepeating("ToggleSprite", 0f, 0.2f);
        yield return new WaitForSeconds(2);
        CancelInvoke("ToggleSprite");
        GetComponent<SpriteRenderer>().enabled = true;
        isVulnerable = true;
    }

    // Update is called once per frame
    void Update()
    {


    }

}
