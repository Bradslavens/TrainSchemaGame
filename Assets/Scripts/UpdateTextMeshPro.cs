using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateTextMeshPro : MonoBehaviour
{
    void Start()
    {
        UpdateAllSignalTexts();
    }

    void UpdateAllSignalTexts()
    {
        foreach (Transform signal in transform)
        {
            if (signal.childCount > 0)
            {
                Transform textChild = signal.Find("Text");
                if (textChild != null)
                {
                    TextMeshPro textMeshPro = textChild.GetComponent<TextMeshPro>();
                    if (textMeshPro != null)
                    {
                        textMeshPro.text = signal.name;
                    }
                    else
                    {
                        Debug.LogError("TextMeshPro component not found on Text child of " + signal.name);
                    }
                }
                else
                {
                    Debug.LogError("Text child not found for " + signal.name);
                }
            }
            else
            {
                Debug.LogError("Signal " + signal.name + " has no children");
            }
        }
    }
}
