//THIS CODE WAS CLEANED-UP WITH AI


using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Handles changing and saving the player's display name.
/// Invokes a global event so other scripts can react when the name changes.
/// </summary>
public class NameChanger : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_InputField nameInput;

    [Header("Settings")]
    [SerializeField] private int value;   // Unknown purpose, but kept for PlayerPrefs

    /// <summary>
    /// The player's current display name (static so other scripts can read it).
    /// </summary>
    public static string DisplayName { get; private set; }

    /// <summary>
    /// Global event triggered whenever the player updates their name.
    /// Passes the new name as a string.
    /// </summary>
    public static Action<string> onChangeName;

    
    /// <summary>
    /// Called by UI button. Updates the name and notifies listeners.
    /// </summary>
    public void ChangeName()
    {
        // Do nothing if there's no event OR empty text
        if (string.IsNullOrWhiteSpace(nameInput.text))
            return;

        // Set new name
        DisplayName = nameInput.text;

        // Invoke listeners (network scripts, UI updates, etc.)
        onChangeName?.Invoke(DisplayName);

        // Save to PlayerPrefs for persistent storage
        PlayerPrefs.SetString("PlayerName", DisplayName);
        PlayerPrefs.Save();
    }


    private void FixedUpdate()
    {
        // Save "value" to PlayerPrefs every physics tick (if needed)
        PlayerPrefs.SetInt("TransportValue", value);
    }
}



//THIS CODE WAS CLEANED-UP WITH AI