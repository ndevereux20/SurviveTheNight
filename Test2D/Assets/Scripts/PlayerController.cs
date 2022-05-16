using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance; 
    public GameObject player;
    public GameObject ammo;
    public GameObject health; 
    public float speed = 1.0f;
    public float curHealth;
    public float maxHealth = 5;

    Animator anim; 
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        curHealth = maxHealth;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseMenu.instance.gameIsPaused == false)
        {
            MovePlayer();
        }
        
    }
    
    void MovePlayer()
    {
        // moves the player using keyboard controlls 
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        
        if (move != Vector3.zero)
        {
            anim.SetBool("is_walking", true);
            anim.SetFloat("input_x", move.x);
            anim.SetFloat("input_y", move.y);
        }
        else
        {
            anim.SetBool("is_walking", false);
        }
 
        transform.position += move * speed * Time.deltaTime;
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        // Destroys the player if a zombie hits it and maks hit points zero
        if (other.gameObject.tag == "Zombie")
        {
            curHealth = curHealth - 1;
            if (curHealth == 0)
            {
                Destroy(player);
                TimeUIController.instance.youWin = false;
                SceneManager.LoadScene("EndGame");
            }

        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Health")
        {
            if(curHealth != maxHealth)
            {
                curHealth = curHealth + 1;
                Destroy(other.gameObject);
            }
        }
        if (other.gameObject.tag == "Ammo")
        {
            Shooting.instance.curAmmo = Shooting.instance.curAmmo + 30;
            if (Shooting.instance.curClipAmmo == 0)
            {
                StartCoroutine(Shooting.instance.Reload());
            }
            Destroy(other.gameObject);
        }
    }   

    
}
