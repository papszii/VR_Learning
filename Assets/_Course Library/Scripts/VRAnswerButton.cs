using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class VRAnswerButton : MonoBehaviour
{
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private int answerIndex;
    
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;
    private Color originalColor;
    private Image buttonImage;

    private void Start()
    {
        // XRSimpleInteractable komponens
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        
        if (interactable == null)
        {
            interactable = gameObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        }

        // Interactable event-ek
        interactable.selectEntered.AddListener(OnButtonSelected);
        interactable.selectExited.AddListener(OnButtonDeselected);

        // Button image referencia
        buttonImage = GetComponent<Image>();
        if (buttonImage != null)
        {
            originalColor = buttonImage.color;
        }

        Debug.Log($"VRAnswerButton {answerIndex} inicializálva!");
    }

    private void OnButtonSelected(SelectEnterEventArgs args)
    {
        // Gomb megnyomása - válasz kiválasztása
        if (quizManager != null)
        {
            Debug.Log($"Válasz {answerIndex} kiválasztva!");
            // A QuizManager SelectAnswer metódusát hívjuk
            quizManager.SelectAnswerVR(answerIndex);
        }

        // Vizuális feedback - gomb villanása
        if (buttonImage != null)
        {
            buttonImage.color = Color.yellow;
        }
    }

    private void OnButtonDeselected(SelectExitEventArgs args)
    {
        // Gomb elengedése
        if (buttonImage != null)
        {
            buttonImage.color = originalColor;
        }
    }
}
