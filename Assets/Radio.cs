using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    private Rigidbody rigg;
    public float anteriorCanal;
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
                    break;
                case 2:
                    // Canal 2
                    Debug.Log("Canal 2");
                    break;
                case 1:
                    // Canal 1
                    Debug.Log("Canal 1");
                    break;
                default:
                    // Vuelta
                    break;
            }
        }
        anteriorCanal = canal;
    }
}
