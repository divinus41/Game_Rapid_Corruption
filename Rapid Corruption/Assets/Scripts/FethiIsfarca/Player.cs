using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Value types
    private int backpack;
    private int paperTrash, plasticTrash;
    private static int score;
    private int frames;

    // Refernce types
    private SpriteRenderer spriteRenderer;
    private BabyRabbitControl babyRabbitControlScript;
    public Canvas canvasComponent;
    public Slider sliderComponent;
    public Text numberOfPaperTextComponent;
    public Text numberOfPlasticTextComponent;
    public Texture2D[] texture2d;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        babyRabbitControlScript = FindObjectOfType<BabyRabbitControl>();

        frames = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        // By press the button 'F', than see the HUD.
        if (Input.GetAxis("HUD") > 0)
            canvasComponent.enabled = true;
        else
            canvasComponent.enabled = false;
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
            {
                KillTrashes(ref paperTrash, ref backpack);
                score += 50;
            }
            else if (other.CompareTag("PlasticTent"))
            {
                KillTrashes(ref plasticTrash, ref backpack);
                score += 50;
            }
        }

        // Set Values for UI.
        sliderComponent.value = backpack;
        numberOfPaperTextComponent.text = paperTrash.ToString();
        numberOfPlasticTextComponent.text = plasticTrash.ToString();
    }

    private void OnGUI()
    {
        // Declare variables
        Vector3 playerPosition;
        GUIStyle style = new GUIStyle();
        Font font;
        Texture2D rabbitTexture2d = null;

        // Set the position from player.
        playerPosition = Camera.main.WorldToScreenPoint(transform.position);

        // Set font for heading and button.
        font = (Font)Resources.Load("Fonts/Screen", typeof(Font));

        style.font = font;
        // Set font size for heading.
        style.fontSize = 50;

        // By press the button 'F', than see the GUI-HUD.
        if (Input.GetAxis("HUD") > 0)
        {
            if (babyRabbitControlScript.Health > 0)
                rabbitTexture2d = texture2d[0];
            else if (babyRabbitControlScript.Health <= 0)
            {
                rabbitTexture2d = texture2d[1];
                score -= 100;

                frames++;

                if (frames > 500)
                    SceneManager.LoadScene(4);
            }

            GUI.DrawTexture(new Rect(playerPosition.x / 16, playerPosition.y / 24, 50, 50), rabbitTexture2d);
            GUI.HorizontalSlider(new Rect(playerPosition.x / 4, playerPosition.y / 8, 150, 50), babyRabbitControlScript.Health, 0f, 10f);
            GUI.DrawTexture(new Rect(playerPosition.x / 16, playerPosition.y / 4, 50, 50), rabbitTexture2d);
            GUI.HorizontalSlider(new Rect(playerPosition.x / 4, playerPosition.y / 3, 150, 50), babyRabbitControlScript.Health, 0f, 10f);
            GUI.DrawTexture(new Rect(playerPosition.x / 16, playerPosition.y / 2, 50, 50), rabbitTexture2d);
            GUI.HorizontalSlider(new Rect(playerPosition.x / 4, playerPosition.y / 1.7f, 150, 50), babyRabbitControlScript.Health, 0f, 10f);

            GUI.Label(new Rect(playerPosition.x * 1.3f, playerPosition.y / 24, 200, 100), "Score: " + score, style);
        }
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
}
