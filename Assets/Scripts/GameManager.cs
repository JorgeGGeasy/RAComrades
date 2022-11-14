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
    
    public static GameManager Instance; // A static reference to the GameManager instance

    void Awake()
    {
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
    }

    public void ResolverRuedas()
    {
        ruedas = true;
        ResolverPuzle();
    }

    public void ResolverLimpieza()
    {
        limpieza = true;
        ResolverPuzle();
    }

    public void ResolverRadio()
    {
        radio = true;
        ResolverPuzle();
    }

    public void ResolverLuces()
    {
        luces = true;
        ResolverPuzle();
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
