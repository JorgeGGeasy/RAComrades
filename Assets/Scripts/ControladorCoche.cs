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

    public Rigidbody rb;
    float frontal;
    float lateral;

    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;

    private float period;

    public GameObject canvasTarjeta;
    // Start is called before the first frame update
    private void Start()
    {
        period = 0f;
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
        if (canvasTarjeta.GetComponent<Canvas>().enabled)
        {
            //Debug.Log("Hay canvas");
            rb.useGravity = true;
            rb.isKinematic = false;
        }
        else
        {
            //Debug.Log("No hay canvas");
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        frontal = CrossPlatformInputManager.VirtualAxisReference("Vertical").GetValue * aceleracionMaxima;
        lateral = CrossPlatformInputManager.VirtualAxisReference("Horizontal").GetValue * sensibilidadGiro;

        foreach (Rueda rueda in ruedas)
        {
            Quaternion rotacion;
            Vector3 pos;
            rueda.collider.GetWorldPose(out pos, out rotacion);
            rueda.modelo.transform.position = pos;
            rueda.modeloEje.transform.position = new Vector3(pos.x, pos.y, pos.z);
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
        rb.isKinematic = true;
        transform.localPosition = posicionInicial;
        transform.localRotation = rotacionInicial;
        Debug.Log("Está cayendo");
        rb.isKinematic = false;
    }

    public void ResetPosQuieta()
    {
        rb.isKinematic = true;
        transform.localPosition = new Vector3(transform.localPosition.x, posicionInicial.y, transform.localPosition.z);
        transform.localRotation = rotacionInicial;
        Debug.Log("Está cayendo");
        rb.isKinematic = false;
    }
}
