using UnityEngine;

public class DemoCampsite : MonoBehaviour
{
    // Value types
    private bool trashStorable = false;

    // Reference types
    private DemoPlayer demoPlayerScript;
    private MouseRotation mouseRotationScript;
    private GameObject playerGameObject;
    public Canvas playAgainCanvas;

    /// <summary>
    /// Get the scripts.
    /// </summary>
    private void Awake()
    {
        // Get the 'DemoPlayer'-Script.
        demoPlayerScript = FindObjectOfType<DemoPlayer>();

        // Get the 'MouseRotation'-Script.
        mouseRotationScript = FindObjectOfType<MouseRotation>();
    }

    // Use this for initialization
    void Start()
    {
        // Disable play again canvas.
        playAgainCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // When press button 'E' or 'Q', than exist don't collision with player.
        if (Input.GetAxisRaw("Fire2") > 0 || Input.GetAxisRaw("Fire2") < 0)
            trashStorable = false;

        // When the player have all trashes, than load play again canvas.
        if (Input.GetAxisRaw("Fire2") < 0)
        {
            if (demoPlayerScript.AllTrashes && demoPlayerScript.Backpack <= 0)
            {
                playAgainCanvas.enabled = true;

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                Time.timeScale = 0;
                mouseRotationScript.enabled = false;
            }
        }
    }

    /// <summary>
    /// By trigger with player, set the variable true.
    /// </summary>
    /// <param name="other">Was effected by.</param>
    private void OnTriggerEnter(Collider other)
    {
        trashStorable = other.CompareTag("Player") ? true : false;
    }

    /// <summary>
    /// By trigger with player, set the variable false.
    /// </summary>
    /// <param name="other">Was effected by.</param>
    private void OnTriggerExit(Collider other)
    {
        trashStorable = false;
    }

    /// <summary>
    /// By collision, show what the player can do.
    /// </summary>
    private void OnGUI()
    {
        // Declare variables
        Vector3 playerPosition;
        GUIStyle style = new GUIStyle();
        Font font;

        if (trashStorable)
        {
            // Get the player from the current scene and set his position.
            playerGameObject = GameObject.FindGameObjectWithTag("Player");
            playerPosition = Camera.main.WorldToScreenPoint(playerGameObject.transform.position);

            // Set font for heading and button.
            font = (Font)Resources.Load("Fonts/Screen", typeof(Font));

            style.font = font;
            // Set font size for heading.
            style.fontSize = 20;

            // Heading.
            GUI.Label(new Rect(playerPosition.x - 130, Screen.height - playerPosition.y + 100, 100, 50), "Press 'Q' to lay down the garbages", style);
        }
    }
}
