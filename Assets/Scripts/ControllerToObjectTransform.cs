using Oculus.Interaction;
using Oculus.Interaction.Input;
using UnityEngine;

public class ControllerToObjectTransform : MonoBehaviour
{
    [SerializeField, Interface(typeof(IController))]
    private MonoBehaviour _controller;
    public IController Controller { get; private set; }

    public GameObject GameObjectToSetPosition;

    public Vector3 GameObjectAdjustTranslation = Vector3.zero;


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

                GameObjectToSetPosition.transform.position = pose.position;

                GameObjectToSetPosition.transform.Translate(GameObjectAdjustTranslation, Space.Self);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!GameObjectToSetPosition)
            GameObjectToSetPosition = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        SetPositionToControllerTip();
    }

}
