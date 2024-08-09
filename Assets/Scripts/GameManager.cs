using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Islemler;

public class GameManager : MonoBehaviour
{
    [Header("OBJECTS")]
    public List<GameObject> characters;
    [SerializeField] GameObject character;
    [SerializeField] List<GameObject> enemies;
    [SerializeField] List<GameObject> OlusmaFx;
    [SerializeField] List<GameObject> yokOlmaFx;
    [SerializeField] List<GameObject> adamLekesiFx;

    [Header("VALUABLES")]
    public static int anlik_karakterSayisi;
    public int dusmanSayisi;
    bool sonkisim;
    bool oyunBittimi;
    [SerializeField] CharacterController cc;
    public AudioSource[] Sounds;

    [Header("CANVAS OBJECTS")]
    [SerializeField] GameObject[] pnls;
    [SerializeField] Image[] buttons;
    public TextMeshProUGUI[] txts;
    public Sprite[] sprites;

    private void Awake()
    {
        SahneIlkIslemler();
    }
    void Start()
    {
        Time.timeScale = 0;
        anlik_karakterSayisi = 1;
        DusmanGetir();
    }
    void SahneIlkIslemler()
    {
        if (PlayerPrefs.GetInt("Sound") == 1 && PlayerPrefs.GetInt("Music") == 1)
        {
            for (int i = 0; i < Sounds.Length; i++)
            {
                Sounds[i].mute = false;
            }
            buttons[1].sprite = sprites[0];
            buttons[2].sprite = sprites[2];
        }
        else if (PlayerPrefs.GetInt("Sound") == 1 && PlayerPrefs.GetInt("Music") == 0)
        {
            Sounds[0].mute = true;
            Sounds[1].mute = false;
            Sounds[2].mute = false;
            Sounds[3].mute = false;

            buttons[1].sprite = sprites[0];
            buttons[2].sprite = sprites[3];
        }
        else if (PlayerPrefs.GetInt("Sound") == 0 && PlayerPrefs.GetInt("Music") == 1)
        {
            Sounds[1].mute = true;
            Sounds[2].mute = true;
            Sounds[3].mute = true;

            buttons[1].sprite = sprites[1];
            buttons[2].sprite = sprites[2];
        }
        else if (PlayerPrefs.GetInt("Sound") == 0 && PlayerPrefs.GetInt("Music") == 0)
        {
            for (int i = 0; i < Sounds.Length; i++)
            {
                Sounds[i].mute = true;
            }
            buttons[1].sprite = sprites[1];
            buttons[2].sprite = sprites[3];
        }

    }
    public void DusmanGetir()
    {
        for (int i = 0; i < dusmanSayisi; i++)
        {
            enemies[i].SetActive(true);
        }
    }
    public void DusmanlariTetikle()
    {
        foreach (var item in enemies)
        {
            if (item.activeInHierarchy)
            {
                item.GetComponent<EnemyController>().carpisma = true;
            }
        }
        sonkisim = true;
        SavasAlani();
    }
    void SavasAlani()
    {
        if (sonkisim)
        {
            if (anlik_karakterSayisi == 1 || dusmanSayisi == 0)
            {
                oyunBittimi = true;
                foreach (var item in enemies)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("saldir", false);
                    }
                }
                foreach (var item in characters)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("saldir", false);
                    }
                }

                character.GetComponent<Animator>().SetBool("saldir", false);

                if (anlik_karakterSayisi < dusmanSayisi || anlik_karakterSayisi == dusmanSayisi)
                {
                    Go();
                }
            }
            else
            {
                if (anlik_karakterSayisi > dusmanSayisi)
                {
                    oyunBittimi = true;
                    foreach (var item in enemies)
                    {
                        if (item.activeInHierarchy)
                        {
                            item.GetComponent<Animator>().SetBool("saldir", false);
                        }
                    }
                    foreach (var item in characters)
                    {
                        if (item.activeInHierarchy)
                        {
                            item.GetComponent<Animator>().SetBool("saldir", false);
                        }
                    }

                    character.GetComponent<Animator>().SetBool("saldir", false);

                    Invoke("Win", 4.5f);
                }
            }
        }
    }
    public void AdamYonetimi(string islem, int gelenSayi, Transform pztsn)
    {
        switch (islem)
        {
            case "toplama":
                Library.Toplama(gelenSayi, characters, pztsn, OlusmaFx);
                Sounds[1].Play();
                break;

            case "carpma":
                Library.Carpma(gelenSayi, characters, pztsn, OlusmaFx);
                Sounds[1].Play();
                break;

            case "cikartma":
                Library.Cikarma(gelenSayi, characters, yokOlmaFx);
                Sounds[2].Play();
                break;

            case "bolme":
                Library.Bolme(gelenSayi, characters, yokOlmaFx);
                Sounds[2].Play();
                break;
        }
    }
    public void YokOlmaFxPlay(Vector3 pztsn, bool balyoz = false, bool durum = false)
    {
        foreach (var item in yokOlmaFx)
        {
            if (!item.activeInHierarchy)
            {
                item.transform.position = pztsn;
                item.SetActive(true);
                item.GetComponent<ParticleSystem>().Play();
                if (!durum)
                {
                    anlik_karakterSayisi--;
                }
                else
                {
                    dusmanSayisi--;
                }
                break;
            }
        }
        if (balyoz)
        {
            Vector3 newpzs = new Vector3(pztsn.x, -0.2f, pztsn.z);

            foreach (var item in adamLekesiFx)
            {
                if (!item.activeInHierarchy)
                {
                    item.transform.position = newpzs;
                    item.SetActive(true);
                    break;
                }
            }
        }
        if (!oyunBittimi)
        {
            SavasAlani();
        }
    }
    void Win()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        pnls[2].SetActive(true);
    }
    void Go()
    {
        pnls[3].SetActive(true);
    }
    public void ButtonSettings(string islem)
    {
        switch (islem)
        {
            case "start":
                Sounds[3].Play();
                pnls[0].SetActive(false);
                Time.timeScale = 1;
                cc.gamestart = true;
                buttons[0].gameObject.SetActive(true);
                txts[0].gameObject.SetActive(true);
                txts[0].text = SceneManager.GetActiveScene().name;
                break;
            case "paused":
                Sounds[3].Play();
                Time.timeScale = 0;
                pnls[1].SetActive(true);
                buttons[0].gameObject.SetActive(false);
                break;
            case "esc":
                Sounds[3].Play();
                Time.timeScale = 1;
                pnls[1].SetActive(false);
                buttons[0].gameObject.SetActive(true);
                break;
            case "retry":
                Sounds[3].Play();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "exitpanel":
                Sounds[3].Play();
                pnls[4].SetActive(true);
                break;
            case "yes":
                Sounds[3].Play();
                Application.Quit();
                break;
            case "no":
                Sounds[3].Play();
                pnls[4].SetActive(false);
                break;
            case "nextlevel":
                Sounds[3].Play();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case "music":
                if (PlayerPrefs.GetInt("Music") == 1)
                {
                    PlayerPrefs.SetInt("Music", 0);
                    Sounds[3].Play();

                    Sounds[0].mute = true;
                    buttons[2].sprite = sprites[3];
                }

                else if (PlayerPrefs.GetInt("Music") == 0)
                {
                    PlayerPrefs.SetInt("Music", 1);
                    Sounds[3].Play();

                    Sounds[0].mute = false;
                    buttons[2].sprite = sprites[2];
                }
                break;

            case "sound":
                if (PlayerPrefs.GetInt("Sound") == 1)
                {
                    PlayerPrefs.SetInt("Sound", 0);

                    Sounds[1].mute = true;
                    Sounds[2].mute = true;
                    Sounds[3].mute = true;
                    Sounds[4].mute = true;

                    buttons[1].sprite = sprites[1];
                }

                else if (PlayerPrefs.GetInt("Sound") == 0)
                {
                    PlayerPrefs.SetInt("Sound", 1);
                    Sounds[3].Play();

                    Sounds[1].mute = false;
                    Sounds[2].mute = false;
                    Sounds[3].mute = false;
                    Sounds[4].mute = false;
                    buttons[1].sprite = sprites[0];
                }
                break;
        }
    }
}
