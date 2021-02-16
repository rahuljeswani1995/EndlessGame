using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // target represents the player
    public Transform target;
    // represents the amount that the player needs to be moved by
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        // determine the difference between our player and camera
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    // use lateupdate instead of update so that camera is updated after player updates
    void LateUpdate()
    {
        // only have z of the offset for forward motion of the camera
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, target.position.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, 10* Time.deltaTime);
    }
}
