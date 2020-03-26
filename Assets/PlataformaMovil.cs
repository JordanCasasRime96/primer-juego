using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    public Transform target;
    public float speed;

    private Vector3 start, end;
    // Start is called before the first frame update
    void Start()
    {

        if(target != null)
        {
            target.parent = null; //Permite que el objeto TARGET ya no sea hijo de la PLATAFORMA_MOVIL_1X1
            start = transform.position;
            end = target.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            float fixedSpeed = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
        }
        if(transform.position == target.position)
        {
            target.position = (target.position == start) ? end : start; //Si "target.position == start" entonces usará la variable "end", o sino usuará "start"
        }
    }
}
