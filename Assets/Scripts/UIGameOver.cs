using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    public GameObject Thorn;
    private Foldout hardLevels;

    private const string FileCountAttempts = "FileCountAttempts.txt";
    private const string fileSpeed = "fileSpeed.txt";

    private void OnEnable() //Функционал UI последней сцены
    {
        VisualElement root = gameObject.GetComponent<UIDocument>().rootVisualElement;
        string countAttempts = "";
        Label LastTry = root.Q<Label>("LastTry");
        Label TryCount = root.Q<Label>("TryCount");

        hardLevels = root.Q<Foldout>("hardLevels");
        Button buttonLow = hardLevels.contentContainer.Q<Button>("Low");
        Button buttonMiddle = hardLevels.contentContainer.Q<Button>("Middle");
        Button buttonHard = hardLevels.contentContainer.Q<Button>("Hard");

        Button buttonRestart = root.Q<Button>("Restart");

        buttonLow.clicked += () => speedGame(20);
        buttonMiddle.clicked += () => speedGame(30);
        buttonHard.clicked += () => speedGame(40);

        buttonRestart.clicked += () => SceneManager.LoadScene("GameScene");

        LastTry.text = "Вы продержались: " + ((int)Time.time - Thorn.GetComponent<Trigger>().SecCount).ToString() + " сек.";
        if (File.Exists(FileCountAttempts))
        {
            var fileContent = File.ReadAllText(FileCountAttempts);
            countAttempts = Int32.Parse(fileContent).ToString();
            TryCount.text = "Всего попыток: " + countAttempts;
        }
        else
            TryCount.text = "Всего попыток: 0";
    }

    void speedGame(int speed)
    {
        File.WriteAllText(fileSpeed, speed.ToString());
        hardLevels.value = false;
    }
}
