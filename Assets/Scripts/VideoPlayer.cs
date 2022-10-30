using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPlayer : MonoBehaviour
{
    private UnityEngine.Video.VideoPlayer v;
    public GameObject planoVideo;
    private bool videoIniciado = true;
    // Start is called before the first frame update
    void Start()
    {
        v = planoVideo.GetComponent<UnityEngine.Video.VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IniciarPausar()
    {
        if (!videoIniciado)
        {
            v.Play();
        }
        else
        {
            v.Pause();
        }
        videoIniciado = !videoIniciado;
    }

    public void Reiniciar()
    {
        v.Stop();
        v.Play();
        videoIniciado = true;
    }
}
