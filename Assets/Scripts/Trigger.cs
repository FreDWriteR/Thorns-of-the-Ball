using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class Trigger : MonoBehaviour
{
    // Start is called before the first frame update

    public int countAttempts;

    public int SecCount;

    private const string FileCountAttempts = "FileCountAttempts.txt";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameOver();
    }

    

    void GameOver()
    {

        if (File.Exists(FileCountAttempts)) //Считываем количество попыток из файла
        {
            var fileContent = File.ReadAllText(FileCountAttempts);
            countAttempts = Int32.Parse(fileContent);
        }
        countAttempts++; //Прибавляем попытку 
        File.WriteAllText(FileCountAttempts, countAttempts.ToString()); //Записываем в файл
        SceneManager.LoadScene("SceneGameOver"); //Загружаем сцену КонецИгры
        
    }
}
