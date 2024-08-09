using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuzgarController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("altkarakterler"))
        {
            other.GetComponent<Rigidbody>().AddForce(new Vector3(-5,0,0),ForceMode.Impulse);
        }
    }
}
