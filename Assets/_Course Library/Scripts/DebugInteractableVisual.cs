using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DebugInteractableVisual : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;
    private Renderer cubeRenderer;
    private Color originalColor;

    private void Start()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        cubeRenderer = GetComponent<Renderer>();
        
        if (cubeRenderer != null)
        {
            originalColor = new Color(1, 1, 1, 1); // Fehér
            cubeRenderer.material.color = originalColor;
        }
        
        if (interactable != null)
        {
            interactable.hoverEntered.AddListener(OnHoverEnter);
            interactable.hoverExited.AddListener(OnHoverExit);
            interactable.selectEntered.AddListener(OnSelectEnter);
            interactable.selectExited.AddListener(OnSelectExit);
        }
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        Debug.Log("🟡 HOVER ENTER!");
        if (cubeRenderer != null)
            cubeRenderer.material.color = Color.yellow; // Sárga
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        Debug.Log("⚪ HOVER EXIT");
        if (cubeRenderer != null)
            cubeRenderer.material.color = originalColor; // Fehér
    }

    private void OnSelectEnter(SelectEnterEventArgs args)
    {
        Debug.Log("🟢 SELECT ENTER - ÉRINTVE!");
        if (cubeRenderer != null)
            cubeRenderer.material.color = Color.green; // Zöld
    }

    private void OnSelectExit(SelectExitEventArgs args)
    {
        Debug.Log("🔴 SELECT EXIT");
        if (cubeRenderer != null)
            cubeRenderer.material.color = originalColor; // Fehér
    }
}
