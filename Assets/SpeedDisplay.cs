using UnityEngine;
using System.Collections;
using Controllers.Cannons;
using UnityStandardAssets.Vehicles.Car;
using Controllers.Vehicles.Ship;
using UnityEngine.UI;

public class SpeedDisplay : MonoBehaviour {
    public ParticleOnOff poff = null;
    public ShipUserControl shipUserContr = null;


    // Use this for initialization
    float lastSpeed = 0;

    private GameObject ImageSpeed, ImageReload;
    private Text TextSpeed, TextReload;

	void Start () {
        ImageReload = transform.Find("ImageReload").gameObject;
        TextReload = ImageReload.transform.Find("TextReload").GetComponent<Text>();
        ImageSpeed = transform.Find("ImageSpeed").gameObject;
        TextSpeed = ImageSpeed.transform.Find("TextSpeed").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {

        if (shipUserContr.currentShipMode == ShipMode.Sailing)
        {
            if (!ImageSpeed.gameObject.activeSelf)
            {
                ImageSpeed.SetActive(true);
                ImageReload.SetActive(false);
            }

            if (poff.speed != lastSpeed)
            {
                lastSpeed = poff.speed;

                TextSpeed.text = "Speed: " + Mathf.FloorToInt(lastSpeed) + " KM/H";
            }
        }
        else if (shipUserContr.currentShipMode == ShipMode.Firing)
        {
            if (!ImageReload.gameObject.activeSelf)
            {
                ImageSpeed.SetActive(false);
                ImageReload.SetActive(true);
            }

            TextReload.text = "Cannons: " + (shipUserContr.GetCannonReloadStatus() == true ? "Reloading" : "Ready");
            
        }
        
	    
	}
}
