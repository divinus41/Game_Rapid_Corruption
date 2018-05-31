using UnityEngine;

public class DemoPause : MonoBehaviour
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
        paused = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Enable/Disable the pause state in game.
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = paused == true ? false : true;
        }

        if (paused)
        {
            Time.timeScale = 0;
            pauseCanvas.enabled = true;
            mouseRotationScript.enabled = false;
        }
        else
        {
            Time.timeScale = 1;
            pauseCanvas.enabled = false;
            mouseRotationScript.enabled = true;
        }
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