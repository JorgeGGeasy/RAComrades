using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilidades : MonoBehaviour
{
    public List<GameObject> objetos;
    public GameObject plano;
    public float valor;
    public int numero;

    public List<GameObject> dameObjetos()
    {
        return objetos;
    }

    public float dameValor()
    {
        return valor;
    }

    public int dameNumeroDedos()
    {
        return numero;
    }

    private void Update()
    {

        if (plano.gameObject.GetComponent<BoxCollider>().enabled == false){
            foreach(GameObject objeto in objetos)
        {
            objeto.GetComponent<Rigidbody>().isKinematic = true;
        }
        }

        foreach(GameObject objeto in objetos)
        {
            float valorPlano = plano.transform.position.y - 1;
            if (objeto.transform.position.y < valorPlano)
            {
                objeto.GetComponent<Rigidbody>().isKinematic = true;
                objeto.transform.position = new Vector3(plano.transform.position.x, plano.transform.position.y + 1, plano.transform.position.z);
                objeto.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    public void radioPulsada(GameObject radio)
    {
        float canal = radio.GetComponent<Rigidbody>().mass;

        canal = canal + 1;
        if(canal == 4)
        {
            canal = 1;
        }

        radio.GetComponent<Rigidbody>().mass = canal;
    }
}
