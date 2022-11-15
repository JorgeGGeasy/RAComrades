using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ManualScript : MonoBehaviour
{


    public Sprite[] imagenes; // array de las distintas imagenes a mostrar
    private Material m_Material; // componente image mostrada en el juego
    private int currentIndex = 0;

    

    public Button btn_avanzar,btn_retroceder;

    // Start is called before the first frame update
    void Start()
    {
        // inicializar las fotos
        m_Material = GetComponent<Renderer>().material;
        m_Material.mainTexture = imagenes[currentIndex].texture;

   

        // añadir callbacks de los botones
        btn_avanzar.onClick.AddListener(siguiente_imagen);
        btn_retroceder.onClick.AddListener(anterior_imagen);
    }

    // Update is called once per frame
    void Update()
    {

        if(currentIndex == 0){
            // estamos al principio bloquear el echar atras
            btn_retroceder.enabled = false;
        }else if(currentIndex == imagenes.Length-1){
            // estamos al final bloquear el echar adelante
            btn_avanzar.enabled = false;
        }else{
            // estamos en la mitad
             btn_avanzar.enabled = true;
             btn_retroceder.enabled = true;
        }
        
    }


    


    void siguiente_imagen(){

        Debug.Log("SIGUIENTE");
        if(currentIndex<imagenes.Length-1){
            currentIndex++;
            m_Material.mainTexture = imagenes[currentIndex].texture;
        }
    }

    void anterior_imagen(){
        Debug.Log("ANTERIOR");
        if(currentIndex>0){
            currentIndex--;
            m_Material.mainTexture = imagenes[currentIndex].texture;
        }
    }
}
