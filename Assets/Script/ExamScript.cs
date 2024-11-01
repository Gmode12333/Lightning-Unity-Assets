using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExamScript : MonoBehaviour
{
    [Header("Title TextMeshPro Question")]
    public TextMeshProUGUI header;

    [Header("Buttons A B C D")]
    public List<Button> buttons = new List<Button>();

    [Header("Question and Answer")]
    public List<QuestionAndAnswer> questions = new List<QuestionAndAnswer>();

    [Header("GameObject And TextMesh")]
    public GameObject start_obj;
    public GameObject final_obj;
    public TextMeshProUGUI Total;
    public TextMeshProUGUI Done;
    public TextMeshProUGUI Correct;
    public TextMeshProUGUI Result;

    private int currentIndex = 0;
    private int correctAnswer = 0;
    private Dictionary<int, int> keyValuePairs = new Dictionary<int, int>    {
            {0, 1},
            {1, 3},
            {2, 3},
            {3, 1},
            {4, 2},
            {5, 4},
            {6, 1},
            {7, 1},
            {8, 3},
            {9, 4},
    };

    [Header("CheckButton")]
    public Button _nextButton;
    public Button _prevButton;

    private Dictionary<int, int> answerValuePairs = new Dictionary<int, int>();
    public void TakingQuestionByCurrentIndex()
    {
        header.text = questions[currentIndex].Question;
        for (int i = 0; i < questions[currentIndex].answers.Count; i++)
        {
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = questions[currentIndex].answers[i];
            buttons[i].image.color = Color.white;
        }
    }

    public void CheckingQuestionByCurrentIndex()
    {
        header.text = questions[currentIndex].Question;
        for (int i = 0; i < questions[currentIndex].answers.Count; i++)
        {
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = questions[currentIndex].answers[i];
            buttons[i].image.color = Color.white;
        }
        
        if (keyValuePairs.TryGetValue(currentIndex, out int value))
        {
            if (answerValuePairs.ContainsKey(currentIndex) && answerValuePairs[currentIndex] == value)
            {
                buttons[value - 1].image.color = Color.green;
            }
            else if (answerValuePairs.ContainsKey(currentIndex) && answerValuePairs[currentIndex] != value)
            {
                buttons[value - 1].image.color = Color.green;
                buttons[answerValuePairs[currentIndex] - 1].image.color = Color.red;
            }
        }
    }
    public void NextQuestion(int index)
    {
        answerValuePairs[currentIndex] = index;
        currentIndex++;

        if (currentIndex < questions.Count)
        {
            TakingQuestionByCurrentIndex();
        }
        else
        {
            CheckingRightAnswers();
        }
    }

    public void CheckingRightAnswers()
    {
        foreach (var answer in keyValuePairs)
        {
            if (answerValuePairs.TryGetValue(answer.Key, out int value) && answer.Value == value)
            {
                correctAnswer++;
            }
        }

        Total.text = "Tổng số câu:  " + keyValuePairs.Count.ToString();
        Done.text = "Tổng số câu đã làm:    " + answerValuePairs.Count.ToString();
        Correct.text = "Số câu đúng:    " + correctAnswer.ToString();

        if (correctAnswer >= 0 && correctAnswer <= 4)
        {
            Result.text = "Bạn chưa đạt yêu cầu";
        }
        else if (correctAnswer >= 5 && correctAnswer <= 6)
        {
            Result.text = "Bạn đạt mức yêu cầu trung bình";
        }
        else if (correctAnswer >= 7 && correctAnswer <= 8)
        {
            Result.text = "Bạn đạt mức yêu cầu khá";
        }
        else
        {
            Result.text = "Bạn nắm nội dung rất tốt";
        }

        final_obj.SetActive(true);
        start_obj.SetActive(false);
    }

    public void ResetFromStart()
    {
        currentIndex = 0;
        correctAnswer = 0;
        answerValuePairs = new Dictionary<int, int>();
        final_obj.SetActive(false);
        start_obj.SetActive(true);

        TakingQuestionByCurrentIndex();
    }

    public void SeeAnswer(int checkingIndex)
    {
        start_obj.SetActive(true);
        final_obj.SetActive(false);

        currentIndex += checkingIndex;
        if (currentIndex <= 0)
        {
            currentIndex = 0;
            _nextButton.gameObject.SetActive(true);
            _prevButton.gameObject.SetActive(false);
        }
        else if (currentIndex > questions.Count - 1)
        {
            currentIndex = questions.Count - 1;
            final_obj.SetActive(true);
            start_obj.SetActive(false);

            _nextButton.gameObject.SetActive(false);
            _prevButton.gameObject.SetActive(false);
        }
        else
        {
            _nextButton.gameObject.SetActive(true);
            _prevButton.gameObject.SetActive(true);
        }

        CheckingQuestionByCurrentIndex();
    }
}
[Serializable]

public class QuestionAndAnswer
{
    public string Question;

    public List<string> answers = new List<string>();
}
