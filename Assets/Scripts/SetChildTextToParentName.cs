using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SetChildTextToParentName : MonoBehaviour
{
    [SerializeField]
    private List<string> sceneNames;

    private void Start()
    {
        // Check if the current scene's name is in the list of scene names
        if (sceneNames.Contains(SceneManager.GetActiveScene().name))
        {
            // Get the child object of the current GameObject
            Transform childTransform = transform.GetChild(0);

            if (childTransform != null)
            {
                // Get the TextMeshPro component of the child GameObject
                TextMeshPro textMeshPro = childTransform.GetComponent<TextMeshPro>();

                if (textMeshPro != null)
                {
                    // Set the text of the TextMeshPro component to the name of the current GameObject
                    textMeshPro.text = gameObject.name;
                }
                else
                {
                    Debug.LogWarning("TextMeshPro component not found on the child GameObject.");
                }
            }
            else
            {
                Debug.LogWarning("Child object not found.");
            }
        }
    }
}
