using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Basket : MonoBehaviour
{
    public float speed = 10.0f;
    Vector3 mousePosition;
    Rigidbody2D rb;
    Vector2 direction;
    public GameObject error;
    public ParticleSystem win;
    bool finished = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (finished)
            return;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetMouseButton(0) && Input.touchCount == 1)
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 987; // select distance units from the camera

            mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
            direction = (mousePosition - transform.position);
            rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        Debug.Log(tag);
        switch (tag)
        {
            case "win":
                win.Play();
                Destroy(collision.gameObject);
                Invoke("Win", 5);
                finished = true;
                break;
            case "try":
                Destroy(collision.gameObject);
                Color c = new Color(1f, 0f, 0f, 0.5f);
                Time.timeScale = 0.1f;
                error.GetComponent<SpriteRenderer>().color = c;
                Invoke("FadeOut", 0.2f);
                Handheld.Vibrate();
                break;
        }
    }
}
