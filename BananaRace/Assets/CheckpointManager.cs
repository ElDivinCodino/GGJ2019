using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public void OnTriggerExit(Collider other)
    {
        if(other.transform.parent.gameObject.name.Contains("Banana"))
        {
            other.transform.parent.gameObject.GetComponent<PositionManager>().SetCheckpoint(transform.forward);
        }
    }
}
