using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;

    Vector3 targetofset;

    public bool sonKisim;

    [SerializeField] GameObject soncm;

    void Start()
    {
        sonKisim = false;
        targetofset = transform.position - target.position;
    }
    void Update()
    {
        if (!sonKisim)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + targetofset, 0.125f);
        }
        else
        {
            transform.position = new Vector3(soncm.transform.position.x, soncm.transform.position.y, soncm.transform.position.z);
        }
    }
}
