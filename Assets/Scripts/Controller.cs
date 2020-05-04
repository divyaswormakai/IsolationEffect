using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public int totalInfections = 5;
    public int infectedPop = 0;
    public int totalRecoveries = 0;
    public int totalDeaths = 0;
    public int population = 0;
    public int totalSafe = 30;

    public TextMeshProUGUI hud, errTxt;
    public TMP_InputField infected, safe;
    public Canvas mainCanvas, customCanvas;
    // Start is called before the first frame update
    void Start()
    {
        customCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        hud.text = "Population: " + population.ToString() + "\nInfected: " + infectedPop.ToString() + "\nSafe: " + totalSafe.ToString() + "\nInfections: " + totalInfections.ToString() + "\nDeaths: " + totalDeaths.ToString() + "\nRecoveries: " + totalRecoveries.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (customCanvas.enabled)
            {
                mainCanvas.enabled = true;
                customCanvas.enabled = false;
            }
        }
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SetCustom()
    {
        mainCanvas.enabled = false;
        customCanvas.enabled = true;
    }

    public void StartCustom()
    {
        string infStr = infected.text;
        string safeStr = safe.text;
        totalInfections = int.Parse(infStr);
        totalSafe = int.Parse(safeStr);

        if (totalInfections + totalSafe > 80 || totalInfections + totalSafe < 20)
        {
            errTxt.text = "Please limit your population from 20 to 80 for demonstration purposes";
        }
        else
        {
            infectedPop = 0;
            totalRecoveries = 0;
            totalDeaths = 0;

            FindObjectOfType<BallScript>().InitializeBalls();
            mainCanvas.enabled = true;
            customCanvas.enabled = false;
        }

        FindObjectOfType<DrawLine>().DestoryLines();
    }

    public void UpdatePopulation()
    {
        errTxt.text = "Total population: " + (int.Parse(infected.text) + int.Parse(safe.text)).ToString();

    }

}
