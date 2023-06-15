using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public float puntuacion;
    public static EndGame instance;
    bool victoria;

    private void Awake()
    {
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

            Debug.Log("FinJuego");
            if (victoria == true)
            {
                Debug.Log("Ganaste");
            }
            else
            {
                Debug.Log("Pediste");
            }
        }
    
    }
}
