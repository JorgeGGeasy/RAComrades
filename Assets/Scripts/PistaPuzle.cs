using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PistaPuzle : MonoBehaviour
{
    private GameManager gameManager;
    private Coroutine corutinaPista;
    [SerializeField]
    private GameObject botonPista;
    [SerializeField]
    private GameObject quieresPista;
    [SerializeField]
    private GameObject pista;
    [SerializeField]
    private TMP_Text pistaTexto;
    [SerializeField]
    private float tiempoPista;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        corutinaPista = StartCoroutine(ActivarPista());
    }

    public void DarPista()
    {
        quieresPista.SetActive(true);
        botonPista.SetActive(false);
    }

    public void RechazarPista()
    {
        quieresPista.SetActive(false);
        botonPista.SetActive(true);
    }

    public void AceptarPista()
    {
        quieresPista.SetActive(false);
        pista.SetActive(true);
        CrearPista();
    }

    public void Continuar()
    {
        pista.SetActive(false);
        corutinaPista = StartCoroutine(ActivarPista());
    }

    public void CrearPista()
    {
        if (!gameManager.ruedas)
        {
            pistaTexto.text = "Para saber que ruedas son las correctas fijate bien en las tiras";
        }
        else if (!gameManager.baterias)
        {
            pistaTexto.text = "Para conectar bien las baterias haz la combinacion correcta de colores";
        }
        else if (!gameManager.limpieza)
        {
            pistaTexto.text = "Para limpiar el coche pasa una esponja por encima del coche el tiempo suficiente, mantenla hasta que veas que el coche esta limpio";
        }
        else if (!gameManager.luces)
        {
            pistaTexto.text = "Apaga las luces de la sala donde te encuentras, o intenta reducir la luminosidad de la camara";
        }
        else if (!gameManager.radio)
        {
            pistaTexto.text = "Pulsa sobre la radio hasta que veas que las ondas salen correctamente";
        }
    }

    IEnumerator ActivarPista()
    {

        yield return new WaitForSeconds(tiempoPista);
        botonPista.SetActive(true);
    }
}
