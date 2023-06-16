using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    public float puntuacion;
    public static EndGame instance;
    bool victoria;

    [SerializeField]
    GameObject hudCanvas, endCanvas, victoriaCanvas, derrotaCanvas;

    [SerializeField]
    TextMeshProUGUI puntuacionTxt;
    public int arregloPuntuacion;

    private void Awake()
    {
        Time.timeScale = 1f;
        hudCanvas.SetActive(true);
        endCanvas.SetActive(false);
        victoriaCanvas.SetActive(false);
        derrotaCanvas.SetActive(false);
        puntuacion = -arregloPuntuacion;

        if (instance == null)
            instance = this;
    }
    private void Update()
    {
        //Debug.Log(puntuacion);
        if (GameObject.FindGameObjectWithTag("CerdoNormal") == null && GameObject.FindGameObjectWithTag("CerdoTanke") == null)
        {
            victoria = true;
            Debug.Log("CerdosEliminados");
            StartCoroutine(Finalizar());
        }
        else if ((GameObject.FindGameObjectWithTag("CerdoNormal") != null || GameObject.FindGameObjectWithTag("CerdoTanke") != null) && GameObject.FindGameObjectsWithTag("Rojo").Length == 0 && GameObject.FindGameObjectsWithTag("Blanco").Length == 0)
        {  
            victoria = false;
            Debug.Log("No más Disparos");
            StartCoroutine(Finalizar());
        }

        IEnumerator Finalizar()
        {
            yield return new WaitForSeconds(4);
            hudCanvas.SetActive(false);
            endCanvas.SetActive(true);
            puntuacionTxt.text = ("PUNTUACION: " + puntuacion);

            Debug.Log("FinJuego");
            if (victoria == true)
            {
                Time.timeScale = 0f;
                float rojos = GameObject.FindGameObjectsWithTag("Rojo").Length;
                float blancos = GameObject.FindGameObjectsWithTag("Blanco").Length;
                if (GameObject.FindGameObjectWithTag("Rojo") != null && GameObject.FindGameObjectWithTag("Blanco") != null)
                {
                    puntuacion = puntuacion + (rojos * 10000) + (blancos * 10000);
                }

                victoriaCanvas.SetActive(true);
                derrotaCanvas.SetActive(false);
                Debug.Log("Ganaste");
                GuardarDatos(true);
                PlayerPrefs.SetFloat("Puntuacion", puntuacion);
            }
            else
            {
                Time.timeScale = 0f;
                victoriaCanvas.SetActive(false);
                derrotaCanvas.SetActive(true);
                Debug.Log("Pediste");
            }
        }
    
    }

    void GuardarDatos(bool hasWon)
    {

        string keyLevel = "PuntuacionNivel" + ManuManager.instance.nivel.ToString();


        if (PlayerPrefs.GetFloat(keyLevel, 0.0f) < puntuacion)
        {
            PlayerPrefs.SetFloat(keyLevel, puntuacion);
        }
        if (hasWon == true)
        {
            PlayerPrefs.SetInt("NivelSuperado" + ManuManager.instance.nivel.ToString(), 1);
        }
        PlayerPrefs.Save();

    }
}
