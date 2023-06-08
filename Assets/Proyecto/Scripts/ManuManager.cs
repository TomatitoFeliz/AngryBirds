using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ManuManager : MonoBehaviour
{
    public Slider menuSettingsSlider;
    bool menuOn = false;

    public GameObject muted, information;
    bool musicMuteOn = false;

    public void IniciarNivel()
    {
        SceneManager.LoadScene("NVL-01");
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

    }
}
