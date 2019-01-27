using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent)
        {
            if (other.transform.parent.gameObject.GetComponent<BananaMovement>())
            {
                other.transform.parent.gameObject.GetComponent<PositionManager>().ResetPosition();
            }
        }
    }
}
