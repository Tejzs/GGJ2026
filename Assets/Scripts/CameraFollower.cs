using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float FollowSpeed = 2f;
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + 2.5f, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}
