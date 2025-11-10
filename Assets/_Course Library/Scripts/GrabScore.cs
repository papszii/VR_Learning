using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabScore : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private int pointsPerGrab = 10;
    
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private bool hasBeenGrabbed = false;

    private void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (!hasBeenGrabbed && scoreManager != null)
        {
            scoreManager.AddScore(pointsPerGrab);
            hasBeenGrabbed = true;
            Debug.Log($"{gameObject.name} megfogva! +{pointsPerGrab} pont!");
        }
    }

    public void ResetGrab()
    {
        hasBeenGrabbed = false;
    }
}
