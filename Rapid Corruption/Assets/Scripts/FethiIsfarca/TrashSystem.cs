using UnityEngine;

public class TrashSystem : MonoBehaviour
{
    // Value types
    public bool throwTrash;
    public float minThrowFar;
    public float maxThrowFar;

    // Reference types
    private Rigidbody trashSystemRigidbody;

    /// <summary>
    /// Get components from the current scene.
    /// </summary>
    private void Awake()
    {
        // Get rigidbody from the current 'TrashSystem'.
        trashSystemRigidbody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start()
    {
        // Enable gravity and disable trigger.
        trashSystemRigidbody.useGravity = true;
        gameObject.GetComponent<Collider>().isTrigger = false;

        // Throw trash.
        if (throwTrash)
        {
            trashSystemRigidbody.AddForce(Vector3.right * Random.Range(minThrowFar, maxThrowFar));
            trashSystemRigidbody.useGravity = true;

            Debug.Log("Trash throwed!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy trash, out of the scene.
        if (transform.position.y < -10f)
            Destroy(gameObject);
    }

    /// <summary>
    /// By collision with 'Terrain', than disable gravity and set trigger.
    /// </summary>
    /// <param name="collision">Was effected by.</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Terrain"))
        {
            trashSystemRigidbody.useGravity = false;
            gameObject.GetComponent<Collider>().isTrigger = true;

            if (transform.rotation.x != 0f || transform.rotation.y != 0f || transform.rotation.z != 0f)
                Destroy(gameObject);

            trashSystemRigidbody.velocity = Vector3.zero;

            this.GetComponent<TrashSystem>().enabled = false;
        }
    }
}
