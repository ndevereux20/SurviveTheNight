using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Dropdown collisionTypeDropdown;
    public Slider slider; 

    public GameObject player;
    public GameObject bulletPrefab;
    public GameObject zombie;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("PlayScene");
        PauseMenu.instance.Resume();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CollisionDetectionDropdown()
    {
        switch (collisionTypeDropdown.value)
        {
            case 0: // Continuous
                player.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                bulletPrefab.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                zombie.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                break;
            case 1: //Discrete
                player.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Discrete;
                bulletPrefab.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Discrete;
                zombie.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Discrete;
                break;
        }
    }

    public void ChangeBulletSpeed()
    {
        Shooting.instance.bulletSpeed = slider.value;
    }
}
