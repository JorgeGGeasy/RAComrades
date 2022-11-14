using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision : MonoBehaviour
{
    public bool pila;
    public GameObject pilaObjeto;
    public bool rueda;
    public GameObject ruedaObjeto;
    public int objetosNecesarios;
    public int objetosConectados;
    private Coroutine limpiar;
    private bool limpiando;

    public Material materialSucio;
    public Material materialMedioSucio;
    public Material materialLimpio;
    private int contador;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "objetoMovible")
        {
            Objeto objeto =  other.gameObject.GetComponent<Objeto>();
            if (objeto.conectar())
            {
                other.gameObject.tag = "conectado";
                cocheListo(objeto.recibirNombre());
                other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                other.gameObject.SetActive(false);
            }
            else if (objeto.limpia())
            {
                Debug.Log("Buen dia");
                limpiar = StartCoroutine(Limpiar());
            }
            
        }
    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "objetoMovible")
        {
            Objeto objeto = other.gameObject.GetComponent<Objeto>();
            if (objeto.limpia())
            {
                if (!limpiando)
                {
                    limpiar = StartCoroutine(Limpiar());
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "objetoMovible")
        {
            Objeto objeto = other.gameObject.GetComponent<Objeto>();
            if (objeto.limpia())
            {
                Debug.Log("Buena tarde");
                StopCoroutine(limpiar);
            }
        }
    }

    public void cocheListo(string objeto)
    {
        if(objeto == "Pila" && pila != true)
        {
            pila = true;
            pilaObjeto.SetActive(true);
            objetosConectados++;
        }
        else if (objeto == "Rueda" && rueda != true)
        {
            rueda = true;
            ruedaObjeto.SetActive(true);
            objetosConectados++;
        }

        if(objetosConectados == objetosNecesarios)
        {
            Debug.Log("Conseguido");
            //Aqui salta a la escena del coche
        }
    }

    IEnumerator Limpiar()
    {
        limpiando = true;
        yield return new WaitForSeconds(5f);
        Debug.Log("Aqui va contador: " + contador);
        contador++;
        switch (contador)
        {
            case 3:
                GetComponent<Renderer>().material = materialLimpio;
                break;
            case 2:
                GetComponent<Renderer>().material = materialMedioSucio;
                break;
            case 1:
                GetComponent<Renderer>().material = materialSucio;
                break;
            default:
                print("Limpieza acabada o por empezar");
                break;
        }
        limpiando = false;
    }
}
