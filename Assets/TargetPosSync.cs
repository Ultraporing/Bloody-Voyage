using UnityEngine;
using System.Collections;

public class TargetPosSync : MonoBehaviour {

    public Transform Target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Target.position;
        transform.rotation = Target.rotation;
        transform.localScale = Target.localScale;
	}
}
