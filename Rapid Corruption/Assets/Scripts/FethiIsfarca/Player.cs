using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Value types
    private bool firstEnableHud;
    private int backpack;
    private int paperTrash, plasticTrash;
    private static int score;

    // Refernce types
    public BabyRabbitControl[] babyRabbitControlScript;
    public Canvas playerCanvas, cameraCanvas;
    public Slider[] babyRabbitSlider;
    public Slider backpackSlider;
    public Text numberOfPaperTextComponent;
    public Text numberOfPlasticTextComponent;
    public Text scoreNumberText;
    public Text timerNumberText;

    // Use this for initialization
    void Start()
    {
        // Enable hud and start the coroutine for the disable hud.
        firstEnableHud = true;
        StartCoroutine(EnableHud());

        // Start the ticking timer coroutine.
        StartCoroutine(TickTimer());
    }

    // Update is called once per frame
    void Update()
    {
        // By press the button 'F', than see the HUD.
        if (Input.GetAxis("HUD") > 0 && !firstEnableHud)
        {
            playerCanvas.enabled = true;
            cameraCanvas.enabled = true;
        }
        else if (Input.GetAxis("HUD") <= 0 && !firstEnableHud)
        {
            playerCanvas.enabled = false;
            cameraCanvas.enabled = false;
        }

        // Set health value of baby rabbits in slider.
        babyRabbitSlider[0].value = babyRabbitControlScript[0].Health;
        babyRabbitSlider[1].value = babyRabbitControlScript[1].Health;
        babyRabbitSlider[2].value = babyRabbitControlScript[2].Health;

        // When all baby rabbits dead, then load lose screen.
        if (babyRabbitSlider[0].value == 0 && babyRabbitSlider[1].value == 0 && babyRabbitSlider[2].value == 0)
            StartCoroutine(LoadLoseScreen());
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
                    score += 10;
                }
                else if (other.CompareTag("Plastic"))
                {
                    plasticTrash++;
                    Debug.Log("You get the trash!");
                    Destroy(other.gameObject);
                    backpack++;
                    score += 10;
                }
            }
            else
                Debug.Log("You have too much trash in your backpack!");
        }

        // ---------- Lay down the trash. ----------
        if (Input.GetAxisRaw("Fire2") < 0)
        {
            // Is a setable trash?
            if (other.CompareTag("PaperTent"))
                KillTrashes(ref paperTrash, ref backpack);
            else if (other.CompareTag("PlasticTent"))
                KillTrashes(ref plasticTrash, ref backpack);
        }

        // Set Values for hud.
        backpackSlider.value = backpack;
        numberOfPaperTextComponent.text = paperTrash.ToString();
        numberOfPlasticTextComponent.text = plasticTrash.ToString();
        scoreNumberText.text = score.ToString();
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
            score += 50;
        }
    }

    /// <summary>
    /// Disable hud from started game.
    /// </summary>
    /// <returns>The seconds for wait.</returns>
    private IEnumerator EnableHud()
    {
        yield return new WaitForSeconds(5);

        firstEnableHud = false;

        StopCoroutine(EnableHud());
    }

    /// <summary>
    /// Decrement/Ticking timer.
    /// </summary>
    /// <returns>The seconds for wait.</returns>
    private IEnumerator TickTimer()
    {
        // Declare variables.
        int minutes = 7;
        int seconds = 1;
        string formatSeconds;

        // Continous loop for decrement seconds.
        for (; ;)
        {
            // Decrement seconds.
            yield return new WaitForSeconds(1);
            seconds--;

            // Set 10th place by seconds.
            formatSeconds = seconds <= 9 ? "0" + seconds.ToString() : seconds.ToString();

            // Output timer.
            timerNumberText.text = string.Format("0{0} : {1}", minutes, formatSeconds);

            // Countdown the minutes and reset seconds, when seconds equal zero.
            if (seconds <= 0)
            {
                minutes--;
                seconds = 60;
            }

            // When minutes equal zero, then start the coroutine for loading lose screen.
            if (minutes <= 0)
            {
                StartCoroutine(LoadLoseScreen());
                StopCoroutine(TickTimer());
            }
        }
    }

    /// <summary>
    /// When not baby rabbits in the level, then load the lose screen.
    /// </summary>
    /// <returns>The seconds for wait.</returns>
    private IEnumerator LoadLoseScreen()
    {
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(2);

        StopAllCoroutines();
    }
}