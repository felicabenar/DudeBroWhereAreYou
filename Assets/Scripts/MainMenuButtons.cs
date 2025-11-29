//THIS CODE WAS CLEANED UP WITH CHAT GPT

using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class MainMenuButtons : MonoBehaviour
{
    [Header("Mirror Components")]
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private GameObject PanelStart;
    [SerializeField] private GameObject PanelStop;
    [SerializeField] private InputField inputFieldAddress;

    [Header("UI Buttons")]
    [SerializeField] private Button hostButton;
    [SerializeField] private Button serverButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button stopButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button aboutButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button back1Button;
    [SerializeField] private Button back2Button;

    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject aboutPanel;
    [SerializeField] private GameObject gamePanel;

    private void Start()
    {
        //Update the canvas text if you have manually changed network managers address from the game object before starting the game scene
        if (NetworkManager.singleton.networkAddress != "localhost") { inputFieldAddress.text = NetworkManager.singleton.networkAddress; }
        //Adds a listener to the main input field and invokes a method when the value changes.
        inputFieldAddress.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        
        // Bind button events safely using null-conditional operator
        hostButton.onClick.AddListener(StartHost);
        serverButton.onClick.AddListener(StartServer);
        joinButton.onClick.AddListener(StartClient);
        stopButton.onClick.AddListener(StopServer);
        settingsButton.onClick.AddListener(() => ShowPanel(settingsPanel));
        aboutButton.onClick.AddListener(() => ShowPanel(aboutPanel));
        exitButton.onClick.AddListener(ExitGame);
        back1Button.onClick.AddListener(ShowMainMenu);
        back2Button.onClick.AddListener(ShowMainMenu);

        //This updates the Unity canvas, we have to manually call it every change, unlike legacy OnGUI.
        SetupCanvas();
        ShowMainMenu(); // Ensure correct initial state
    }

    // ------------------------------
    // Network Actions
    // ------------------------------

    // Invoked when the value of the text field changes.
    public void ValueChangeCheck()
    {
        NetworkManager.singleton.networkAddress = inputFieldAddress.text;
    }
    
    /// <summary>Starts the game as a Host (server + client).</summary>
    private void StartHost()
    {
        Debug.Log("Hosting the Game...");
        NetworkManager.singleton.StartHost();
        SetupCanvas();
        HideAllPanels();
    }

    public void StartServer()
    {
        NetworkManager.singleton.StartServer();
        SetupCanvas();
        HideAllPanels();
    }

    /// <summary>Joins the game as a client.</summary>
    private void StartClient()
    {
        Debug.Log("Joining game...");
        networkManager.StartClient();
        SetupCanvas();
    }

    public void StopServer()
    {
        // stop host if host mode
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        }
        // stop client if client-only
        else if (NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopClient();
        }
        // stop server if server-only
        else if (NetworkServer.active)
        {
            NetworkManager.singleton.StopServer();
        }

        SetupCanvas();
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

    private void SetupCanvas()
    {
        // Here we will dump majority of the canvas UI that may be changed.

        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
            if (NetworkClient.active)
            {
                PanelStart.SetActive(false);
                PanelStop.SetActive(true);
            }
            else
            {
                PanelStart.SetActive(true);
                PanelStop.SetActive(false);
            }
        }
        else
        {
            PanelStart.SetActive(false);
            PanelStop.SetActive(true);
        }
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
