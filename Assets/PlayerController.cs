using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float speed = 100f;
    public bool grounded;
    public float jumpPower = 6.5f;

    private Rigidbody2D rb2d;
    private Animator anim;
    private bool jump;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Grounded", grounded);

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded) //Si presionar arriba y estas tocando el suelo
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        Vector3 fixedVelocity = rb2d.velocity;
        fixedVelocity.x *= 0.75f; //Aumentar la fricción al caminar en el suelo

        if (grounded) //Si tocas el suelo aplicas la fricción
        {
            rb2d.velocity = fixedVelocity;
        }

        float h = Input.GetAxis("Horizontal");
        rb2d.AddForce(Vector2.right * speed * h);

        //Limitar la aceleración al caminar
        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

        //Cambiar direccion izquierda o derecha la imagen del personaje
        if (h > 0.1f) //Si miro a la derecha
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (h < -0.1f) //Si miro a la izquierda
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        //Limitar la aceleración al caminar
        /*
        if (rb2d.velocity.x > maxSpeed) //Caminar a la derecha
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }
        if (rb2d.velocity.x < -maxSpeed) //Caminar a la izquierda
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
        */

        if(jump) //Si generas un salto
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0); //Si saltas, tu velocidad Y es cero.
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); //Creas una fuerza hacia arriba
            jump = false; //No puedes saltar
        }

    }

    //Reaparecer del inicio cuando salgas del limite
    void OnBecameInvisible()
    {
        transform.position = new Vector3(0, 0, 0);
    }
}
