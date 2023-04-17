using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObstacleController : MonoBehaviour
{
    public AudioClip buzzerSound;
    public int numberOfOptions = 4;

    private TrainCarController trainCarController;
    private AudioSource audioSource;
    private List<GameObject> obstacles;
    private List<GameObject> selectedObstacles;
    private TMP_Text[] answerButtonTexts;

    private GameObject multipleChoiceUI;

    private Button[] answerButtons;

    public static int totalQuestions = 0;
    public static int correctAnswers = 0;

    private void Start()
    {
        trainCarController = FindObjectOfType<TrainCarController>();
        audioSource = GetComponent<AudioSource>();

        if (multipleChoiceUI != null)
        {
            multipleChoiceUI.SetActive(false);
        }

        GameObject[] allObstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        obstacles = new List<GameObject>();

        foreach (GameObject obstacle in allObstacles)
        {
            BoxCollider2D collider = obstacle.GetComponent<BoxCollider2D>();
            if (collider != null && collider.enabled)
            {
                obstacles.Add(obstacle);
            }
        }

        // Remove the current obstacle from the list of possible obstacles
        obstacles.Remove(gameObject);

        // Assign the TMP_Text components at runtime
        answerButtonTexts = new TMP_Text[answerButtons.Length];
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtonTexts[i] = answerButtons[i].GetComponentInChildren<TMP_Text>();
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

        selectedObstacles = SelectRandomObstacles(numberOfOptions - 1);
        selectedObstacles.Add(gameObject);
        selectedObstacles = ShuffleList(selectedObstacles);

        for (int i = 0; i < answerButtonTexts.Length; i++)
        {
            int index = i;
            answerButtonTexts[i].text = selectedObstacles[i].name;
            answerButtonTexts[i].GetComponentInParent<Button>().onClick.RemoveAllListeners();
            answerButtonTexts[i].GetComponentInParent<Button>().onClick.AddListener(() => CheckAnswer(index));
        }
    }

    private List<GameObject> SelectRandomObstacles(int count)
    {
        List<GameObject> randomObstacles = new List<GameObject>();
        List<GameObject> tempList = new List<GameObject>(obstacles);

        while (count > 0 && tempList.Count > 0)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            randomObstacles.Add(tempList[randomIndex]);
            tempList.RemoveAt(randomIndex);
            count--;
        }

        return randomObstacles;
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

        if (selectedObstacles[buttonIndex] == gameObject)
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

