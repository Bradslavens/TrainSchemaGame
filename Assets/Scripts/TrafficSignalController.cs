using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrafficSignalController : MonoBehaviour
{
    public AudioClip buzzerSound;
    public int numberOfOptions = 4;

    private TrainCarController trainCarController;
    private AudioSource audioSource;
    private List<GameObject> signals;
    private List<GameObject> selectedSignals;
    private TMP_Text[] answerButtonTexts;

    private GameObject multipleChoiceUI;
    private Button[] answerButtons;


    private void Start()
    {
        trainCarController = FindObjectOfType<TrainCarController>();
        audioSource = GetComponent<AudioSource>();

        multipleChoiceUI.SetActive(false);

        signals = new List<GameObject>(GameObject.FindGameObjectsWithTag("Signal"));

        // Remove the current signal from the list of possible signals
        signals.Remove(gameObject);

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

        selectedSignals = SelectRandomSignals(numberOfOptions - 1);
        selectedSignals.Add(gameObject);
        selectedSignals = ShuffleList(selectedSignals);

        for (int i = 0; i < answerButtonTexts.Length; i++)
        {
            int index = i;
            answerButtonTexts[i].text = selectedSignals[i].name;
            answerButtonTexts[i].GetComponentInParent<Button>().onClick.RemoveAllListeners();
            answerButtonTexts[i].GetComponentInParent<Button>().onClick.AddListener(() => CheckAnswer(index));
        }
    }

    private List<GameObject> SelectRandomSignals(int count)
    {
        List<GameObject> randomSignals = new List<GameObject>();
        List<GameObject> tempList = new List<GameObject>(signals);

        while (count > 0 && tempList.Count > 0)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            randomSignals.Add(tempList[randomIndex]);
            tempList.RemoveAt(randomIndex);
            count--;
        }

        return randomSignals;
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
        if (selectedSignals[buttonIndex] == gameObject)
        {
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

}
