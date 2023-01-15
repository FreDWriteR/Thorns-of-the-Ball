using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using UnityEngine.SceneManagement;

public class UIBegin : MonoBehaviour
{
    [SerializeField] private GameObject Wall;
    private Foldout hardLevels;


    private const string fileSpeed = "fileSpeed.txt";
    
    
    private void OnEnable() //Функционал UI первой сцены
    {
        VisualElement root = gameObject.GetComponent<UIDocument>().rootVisualElement;

        Button buttonStart = root.Q<Button>("buttonStart");

        buttonStart.clicked += () => SceneManager.LoadScene("GameScene");

        hardLevels = root.Q<Foldout>("hardLevels");
        Button buttonLow = hardLevels.contentContainer.Q<Button>("Low");
        Button buttonMiddle = hardLevels.contentContainer.Q<Button>("Middle");
        Button buttonHard = hardLevels.contentContainer.Q<Button>("Hard");


        buttonLow.clicked += () => speedGame(0.1f);
        buttonMiddle.clicked += () => speedGame(0.3f);
        buttonHard.clicked += () => speedGame(0.5f);
    }

    void speedGame(float speed)
    {
        File.WriteAllText(fileSpeed, speed.ToString());
        hardLevels.value = false;
    }
}
