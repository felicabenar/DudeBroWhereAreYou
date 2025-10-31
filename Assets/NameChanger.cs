using System;
using TMPro;
using UnityEngine;

public class NameChanger : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;

    // Create an event called onChangeName
    // onChangeName must pass in a <string> when its called
    public static Action<string> onChangeName;

    public void ChangeName()
    {
        // If nothing is listening to onChangeName, do nothing
        if (onChangeName != null && nameInput.text.Length > 0)
        {
            Debug.Log("Changing Name");
            onChangeName.Invoke(nameInput.text);
        }
    }
}
