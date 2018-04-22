using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region variables
    //animator is integrated in movementScript to optimize synchronisation
    private Transform rabbitBody;
    private Animator animator;

    private float speed;
    private float boostSpeedBonus;
    private int boostDuration;
    private int speedBoostDecay;
    private float sprintSpeedBonus;
    public bool sprintEnable = false;
    private CharacterMotorC cmotor;
    private Vector3 forwardVector;

    AudioSource carrotSound;



    #endregion


    void Awake()
    {
        speed = 1.0f;
        speedBoostDecay = 30;
        cmotor = GetComponent<CharacterMotorC>();
        rabbitBody = gameObject.transform.GetChild(0);
        animator = rabbitBody.GetComponent<Animator>();
    }


    void Update()
    {
        #region animator information
        //movement animator information
        animator.SetFloat("inputForward", Input.GetAxisRaw("Vertical"));
        animator.SetFloat("inputSides", Input.GetAxisRaw("Horizontal"));
        //grabing animator information
        animator.SetBool("grab", Input.GetButton("Fire2"));
        //removed
        //animator.SetBool("Jump", Input.GetButton("Jump"));
        #endregion

        #region move and sprint
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        transform.Rotate(0, x, 0);

        //sprint gets enabled/disabled in BabyRabbitMovement script
        if (Input.GetButton("Fire3") && sprintEnable)
        {
            //sprint (only possible if babyrabbits dont follow)
            sprintSpeedBonus = 0.5f;
            //sprint animator information
            animator.SetBool("sprint", true);
        }
        else
        {
            sprintSpeedBonus = 0f;
            //sprint animator information
            animator.SetBool("sprint", false);
        }
        #endregion

        #region speedBooster runs out
        if (boostDuration > 0)
        {
            boostDuration--;
        }
        else if (boostDuration <= 0 && boostSpeedBonus > 0)
        {
            speedBoostDecay--;
            if (speedBoostDecay <= 0)
            {
                speedBoostDecay = 45;
                boostSpeedBonus -= 1f;
                animator.speed -= 1;
            }
            
        }
        #endregion

        //the actual movement part
        //multiply forward speed by the different modifiers
        forwardVector = new Vector3(0, 0, Input.GetAxis("Vertical") * (speed + sprintSpeedBonus + boostSpeedBonus));
        // use characterMotor to move
        cmotor.inputMoveDirection = transform.rotation * forwardVector;
        
    }

    //booster-activation
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Booster")
        {
            boostDuration = 300;
            boostSpeedBonus += 1f;
            animator.speed += 1;
            Destroy(other.gameObject);

            //play sound
            carrotSound = GetComponent<AudioSource>();

            carrotSound.Play();

        }
    }

    public float Speed
    {
        get { return speed; }
    }

}
