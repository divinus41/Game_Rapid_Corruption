using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    /// <summary>
    /// Enable the cursor.
    /// </summary>
    private void Awake()
    {
        // Set cursor visible in the scene.
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Load the level.
    /// </summary>
    public void PlayButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Quit the game.
    /// </summary>
    public void QuitButtonClick()
    {
        Application.Quit();
    }
}
