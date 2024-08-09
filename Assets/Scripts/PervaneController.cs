using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PervaneController : MonoBehaviour
{
    Animator anm;

    [SerializeField] float beklemeSuresi;

    [SerializeField] BoxCollider ruzgarAlani;

    void Start()
    {
        anm = GetComponent<Animator>();
    }

    public void AnimasyonDurum(string dondur)
    {
        if (dondur=="evet")
        {
            ruzgarAlani.gameObject.SetActive(true);
            anm.SetBool("calistir", true);
        }
        else
        {
            ruzgarAlani.gameObject.SetActive(false);
            anm.SetBool("calistir", false);
            StartCoroutine(PervaneDondur());
        }
    }
    IEnumerator PervaneDondur()
    {
        yield return new WaitForSeconds(beklemeSuresi);
        AnimasyonDurum("evet");
    }
}
