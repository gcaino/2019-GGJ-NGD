using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float jumpHeight = 300;
    public bool hasBabies = true;
    float baseSpeed;

    private void Start()
    {
        //Asigna valor a baseSpeed para controlar la velocidad de sprint
        baseSpeed = speed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            //Dashea al apretar shift 
            speed = speed * 2;
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //Al levantar shift derecho vuelve la velocidad a la normalidad
            speed = baseSpeed;
        }

        //movimiento basico de derecha a izquierda

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {

            transform.Translate(speed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed * Time.deltaTime, 0f, 0f);
        }


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Si se hace contacto con el piso y se aprieta espacio, salta.
        if (collision.gameObject.tag == "Floor" && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight * Time.deltaTime), ForceMode2D.Impulse);
        }
    }

}



