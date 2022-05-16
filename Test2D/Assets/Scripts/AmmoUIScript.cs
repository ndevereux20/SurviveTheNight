using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUIScript : MonoBehaviour
{
    public Text ammo;
    public Text ammoRemaining;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // For the ammo in the current clip
        if(Shooting.instance.curClipAmmo < 10)
        {
            ammo.text = "0" + Shooting.instance.curClipAmmo;
        }
        else if(Shooting.instance.curClipAmmo >= 10)
        {
            ammo.text = "" + Shooting.instance.curClipAmmo;
        }

        //for the remaining ammo in the gun 
        if(Shooting.instance.curAmmo >= 10)
        {
            ammoRemaining.text = "" + Shooting.instance.curAmmo; 
        }
        else if (Shooting.instance.curAmmo < 10)
        {
            ammoRemaining.text = "0" + Shooting.instance.curAmmo;
        }
    }
}
