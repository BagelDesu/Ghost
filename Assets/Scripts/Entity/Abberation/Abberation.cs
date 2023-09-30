using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abberation : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Abberations;
    private int currentAbberation = 0;
    [SerializeField]
    private AudioSource[] AbberationAudio;

    public void AdvanceAbberation()
    {

        if (currentAbberation < Abberations.Length)
        {
            if (currentAbberation > 0)
            {
                Abberations[currentAbberation - 1].SetActive(false);
            }
            Abberations[currentAbberation].SetActive(true);
        }

        switch (currentAbberation)
        {
            case 0:
                AbberationAudio[0].Play();
                break;
            case 1:
                AbberationAudio[0].Stop();
                AbberationAudio[1].Play();
                break;
            case 2:
                AbberationAudio[2].Play();
                break;
            case 3:
                AbberationAudio[3].Play();
                break;
            default:
                break;
        }


        if (currentAbberation >= Abberations.Length)
        {
            GameManager.Instance.ExitGame();
        }

        currentAbberation++;
    }


    public void HideAllAbberations()
    {
        foreach (GameObject item in Abberations)
        {
            item.SetActive(false);
        }
        foreach (AudioSource item in AbberationAudio)
        {
            item.Stop();
        }
    }

}
