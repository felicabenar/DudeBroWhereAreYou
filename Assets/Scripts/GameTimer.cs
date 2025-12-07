//THIS CODE WAS CLEANED-UP WITH AI


using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

/// <summary>
/// Handles the round timer, win/lose UI events, and network synchronization.
/// Only the server counts down the timer; clients receive updates via SyncVars & RPCs.
/// </summary>
public class GameTimer : NetworkBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float gameTime = 300f;   // Total duration of the game (in seconds)

    [SyncVar(hook = nameof(OnTimeLeftChanged))]
    private float timeLeft;                           // Synced timer value

    [SyncVar]
    private bool gameActive = false;                  // True while the game is running

    [Header("UI References")]
    public Text timerDisplay;
    public GameObject winScreen;
    public GameObject loseScreen;

    [Header("UI Behavior")]
    public float uiDisplayDuration = 5f;              // Time to show win/lose UI

    public static GameTimer Instance { get; private set; }

    private Coroutine hideUICoroutine;


    // ---------------------------------------------------------------------
    // Initialization
    // ---------------------------------------------------------------------

    private void Awake()
    {
        // Simple singleton instance (only one allowed)
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Start timer automatically on load
        timeLeft = gameTime;
        gameActive = true;
        ResetUI();
        UpdateDisplay();
    }


    // ---------------------------------------------------------------------
    // Server Logic (Timer Countdown)
    // ---------------------------------------------------------------------

    private void FixedUpdate()
    {
        // Timer only runs on server
        if (!isServer || !gameActive) return;

        timeLeft -= Time.deltaTime;

        // Timer ran out
        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            gameActive = false;
            RpcLose();
        }
    }


    // ---------------------------------------------------------------------
    // SyncVar Hooks & Display Updates
    // ---------------------------------------------------------------------

    /// <summary>
    /// Called on clients when timeLeft changes; refreshes the UI display.
    /// </summary>
    private void OnTimeLeftChanged(float oldValue, float newValue) => UpdateDisplay();

    /// <summary>
    /// Formats timeLeft and updates the timer UI text.
    /// </summary>
    private void UpdateDisplay()
    {
        if (!timerDisplay) return;

        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);

        timerDisplay.text = $"{minutes:00}:{seconds:00}";
    }


    // ---------------------------------------------------------------------
    // Public Server API (Game Flow)
    // ---------------------------------------------------------------------

    /// <summary>
    /// Resets and starts a new game round (server only).
    /// </summary>
    [Server]
    public void StartGame()
    {
        timeLeft = gameTime;
        gameActive = true;
        RpcResetUI();
    }

    /// <summary>
    /// Called when a player is tagged; ends game early with a win state.
    /// </summary>
    [Server]
    public void PlayerTagged()
    {
        if (!gameActive) return;

        gameActive = false;
        RpcWin();
    }


    // ---------------------------------------------------------------------
    // RPCs (UI Control on Clients)
    // ---------------------------------------------------------------------

    [ClientRpc] 
    private void RpcWin()
    {
        ShowUI(winScreen, loseScreen);
        StartHideUICoroutine();
    }

    [ClientRpc]
    private void RpcLose()
    {
        ShowUI(loseScreen, winScreen);
        StartHideUICoroutine();
    }

    [ClientRpc]
    private void RpcResetUI() => ResetUI();


    // ---------------------------------------------------------------------
    // UI Helpers
    // ---------------------------------------------------------------------

    /// <summary>
    /// Enables one UI object and disables another.
    /// </summary>
    private void ShowUI(GameObject show, GameObject hide)
    {
        if (hide) hide.SetActive(false);
        if (show) show.SetActive(true);
    }

    /// <summary>
    /// Hides all UI and resets the timer display.
    /// </summary>
    private void ResetUI()
    {
        StopHideUICoroutine();

        if (winScreen) winScreen.SetActive(false);
        if (loseScreen) loseScreen.SetActive(false);

        UpdateDisplay();
    }

    /// <summary>
    /// Starts the coroutine that hides UI after a delay.
    /// </summary>
    private void StartHideUICoroutine()
    {
        StopHideUICoroutine();
        hideUICoroutine = StartCoroutine(HideUIAfterDelay(uiDisplayDuration));
    }

    private IEnumerator HideUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (winScreen) winScreen.SetActive(false);
        if (loseScreen) loseScreen.SetActive(false);

        hideUICoroutine = null;
    }

    /// <summary>
    /// Safely stops the current hide-UI coroutine if running.
    /// </summary>
    private void StopHideUICoroutine()
    {
        if (hideUICoroutine == null) return;

        StopCoroutine(hideUICoroutine);
        hideUICoroutine = null;
    }
}



//THIS CODE WAS CLEANED-UP WITH AI
