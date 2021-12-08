using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleActor : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    Animator animator;

    float delta = 0;

    void Start()
    {
        Invoke("BubbleBoom", 7);

        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Invoke("BubbleUp", 0.5f);
    }

    void Update()
    {
        delta += Time.deltaTime;

        if (4.3 < transform.position.y)
        {
            Invoke("BubbleBoom", 3);
            rigidbody2D.velocity = Vector2.zero;
        }

        if (rigidbody2D.velocity.y == 0 && transform.position.y < 4.29)
        {
            Invoke("BubbleBoom", 3);
        }
    }
    
    void DestroyBubble()
    {
        Destroy(gameObject);
    }

    void BubbleUp()
    {
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.AddForce(transform.up * 200f);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("DragonPlayer") && 1 < delta)
            BubbleBoom();
    }

    void BubbleBoom()
    {
        animator.SetTrigger("Boom");

        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        rigidbody2D.velocity = Vector2.zero;
        
        DestroyImmediate(rigidbody2D);
        Invoke("DestroyBubble", 1);
    }
}
