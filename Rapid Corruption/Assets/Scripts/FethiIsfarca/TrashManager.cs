using UnityEngine;
using UnityEngine.SceneManagement;

public class TrashManager : MonoBehaviour
{
    // Value types
    private int instantiateCount = 0;
    private int frames = 0;
    private int seconds = 0;
    public int maxSeconds;

    // Reference types
    public GameObject[] trashPrefabs;
    public GameObject[] spawnPoints;

    // Update is called once per frame
    void Update()
    {
        // Declare variables
        GameObject trash;

        frames++;

        // After 60 frames, increment seconds.
        if (frames >= 60)
        {
            // Increment seconds.
            seconds++;

            // Reset frames.
            frames = 0;

            // After x seconds, instantiate trash in the scene.
            if (seconds >= maxSeconds)
            {
                // Instantiate random trash in random spawn point.
                trash = Instantiate(trashPrefabs[Random.Range(0, trashPrefabs.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].transform);

                // Clone trash in 'TrashManager'.
                transform.parent = trash.transform;

                Debug.Log("Trash spawned!");

                instantiateCount++;

                if (instantiateCount >= 100)
                    SceneManager.LoadScene(4);

                // Reset seconds.
                seconds = 0;
            }
        }
	}
}
