using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuscaTargets : MonoBehaviour
{

    public GameObject canvasTarjeta;
    public GameObject buscaTarjeta;
    // Update is called once per frame
    void Update()
    {
        if (canvasTarjeta.GetComponent<Canvas>().enabled)
        {
            if (buscaTarjeta.activeInHierarchy)
            {
                buscaTarjeta.SetActive(false);
            }

        }
        else
        {
            if (!buscaTarjeta.activeInHierarchy)
            {
                buscaTarjeta.SetActive(true);
            }
        }
    }
}
