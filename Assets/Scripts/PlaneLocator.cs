using Oculus.Interaction;
using Oculus.Interaction.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneLocator : MonoBehaviour
{
    [SerializeField, Interface(typeof(IController))]
    private MonoBehaviour _controller;
    public IController Controller { get; private set; }

    // Three points define a plane.
    // Using separate values instead of array for a clearer visibility
    private Vector3 firstPointOnPlane;
    private Vector3 secondPointOnPlane;
    private Vector3 thirdPointOnPlane;
    private int currentPointToSet = 0;


    protected virtual void Awake()
    {
        Controller = _controller as IController;
    }

    void SetPositionToControllerTip()
    {
        if (Controller.TryGetPointerPose(out Pose pose))
        {
            if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch))
            {
                print("Setting plane params");
                // Press controller three times to define three points
                // Which together define a plane
                switch(currentPointToSet)
                {
                    case 0:
                        firstPointOnPlane = pose.position;
                        print("First point position set!");
                        break;
                    case 1:
                        secondPointOnPlane = pose.position;
                        print("Second point position set!");
                        break;
                    case 2:
                        thirdPointOnPlane = pose.position;
                        print("Third point position set!");
                        Plane plane = new Plane(firstPointOnPlane, secondPointOnPlane, thirdPointOnPlane);

                        // For Unity's default plane GameObject the Y axis is the plane's normal.  So we need a perpendicular to the plane's normal.
                        Vector3 perpendicular;
                        if (Vector3.Angle(plane.normal, Vector3.up) > 5)
                        {
                            perpendicular = Vector3.Cross(plane.normal, Vector3.up);
                        }
                        else
                        {
                            perpendicular = Vector3.Cross(plane.normal, Vector3.right);
                        }

                        Quaternion planeRotation = new Quaternion();
                        planeRotation.SetLookRotation(perpendicular, plane.normal);
                        this.gameObject.transform.rotation = planeRotation;
                        this.gameObject.transform.position = firstPointOnPlane;
                        Vector3 translation = new Vector3(0.105f, 0f, 0.1005f);
                        this.gameObject.transform.Translate(translation, Space.Self);
                        break;
                    default:
                        break;
                }
                currentPointToSet = (currentPointToSet + 1) % 3;

            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            SetPositionToControllerTip();
    }
}
