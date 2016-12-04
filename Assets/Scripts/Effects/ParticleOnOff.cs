using UnityEngine;
using Controllers.Vehicles.Ship;
using Network.Controllers.Vehicles.Ship;

public class ParticleOnOff : MonoBehaviour {

    public ParticleSystem ps = null;
    public LocalShipController cc = null;
    public RemoteShipController rc = null;
    public float speed = 0;
    public bool playing = false;
	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {

        if (cc != null)
        {
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
        else if (rc != null)
        {
            speed = rc.CurrentSpeed;
            playing = ps.isPlaying;

            if (rc.CurrentSpeed > 1 && !ps.isPlaying)
            {
                ps.Play();
            }
            else if (rc.CurrentSpeed < 1 && ps.isPlaying)
            {
                ps.Stop();
            }
        }
        
	}
}
