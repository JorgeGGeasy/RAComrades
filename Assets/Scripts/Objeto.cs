using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto : MonoBehaviour
{
    public objetoPuzle objetoPuzle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool conectar()
    {
        if (objetoPuzle.seConecta)
        {
            return true;
        }
        return false;
    }

    public string recibirNombre()
    {
        return objetoPuzle.nombre;
    }
}
