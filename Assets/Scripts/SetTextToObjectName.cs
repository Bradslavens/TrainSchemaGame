using UnityEngine;
using TMPro;

public class SetTextToObjectName : MonoBehaviour
{
    private void Awake()
    {
        // Get the child object of the current GameObject
        Transform childTransform = transform.GetChild(0);

        if (childTransform != null)
        {
            // Get the child object of the first child GameObject
            Transform grandChildTransform = childTransform.GetChild(0);

            if (grandChildTransform != null)
            {
                // Get the TextMeshPro component of the grandchild GameObject
                TextMeshPro textMeshPro = grandChildTransform.GetComponent<TextMeshPro>();

                if (textMeshPro != null)
                {
                    // Set the text of the TextMeshPro component to the name of the current GameObject
                    textMeshPro.text = gameObject.name;
                }
                else
                {
                    Debug.LogWarning("TextMeshPro component not found on the grandchild GameObject.");
                }
            }
            else
            {
                Debug.LogWarning("Grandchild object not found.");
            }
        }
        else
        {
            Debug.LogWarning("Child object not found.");
        }
    }
}
