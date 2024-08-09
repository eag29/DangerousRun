using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Islemler
{
    public class Library : MonoBehaviour
    {
        public static void Toplama(int gelenSayi, List<GameObject> characters, Transform pztsn, List<GameObject> OlusmaFx)
        {
            int sayi = 0;
            foreach (var item in characters)
            {
                if (sayi < gelenSayi)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item2 in OlusmaFx)
                        {
                            if (!item2.activeInHierarchy)
                            {
                                item2.SetActive(true);
                                item2.transform.position = pztsn.position;
                                item2.GetComponent<ParticleSystem>().Play();
                                break;
                            }
                        }
                        item.transform.position = pztsn.transform.position;
                        item.SetActive(true);
                        sayi++;
                    }
                }
                else
                {
                    sayi = 0;
                    break;
                }
            }
            GameManager.anlik_karakterSayisi += gelenSayi;
        }
        public static void Carpma(int gelenSayi, List<GameObject> characters, Transform pztsn, List<GameObject> OlusmaFx)
        {
            int donguSayisi = (GameManager.anlik_karakterSayisi * gelenSayi) - GameManager.anlik_karakterSayisi;

            int sayi2 = 0;
            foreach (var item in characters)
            {
                if (sayi2 < donguSayisi)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item2 in OlusmaFx)
                        {
                            if (!item2.activeInHierarchy)
                            {
                                item2.SetActive(true);
                                item2.transform.position = pztsn.position;
                                item2.GetComponent<ParticleSystem>().Play();
                                break;
                            }
                        }
                        item.transform.position = pztsn.transform.position;
                        item.SetActive(true);
                        sayi2++;
                    }
                }
                else
                {
                    sayi2 = 0;
                    break;
                }
            }
            GameManager.anlik_karakterSayisi *= gelenSayi;
        }
        public static void Cikarma(int gelenSayi, List<GameObject> characters, List<GameObject> YokOlmaFx)
        {

            if (GameManager.anlik_karakterSayisi<gelenSayi)
            {
                foreach (var item in characters)
                {
                    foreach (var item2 in YokOlmaFx)
                    {
                        if (!item2.activeInHierarchy)
                        {
                            Vector3 newpzs = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);

                            item2.SetActive(true);
                            item2.transform.position = newpzs;
                            item2.GetComponent<ParticleSystem>().Play();
                            break;
                        }
                    }
                    if (item.activeInHierarchy)
                    {
                        item.transform.position = Vector3.zero;
                        item.SetActive(false);
                    }
                    GameManager.anlik_karakterSayisi = 1;
                }
            }
            else
            {
                int sayi3 = 0;
                foreach (var item in characters)
                {
                    if (sayi3!=gelenSayi)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var item2 in YokOlmaFx)
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    Vector3 newpzs = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);

                                    item2.SetActive(true);
                                    item2.transform.position = newpzs;
                                    item2.GetComponent<ParticleSystem>().Play();
                                    break;
                                }
                            }
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            sayi3++;
                        }
                    }
                    else
                    {
                        sayi3 = 0;
                        break;
                    }
                }
                GameManager.anlik_karakterSayisi -= gelenSayi;
            }
        }
        public static void Bolme(int gelenSayi, List<GameObject> characters, List<GameObject> YokOlmaFx)
        {

            if (GameManager.anlik_karakterSayisi < gelenSayi)
            {
                foreach (var item in characters)
                {
                    foreach (var item2 in YokOlmaFx)
                    {
                        if (!item2.activeInHierarchy)
                        {
                            Vector3 newpzs = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);

                            item2.SetActive(true);
                            item2.transform.position = newpzs;
                            item2.GetComponent<ParticleSystem>().Play();
                            break;
                        }
                    }
                    if (item.activeInHierarchy)
                    {
                        item.transform.position = Vector3.zero;
                        item.SetActive(false);
                    }
                    GameManager.anlik_karakterSayisi = 1;
                }
            }
            else
            {
                int bolen = GameManager.anlik_karakterSayisi / gelenSayi;

                int sayi4 = 0;
                foreach (var item in characters)
                {
                    if (sayi4 != bolen)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var item2 in YokOlmaFx)
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    Vector3 newpzs = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);

                                    item2.SetActive(true);
                                    item2.transform.position = newpzs;
                                    item2.GetComponent<ParticleSystem>().Play();
                                    break;
                                }
                            }
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            sayi4++;
                        }
                    }
                    else
                    {
                        sayi4 = 0;
                        break;
                    }
                }
                if (GameManager.anlik_karakterSayisi %gelenSayi ==0)
                {
                    GameManager.anlik_karakterSayisi/= gelenSayi;
                }
                else if (GameManager.anlik_karakterSayisi % gelenSayi == 1)
                {
                    GameManager.anlik_karakterSayisi /= gelenSayi;
                    GameManager.anlik_karakterSayisi++;
                }
                else if (GameManager.anlik_karakterSayisi % gelenSayi == 2)
                {
                    GameManager.anlik_karakterSayisi /= gelenSayi;
                    GameManager.anlik_karakterSayisi+=2;
                }
            }
        }
    }
}

