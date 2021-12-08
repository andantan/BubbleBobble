using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;

    public int nextMove;

    bool trapped = false;


    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.gameObject.SetActive(false);
        Invoke("Start", 5);
    }
    
    void Start()
    {
        this.transform.position = new Vector3(4, 4, 0);
        this.gameObject.SetActive(true);
        Invoke("ThinkDirection", 2);
    }

    void FixedUpdate()
    {
        //move
        if (nextMove == -1 && this.transform.position.x >= -5)
            this.transform.Translate(Vector2.left * 2.0f * Time.deltaTime);
        else if (nextMove == -1 && this.transform.position.x <= 5)
            this.transform.Translate(Vector2.right * 2.0f * Time.deltaTime);

        if (nextMove == 0 && this.transform.position.y >= -4.3)
            this.transform.Translate(Vector2.down * 2.0f * Time.deltaTime);
        else if(this.transform.position.y <= 4.3)
            this.transform.Translate(Vector2.up * 2.0f * Time.deltaTime);

        if (nextMove == 1 && this.transform.position.x <= 5)
            this.transform.Translate(Vector2.right * 2.0f * Time.deltaTime);
        else if (nextMove == 1 && this.transform.position.x >= -5)
            this.transform.Translate(Vector2.left * 2.0f * Time.deltaTime);

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Bubble")
        {
            trapped = true;

            Destroy(other.gameObject);

            animator.SetTrigger("isCatch");
        }
        if(other.gameObject.tag=="DragonPlayer" && trapped)
        {
            Destroy(this.gameObject);
        }
    }

    void ThinkDirection()
    {
        nextMove = Random.Range(-1, 2);

        //flip sprite
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;

        Invoke("ThinkDirection", 2);
    }
}
