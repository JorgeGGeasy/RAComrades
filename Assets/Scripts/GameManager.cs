using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public bool baterias;
    public bool ruedas;
    public bool limpieza;
    public bool radio;
    public bool luces;

    private AudioClipManager audioClipManager;

    public static GameManager Instance; // A static reference to the GameManager instance

    [SerializeField]
    private GameObject canvasCoche;

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
        if(!baterias){   
            baterias = true;
            audioClipManager.SeleccionarAudio(2,0.5f);
        }
    }

    public void ResolverRuedas()
    {
        if (!ruedas)
        {
            ruedas = true;
            ResolverPuzle();
            audioClipManager.SeleccionarAudio(2, 0.5f);
        }
        
    }

    public void ResolverLimpieza()
    {
        if (!limpieza)
        {
            limpieza = true;
            ResolverPuzle();
            audioClipManager.SeleccionarAudio(2, 0.5f);
        }
    }

    public void ResolverRadio()
    {
        radio = true;
        ResolverPuzle();
        audioClipManager.SeleccionarAudio(2, 0.5f);
    }

    // devuelve si se tienen o no que encenderse las luces
    public bool ResolverLuces(bool hayLuz)
    {
        if(baterias && !hayLuz){
            if(!luces){
                audioClipManager.SeleccionarAudio(2, 0.5f);
            }
            luces = true;
            ResolverPuzle();
            return true;
        }else{
            return false;
        }
        
    }

    public void ResolverPuzle()
    {
        if(baterias && ruedas && limpieza && radio && luces)
        {
            // PASADO
            Debug.Log("Puzle conseguido");
            canvasCoche.SetActive(true);
            
        }
    }

    public void IrANivelCoche(){
        SceneManager.LoadScene("Coche", LoadSceneMode.Single);
    }

}
