using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform cam;
    [SerializeField] private LayerMask grabbableObjectLayer;
    [SerializeField] private Transform grabbedObjTarget;

    [Header("Settings")]
    [SerializeField] private float interactionDistance;

    private RaycastHit rayHit;
    private GrabbableObject grabbableObject;
    private bool lookingAtGrabbable;
    private bool holdingItem = false;

    private void Update()
    {
        if (Physics.Raycast(cam.position, cam.forward, out rayHit, interactionDistance, grabbableObjectLayer))
        {
            if (!lookingAtGrabbable)
            {
                lookingAtGrabbable = true;
                UIManager.Instance.uiBottomMessage.SetText("Press [Mouse1] To grab");
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (holdingItem) return;
                grabbableObject = rayHit.collider.GetComponentInParent<GrabbableObject>();
                grabbableObject.Grab(grabbedObjTarget);
                holdingItem = true;
            }
        }

        else
        {
            if (lookingAtGrabbable)
            {
                lookingAtGrabbable = false;
                UIManager.Instance.uiBottomMessage.SetText("");
            }
        }

        if (!holdingItem) return;
        if (Input.GetMouseButtonUp(0))
        {
            if (grabbableObject != null) grabbableObject.Release();
            holdingItem = false;
            grabbableObject = null;
        }

    }
}
