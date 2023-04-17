using UnityEngine;
using TMPro;

public class ToggleTextAndColliderVisibility : MonoBehaviour
{
    [SerializeField]
    private bool isTextVisible = true;
    [SerializeField]
    private bool isColliderVisible = true;

    public void ToggleTextVisibility()
    {
        isTextVisible = !isTextVisible;
        TextMeshPro[] textMeshPros = GetComponentsInChildren<TextMeshPro>();

        foreach (TextMeshPro textMeshPro in textMeshPros)
        {
            textMeshPro.enabled = isTextVisible;
        }
    }

    public void ToggleColliderVisibility()
    {
        isColliderVisible = !isColliderVisible;
        BoxCollider2D[] boxColliders = GetComponentsInChildren<BoxCollider2D>();

        foreach (BoxCollider2D boxCollider in boxColliders)
        {
            boxCollider.enabled = isColliderVisible;
        }
    }
}
