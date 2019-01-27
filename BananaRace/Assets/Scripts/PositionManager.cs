using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    Vector3 lastPosition;
    Vector3 lastRotation;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        lastRotation = transform.forward;
    }

    public void SetCheckpoint(Vector3 fwd)
    {
        lastPosition = transform.position + new Vector3(0,1,0);
        lastRotation = fwd;
    }

    public void ResetPosition()
    {
        transform.position = lastPosition;
        transform.forward = lastRotation;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
