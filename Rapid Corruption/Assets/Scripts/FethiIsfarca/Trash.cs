using UnityEngine;

public class Trash : MonoBehaviour
{
    // Value types
    private bool collisionWithPlayer;

    // Reference types
    private GameObject playerGameObject;

    /// <summary>
    /// Get components from the current scene.
    /// </summary>
    private void Awake()
    {
        // Find the asset from player in the current scene.
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // When press button 'E' or 'Q', than exist don't collision with player.
        if (Input.GetAxisRaw("Fire2") > 0 || Input.GetAxisRaw("Fire2") < 0)
            collisionWithPlayer = false;
    }

    /// <summary>
    /// By collision with player, set the variable true;
    /// </summary>
    /// <param name="collision">Was effected by.</param>
    private void OnTriggerEnter(Collider other)
    {
        collisionWithPlayer = other.CompareTag("Player") ? true : false;
    }

    /// <summary>
    /// By exit collision from player, set the variable false. 
    /// </summary>
    /// <param name="collision">Was effected by.</param>
    private void OnTriggerExit(Collider other)
    {
        collisionWithPlayer = false;
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

        if (collisionWithPlayer)
        {
            // Set the position from player.
            playerPosition = Camera.main.WorldToScreenPoint(playerGameObject.transform.position);

            // Set font for heading and button.
            font = (Font)Resources.Load("Fonts/Screen", typeof(Font));

            style.font = font;
            // Set font size for heading.
            style.fontSize = 20;

            // Heading.
            GUI.Label(new Rect(playerPosition.x - 130, Screen.height - playerPosition.y + 50, 100, 50), "Press 'E' to get garbage", style);
        }
    }
}
