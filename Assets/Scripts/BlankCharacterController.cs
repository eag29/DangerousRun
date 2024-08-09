using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlankCharacterController : MonoBehaviour
{
    NavMeshAgent na;
    Animator anm;

    [SerializeField] SkinnedMeshRenderer smr;
    [SerializeField] Material newMaterial;

    [SerializeField] GameObject target;

    bool yuru;

    void Start()
    {
        yuru = false;
        na = GetComponent<NavMeshAgent>();
        anm = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (yuru)
        {
            na.SetDestination(target.transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("altkarakterler"))
        {
            if (gameObject.CompareTag("blankcharacter"))
            {
                MateryalDegistir();
                yuru = true;
            }
        }
    }
    void MateryalDegistir()
    {
        Material[] mats = smr.materials;
        mats[0] = newMaterial;
        smr.material = newMaterial;

        anm.SetBool("saldir", true);
        gameObject.tag = "altkarakterler";
        GameManager.anlik_karakterSayisi++;
    }
}
