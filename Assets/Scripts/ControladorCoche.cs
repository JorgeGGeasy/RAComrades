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

    public float antiRoll;

    public GameObject canvasTarjeta;
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
    private void FixedUpdate()
    {
        WheelHit hit;
        float travelLF = 1.0f;
        float travelRF = 1.0f;
        float travelLT = 1.0f;
        float travelRT = 1.0f;

        bool groundLF = ruedas[0].collider.GetGroundHit(out hit);
        if(groundLF)
            travelLF = (ruedas[0].collider.transform.InverseTransformPoint(hit.point).y - ruedas[0].collider.radius) / ruedas[0].collider.suspensionDistance;

        bool groundRF = ruedas[4].collider.GetGroundHit(out hit);
        if (groundRF)
            travelRF = (ruedas[4].collider.transform.InverseTransformPoint(hit.point).y - ruedas[4].collider.radius) / ruedas[4].collider.suspensionDistance;

        bool groundLT = ruedas[3].collider.GetGroundHit(out hit);
        if (groundLF)
            travelLF = (ruedas[3].collider.transform.InverseTransformPoint(hit.point).y - ruedas[3].collider.radius) / ruedas[3].collider.suspensionDistance;

        bool groundRT = ruedas[7].collider.GetGroundHit(out hit);
        if (groundRT)
            travelRT = (ruedas[7].collider.transform.InverseTransformPoint(hit.point).y - ruedas[7].collider.radius) / ruedas[7].collider.suspensionDistance;

        float fuerzaAntiRollF = (travelLF - travelRF) * antiRoll;
        float fuerzaAntiRollT = (travelLT - travelRT) * antiRoll;

        if (groundLF)
            rb.AddForceAtPosition(ruedas[0].collider.transform.up * -fuerzaAntiRollF, ruedas[0].collider.transform.position);
        if (groundRF)
            rb.AddForceAtPosition(ruedas[4].collider.transform.up * -fuerzaAntiRollF, ruedas[4].collider.transform.position);
        if (groundLT)
            rb.AddForceAtPosition(ruedas[3].collider.transform.up * -fuerzaAntiRollT, ruedas[3].collider.transform.position);
        if (groundRT)
            rb.AddForceAtPosition(ruedas[7].collider.transform.up * -fuerzaAntiRollT, ruedas[7].collider.transform.position);
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
