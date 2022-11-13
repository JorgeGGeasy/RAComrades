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
        foreach(GameObject objeto in objetos)
        {
            float valorPlano = plano.transform.position.y - 5;
            if (objeto.transform.position.y < valorPlano)
            {
                objeto.transform.position = new Vector3(plano.transform.position.x, plano.transform.position.y + 2, plano.transform.position.z);
            }
        }
    }
}
