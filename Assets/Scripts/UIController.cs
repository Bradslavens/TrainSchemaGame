using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject uiPanel; // Drag and drop the UI panel game object here in the Inspector window

    private void Start()
    {
        // Hide the UI panel on start
        uiPanel.SetActive(false);
    }

    public void ShowUI()
    {
        // Show the UI panel
        uiPanel.SetActive(true);
    }

    public void HideUI()
    {
        // Hide the UI panel
        uiPanel.SetActive(false);
    }

}
