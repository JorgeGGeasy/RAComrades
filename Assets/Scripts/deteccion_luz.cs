using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using TMPro;

public class deteccion_luz : MonoBehaviour
{
    #region PRIVATE_MEMBERS

#if UNITY_EDITOR
    PixelFormat mPixelFormat = PixelFormat.RGB888; // Editor passes in a RGBA8888 texture instead of RGB888
#else
    PixelFormat mPixelFormat = PixelFormat.RGB888; // Use RGB888 for mobile
#endif
    private bool mFormatRegistered = false;
    private Texture2D texture;

    private int width;
    private int height;
    #endregion // PRIVATE_MEMBERS

    #region MONOBEHAVIOUR_METHODS


    // Start is called before the first frame update
    void Start()
    {
        // Register Vuforia life-cycle callbacks:
        VuforiaApplication.Instance.OnVuforiaStarted += RegisterFormat;
        VuforiaApplication.Instance.OnVuforiaPaused += OnPause;
        VuforiaBehaviour.Instance.World.OnStateUpdated += OnVuforiaUpdated;
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
        if (mFormatRegistered)
        {
            texture = new Texture2D(width, height, TextureFormat.RGB24, false);
            Vuforia.Image image = VuforiaBehaviour.Instance.CameraDevice.GetCameraImage(mPixelFormat);
            image.CopyBufferToTexture(texture);
            texture.Apply();


            Debug.Log(
                "\nImage Format: " + image.PixelFormat +
                "\nImage Size: " + image.Width + " x " + image.Height +
                "\nBuffer Size: " + image.BufferWidth + " x " + image.BufferHeight +
                "\nImage Stride: " + image.Stride + "\n"
                
            );
            

            if(isTexturaConLuz(texture)){
                Debug.Log("Hay Luz");
            }else{
                Debug.Log("No hay Luz");
            }   
            
        }
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

        if (V >= 0.3)
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
