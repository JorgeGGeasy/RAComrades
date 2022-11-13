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
}
