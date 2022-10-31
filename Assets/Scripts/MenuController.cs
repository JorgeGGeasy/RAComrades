using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
    }

    // Empieza controlador del menu
    public void IniciarCoche()
    {
        SceneManager.LoadScene("Coche", LoadSceneMode.Single);
    }

    public void IniciarReparar()
    {
        SceneManager.LoadScene("Reparar", LoadSceneMode.Single);
    }

    public void IniciarInstrucciones()
    {
        SceneManager.LoadScene("Instrucciones", LoadSceneMode.Single);
    }

    public void IniciarVideo()
    {
        SceneManager.LoadScene("Video", LoadSceneMode.Single);
    }

    public void IniciarManual()
    {
        SceneManager.LoadScene("Manual", LoadSceneMode.Single);
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }
    // Acaba controlador del menu

    public void Volver()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
