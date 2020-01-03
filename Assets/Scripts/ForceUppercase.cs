using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ForceUppercase : MonoBehaviour
{
    void Update()
    {
        var textComponents = GetComponents<Text>();

        foreach(var component in textComponents)
        {
            component.text = component.text.ToUpper();
        }
    }
}
