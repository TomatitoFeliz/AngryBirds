using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuntuacionNVL : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI pLvl1, pLvl2, pLvl3;
    private void Update()
    {
        pLvl1.text = PlayerPrefs.GetFloat("PuntuacionNivel1").ToString();
        pLvl2.text = PlayerPrefs.GetFloat("PuntuacionNivel2").ToString();
        pLvl3.text = PlayerPrefs.GetFloat("PuntuacionNivel3").ToString();
    }
}
