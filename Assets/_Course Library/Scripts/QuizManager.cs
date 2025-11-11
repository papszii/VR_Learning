using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] answers = new string[4];
    public int correctAnswerIndex = 0;
    public int points = 10;
}

public class QuizManager : MonoBehaviour
{
    [SerializeField] private Question[] questions;
    [SerializeField] private ScoreManager scoreManager;

    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button[] answerButtons = new Button[4];
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private Button nextButton;
    [SerializeField] private TextMeshProUGUI progressText; // pl: "1/10"

    [SerializeField] private Color correctColor = Color.green;
    [SerializeField] private Color incorrectColor = Color.red;
    [SerializeField] private Color defaultColor = Color.white;

    private int currentQuestionIndex = 0;
    private bool questionAnswered = false;

    private void Start()
    {
        if (questions.Length == 0)
        {
            Debug.LogError("Nincsenek kérdések a quiz-ben!");
            return;
        }

        // Answer buttonok clickeire
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int answerIndex = i;
            answerButtons[i].onClick.AddListener(() => SelectAnswer(answerIndex));
        }

        // Next button
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(NextQuestion);
            nextButton.gameObject.SetActive(false);
        }

        LoadQuestion(0);
    }

    private void LoadQuestion(int questionIndex)
    {
        if (questionIndex >= questions.Length)
        {
            QuizVege();
            return;
        }

        currentQuestionIndex = questionIndex;
        Question currentQuestion = questions[questionIndex];

        // Kérdés szövege
        questionText.text = currentQuestion.questionText;

        // Válaszok betöltése
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < currentQuestion.answers.Length && !string.IsNullOrEmpty(currentQuestion.answers[i]))
            {
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[i];
                answerButtons[i].gameObject.SetActive(true);
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }

            // Button szín zurítása
            ColorBlock colors = answerButtons[i].colors;
            colors.normalColor = defaultColor;
            answerButtons[i].colors = colors;
        }

        // UI frissítés
        feedbackText.text = "";
        if (nextButton != null)
            nextButton.gameObject.SetActive(false);

        // Progress szöveg
        if (progressText != null)
            progressText.text = $"{questionIndex + 1}/{questions.Length}";

        questionAnswered = false;

        // Buttonok interaktívak
        foreach (Button btn in answerButtons)
        {
            btn.interactable = true;
        }
    }

    public void SelectAnswer(int answerIndex)
    {
        if (questionAnswered) return;

        Question currentQuestion = questions[currentQuestionIndex];
        questionAnswered = true;

        // Buttonok kikapcsolása
        foreach (Button btn in answerButtons)
        {
            btn.interactable = false;
        }

        bool isCorrect = answerIndex == currentQuestion.correctAnswerIndex;

        // Helyes válasz zöld, helytelen piros
        ColorBlock correctColors = answerButtons[currentQuestion.correctAnswerIndex].colors;
        correctColors.normalColor = correctColor;
        answerButtons[currentQuestion.correctAnswerIndex].colors = correctColors;

        if (!isCorrect)
        {
            ColorBlock incorrectColors = answerButtons[answerIndex].colors;
            incorrectColors.normalColor = incorrectColor;
            answerButtons[answerIndex].colors = incorrectColors;
        }

        // Feedback és pontozás
        if (isCorrect)
        {
            feedbackText.text = "Helyes! ✓";
            feedbackText.color = correctColor;

            if (scoreManager != null)
            {
                scoreManager.AddScore(currentQuestion.points);
                Debug.Log($"+{currentQuestion.points} pont! Összesen: {scoreManager.GetScore()}");
            }
        }
        else
        {
            feedbackText.text = "Helytelen! ✗";
            feedbackText.color = incorrectColor;
        }

        // Next button megjelenítése
        if (nextButton != null)
            nextButton.gameObject.SetActive(true);
    }

    // VR verzió - XRSimpleInteractable-ből hívódik
    public void SelectAnswerVR(int answerIndex)
    {
        SelectAnswer(answerIndex);
    }

    private void NextQuestion()
    {
        if (currentQuestionIndex + 1 < questions.Length)
        {
            LoadQuestion(currentQuestionIndex + 1);
        }
        else
        {
            QuizVege();
        }
    }

    private void QuizVege()
    {
        questionText.text = "Quiz vége!";
        feedbackText.text = $"Gratulálunk! Végső pontszám: {scoreManager.GetScore()}";
        feedbackText.color = correctColor;

        foreach (Button btn in answerButtons)
        {
            btn.gameObject.SetActive(false);
        }

        if (nextButton != null)
        {
            nextButton.GetComponentInChildren<TextMeshProUGUI>().text = "Újra";
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(() => LoadQuestion(0));
            nextButton.gameObject.SetActive(true);
        }
    }

    public void ResetQuiz()
    {
        currentQuestionIndex = 0;
        questionAnswered = false;
        LoadQuestion(0);
    }
}
