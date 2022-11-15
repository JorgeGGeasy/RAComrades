using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool baterias;
    public bool ruedas;
    public bool limpieza;
    public bool radio;
    public bool luces;

    private AudioClipManager audioClipManager;

    public static GameManager Instance; // A static reference to the GameManager instance

    void Awake()
    {
        audioClipManager = FindObjectOfType<AudioClipManager>();
        if (Instance == null) // If there is no instance already
        {
            DontDestroyOnLoad(gameObject); // Keep the GameObject, this component is attached to, across different scenes
            Instance = this;
        }
        else if (Instance != this) // If there is already an instance and it's not this instance
        {
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
        }
    }

    public void ResolverBaterias()
    {
        baterias = true;
        audioClipManager.SeleccionarAudio(2,0.5f);
    }

    public void ResolverRuedas()
    {
        ruedas = true;
        ResolverPuzle();
        audioClipManager.SeleccionarAudio(2, 0.5f);
    }

    public void ResolverLimpieza()
    {
        limpieza = true;
        ResolverPuzle();
        audioClipManager.SeleccionarAudio(2, 0.5f);
    }

    public void ResolverRadio()
    {
        radio = true;
        ResolverPuzle();
        audioClipManager.SeleccionarAudio(2, 0.5f);
    }

    public void ResolverLuces()
    {
        luces = true;
        ResolverPuzle();
        audioClipManager.SeleccionarAudio(2, 0.5f);
    }

    public void ResolverPuzle()
    {
        if(baterias && ruedas && limpieza && radio && luces)
        {
            // PASADO
            Debug.Log("Puzle conseguido");
        }
    }

}
