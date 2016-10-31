using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class ParticleOnOff : MonoBehaviour {

    public ParticleSystem ps = null;
    public CarController cc = null;
    public float speed = 0;
    public bool playing = false;
	// Use this for initialization
	void Start () {
        cc = GetComponent<CarController>();
	}
	
	// Update is called once per frame
	void Update () {
        speed = cc.CurrentSpeed;
        playing = ps.isPlaying;

	    if (cc.CurrentSpeed > 1 && !ps.isPlaying)
        {
            ps.Play();
        }
        else if (cc.CurrentSpeed < 1 && ps.isPlaying)
        {
            ps.Stop();
        }
	}
}
