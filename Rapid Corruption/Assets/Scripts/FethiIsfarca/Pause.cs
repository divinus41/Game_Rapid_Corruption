using UnityEngine;

public class Pause : MonoBehaviour
{
    // Value types
    private bool paused;

    // Reference types
    private MouseRotation mouseRotationScript;
    public Canvas pauseCanvas;

    /// <summary>
    /// Get the scripts
    /// </summary>
    private void Awake()
    {
        // Get the mouse rotation script.
        mouseRotationScript = FindObjectOfType<MouseRotation>();
    }

    // Use this for initialization
    void Start()
    {
        // Don't in the pause.
        paused = false;
	}
	
	// Update is called once per frame
	void Update()
    {
        // Enable/Disable the pause state in game.
        if (Input.GetKeyDown(KeyCode.P))
        {
            mouseRotationScript.enabled = mouseRotationScript.enabled == true ? false : true;
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
            paused = paused == true ? false : true;
        }
        pauseCanvas.enabled = paused;
    }

    /// <summary>
    /// Get/Set the pause in game.
    /// </summary>
    public bool Paused
    {
        get { return paused; }
        set { paused = value; }
    }
}
