using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuscaTargetsMenu : MonoBehaviour
{
    public GameObject canvasTarjetaCoche;
    public GameObject canvasTarjetaManual;
    public GameObject buscaTarjeta;

    // Update is called once per frame
    void Update()
    {
        if (canvasTarjetaCoche.GetComponent<Canvas>().enabled || canvasTarjetaManual.GetComponent<Canvas>().enabled)
        {
            if (buscaTarjeta.activeInHierarchy)
            {
                buscaTarjeta.SetActive(false);
            }

            Debug.Log("Activa");
        }
        else
        {
            if (!buscaTarjeta.activeInHierarchy)
            {
                buscaTarjeta.SetActive(true);
            }

            Debug.Log("Desactivada");
        }
    }
}
