using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject multipleChoiceUI;
    public Button[] answerButtons;

    public GameObject scoreBoard;

    [SerializeField]
    private TextMeshProUGUI scoreText;

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
        AssignUIElementsToAllCrossings(); // Add this line
    }

    private void AssignUIElementsToAllSignals()
    {
        TrafficSignalController[] trafficSignalControllers = FindObjectsOfType<TrafficSignalController>();
        foreach (TrafficSignalController trafficSignalController in trafficSignalControllers)
        {
            trafficSignalController.AssignUIElements(multipleChoiceUI, answerButtons);
        }
    }

    // Add this new method
    private void AssignUIElementsToAllCrossings()
    {
        CrossingController[] crossingControllers = FindObjectsOfType<CrossingController>();
        foreach (CrossingController crossingController in crossingControllers)
        {
            crossingController.AssignUIElements(multipleChoiceUI, answerButtons);
        }
    }

    public void ShowScoreBoard()
    {
        if (scoreBoard != null)
        {
            // Calculate and display the score
            float score = TrafficSignalController.CalculateScore();
            scoreText.text = string.Format("Score: {0:0}", score * 100);
            scoreBoard.SetActive(true);
        }
    }
}
