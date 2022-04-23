using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterParticles : MonoBehaviour
{

    private ParticleSystem ps;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps.Play();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!ps.isPlaying && !audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
