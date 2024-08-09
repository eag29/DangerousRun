using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AltCharacterController : MonoBehaviour
{
    NavMeshAgent na;

    [SerializeField] Transform target;
    [SerializeField] GameManager gm;

    void Start()
    {
        na = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        na.SetDestination(target.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ýgne") || other.CompareTag("balyoz"))
        {
            gameObject.SetActive(false);
            gm.YokOlmaFxPlay(PozisyonVer(),true);
        }
        else if (other.CompareTag("enemy"))
        {
            gm.YokOlmaFxPlay(PozisyonVer(), false, false);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("blankcharacter"))
        {
            gm.characters.Add(other.gameObject);
        }
    }
    Vector3 PozisyonVer()
    {
        return new Vector3(transform.position.x, 0.23f, transform.position.z);
    }
}
