using UnityEngine;
using System.Collections;

public class SpeedDisplay : MonoBehaviour {
    public ParticleOnOff poff = null;
    // Use this for initialization
    float lastSpeed = 0;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (poff.speed != lastSpeed)
        {
            lastSpeed = poff.speed;
            GetComponent<UnityEngine.UI.Text>().text = "Speed: " + Mathf.FloorToInt(lastSpeed) + " KM/H";
        }
	    
	}
}
