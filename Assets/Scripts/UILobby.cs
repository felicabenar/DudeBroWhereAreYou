//THIS CODE WAS CLEANED-UP WITH AI 



using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UILobby : MonoBehaviour
{
    [SerializeField] InputField joinMatchInput;   // Input field where player enters match code/IP
    [SerializeField] Button joinButton;           // Button for joining a match
    [SerializeField] Button hostButton;           // Button for hosting a match
    [SerializeField] Button quitButton;           // Button for quitting the game

    public void Host()
    {
        // Disable UI elements to prevent further interaction while hosting
        joinMatchInput.interactable = false;
        joinButton.interactable = false;
        hostButton.interactable = false;
    }

    public void Join()
    {
        // Disable UI elements to prevent changing input while joining
        joinMatchInput.interactable = false;
        joinButton.interactable = false;
        hostButton.interactable = false;
    }

    public void Quit()
    {
        // Close the game application
        Application.Quit();
    }
}




//THIS CODE WAS CLEANED-UP WITH AI 