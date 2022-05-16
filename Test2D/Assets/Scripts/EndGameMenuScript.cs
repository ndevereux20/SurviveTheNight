using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameMenuScript : MonoBehaviour
{
    public TextMeshProUGUI endGameText;

    // Start is called before the first frame update
    void Start()
    {
        if (TimeUIController.instance.youWin == true)
        {
            endGameText.text = "CONGRATULATIONS! YOU SURVIVED THE NIGHT!";
        }
        else
        {
            endGameText.text = "YOU FAILED TO SURVIVE THE NIGHT!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
