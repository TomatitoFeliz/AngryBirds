using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public float puntuacion;
    public static EndGame instance;
    bool victoria;

    [SerializeField]
    GameObject hudCanvas, endCanvas, victoriaCanvas, derrotaCanvas;

    private void Awake()
    {
        hudCanvas.SetActive(true);
        endCanvas.SetActive(false);
        victoriaCanvas.SetActive(false);
        derrotaCanvas.SetActive(false);

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
        else if (GameObject.FindGameObjectWithTag("CerdoNormal") != null && GameObject.FindGameObjectWithTag("CerdoTanke") != null && GameObject.FindGameObjectsWithTag("Rojo").Length == 0 && GameObject.FindGameObjectsWithTag("Blanco").Length == 0)
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

            Debug.Log("FinJuego");
            if (victoria == true)
            {
                victoriaCanvas.SetActive(true);
                derrotaCanvas.SetActive(false);
                Debug.Log("Ganaste");
            }
            else
            {
                victoriaCanvas.SetActive(false);
                derrotaCanvas.SetActive(true);
                Debug.Log("Pediste");
            }
        }
    
    }
}
