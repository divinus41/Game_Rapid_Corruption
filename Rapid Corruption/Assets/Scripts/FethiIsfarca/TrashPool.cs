using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrashPool : MonoBehaviour
{
    // Value types
    private int trash;

    // Use this for initialization
    void Start()
    {
        // Init the child from the pool.
        trash = 0;

        // Start the coroutine for activation trashes.
        StartCoroutine(TrashActivator());
	}

    /// <summary>
    /// Have you trash in the pool, than activate after ten seconds, else load a lose screen.
    /// </summary>
    /// <returns></returns>
    private IEnumerator TrashActivator()
    {
        // Wait for ten seconds.
        yield return new WaitForSeconds(10);

        // Have you trash in the pool, than activate after ten seconds, else load a lose screen.
        try
        {
            gameObject.transform.GetChild(trash).gameObject.SetActive(true);
            trash++;
        }
        catch
        {
            SceneManager.LoadScene(2); // Load lose screen.
        }

        // Stop the current coroutine and start the new coroutine.
        StopAllCoroutines();
        StartCoroutine(TrashActivator());
    }
}