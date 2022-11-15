using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AyudaInicial : MonoBehaviour
{
     [SerializeField]
    private GameObject panelAyudaInicial;
     [SerializeField]
    private TMP_Text textoAyuda;
    [SerializeField]
    private string stringAyuda;


    // hecho de esta forma para que en el manual se lanze en el onTargetFound del imageTarget
    public void IniciarScriptAyudaInicial(){
        textoAyuda.text = stringAyuda;
        StartCoroutine(RutinaAyudaInicial());
    }

    IEnumerator RutinaAyudaInicial(){
        // esperar 1 segundo
         yield return new WaitForSeconds(1.0f);
        
        // mostrar
        panelAyudaInicial.SetActive(true);

        // esperar 5 segundos
        yield return new WaitForSeconds(8.0f);

        // eliminar panel
        panelAyudaInicial.SetActive(false);
    }
}
