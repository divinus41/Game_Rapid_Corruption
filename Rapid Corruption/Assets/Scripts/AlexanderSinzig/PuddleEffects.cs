using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleEffects : MonoBehaviour {

    #region variables
    private Vector3 trashPosition;
    private string objectName;
    private GameObject collidingBaby;
    private BabyRabbitControl babyRabbitControlScript;
    private Transform child;

    private int damageIntervall;
    private int intervallCounter;
    #endregion


    void Awake () {
        damageIntervall = 90;
        intervallCounter = damageIntervall;

        objectName = gameObject.name;
        //puddle-area saves its position wich equals the puddle-position
        if (objectName != "Puddle")
        {
            trashPosition = transform.position;
            //child = gameObject.transform.GetChild(0);
            //puddlePosition = child.position;
        }
	}

    #region Trigger
    private void OnTriggerEnter (Collider other)
    {
        //only if a RabbitBaby enters the area
        if (objectName != "Puddle" && other.gameObject.tag == "BabyRabbit")
        {
            //get acess to the script of the colliding RabbitBaby
            collidingBaby = other.gameObject;
            babyRabbitControlScript = collidingBaby.GetComponent<BabyRabbitControl>();
            //send position of the puddle
            if (trashPosition != new Vector3(0f, 0f, 0f))
            {

                babyRabbitControlScript.EnterArea(objectName, trashPosition);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //only if a RabbitBaby leaves the area
        if (objectName != "Puddle" && other.gameObject.tag == "BabyRabbit")
        {
            //get acess to the script of the colliding RabbitBaby
            collidingBaby = other.gameObject;
            babyRabbitControlScript = collidingBaby.GetComponent<BabyRabbitControl>();
            //send position of the puddle
            if (trashPosition != new Vector3(0f, 0f, 0f))
            {
                babyRabbitControlScript.LeaveArea(objectName, trashPosition);
            }
        }
    }

    /// <summary>
    /// The puddle deals damage to all BabyRabbits inside the trigger zone, in an intervall
    /// </summary>
    /// <param name="other">the object inside the trigger zone</param>
    private void OnTriggerStay(Collider other)
    {
        if (objectName == "Puddle")
        {
            if (other.gameObject.tag == "BabyRabbit")
            {
                //get acess to the script of the colliding RabbitBaby
                collidingBaby = other.gameObject;
                babyRabbitControlScript = collidingBaby.GetComponent<BabyRabbitControl>();

                //count down or dmage the BabyRabbit
                if (intervallCounter > 0)
                {
                    intervallCounter--;
                }
                else
                {
                    intervallCounter = damageIntervall;
                    babyRabbitControlScript.TakeDamage();
                }
            }
        }
    }
    #endregion
}
