using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject multipleChoiceUI;
    public Button[] answerButtons;

    public GameObject scoreBoard;

    private void Start()
    {
        if (scoreBoard != null)
        {
            scoreBoard.SetActive(false);
        }
    }


    private void Awake()
    {
        AssignUIElementsToAllSignals();

    }

    private void AssignUIElementsToAllSignals()
    {
        TrafficSignalController[] trafficSignalControllers = FindObjectsOfType<TrafficSignalController>();
        foreach (TrafficSignalController trafficSignalController in trafficSignalControllers)
        {
            trafficSignalController.AssignUIElements(multipleChoiceUI, answerButtons);
        }
    }

    public void ShowScoreBoard()
    {
        if (scoreBoard != null)
        {
            scoreBoard.SetActive(true);
        }
    }

}
