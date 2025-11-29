using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameChanger : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;
    public static string DisplayName { get; private set; }

    // Create an event called onChangeName
    // onChangeName must pass in a <string> when its called
    public static Action<string> onChangeName;
    private string PlayerName;

    public void ChangeName()
    {
        // If nothing is listening to onChangeName, do nothing
        if (onChangeName != null && nameInput.text.Length > 0)
        {
            Debug.Log("Changing Name");
            onChangeName.Invoke(nameInput.text);
            PlayerPrefs.SetString("onChangeName", PlayerName);
        }
    }
}


/*
using TMPro;
using UnityEngine;
using UnityEngine.UI;

    public class PlayerNameInput : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private TMP_InputField nameInputField = null;
        [SerializeField] private Button continueButton = null;

        public static string DisplayName { get; private set; }

        private const string PlayerPrefsNameKey = "PlayerName";

        private void Start() => SetUpInputField();

        private void SetUpInputField()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefsNameKey)) { return; }

            string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);

            nameInputField.text = defaultName;

            SetPlayerName(defaultName);
        }

        public void SetPlayerName(string name)
        {
            continueButton.interactable = !string.IsNullOrEmpty(name);
        }

        public void SavePlayerName()
        {
            DisplayName = nameInputField.text;

            PlayerPrefs.SetString(PlayerPrefsNameKey, DisplayName);
        }
    }
*/