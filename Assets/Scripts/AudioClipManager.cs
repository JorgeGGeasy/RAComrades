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

    [SerializeField]
    private AudioClip[] audiosRadio;


    public AudioSource controlAudioRadio;

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

    public void AudioRadio(int indice, float volumen)
    {
        controlAudioRadio.PlayOneShot(audiosRadio[indice], volumen);
    }

}
