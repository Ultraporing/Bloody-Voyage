using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : MonoBehaviour
{

    const float yMinLimit = -360;
    const float yMaxLimit = 360;
    const float xMinLimit = -360;
    const float xMaxLimit = 360;
    const float tiltAngle = 30.0f;
    const float smooth = 2.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    { //Every frame, do this as late as you can
        float tiltAroundY = Input.GetAxis("Mouse X") * tiltAngle;
        float tiltAroundX = Input.GetAxis("Mouse Y") * tiltAngle;
        tiltAroundY = ClampAngle(tiltAroundY, yMinLimit, yMaxLimit);
        tiltAroundX = ClampAngle(tiltAroundX, xMinLimit, xMaxLimit);

        Quaternion target = Quaternion.Euler(tiltAroundY, tiltAroundX, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }

    private int ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp((int)(angle), (int)(min), (int)(max));
    }
}
