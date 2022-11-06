using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public enum Eje
{
    Delantero,
    Trasero
}
[Serializable]
public struct Rueda
{
    public GameObject modelo;
    public WheelCollider collider;
    public GameObject modeloEje;
    public Eje eje;
}

public class ControladorCoche : MonoBehaviour
{
    [SerializeField]
    private float aceleracionMaxima = 20.0f;
    [SerializeField]
    private float sensibilidadGiro = 1.0f;
    [SerializeField]
    private float anguloGiroMaximo = 45.0f;
    [SerializeField]
    private List<Rueda> ruedas;

    private Rigidbody rb;
    float frontal;
    float lateral;

    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    private void Awake()
    {
        posicionInicial = gameObject.transform.localPosition;
        rotacionInicial = gameObject.transform.localRotation;
        Debug.Log(posicionInicial);
    }

    // Update is called once per frame
    private void Update()
    {
        frontal = CrossPlatformInputManager.VirtualAxisReference("Vertical").GetValue * aceleracionMaxima;
        lateral = CrossPlatformInputManager.VirtualAxisReference("Horizontal").GetValue * sensibilidadGiro;

        foreach (Rueda rueda in ruedas)
        {
            Quaternion rotacion;
            Vector3 pos;
            rueda.collider.GetWorldPose(out pos, out rotacion);
            rueda.modelo.transform.position = pos;
            rueda.modeloEje.transform.position = new Vector3(/*rueda.modeloEje.transform.position.x*/pos.x + 0.01203448f, pos.y, rueda.modeloEje.transform.position.z);
            rueda.modelo.transform.rotation = rotacion;
        }
    }

    private void LateUpdate()
    {
        foreach(Rueda rueda in ruedas)
        {
            rueda.collider.motorTorque = frontal * 500 * Time.deltaTime;
        }
        foreach(Rueda rueda in ruedas)
        {
            if(rueda.eje == Eje.Delantero)
            {
                float anguloGiro = lateral * sensibilidadGiro * anguloGiroMaximo;
                rueda.collider.steerAngle = Mathf.Lerp(rueda.collider.steerAngle, anguloGiro, 0.5f);
            }
        }
    }

    public void ResetPos()
    {
        transform.localPosition = posicionInicial;
        transform.localRotation = rotacionInicial;
    }
}
