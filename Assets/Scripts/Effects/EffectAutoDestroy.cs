using UnityEngine;
using System.Collections;

public class EffectAutoDestroy : MonoBehaviour {

    private AudioSource[] AudioSources;
    private ParticleSystem[] ParticleSystems;
    public bool AllDone = false;

	// Use this for initialization
	void Start () {
        AudioSources = FindObjectsOfType<AudioSource>();
        ParticleSystems = FindObjectsOfType<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (CheckIfAllDone())
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(transform.gameObject);
            }
        }
	}

    bool CheckIfAllDone()
    {
        foreach (AudioSource aSrc in AudioSources)
        {
            if (aSrc != null)
            {
                if (aSrc.isPlaying)
                {
                    return false;
                }
            }
        }

        foreach (ParticleSystem pSys in ParticleSystems)
        {
            if (pSys != null)
            {
                if (pSys.isPlaying)
                {
                    return false;
                }
            }
            
        }

        return true;
    }
}
