using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ManuManager : MonoBehaviour
{
    public Slider menuSettingsSlider;
    bool menuOn = false;

    public GameObject muted, information;
    public AudioMixer audioMixer;
    bool musicMuteOn = false;

    public float nivel;

    public static ManuManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Start()
    {
        //revisar interfaZ...
        MusicVolume();

    }
    public void IniciarNivel()
    {
        SceneManager.LoadScene("MenuNiveles");
    }

    public void SalirJuego()
    {
        Debug.Log("SalirJuego");
        Application.Quit();
    }

    public void MenuSettings()
    {
        if (menuOn == false)
        {
            LeanTween.value(gameObject, 0, 1, 0.5f).setEase(LeanTweenType.linear).setOnUpdate((float val) =>
            {
                menuSettingsSlider.value = val;
            });
            menuOn = true;
        }
        else if (menuOn == true)
        {
            LeanTween.value(gameObject, 1, 0, 0.5f).setEase(LeanTweenType.linear).setOnUpdate((float val) =>
            {
                menuSettingsSlider.value = val;
            });
            menuOn = false;
        }
    }

    public void MenuInformationOn()
    {
        LeanTween.moveLocalX(information, 0, 0.7f).setEaseInBack();
    }
    public void MenuInformationOff()
    {
        LeanTween.moveLocalX(information, -2000, 2f).setEaseInBack();
    }
    
    public void MuteMusic()
    {
        if (musicMuteOn == false)
        {
            audioMixer.SetFloat("Musica", -80f);
            MusicVolume();
           
        }
        else if (musicMuteOn == true)
        {
            audioMixer.SetFloat("Musica", 0);
            MusicVolume();
     
        }
    }

    private void MusicVolume()
    {
        float db;
        audioMixer.GetFloat("Musica",out db);
        Debug.Log(db);
        if (db ==-80f)
        {
            muted.SetActive(true);
            musicMuteOn = true;
        }
        else if (db == 0)
        {
            muted.SetActive(false);
            musicMuteOn = false;
        }
    }

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene("NVL-0" + nivel.ToString());
    }

    public void SiguienteNivel()
    {
        SceneManager.LoadScene("NVL-0" + (nivel + 1).ToString());
    }

}
