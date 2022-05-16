using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance; 
    public bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public Dropdown collisionTypeDropdown;
    public Slider slider; // changing bullet speed 

    public GameObject player;
    public GameObject activePlayer;
    public GameObject bulletPrefab;
    public GameObject[] bullets;
    public GameObject zombie;
    public GameObject[] zombies;

    // Start is called before the first frame update
    void Start()
    {
        instance = this; 
        activePlayer = GameObject.FindGameObjectsWithTag("Player")[0];  //returns GameObject[]
    }
    // Update is called once per frame
    void Update()
    {
        zombies = GameObject.FindGameObjectsWithTag("Zombie");  //returns GameObject[]
        bullets = GameObject.FindGameObjectsWithTag("Bullet");  //returns GameObject[]

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void CollisionDetectionDropdown()
    {
        switch(collisionTypeDropdown.value){
            case 0: // Continuous
                player.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                activePlayer.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;

                bulletPrefab.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                for(int i = 0; i < bullets.Length; i++)
                {
                    bullets[i].GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                }

                zombie.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                for (int i = 0; i < zombies.Length; i++)
                {
                    zombies[i].GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                }

                break;
            case 1: //Discrete
                player.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Discrete;
                activePlayer.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Discrete;

                bulletPrefab.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Discrete;
                for (int i = 0; i < bullets.Length; i++)
                {
                    bullets[i].GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Discrete;
                }

                zombie.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Discrete;
                for (int i = 0; i < zombies.Length; i++)
                {
                    zombies[i].GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Discrete;
                }
                break;
        }   
    }
    public void ChangeBulletSpeed()
    {
        Shooting.instance.bulletSpeed = slider.value;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
}
