using UnityEngine;
using UnityEngine.UI;

public class DemoPlayer : MonoBehaviour
{
    // Value types
    private bool allTrashes;
    private int backpack;
    private int paperTrash, plasticTrash;

    // Refernce types
    public Canvas playerCanvas, cameraCanvas;
    public Slider backpackSlider;
    public Text numberOfPaperTextComponent;
    public Text numberOfPlasticTextComponent;
    public Text numberOfTotalGarbages;
    public Text[] triggerZoneTexts;

    // Use this for initialization
    void Start()
    {
        // Set the standart value for the boolean.
        allTrashes = false;

        // Set the standart value for the text components from the triggerzone.
        triggerZoneTexts[0].enabled = true;
        triggerZoneTexts[1].enabled = false;
        triggerZoneTexts[2].enabled = false;
        triggerZoneTexts[3].enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // By press the button 'F', than see the HUD.
        if (Input.GetAxis("HUD") > 0)
        {
            playerCanvas.enabled = true;
            cameraCanvas.enabled = true;
        }
        else if (Input.GetAxis("HUD") <= 0)
        {
            playerCanvas.enabled = false;
            cameraCanvas.enabled = false;
        }

        // Check, if the player has all trashes.
        if (backpack >= 8)
            allTrashes = true;
    }

    /// <summary>
    /// Enable the text component from the triggerzone.
    /// </summary>
    /// <param name="other">Text component from the triggerzone.</param>
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "One":
                triggerZoneTexts[0].enabled = true;
                triggerZoneTexts[1].enabled = false;
                triggerZoneTexts[2].enabled = false;
                triggerZoneTexts[3].enabled = false;
                break;
            case "Two":
                triggerZoneTexts[0].enabled = false;
                triggerZoneTexts[1].enabled = true;
                triggerZoneTexts[2].enabled = false;
                triggerZoneTexts[3].enabled = false;
                break;
            case "Three":
                triggerZoneTexts[0].enabled = false;
                triggerZoneTexts[1].enabled = false;
                triggerZoneTexts[2].enabled = true;
                triggerZoneTexts[3].enabled = false;
                break;
            case "Four":
                triggerZoneTexts[0].enabled = false;
                triggerZoneTexts[1].enabled = false;
                triggerZoneTexts[2].enabled = false;
                triggerZoneTexts[3].enabled = true;
                break;
        }
    }

    /// <summary>
    /// By trigger with campsite and press button 'Q', than lay down the trashes. 
    /// </summary>
    /// <param name="other">Was effected by.</param>
    private void OnTriggerStay(Collider other)
    {
        // ---------- Get the trash (Destroy). ----------
        if (Input.GetAxisRaw("Fire2") > 0)
        {
            // Have you a place in your backpack?
            if (backpack < 10)
            {
                // Is a getable trash?
                if (other.CompareTag("Paper"))
                {
                    paperTrash++;
                    Debug.Log("You get the trash!");
                    Destroy(other.gameObject);
                    backpack++;
                }
                else if (other.CompareTag("Plastic"))
                {
                    plasticTrash++;
                    Debug.Log("You get the trash!");
                    Destroy(other.gameObject);
                    backpack++;
                }
            }
            else
                Debug.Log("You have too much trash in your backpack!");
        }

        // ---------- Lay down the trash. ----------
        if (allTrashes)
        {
            if (Input.GetAxisRaw("Fire2") < 0)
            {
                // Is a setable trash?
                if (other.CompareTag("PaperTent"))
                    KillTrashes(ref paperTrash, ref backpack);
                else if (other.CompareTag("PlasticTent"))
                    KillTrashes(ref plasticTrash, ref backpack);
            }
        }

        // Set Values for hud.
        backpackSlider.value = backpack;
        numberOfPaperTextComponent.text = paperTrash.ToString();
        numberOfPlasticTextComponent.text = plasticTrash.ToString();
        numberOfTotalGarbages.text = backpack.ToString();
    }

    /// <summary>
    /// Kill trashes from backpack.
    /// </summary>
    /// <param name="trash">Number of trash-kind.</param>
    /// <param name="backpack">Number of trashes in backpack.</param>
    void KillTrashes(ref int trash, ref int backpack)
    {
        // When have a trash, than reduce the backpack.
        if (trash > 0)
        {
            for (int i = 0; i < trash; i++)
                backpack--;

            trash = 0;
        }
    }

    /// <summary>
    /// Amount of trashes in the backpack.
    /// </summary>
    public int Backpack
    {
        get { return backpack; }
        set { backpack = value; }
    }

    /// <summary>
    /// Get/Set all trashes.
    /// </summary>
    public bool AllTrashes
    {
        get { return allTrashes; }
        set { allTrashes = value; }
    }
}
