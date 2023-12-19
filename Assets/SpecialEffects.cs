using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffects : MonoBehaviour
{
    public static SpecialEffects specialEffects;

    private GameObject smokeVFX;
    private AudioClip coinVFX;

    private void Awake()
    {
        if (specialEffects != null && specialEffects != this)
        {
            Destroy(this);
        }
        else
        {
            specialEffects = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        smokeVFX = Resources.Load<GameObject>("SmokeVFX");
        coinVFX = Resources.Load<AudioClip>("eu4_gold");
    }

    public void CreateSmoke(Transform _transform)
    {
        GameObject smoke = Instantiate(smokeVFX, _transform.position, Quaternion.identity);
        Destroy(smoke, smoke.GetComponent<ParticleSystem>().main.duration);
    }

    public void CoinVFX()
    {
        GameObject speaker = new GameObject("Speaker");
        speaker.gameObject.AddComponent<AudioSource>();
        speaker.GetComponent<AudioSource>().clip = coinVFX;
        speaker.GetComponent<AudioSource>().Play();
        float sfxLength = coinVFX.length;
        Destroy(speaker, sfxLength);
    }

}
