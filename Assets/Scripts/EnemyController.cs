using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent na;
    Animator anm;

    public bool carpisma;

    [SerializeField] GameObject target;
    [SerializeField] GameManager gm;

    void Start()
    {
        carpisma = false;
        na = GetComponent<NavMeshAgent>();
        anm = GetComponent<Animator>();
    }

    void Update()
    {
        if (carpisma)
        {
            na.SetDestination(target.transform.position);
            anm.SetBool("saldir", true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("altkarakterler"))
        {
            GameManager.anlik_karakterSayisi--;
            gm.YokOlmaFxPlay(PozisyonVer(), false, true);
            gameObject.SetActive(false);
        }
    }
    Vector3 PozisyonVer()
    {
        return new Vector3(transform.position.x, 0.23f, transform.position.z);
    }
}
