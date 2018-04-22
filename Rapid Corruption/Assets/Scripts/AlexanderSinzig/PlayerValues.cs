using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerValues : MonoBehaviour {

    public float headingDirection;

    //für punktestand verwendbar
    /*
    //for gold management and showing label
    public int playerNewGold;
    private int playerCurrentGold;
    private bool showGold = false;
    private int labelTimer;
    private int maxLabelTimer;
    */

    private void Awake()
    {
        /*
        playerNewGold = 0;
        playerCurrentGold = 0;
        labelTimer = 0;
        maxLabelTimer = 90;
        */
    }

    void Update () {

        //headingDirection is set to the y-rotation the player has and is updated as long as the player holds the button
		if (Input.GetButton("Fire1"))
        {
            headingDirection = transform.rotation.eulerAngles.y;
        }

        /*
        //show the amount of Gold for a short time, when the amount changes
        if (playerCurrentGold != playerNewGold)
        {
            playerCurrentGold = playerNewGold;
            showGold = true;
            labelTimer = maxLabelTimer;
        }

        //if the timer reaches 0, the label is no longer shown
        if (labelTimer > 0)
        {
            labelTimer--;
        }
        else
        {
            showGold = false;
        }
        */
	}

    /*
    /// <summary>
    /// the label wich is shown every time you collect gold
    /// </summary>
    void OnGUI()
    {
        
        if (showGold)
            GUI.Label(new Rect(100, 10, 200, 40), "Gold: " + playerCurrentGold);
    }
    */
}
