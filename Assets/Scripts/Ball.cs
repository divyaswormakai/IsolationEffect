using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float xVel, yVel;
    Vector2 screenBounds;
    Controller controller;
    SpriteRenderer spriteRenderer;
    float checkTime = 5f;
    // Start is called before the first frame update
    void Awake()
    {
        controller = FindObjectOfType<Controller>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));


        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                {
                    xVel = 1;
                    yVel = 1;
                    break;
                }
            case 1:
                {
                    xVel = 1;
                    yVel = -1;
                    break;
                }
            case 2:
                {
                    xVel = -1;
                    yVel = 1;
                    break;
                }
            default:
                {
                    xVel = -1;
                    yVel = -1;
                    break;
                }
        }
    }

    void Update()
    {
        Vector2 pos = transform.position;
        pos.x += xVel * Time.deltaTime;
        pos.y += yVel * Time.deltaTime;
        transform.position = pos;

        if (pos.x < (-1 * (screenBounds.x - 0.5f)) || pos.x > (screenBounds.x - 0.5f))
        {
            xVel *= -1;
        }
        if (pos.y < (-1 * (screenBounds.y - 0.5f)) || pos.y > (screenBounds.y - 0.5f))
        {
            yVel *= -1;
        }
        checkTime -= Time.deltaTime;
        if (checkTime < 0f)
        {
            checkTime = 5f;
            if (spriteRenderer.color == Color.red)
            {
                int randDeath = Random.Range(0, 100);
                if (randDeath < 7)
                {
                    spriteRenderer.color = Color.gray;
                    xVel = 0;
                    yVel = 0;
                    Destroy(GetComponent<Rigidbody2D>());
                    Destroy(GetComponent<CircleCollider2D>());
                }
                else
                {
                    int randRecover = Random.Range(0, 100);
                    if (randRecover < 31)
                    {
                        spriteRenderer.color = Color.blue;
                        controller.totalRecoveries++;
                    }
                }
                FindObjectOfType<BallScript>().UpdateInfected();
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        Vector2 temp = Vector2.Reflect(new Vector2(xVel, yVel), collision.GetContact(0).normal);
        xVel = temp.x;
        yVel = temp.y;
        if (collision.gameObject.name.Contains("circle"))
        {
            if (collision.gameObject.GetComponent<SpriteRenderer>().color == Color.red)
            {
                spriteRenderer.color = Color.red;
                controller.totalInfections++;
                FindObjectOfType<BallScript>().UpdateInfected();
            }
        }

    }

}
