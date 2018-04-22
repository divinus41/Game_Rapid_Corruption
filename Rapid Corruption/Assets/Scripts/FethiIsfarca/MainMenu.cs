using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Output the main menu.
    /// </summary>
    private void OnGUI()
    {
        // Declare variables
        GUIStyle style = new GUIStyle();
        Font font;

        // Set font for heading and button.
        font = (Font)Resources.Load("Fonts/Screen", typeof(Font));

        style.font = font;
        // Set font size for heading.
        style.fontSize = 50; 

        // Heading.
        GUI.Label(new Rect((Screen.width / 2 - 420), (Screen.height / 2 - 250), 300, 100), "Rapid corruption", style);

        // Set font size for buttons.
        style.fontSize = 20;

        // Buttons.
        if (GUI.Button(new Rect((Screen.width / 2 - 420), (Screen.height / 2 - 120), 200, 20), "Play", style))
            SceneManager.LoadScene(3);
        else if (GUI.Button(new Rect((Screen.width / 2 - 420), (Screen.height / 2 - 90), 200, 20), "Options", style))
            SceneManager.LoadScene(2);
        else if (GUI.Button(new Rect((Screen.width / 2 - 420), (Screen.height / 2 - 60), 200, 20), "Quit", style))
            Application.Quit();
    }
}
