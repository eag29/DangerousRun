using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] CameraController cc;

    bool dokunma_acik;
    public bool gamestart;

    [SerializeField] GameObject ortapztsn;

    void Start()
    {
        dokunma_acik = true;
        gamestart = false;
        if (dokunma_acik && gamestart)
        {
            transform.Translate(transform.forward * 5f * Time.deltaTime);
        }
    }
    void Update()
    {
        if (dokunma_acik)
        {
            if (Input.touchCount > 0)
            {
                Touch tch = Input.GetTouch(0);

                if (tch.phase == TouchPhase.Moved)
                {
                    gm.txts[0].gameObject.SetActive(false);

                    float touchX = tch.position.x / Screen.width;

                    if (touchX < 0.5f)
                    {
                        transform.position += Vector3.left * 0.05f;
                    }
                    else
                    {
                        transform.position += Vector3.right * 0.05f;
                    }
                }
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(ortapztsn.transform.position.x, ortapztsn.transform.position.y, ortapztsn.transform.position.z), 0.125f);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("ýgne") || collision.gameObject.CompareTag("direk"))
        {
            if (transform.position.x > 0)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z), 0.125f);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - 0.2f, transform.position.y, transform.position.z), 0.125f);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "toplama" || other.tag == "carpma" || other.tag == "cikartma" || other.tag == "bolme")
        {
            int sayi = int.Parse(other.name);
            gm.AdamYonetimi(other.tag, sayi, other.transform);
        }
        else if (other.tag == "sontetikleyici")
        {
            dokunma_acik = false;
            cc.sonKisim = true;
            gm.DusmanlariTetikle();
        }
        else if (other.CompareTag("blankcharacter"))
        {
            gm.characters.Add(other.gameObject);
            gm.Sounds[1].Play();
        }
    }
}
