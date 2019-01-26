using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float jumpHeight = 300;
    public float babyCount;
    public bool hasBabies = true;
    public bool vulnerable = true;

    void Update()
    {
        //movimiento basico de derecha a izquierda

        if (vulnerable == true)
        {

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {

                transform.Translate(speed * Time.deltaTime, 0f, 0f);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(-speed * Time.deltaTime, 0f, 0f);
            }
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

    IEnumerator gotHit()
    {
        yield return new WaitForSeconds(1.5f);
        vulnerable = true;
        Color temp = GetComponent<SpriteRenderer>().color;
        temp.a = 1f;
        GetComponent<SpriteRenderer>().color = temp;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Si se hace contacto con el piso y se aprieta espacio, salta.
        if ((collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Tree") && Input.GetKeyDown(KeyCode.Space) && vulnerable == true)
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

        //si se colisiona con un enemigo y hay bebes, el babyCount baja, si no hay bebes, moris
        //la colision te empuja hacia atras, te vuelve ivulnerable 3 segundos y transparente.
        if (collision.gameObject.tag == "Enemy" && hasBabies == true && vulnerable == true)
        {
            print("pierdo bebe");
            babyCount = 0;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-300 * Time.deltaTime, 150 * Time.deltaTime), ForceMode2D.Impulse);
            vulnerable = false;
            Color temp = GetComponent<SpriteRenderer>().color;
            temp.a = 0.5f;
            GetComponent<SpriteRenderer>().color = temp;
            StartCoroutine(gotHit());
        }

        else if (collision.gameObject.tag == "Enemy" && hasBabies == false && vulnerable == true)
        {
            Destroy(gameObject);
        }

    }
}



