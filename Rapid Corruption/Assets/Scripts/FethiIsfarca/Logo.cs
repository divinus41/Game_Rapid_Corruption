using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour
{
    // Value types
    private int frames;
    private int seconds;
	
	// Update is called once per frame
	void Update()
    {
        frames++;

        if (frames > 60)
        {
            frames = 0;

            seconds++;
        }

        if (seconds > 25 || Input.GetAxis("HUD") > 0)
            SceneManager.LoadScene(1);
	}

    private void OnGUI()
    {
        // Declare variables
        GUIStyle style = new GUIStyle();
        Font font;

        // Set font for heading and button.
        font = (Font)Resources.Load("Fonts/Screen", typeof(Font));

        style.font = font;
        // Set font size for heading.
        style.fontSize = 20;

        if (seconds > 5)
            GUI.Label(new Rect(Screen.height / 2, Screen.width / 2, 200, 50), "Press 'Space' to skip", style);
    }
}
