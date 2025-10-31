using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Singleton Game Manager for handling global game state,
/// including scene management and player data.
/// </summary>
public class GameManager : MonoBehaviour
{
	// -----------------------------
	// Singleton Instance
	// -----------------------------
	public static GameManager Instance { get; private set; }

	// -----------------------------
	// Fields
	// -----------------------------
	[SerializeField] 
	private string currentScene;   // Tracks the currently loaded scene

	public string playerName;      // Stores the player's name

	// -----------------------------
	// Unity Lifecycle
	// -----------------------------
	private void Awake()
	{
		// Ensure only one GameManager exists across scenes
		if (Instance == null)
		{
			Instance = this;
			// Optional: uncomment to persist across scenes
			// DontDestroyOnLoad(gameObject);
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Init();
	}

	// -----------------------------
	// Initialization
	// -----------------------------
	/// <summary>
	/// Sets up initial state when the game starts.
	/// </summary>
	private void Init()
	{
		// If only one scene is loaded (likely startup),
		// default to the main menu scene
		if (SceneManager.sceneCount == 1)
		{
			SetSceneName("01 Menu");
		}
	}

	// -----------------------------
	// Scene Management
	// -----------------------------
	/// <summary>
	/// Loads a new scene additively and unloads the previous one.
	/// </summary>
	/// <param name="name">Scene name to load.</param>
	public void SetSceneName(string name)
	{
		// Unload previous scene if more than one is loaded
		if (SceneManager.sceneCount > 1 && !string.IsNullOrEmpty(currentScene))
		{
			SceneManager.UnloadSceneAsync(currentScene);
		}

		// Load new scene additively
		SceneManager.LoadScene(name, LoadSceneMode.Additive);
		currentScene = name;
	}

	// -----------------------------
	// Player Data Management
	// -----------------------------
	/// <summary>
	/// Updates the stored player name.
	/// </summary>
	/// <param name="name">New player name.</param>
	public void ChangeName(string name)
	{
		playerName = name;
	}
}