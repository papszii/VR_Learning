using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRQuizTrigger : MonoBehaviour
{
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private Canvas quizCanvas;
    
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;

    private void Start()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnQuizTrigger);
        }
        
        // Quiz kezdetben legyen ki
        if (quizCanvas != null)
            quizCanvas.gameObject.SetActive(false);
    }

    private void OnQuizTrigger(SelectEnterEventArgs args)
    {
        // Quiz canvas megjelenítése
        if (quizCanvas != null)
        {
            quizCanvas.gameObject.SetActive(true);
        }

        // Quiz reset
        if (quizManager != null)
        {
            quizManager.ResetQuiz();
        }

        Debug.Log("Quiz megnyitva!");
    }

    public void CloseQuiz()
    {
        if (quizCanvas != null)
            quizCanvas.gameObject.SetActive(false);
    }
}
