//THIS CODE WAS CLEANED UP WITH CHAT GPT

using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class MainMenuButtons : MonoBehaviour
{
    [Header("Mirror Components")]
    [SerializeField] private NetworkManager networkManager;

    [Header("UI Buttons")]
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button aboutButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button back1Button;
    [SerializeField] private Button back2Button;

    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject aboutPanel;

    private void Start()
    {
        // Bind button events safely using null-conditional operator
        hostButton?.onClick.AddListener(StartHost);
        joinButton?.onClick.AddListener(StartClient);
        settingsButton?.onClick.AddListener(() => ShowPanel(settingsPanel));
        aboutButton?.onClick.AddListener(() => ShowPanel(aboutPanel));
        exitButton?.onClick.AddListener(ExitGame);
        back1Button?.onClick.AddListener(ShowMainMenu);
        back2Button?.onClick.AddListener(ShowMainMenu);

        ShowMainMenu(); // Ensure correct initial state
    }

    // ------------------------------
    // Network Actions
    // ------------------------------

    /// <summary>Starts the game as a Host (server + client).</summary>
    private void StartHost()
    {
        Debug.Log("Hosting the game...");
        networkManager.StartHost();
        HideAllPanels();
    }

    /// <summary>Joins the game as a client.</summary>
    private void StartClient()
    {
        Debug.Log("Joining game...");
        networkManager.StartClient();
        HideAllPanels();
    }

    // ------------------------------
    // UI Navigation
    // ------------------------------

    /// <summary>
    /// Shows the specified panel and hides all other panels.
    /// </summary>
    private void ShowPanel(GameObject panelToShow)
    {
        mainMenuPanel?.SetActive(panelToShow == mainMenuPanel);
        settingsPanel?.SetActive(panelToShow == settingsPanel);
        aboutPanel?.SetActive(panelToShow == aboutPanel);
    }

    /// <summary>Shows the main menu panel.</summary>
    private void ShowMainMenu() => ShowPanel(mainMenuPanel);

    /// <summary>Hides all menu panels — used after connecting.</summary>
    private void HideAllPanels()
    {
        mainMenuPanel?.SetActive(false);
        settingsPanel?.SetActive(false);
        aboutPanel?.SetActive(false);
    }

    // ------------------------------
    // Exit Application
    // ------------------------------

    /// <summary>Exits game or stops Play Mode inside the editor.</summary>
    private void ExitGame()
    {
        Debug.Log("Exiting...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void Update()
    {
        // Automatically hide menu after connecting or hosting
        if (NetworkClient.isConnected || NetworkServer.active)
            HideAllPanels();
    }
}

//THIS CODE WAS CLEANED UP WITH CHAT GPT
