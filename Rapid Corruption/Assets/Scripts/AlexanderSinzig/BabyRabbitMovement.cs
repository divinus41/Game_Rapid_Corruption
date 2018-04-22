using UnityEngine;

public class BabyRabbitMovement : MonoBehaviour
{
    #region variables
    //variables for following
    public Transform target;
    public float followDistance = 3f;
    private float trashDistance = 4f;

    //variables for own components
    private BabyRabbitControl babyRabbitControlScript;
    private Animator animator;
    private CharacterMotorC cmotor;

    //variables for player speed/sprint
    private PlayerMovement playerMovementScript;
    private float babySpeed;
    #endregion

    private void Start()
    {

        babyRabbitControlScript = GetComponent<BabyRabbitControl>();
        //find animator and other scripts
        animator = GetComponent<Animator>();
        cmotor = GetComponent<CharacterMotorC>();

        //set babyspeed value
        playerMovementScript = FindObjectOfType<PlayerMovement>();
        babySpeed = playerMovementScript.Speed;
    }

    void Update()
    {
        #region force move
        //highest priority
        //follow player as usual
        if (babyRabbitControlScript.ForceMove)
        {
            //find payer as spare target
            if (target == null)
            {
                target = GameObject.Find("Player").transform;
            }
            MoveToPoint(target.position, followDistance);
        }
        #endregion
        else
        #region standard-behaviour
        {
            //point of interest
            //second highest priority
            //go to the veryInteresting pointOfInterest
            if (babyRabbitControlScript.VeryInteresting)
            {
                MoveToPoint(babyRabbitControlScript.PointOfInterest, trashDistance);
            }
            else
            {
                //follow player
                if (!babyRabbitControlScript.Stay)
                {
                    //look at target
                    if (target == null)
                    {
                        target = GameObject.Find("Player").transform;
                    }

                    MoveToPoint(target.position, followDistance);
                }
            }
        #endregion

            #region stay command
            if (babyRabbitControlScript.Stay)
            {
                //enable sprint for player
                playerMovementScript.sprintEnable = true;

                //turn to the interesting pointOfInterest
                //third highest priority
                if (babyRabbitControlScript.Interesting && babyRabbitControlScript.StartRoaming)
                {
                    MoveToPoint(babyRabbitControlScript.PointOfInterest, trashDistance);
                }
                else
                {
                    Stand();
                }
            }
             else
             {
                //disable sprint for player
                playerMovementScript.sprintEnable = false;
             }
             #endregion
        }

        if (babyRabbitControlScript.Dead)
        {
            cmotor.inputMoveDirection = transform.rotation * Vector3.zero;
            animator.SetBool("dying", true);
        }
    }

#region move/stad functions
    //all movements
    void MoveToPoint (Vector3 point, float minimumDistance)
    {
        //turn to the object
        transform.LookAt(point);

        Vector3 forwardVector;
        forwardVector = new Vector3(0, 0, babySpeed);

        if (Vector3.Distance(point, transform.position) >= minimumDistance)
        {
            //move forward
            cmotor.inputMoveDirection = transform.rotation * forwardVector;
            //forward animation
            animator.SetBool("moving", true);

        }
        else
        {
            Stand();
        }
    }

    //hold position
    void Stand()
    {
        cmotor.inputMoveDirection = transform.rotation * Vector3.zero;
        animator.SetBool("moving", false);
    }
    #endregion
}