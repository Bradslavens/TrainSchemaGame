using UnityEngine;

public class DeactivateChildColliders : MonoBehaviour
{
    private void Awake()
    {
        // Iterate through all child objects
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);

            if (childTransform != null)
            {
                // Get the Collider2D component of the child GameObject
                Collider2D collider2D = childTransform.GetComponent<Collider2D>();

                if (collider2D != null)
                {
                    // Deactivate the Collider2D component
                    collider2D.enabled = false;
                }
                else
                {
                    Debug.LogWarning("Collider2D component not found on the child GameObject.");
                }
            }
            else
            {
                Debug.LogWarning("Child object not found.");
            }
        }
    }
}
