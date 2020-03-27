using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxSpeed = 2f;
    public float speed = 2f;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb2d.AddForce(Vector2.right * speed); //Desplazamiento lateral
        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

        if(rb2d.velocity.x > -0.01f && rb2d.velocity.x < 0.01f)
        {
            speed = -speed;
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log("Player detected!");
            //Destroy(gameObject); //Borrar al enemigo cuando toques al player
            float yOffset = 0.4f;
            if ((transform.position.y + yOffset) < col.transform.position.y)
            {
                col.SendMessageUpwards("EnemyJump");
                Destroy(gameObject);
            }
            else
            {
                col.SendMessageUpwards("EnemyKnockBack", transform.position.x);
            }
        }
    }
}
