using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject follow;
    public Vector2 minCamPos, maxCamPos;
    public float smoothTime; //EL tiempo de retardo al moverse la cámara

    private Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float posX = follow.transform.position.x; //Captura la posición X del Object PLayer
        //float posY = follow.transform.position.y; //Captura la posición Y del Object PLayer
        //Efecto de suavizado al moverse la cámara con el personaje
        float posX = Mathf.SmoothDamp(transform.position.x, follow.transform.position.x, ref velocity.x, smoothTime); //Captura la posición X del Object PLayer
        float posY = Mathf.SmoothDamp(transform.position.y, follow.transform.position.y, ref velocity.y, smoothTime);

        transform.position = new Vector3(
            Mathf.Clamp(posX, minCamPos.x, maxCamPos.x), //Limita en el eje X el movimiento de la cámara y se desplaza
            Mathf.Clamp(posY, minCamPos.y, maxCamPos.y), //Limita en el eje Y el movimiento de la cámara y se desplaza
            transform.position.z);
    }
}
