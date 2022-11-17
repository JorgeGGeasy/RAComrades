using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    private GameManager gameManager;

    private Rigidbody rigg;
    public float anteriorCanal;
    [SerializeField]
    private GameObject[] botones;

    [SerializeField]
    private Material[] coloresBotones;

    [SerializeField]
    private Material colorBaseBoton;

    [SerializeField]
    private ParticleSystem ondasMalas;

    [SerializeField]
    private ParticleSystem ondasBuenas;

    private AudioClipManager audioClipManager;


    private void Awake()
    {
        audioClipManager = FindObjectOfType<AudioClipManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigg = GetComponent<Rigidbody>();
        gameManager = GameManager.Instance;
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
                    Mover(2, 1);
                    ondasMalas.Stop();
                    ondasBuenas.Play();

                    audioClipManager.AudioRadio(2,0.5f);
                    gameManager.ResolverRadio();

                    break;
                case 2:
                    // Canal 2
                    Debug.Log("Canal 2");
                    Mover(1, 0);

                    audioClipManager.AudioRadio(1, 0.01f);

                    break;
                case 1:
                    // Canal 1
                    Debug.Log("Canal 1");
                    Mover(0, 2);
                    ondasMalas.Play();
                    ondasBuenas.Stop();

                    audioClipManager.AudioRadio(0, 0.5f);

                    gameManager.radio = false;

                    break;
                default:
                    // Vuelta
                    break;
            }
        }
        anteriorCanal = canal;
    }

    public void Mover(int canal, int canalPrevio)
    {
        audioClipManager.controlAudioRadio.Stop();
        audioClipManager.AudioRadio(3, 0.5f);
        botones[canal].transform.localPosition = new Vector3((float)botones[canal].transform.localPosition.x,
                                                                        (float)botones[canal].transform.localPosition.y - 0.139f,
                                                                        (float)botones[canal].transform.localPosition.z);
        botones[canalPrevio].transform.localPosition = new Vector3((float)botones[canalPrevio].transform.localPosition.x,
                                                                   0f,
                                                                   (float)botones[canalPrevio].transform.localPosition.z);
        botones[canal].GetComponent<Renderer>().material = coloresBotones[canal];
        botones[canalPrevio].GetComponent<Renderer>().material = colorBaseBoton;
    }
}
