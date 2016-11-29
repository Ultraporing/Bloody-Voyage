using UnityEngine;
using System.Collections;

public class CannonballController : MonoBehaviour
{
    private EffectSpawnOnSurface EffectSpawnOnSurface = null;
    private Rigidbody CannonballRigidbody = null;

	// Use this for initialization
	void Start ()
    {
        EffectSpawnOnSurface = GetComponent<EffectSpawnOnSurface>();
        CannonballRigidbody = GetComponent<Rigidbody>();
	}

    public void SendForce(float power)
    {
        EffectSpawnOnSurface = GetComponent<EffectSpawnOnSurface>();
        CannonballRigidbody = GetComponent<Rigidbody>();

        CannonballRigidbody.isKinematic = false;
        CannonballRigidbody.AddForce(transform.forward * power);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        EffectSpawnOnSurface.Spawn(collision);
        Destroy(gameObject);
    }
}
