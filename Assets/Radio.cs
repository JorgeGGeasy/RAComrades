using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    private Rigidbody rigg;
    public float anteriorCanal;
    [SerializeField]
    private GameObject[] botones;

    [SerializeField]
    private Material[] coloresBotones;

    [SerializeField]
    private Material colorBaseBoton;



    // Start is called before the first frame update
    void Start()
    {
        rigg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float canal = rigg.mass;
        if(canal != anteriorCanal)
        {
            switch (canal)
            {
                case 3:
                    // Canal 3
                    Debug.Log("Canal 3");
                    botones[canal-1].GetComponent<Renderer>().material = coloresBotones[canal-1];
                    botones[canal-1].GetComponent<Renderer>().material = coloresBotones[canal-1];
                    /*botones[canal].transform.localPosition = new Vector3((float) botones[canal].transform.localPosition.x,
                                                                        (float) botones[canal].transform.localPosition.y-0.139f,
                                                                        (float) botones[canal].transform.localPosition.z);*/
                    /*botones[2].transform.localPosition = new Vector3((float) botones[2].transform.localPosition.x,
                                                                        (float) 0.0f,
                                                                        (float) botones[2].transform.localPosition.z);*/
                    break;
                case 2:
                    // Canal 2
                    Debug.Log("Canal 2");
                    /*botones[canal].transform.localPosition = new Vector3((float) botones[canal].transform.localPosition.x,
                                                                        (float) botones[canal].transform.localPosition.y-0.139f,
                                                                        (float) botones[canal].transform.localPosition.z);*/
                    /*botones[1].transform.localPosition = new Vector3((float) botones[1].transform.localPosition.x,
                                                                        (float) 0.0f,
                                                                        (float) botones[1].transform.localPosition.z);*/
                    break;
                case 1:
                    // Canal 1
                    Debug.Log("Canal 1");
                    /*botones[canal].transform.localPosition = new Vector3((float) botones[canal].transform.localPosition.x,
                                                                        (float) botones[canal].transform.localPosition.y-0.139f,
                                                                        (float) botones[canal].transform.localPosition.z);*/
                   /* botones[3].transform.localPosition = new Vector3((float) botones[3].transform.localPosition.x,
                                                                    (float)  0.0f,
                                                                    (float)  botones[3].transform.localPosition.z);*/
                    
                    break;
                default:
                    // Vuelta
                    break;
            }
        }
        anteriorCanal = canal;
    }



}
