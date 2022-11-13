using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corutina : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       Coroutine c =  StartCoroutine(Prueba());
        StopCoroutine(c);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Prueba()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("fjglfdjlkfdj");
    }
}
