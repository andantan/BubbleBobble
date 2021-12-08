using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bubble;
    public int speed = 8;
    public int jump_force = 1000;
    public int attack_speed = 10;


    float size = 1.5f;
    float span = 0.1f;
    float delta = 0;
    float key = 0;


    Rigidbody2D rigidbody2D;
    Animator animator;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        delta += Time.deltaTime;

        Move();

        // Debug.Log(transform.localScale.x);
    }

    void Move()
    {
        // Movement
        if (Input.GetKey(KeyCode.RightArrow)) // Move right
        {
            key = size;

            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            
            animator.SetBool("IsWalking", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) // Move left
        {
            key = -1 * size;

            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);         

            animator.SetBool("IsWalking", true);
        }
        else // Standing
        {
            animator.SetBool("IsWalking", false);
        }

        if (Input.GetKey(KeyCode.A) && span < delta)
        {
            if (key == size) Attack(Vector2.right, key);
            else Attack(Vector2.left, key);
        }   

        // Modify X-Scale-Direction (Right -> 1, Left -> -1)
        if (key != 0)
            transform.localScale = new Vector3(key, size, 1);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && rigidbody2D.velocity.y > -1.0f)
        {
            rigidbody2D.AddForce(transform.up * jump_force);

            animator.SetTrigger("DoJump");
        }

        if (4.3 < transform.position.y)
        {
            rigidbody2D.velocity = Vector2.zero;
        }

        if (transform.position.y < -5.3f)
        {
            transform.position = new Vector2(transform.position.x, 5.3f);
        }

        // Debug.Log(rigidbody2D.velocity.y);
    }

    void Attack(Vector2 vector, float key)
    {
        Vector3 bubbleInitiatePosition = transform.position;

        delta = 0;

        bubbleInitiatePosition.x += (key / 10);

        GameObject BubbleObj = Instantiate(bubble, bubbleInitiatePosition, transform.rotation);

        Rigidbody2D rigid = BubbleObj.GetComponent<Rigidbody2D>();

        rigid.AddForce(vector * attack_speed, ForceMode2D.Impulse);
    }
}
