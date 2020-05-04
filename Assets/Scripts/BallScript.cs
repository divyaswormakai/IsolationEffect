using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    public GameObject ballPrefab;
    public Button restartBtn;
    Vector2 screenBounds;

    List<GameObject> balls;
    Controller controller;


    // Start is called before the first frame update
    void Start()
    {
        balls = new List<GameObject>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        controller = FindObjectOfType<Controller>();

        InitializeBalls();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateInfected()
    {
        int temp1 = 0;
        int temp2 = 0;
        int temp3 = 0;
        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i].GetComponent<SpriteRenderer>().color == Color.red)
            {
                temp1++;
            }
            if (balls[i].GetComponent<SpriteRenderer>().color == Color.blue)
            {
                temp2++;
            }
            if (balls[i].GetComponent<SpriteRenderer>().color == Color.gray)
            {
                temp3++;
            }
            controller.infectedPop = temp1;
            controller.totalSafe = temp2;
            controller.totalDeaths = temp3;

        }
    }

    public void InitializeBalls()
    {
        DestroyAllBalls();
        balls.Clear();
        print("New creation");
        for (int i = 0; i < controller.totalSafe; i++)
        {
            float xPos = Random.Range((screenBounds.x - 2f) * -1, screenBounds.x - 2f);
            float yPos = Random.Range((screenBounds.y - 2f) * -1, screenBounds.y - 2f);
            GameObject temp = Instantiate(ballPrefab, new Vector2(xPos, yPos), Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().color = Color.blue;
            balls.Add(temp);
        }
        for (int i = 0; i < controller.totalInfections; i++)
        {
            float xPos = Random.Range((screenBounds.x - 2f) * -1, screenBounds.x - 2f);
            float yPos = Random.Range((screenBounds.y - 2f) * -1, screenBounds.y - 2f);
            GameObject temp = Instantiate(ballPrefab, new Vector2(xPos, yPos), Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().color = Color.red;
            balls.Add(temp);
        }
        controller.population = controller.totalInfections + controller.totalSafe;
        UpdateInfected();
    }

    void DestroyAllBalls()
    {
        foreach (GameObject obj in balls)
        {
            Destroy(obj);
        }
        print("Destroyed");
    }
}
