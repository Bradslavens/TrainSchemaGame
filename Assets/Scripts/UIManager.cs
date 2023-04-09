using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject multipleChoiceUI;
    public Button[] answerButtons;

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
}
