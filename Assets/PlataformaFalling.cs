using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaFalling : MonoBehaviour
{
    public float fallDelay = 1f;
    public float respawnDelay = 3f;

    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;
    private Vector3 start;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Invoke("Fall", fallDelay);
            Invoke("Respawn", fallDelay + respawnDelay);
        }
    }

    void Fall() //Cuando empieza a caer
    {
        rb2d.isKinematic = false;
        bc2d.isTrigger = true;
    }

    //Restaurar la plataforma a su posicion original
    void Respawn()
    {
        transform.position = start; //Vuelve a la posición original
        rb2d.isKinematic = true; //Vuelve cinematico la plataforma
        rb2d.velocity = Vector3.zero; //Volver a la velocidad cero todo
        bc2d.isTrigger = false; //Restaura a su posición el BoxCollider
    }
}
