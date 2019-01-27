using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.gameObject.name.Contains("banana"))
        {
            other.transform.parent.gameObject.GetComponent<PositionManager>().ResetPosition();
        }
    }
}
