using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Disparo : MonoBehaviour
{
    private Camera camara;
    private Rigidbody2D bolaRigidbody;
    private SpringJoint2D bolaSprintJoint;

    //public List<GameObject> pajaros;
    
    private GameObject bola;
    public GameObject pivoteGameObject;
    public Rigidbody2D pivote;
    public float tiempoQuitarSprintJoin;
    public float tiempoFinJuego;

    private Vector2 pPivote;
    private Vector2 diferencia;
    private Vector2 pInicial;

    private bool estaArrastrando = false;

    private LineRenderer bolaLineRenderer;
    private GameObject canasto;
    private CircleCollider2D collider2D;

    //private GameObject cerdoInstanciado;

    public int nPajaro;

    public GameObject[] canastos;

    void Start()
    {
        canasto = GameObject.Find("Canasto");
        bola = GameObject.Find("pajaro");

        canasto.SetActive(false);

        bolaLineRenderer = bola.GetComponent<LineRenderer>();
        bolaLineRenderer.enabled = false;

        pInicial = bola.transform.position;
        pPivote = pivoteGameObject.transform.position;

        collider2D = bola.GetComponent<CircleCollider2D>();

        bolaSprintJoint = bola.GetComponent<SpringJoint2D>();        
        bolaSprintJoint.connectedBody = pivote;

        camara = Camera.main;

        bolaRigidbody = bola.GetComponent<Rigidbody2D>();

       /*cerdoInstanciado =  Instantiate(Resources.Load("Cerdo") as GameObject, new Vector3(0, 0,0), Quaternion.identity);

        cerdoInstanciado.gameObject.name = "CerdoInstanciado";*/
    }

    void Update()
    {
        bolaLineRenderer.SetPosition(1, canasto.transform.position);

        //Si no tiene asociado rigidbody nos salimos:
        if (bolaRigidbody == null) { return;  }

       if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
          
            if (estaArrastrando)
            {
                LanzarBola();
            }
        
            estaArrastrando = false;

            return;
        }

        //Tomar control de la bola:
        Vector2 posicionTocar = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector2 posicionMundo = camara.ScreenToWorldPoint(posicionTocar);
        diferencia = (posicionMundo - pPivote);

        if (diferencia.x < 4 && diferencia.x > -4 && diferencia.y < 4 && diferencia.y > -4)
        {
            canasto.SetActive(true);
            bolaLineRenderer.enabled = true;

            bolaSprintJoint.enabled = true;
            estaArrastrando = true;
            bolaRigidbody.position = posicionMundo;
        }
        else
        {
            canasto.SetActive(false);
            bolaLineRenderer.enabled = false;

            bolaRigidbody.position = pInicial;
            bolaSprintJoint.enabled = false;
            estaArrastrando = false;
        }


        Debug.Log(posicionMundo);
    }

    private void LanzarBola()
    {
        canasto.SetActive(false);
        bolaLineRenderer.enabled = false;

        //La bola reacciona a la física dinámica:
        bolaRigidbody.isKinematic = false;
        bolaRigidbody = null;

        //retraso para desactivar el SprintJoin:
        Invoke(nameof(QuitarSprintJoin), tiempoQuitarSprintJoin);
    }

    private void QuitarSprintJoin()
    {
        collider2D.enabled = true;
        bolaSprintJoint.enabled = false;
        bolaSprintJoint = null;

        Invoke(nameof(FinJuego), tiempoFinJuego);
    }

    private void FinJuego()
    {
        Destroy(canasto);
        nPajaro++;
        Debug.Log("SiguienteTiro");
        // instancias
        if (GameObject.Find("pajaro" + nPajaro.ToString())  != null)
        {
            canastos[nPajaro - 1].SetActive(true);
            canasto = GameObject.Find("Canasto" + nPajaro.ToString());
            canasto.SetActive(false);
            bola = GameObject.Find("pajaro" + nPajaro.ToString());
            bolaLineRenderer = bola.GetComponent<LineRenderer>();
            collider2D = bola.GetComponent<CircleCollider2D>();
            bolaRigidbody = bola.GetComponent<Rigidbody2D>();
            bolaSprintJoint = bola.GetComponent<SpringJoint2D>();
            bolaSprintJoint.connectedBody = pivote;
        }

    }
}
