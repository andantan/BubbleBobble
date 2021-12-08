using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    public GameManager GM;
    public int nextMove;

    bool trapped = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.transform.position = new Vector2(0, 10);
        Invoke("ThinkDirection", 1.5f);
    }

    void FixedUpdate()
    {
        //move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        if (this.transform.position.y <= -6)
            this.transform.position = new Vector2(this.transform.position.x, 10);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bubble")
        {
            trapped = true;

            Destroy(other.gameObject);
                    
            animator.SetTrigger("isCatch");
        }
        if (other.gameObject.tag == "DragonPlayer" && trapped)
        {
            Destroy(this.gameObject);
            GM.monsterNum -= 1;
        }
        
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Bubble")
            Destroy(gameObject);
    }

    void ThinkDirection()
    {
        nextMove = Random.Range(-1, 2);

        //flip sprite
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;

        Invoke("ThinkDirection", 3);
    }

    void TrappedByPlayer()
    {
    }
}
