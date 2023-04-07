using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject multipleChoiceUI;
    public Button[] answerButtons;
    public TrafficSignalController trafficSignalPrefab;

    private void Start()
    {
        TrafficSignalController[] trafficSignalControllers = FindObjectsOfType<TrafficSignalController>();

        foreach (TrafficSignalController controller in trafficSignalControllers)
        {
            controller.AssignUIElements(multipleChoiceUI, answerButtons);
        }
    }
}
