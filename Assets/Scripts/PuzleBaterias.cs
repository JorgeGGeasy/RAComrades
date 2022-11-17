using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzleBaterias : MonoBehaviour
{
    private GameManager gameManager;
    

    private Color colorb1;
    private Color colorb2;
    private int contBaterias = 0;

    [SerializeField]
    private Color colorResultado;
    [SerializeField]
    private Material pantallaResultado;
    [SerializeField]
    private Vector3 posicionBateria1;
    [SerializeField]
    private Vector3 posicionBateria2;
    [SerializeField]
    private Vector3 escalaPequenya, escalaGrande;
    [SerializeField]
    private float expForce, radius;
    [SerializeField]
    private ParticleSystem explosion;
    private AudioClipManager audioClipManager;

    [SerializeField]
    private GameObject[] baterias;


    private void Awake()
    {
        audioClipManager = FindObjectOfType<AudioClipManager>();
    }

    /// return -> 0 -> true, 1-> false, 2 -> estado intermedio
    private int comprobarResultado(){
        if(contBaterias == 2){
            Color colorMezcla = (colorb1 + colorb2) / 2;

            Debug.Log("BATERIA 1 - R: " + (int)colorb1.r*255 + " G: " + (int)colorb1.g*255 + " B: " + (int)colorb1.b*255);
            Debug.Log("BATERIA 2 - R: " + (int)colorb2.r*255 + " G: " + (int)colorb2.g*255 + " B: " + (int)colorb2.b*255);
            Debug.Log("BATERIA MEZCLA - R: " + (int)colorMezcla.r*255 + " G: " + (int)colorMezcla.g*255 + " B: " + (int)colorMezcla.b*255);
            Debug.Log("BATERIA MEZCLA - R: " + (int)colorResultado.r*255 + " G: " + (int)colorResultado.g*255 + " B: " + (int)colorResultado.b*255);

            pantallaResultado.color = colorMezcla;

            if(coloresIguales(colorMezcla, colorResultado)){
                // okay
                return 0;
            }else{
                // hacer saltar las baterias
                return 1;
            }
        }else if(contBaterias == 1){
            pantallaResultado.color = colorb1;
            return 2;
        }

        return 2;

    }


    private bool coloresIguales(Color color1, Color color2){
        return (((int) color1.r == (int)color2.r) && ((int) color1.b == (int)color2.b) && ((int) color1.g == (int)color2.g));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "objetoMovible"){

            Objeto objeto =  other.gameObject.GetComponent<Objeto>();
           
            string nombre = objeto.recibirNombre();
            if(nombre == "bateria"){
                switch(contBaterias){
                    case 0:
                        colocarBateria(other.gameObject, posicionBateria1);
                        Debug.Log("BATERIA - Se pone bateria 1");
                        colorb1 = other.gameObject.transform.Find("bateria_material").gameObject.GetComponent<Renderer>().material.GetColor("_Color");
                        contBaterias++;
                        break;
                    case 1:
                        colocarBateria(other.gameObject, posicionBateria2);
                        Debug.Log("BATERIA - Se pone bateria 2");
                        colorb2 = other.gameObject.transform.Find("bateria_material").gameObject.GetComponent<Renderer>().material.GetColor("_Color");
                        contBaterias++;
                        break;
                    default:
                        Debug.Log("");
                        break;
                        

                }
                int result = comprobarResultado() ;
                if(result == 1){
                    // combinacion erronea
                    Debug.Log("BATERIA - COLORES NO IGUALES");
                    
                    
                    // de alguna forma poner las baterias a movible
                    reiniciarPuzle();
                    StartCoroutine(ReiniciarContador());
                    
                    // desfreeze todo
                }else if(result == 0){
                    // correcto
                    gameManager.ResolverBaterias();
                    Debug.Log("BATERIA - COLORES IGUALES");
                }
            }
            
        }
    }


    private void reiniciarPuzle(){

         foreach(GameObject b in baterias){
            b.GetComponent<Rigidbody>().constraints = 0;
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearby in colliders)
        {
            Rigidbody rigg = nearby.GetComponent<Rigidbody>();

            if (rigg != null)
            {
                rigg.isKinematic = false;
                rigg.useGravity = true;
                rigg.AddExplosionForce(expForce, transform.position, radius);
                audioClipManager.SeleccionarAudio(1, 0.1f);
                explosion.Play();
                
                rigg.gameObject.tag = "objetoMovible";
                rigg.gameObject.transform.localScale = escalaGrande;
            }
        }

       
    }

    

    private void colocarBateria(GameObject bateria, Vector3 posicion){
        bateria.tag = "conectado";
        audioClipManager.SeleccionarAudio(0, 0.5f);
        bateria.GetComponent<Rigidbody>().useGravity = false;
        bateria.GetComponent<Rigidbody>().isKinematic = true;
        bateria.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        bateria.transform.position = posicion;
        bateria.transform.eulerAngles = new Vector3(0,-90,0);
        bateria.transform.localScale = escalaPequenya;
    }

    void Start()
    {
        pantallaResultado.color = Color.grey;
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ReiniciarContador()
    {
        yield return new WaitForSeconds(2f);
        contBaterias = 0;
    }
}
