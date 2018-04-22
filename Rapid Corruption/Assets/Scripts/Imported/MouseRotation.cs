using UnityEngine;

public class MouseRotation : MonoBehaviour
{

    public float turnSpeed = 4.0f;
    public Transform player;

    private Vector3 offset;

    void Start()
    {
        Cursor.visible = false;

        offset = new Vector3(player.position.x, player.position.y + 200f, player.position.z);
    }

    void LateUpdate()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        transform.position = player.position + offset / 64;
        transform.LookAt(player.position);
    }
}
