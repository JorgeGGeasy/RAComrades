using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manipular : MonoBehaviour
{
    public float rotSpeed = 0.4f;
    private float initialDistance;
    private Vector3 initialScale;
    

    // atributos para resetear la poscion
    public Button btn_resetear;
    //private Vector3 escalaInicial = new Vector3(0.7883818f,0.008797552f,1.045946f);
    //private Vector3 posicionInicial =  new Vector3(0.0f,0.02f,0.0f);
    private Vector3 escalaInicial;
    private Vector3 posicionInicial;
    private Vector3 angulosInicial =  Vector3.zero;

    void resetear_posicion_manual(){
        Debug.Log("RESETEAR");
        this.transform.localPosition = posicionInicial;
        this.transform.localEulerAngles  = angulosInicial;
        this.transform.localScale  = escalaInicial;
    }

    void Start(){

        escalaInicial = gameObject.transform.localScale;
        posicionInicial = gameObject.transform.localPosition;
        // inicializar la posicion original
        btn_resetear.onClick.AddListener(resetear_posicion_manual);
    }

    void Update()
    {
        if (objIsTouched())
        {
            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Quaternion initRotation = this.transform.rotation;
                    this.transform.Rotate(Input.GetTouch(0).deltaPosition.y * rotSpeed, -Input.GetTouch(0).deltaPosition.x * rotSpeed, 0, Space.World);
                }
            }

            if (Input.touchCount == 2)
            {
                var touchZero = Input.GetTouch(0);
                var touchOne = Input.GetTouch(1);

                if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled
                   || touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
                {
                    return;
                }

                if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                {
                    initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                    initialScale = this.transform.localScale;
                }
                else
                {
                    var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);
                    if (Mathf.Approximately(initialDistance, 0)) return;
                    var factor = currentDistance / initialDistance;
                    this.transform.localScale = initialScale * factor;
                }
            }
        } 
    }

    private bool objIsTouched()
    {
        foreach(Touch t in Input .touches)
        {
            Ray m_ray = Camera.main.ScreenPointToRay(t.position);
            RaycastHit m_hit;
            if (Physics.Raycast(m_ray, out m_hit, 100))
            {
                return true;
            }
        }
        return false;
    }
}