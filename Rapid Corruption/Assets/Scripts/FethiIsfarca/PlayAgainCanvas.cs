using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainCanvas : MonoBehaviour
{
    /// <summary>
    /// Reload the demo.
    /// </summary>
    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
}