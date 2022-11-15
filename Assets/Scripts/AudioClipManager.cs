using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audios;


    public AudioSource controlAudio;

    [SerializeField]
    private AudioSource limpiezaAudio;

    public void SeleccionarAudio(int indice, float volumen)
    {
        controlAudio.PlayOneShot(audios[indice], volumen);
    }

    public void AudioLimpieza(bool limpiando)
    {
        if (limpiando){
            limpiezaAudio.Play();
        }
        else
        {
            limpiezaAudio.Stop();
        }
        
    }
}
