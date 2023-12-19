using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    // Start is called before the first frame update
    private List<Tuple<Vector2, TimeSpan>> path = new List<Tuple<Vector2, TimeSpan>>();

    private Animator animator;

    private Rigidbody2D rb;

    private BoxCollider2D swordCollider;

    private GameObject player;

    public int attackCooldown = 1;

    private float attackCounter = 0;

    private bool canAttack = true;

    private Transform sword;
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        path.Add(new Tuple<Vector2, TimeSpan>(new Vector2(0, 0), new TimeSpan(0, 0, 1)));
        path.Add(new Tuple<Vector2, TimeSpan>(new Vector2(0, 1), new TimeSpan(0, 0, 1)));
        path.Add(new Tuple<Vector2, TimeSpan>(new Vector2(1, 0), new TimeSpan(0, 0, 1)));
        path.Add(new Tuple<Vector2, TimeSpan>(new Vector2(0, -1), new TimeSpan(0, 0, 1)));
        path.Add(new Tuple<Vector2, TimeSpan>(new Vector2(-1, 0), new TimeSpan(0, 0, 1)));

        swordCollider = gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
        sword = gameObject.transform.GetChild(0);
        DisableSwordCollider();

        StartCoroutine(Move());
    }


    IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitUntil(() =>
            {
                return animator.GetBool("attacking") == false;
            });
            //move in the direction of the first element in tuple for the duration of the second element
            foreach (Tuple<Vector2, TimeSpan> tuple in path)
            {
                animator.SetFloat("moveX", tuple.Item1.x);
                animator.SetFloat("moveY", tuple.Item1.y);
                rb.velocity = tuple.Item1 * speed;

                animator.SetBool("moving", true);
                yield return new WaitForSeconds((float)tuple.Item2.TotalSeconds);

            }
        }

    }

    public void EnableSwordCollider()
    {
        animator.SetInteger("player_dir_x", 0);
        animator.SetInteger("player_dir_y", 0);
        swordCollider.enabled = true;
    }

    public void DisableSwordCollider()
    {
        swordCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!canAttack)
        {
            attackCounter += Time.deltaTime;
            if (attackCounter >= attackCooldown)
            {
                canAttack = true;

            }
        }

        // if close to the player, try to attack him
        if (!player.IsDestroyed() && Vector3.Distance(transform.position, player.transform.position) < 1.5f && canAttack)
        {
            canAttack = false;
            rb.velocity = Vector2.zero;
            attackCounter = 0;

            //decide which direction to attack
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            if (Math.Abs(direction.y) > 0.5f)
            {
                direction.x = 0;
                direction.y = direction.y > 0 ? 1 : -1;
            }
            else
            {
                direction.x = direction.x > 0 ? 1 : -1;
                direction.y = 0;
            }
            //rotate the sword collider
            if (direction.x > 0)
            {
                sword.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (direction.x < 0)
            {
                sword.rotation = Quaternion.Euler(0, 0, 270);
            }
            else if (direction.y > 0)
            {
                sword.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (direction.y < 0)
            {
                sword.rotation = Quaternion.Euler(0, 0, 180);
            }

            animator.SetInteger("player_dir_x", (int)direction.x);
            animator.SetInteger("player_dir_y", (int)direction.y);

            animator.SetBool("moving", false);

            animator.SetBool("attacking", true);



        }

    }

    void StopAttack()
    {
        animator.SetBool("attacking", false);
    }
}
