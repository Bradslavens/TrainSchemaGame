using UnityEngine;
using TMPro;

public class SetChildTextToParentName : MonoBehaviour
{
    private void Start()
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
