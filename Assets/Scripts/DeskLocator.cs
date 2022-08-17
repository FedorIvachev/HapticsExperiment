using Oculus.Interaction;
using Oculus.Interaction.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DeskLocator : MonoBehaviour
{
    [SerializeField, Interface(typeof(IController))]
    private MonoBehaviour _controller;
    public IController Controller { get; private set; }

    


    protected virtual void Awake()
    {
        Controller = _controller as IController;
    }


    void SetPositionToControllerTip()
    {
        if (Controller.TryGetPointerPose(out Pose pose))
        {
            if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
            {

                print("Setting desk position");
                this.gameObject.transform.position = pose.position;

                Vector3 translation = new Vector3(-0.5f, 0f, 0.5f);
                this.gameObject.transform.Translate(translation, Space.Self);
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
        if (Input.GetKeyDown("space"))
        {
            print("keypress detected");
        }
    }
}
