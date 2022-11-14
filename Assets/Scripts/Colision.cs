using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision : MonoBehaviour
{
    private GameManager gameManager;

    public bool rueda;
    public List<GameObject> ruedas;
    private Coroutine limpiar;
    private bool limpiando;

    public Material materialSucio;
    public Material materialMedioSucio;
    public Material materialLimpio;
    private int contador;
    private int ruedasConectadas;

    public float expForce, radius;

    public void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "objetoMovible")
        {
            Objeto propiedades =  other.gameObject.GetComponent<Objeto>();
            if (propiedades.conectar())
            {
                conectarAlCoche(other.gameObject, propiedades);
            }
            else if (propiedades.limpia())
            {
                limpiar = StartCoroutine(Limpiar());
            }
            
        }
    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "objetoMovible")
        {
            Objeto objeto = other.gameObject.GetComponent<Objeto>();
            if (objeto.limpia())
            {
                if (!limpiando)
                {
                    limpiar = StartCoroutine(Limpiar());
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "objetoMovible")
        {
            Objeto objeto = other.gameObject.GetComponent<Objeto>();
            if (objeto.limpia())
            {
                StopCoroutine(limpiar);
            }
        }
    }

    public void conectarAlCoche(GameObject objeto, Objeto propiedades)
    {
        if (propiedades.recibirNombre() == "Rueda" && rueda != true)
        {
            if (propiedades.ruedaCorrecta())
            {
                objeto.gameObject.tag = "conectado";
                switch (ruedasConectadas)
                {
                    case 0:
                        objeto.transform.position = transform.position;
                        objeto.transform.position = new Vector3(objeto.transform.position.x, objeto.transform.position.y, 0.149f);
                        break;
                    case 1:
                        objeto.transform.position = transform.position;
                        objeto.transform.position = new Vector3(-0.438f, objeto.transform.position.y, objeto.transform.position.z);
                        break;
                    case 2:
                        objeto.transform.position = transform.position;
                        objeto.transform.position = new Vector3(objeto.transform.position.x, objeto.transform.position.y, -0.149f);
                        break;
                    default:
                        print("Ya no se colocan ruedas");
                        break;
                }
                ruedasConectadas++;
            }
            else
            {
                crearExplosion();
            }
            cocheListo();

            
            objeto.gameObject.GetComponent<Rigidbody>().useGravity = false;
            objeto.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void crearExplosion()
    {
        Debug.Log("Exploto");
        ruedasConectadas = 0;
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearby in colliders)
        {
            Rigidbody rigg = nearby.GetComponent<Rigidbody>();

            if (rigg != null)
            {
                rigg.isKinematic = false;
                rigg.useGravity = true;
                rigg.AddExplosionForce(expForce, transform.position, radius);
                rigg.gameObject.tag = "objetoMovible";
            }
        }
    }

    public void cocheListo(){

        if (ruedasConectadas >= 3)
        {
            rueda = true;
            gameManager.ResolverRuedas();
        }
    }

    IEnumerator Limpiar()
    {
        limpiando = true;
        yield return new WaitForSeconds(5f);
        Debug.Log("Aqui va contador: " + contador);
        contador++;
        switch (contador)
        {
            case 3:
                GetComponent<Renderer>().material = materialLimpio;
                gameManager.ResolverLimpieza();
                break;
            case 2:
                GetComponent<Renderer>().material = materialMedioSucio;
                break;
            case 1:
                GetComponent<Renderer>().material = materialSucio;
                break;
            default:
                print("Limpieza acabada o por empezar");
                break;
        }
        limpiando = false;
    }
}
