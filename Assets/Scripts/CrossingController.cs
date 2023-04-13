using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrossingController : MonoBehaviour
{
    public AudioClip buzzerSound;
    public int numberOfOptions = 4;

    private TrainCarController trainCarController;
    private AudioSource audioSource;
    private List<GameObject> crossings;
    private List<GameObject> selectedCrossings;
    private TMP_Text[] answerButtonTexts;

    public GameObject multipleChoiceUI; // Make it public

    public Button[] answerButtons; // Make it public

    public static int totalQuestions = 0;
    public static int correctAnswers = 0;

    private void Start()
    {
        trainCarController = FindObjectOfType<TrainCarController>();
        audioSource = GetComponent<AudioSource>();

        if (multipleChoiceUI == null)
        {
            Debug.LogWarning("multipleChoiceUI is not assigned.");
        }
        else
        {
            multipleChoiceUI.SetActive(false);
        }

        if (answerButtons == null || answerButtons.Length == 0)
        {
            Debug.LogWarning("answerButtons are not assigned.");
        }

        crossings = new List<GameObject>(GameObject.FindGameObjectsWithTag("Crossing"));

        // Remove the current crossing from the list of possible crossings
        crossings.Remove(gameObject);

        // Assign the TMP_Text components at runtime
        if (answerButtons != null)
        {
            answerButtonTexts = new TMP_Text[answerButtons.Length];
            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerButtonTexts[i] = answerButtons[i].GetComponentInChildren<TMP_Text>();
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TrainCar"))
        {
            trainCarController.StopTrainCar();
            ShowMultipleChoiceUI();
        }
    }

    private void ShowMultipleChoiceUI()
    {
        multipleChoiceUI.SetActive(true);

        selectedCrossings = SelectRandomCrossings(numberOfOptions - 1);
        selectedCrossings.Add(gameObject);
        selectedCrossings = ShuffleList(selectedCrossings);

        for (int i = 0; i < answerButtonTexts.Length; i++)
        {
            int index = i;
            answerButtonTexts[i].text = selectedCrossings[i].name;
            answerButtonTexts[i].GetComponentInParent<Button>().onClick.RemoveAllListeners();
            answerButtonTexts[i].GetComponentInParent<Button>().onClick.AddListener(() => CheckAnswer(index));
        }
    }

    private List<GameObject> SelectRandomCrossings(int count)
    {
        List<GameObject> randomCrossings = new List<GameObject>();
        List<GameObject> tempList = new List<GameObject>(crossings);

        while (count > 0 && tempList.Count > 0)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            randomCrossings.Add(tempList[randomIndex]);
            tempList.RemoveAt(randomIndex);
            count--;
        }

        return randomCrossings;
    }

    private List<GameObject> ShuffleList(List<GameObject> inputList)
    {
        List<GameObject> randomList = new List<GameObject>();

        while (inputList.Count > 0)
        {
            int randomIndex = Random.Range(0, inputList.Count);
            randomList.Add(inputList[randomIndex]);
            inputList.RemoveAt(randomIndex);
        }

        return randomList;
    }

    private void CheckAnswer(int buttonIndex)
    {
        totalQuestions++; // Increment the total questions

        if (selectedCrossings[buttonIndex] == gameObject)
        {
            correctAnswers++; // Increment the correct answers
            multipleChoiceUI.SetActive(false);
            trainCarController.ResumeTrainCar();
        }
        else
        {
            answerButtonTexts[buttonIndex].text = "X";
            PlayBuzzerSound();
        }
    }

    private void PlayBuzzerSound()
    {
        if (buzzerSound != null)
        {
            audioSource.PlayOneShot(buzzerSound);
        }
    }

    public void AssignUIElements(GameObject ui, Button[] buttons)
    {
        multipleChoiceUI = ui;
        answerButtons = buttons;

        // Assign the TMP_Text components at runtime
        answerButtonTexts = new TMP_Text[answerButtons.Length];
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtonTexts[i] = answerButtons[i].GetComponentInChildren<TMP_Text>();
        }
    }

    public static float CalculateScore()
    {
        return (float)correctAnswers / totalQuestions;
    }

}
