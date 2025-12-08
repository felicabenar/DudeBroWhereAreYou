//THIS CODE WAS CLEANED UP WITH AI



using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    // Panel where the player enters their name
    [SerializeField] public GameObject namePanel;       

    // Panel that appears when the game is paused
    [SerializeField] public GameObject pausePanel; 

    // Button that opens the pause menu
    [SerializeField] Button pauseButton;   

    // Button used to resume gameplay
    [SerializeField] Button resumeButton;  

    // Button used to confirm name input
    [SerializeField] Button setButton;  

    public void Start()
    {
        // Show name input panel at the start of the game
        namePanel.SetActive(true);
    }

    public void SetName()
    {
        // If the set button exists, hide the name input panel
        if (setButton)
        {
            namePanel.SetActive(false);
        }
    }

    public void PauseGame()
    {
        // Show pause menu panel
        pausePanel.SetActive(true);

        // Ensure resume button is clickable
        resumeButton.interactable = true;

        // Potential pause state placeholder (currently unused)
        //isPaused = true;
    }

    public void ResumeGame()
    {
        // Hide pause menu panel
        pausePanel.SetActive(false);

        // Ensure pause button is clickable again
        pauseButton.interactable = true;

        // Potential pause state placeholder (currently unused)
        //isPaused = false;
    }
}




//THIS CODE WAS CLEANED UP WITH AI