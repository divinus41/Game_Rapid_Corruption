using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotSpawnerScript : MonoBehaviour {

    private int randomNumber;
    private Transform randomChild;
    private int spawnTimer;
    private int spawnIntervall;
    public GameObject carrotPrefab;

	
	void Awake () {
        spawnIntervall = 1500;
    }
	
	
	void Update () {
        spawnTimer++;
        GameObject carrot;

        //get random child and spawn a carrot at its location if the slot is empty
        if (spawnTimer >= spawnIntervall)
        {
            randomNumber = Random.Range(0, transform.childCount);
            randomChild = gameObject.transform.GetChild(randomNumber);

            //only if there isn't a carrot at the childs location already
            if (randomChild.childCount == 0)
            {
                carrot = Instantiate(carrotPrefab, randomChild.transform);
                //reset spawnTimer
                spawnTimer = 0;
            }
            
        }
	}
}
