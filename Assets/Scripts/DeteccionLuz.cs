using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using TMPro;

public class DeteccionLuz : MonoBehaviour
{
    #region PRIVATE_MEMBERS

#if UNITY_EDITOR
    PixelFormat mPixelFormat = PixelFormat.RGB888; // Editor passes in a RGBA8888 texture instead of RGB888
#else
    PixelFormat mPixelFormat = PixelFormat.RGB888; // Use RGB888 for mobile
#endif
    private bool mFormatRegistered = false;
    private Texture2D texture;
    public float period = 0.0f;
    private float timeInterval = 1.0f; //seconds

    private int width;
    private int height;

    public GameObject luzCoche;
    public GameObject solEscena;

    [Range(0.0f, 1.0f)]
    public float umbralDeteccionLuz;

    [SerializeField]
    private bool isMinijuegoLuz = false;
    private GameManager gameManager;

    #endregion // PRIVATE_MEMBERS

    #region MONOBEHAVIOUR_METHODS


    // Start is called before the first frame update
    void Start()
    {
        // Register Vuforia life-cycle callbacks:
        VuforiaApplication.Instance.OnVuforiaStarted += RegisterFormat;
        VuforiaApplication.Instance.OnVuforiaPaused += OnPause;
        VuforiaBehaviour.Instance.World.OnStateUpdated += OnVuforiaUpdated;

        gameManager = GameManager.Instance;
    }


    void OnDestroy()
    {
        // Unregister Vuforia life-cycle callbacks:
        VuforiaApplication.Instance.OnVuforiaStarted -= RegisterFormat;
        VuforiaApplication.Instance.OnVuforiaPaused -= OnPause;
        VuforiaBehaviour.Instance.World.OnStateUpdated -= OnVuforiaUpdated;
    }

    #endregion // MONOBEHAVIOUR_METHODS

    #region PRIVATE_METHODS
    /// 
    /// Called each time the Vuforia state is updated
    /// 
    void OnVuforiaUpdated()
    {
        if (period > timeInterval)
        {
            // hay que hacer el proceso de pillar una imagen mas lento para que no se sature 
            period = 0;

            if (mFormatRegistered)
            {
                Debug.Log("Va a pillar imagen");
                texture = new Texture2D(width, height, TextureFormat.RGB24, false);
                Vuforia.Image image = VuforiaBehaviour.Instance.CameraDevice.GetCameraImage(mPixelFormat);
                image.CopyBufferToTexture(texture);
                texture.Apply();
                if(luzCoche !=null){
                    bool hayLuz = isTexturaConLuz(texture);
                    configurarLucesSegunLuzAmbiente(hayLuz);
                    
                }
            }else{
                Debug.Log("No pilla imagen");
            }
        }
        period += UnityEngine.Time.deltaTime;
    }

    
    void configurarLucesSegunLuzAmbiente(bool hayLuz){
        // si esta el minijuego activo, encender las luces del coche si estan las baterias
        if(isMinijuegoLuz){
             luzCoche.SetActive(gameManager.ResolverLuces(hayLuz)); 
        }else{  
            luzCoche.SetActive(!hayLuz); 
        }

        solEscena.SetActive(hayLuz); 
        
    }

    ///
    /// Dada una textura obtiene el color medio y
    /// devuelve true si tiene suficiente luz (componente V de HSV es mayor que .3)
    /// (componente V 0 -> negro 1 -> blanco)
    ///
    bool isTexturaConLuz(Texture2D tex){


       
        // obtener color medio
        Color colorMedio = averageColor(tex);

        
        // transformar de RGB a HSV
        float H, S, V; // datos normalizados de 0 a 1, H va de 0 a 360 y la S y la V de 0 a 100

        Color.RGBToHSV(colorMedio, out H, out S, out V);

        Debug.Log("Entro en texturaConLuz y" + " H >> " + H + " S >> " + S + " V >> " + V);

        if (V >= umbralDeteccionLuz)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    ///
    /// Obtener el color medio de una textura
    ///
    Color  averageColor ( Texture2D tex ) {
        Color32[] texColors = tex.GetPixels32();
 
        int total = texColors.Length;
 
        float r = 0;
        float g = 0;
        float b = 0;
 
        for(int i = 0; i < total; i++)
        {
 
            r += texColors[i].r;
 
            g += texColors[i].g;
 
            b += texColors[i].b;
 
        }

       

        return new Color32((byte)(r / total) , (byte)(g / total) , (byte)(b / total) , 0);
       
    }

    /// 
    /// Called when app is paused / resumed
    /// 
    void OnPause(bool paused)
    {
        if (paused)
        {
            Debug.Log("App was paused");
            UnregisterFormat();
        }
        else
        {
            Debug.Log("App was resumed");
            RegisterFormat();
        }
    }
    /// 
    /// Register the camera pixel format
    /// 
    void RegisterFormat()
    {
        // Vuforia has started, now register camera image format
        bool success = VuforiaBehaviour.Instance.CameraDevice.SetFrameFormat(mPixelFormat, true);
        if (success)

        {
            Debug.Log("Successfully registered pixel format " + mPixelFormat.ToString());
            mFormatRegistered = true;
        }
        else
        {
            Debug.LogError(
                "Failed to register pixel format " + mPixelFormat.ToString() +
                "\n the format may be unsupported by your device;" +
                "\n consider using a different pixel format.");
            mFormatRegistered = false;
        }
    }
    /// 
    /// Unregister the camera pixel format (e.g. call this when app is paused)
    /// 
    void UnregisterFormat()
    {
        Debug.Log("Unregistering camera pixel format " + mPixelFormat.ToString());
        VuforiaBehaviour.Instance.CameraDevice.SetFrameFormat(mPixelFormat, false);
        mFormatRegistered = false;
    }
    #endregion //PRIVATE_METHODS
}
