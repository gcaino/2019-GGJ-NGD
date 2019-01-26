using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float jumpHeight = 300;
    public float babyCount;
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

        //Si tenes bebes, el bool hasBabies es true.
        if (babyCount > 0)
        {
            hasBabies = true;
        }

        else
        {
            hasBabies = false;
        }

        // failsafe para que no se pase de 5 bebes.
        if (babyCount > 5)
        {
            babyCount = 5;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Si se hace contacto con el piso y se aprieta espacio, salta.
        if ((collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Tree") && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight * Time.deltaTime), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //si se recolecta un bebe, el bool de hasBabies es verdadero
        if (collision.gameObject.tag == "Baby")
        {
            print("recolecto bebe");
            babyCount += 1;
        }

        //si se colisiona con un enemigo y hay bebes, el babyCount baja, si no hay bebes, moris.
        if (collision.gameObject.tag == "Enemy" && hasBabies == true)
        {
            print("pierdo bebe");
            babyCount = 0;
        }

        else if (collision.gameObject.tag == "Enemy" && hasBabies == false)
        {
            Destroy(gameObject);
        }

    }
}



