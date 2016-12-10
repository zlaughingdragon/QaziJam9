using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


[RequireComponent (typeof(Rigidbody))] //ensure the object has a RigidBody.

public class Controller : MonoBehaviour
{

    


    /* note: if [serializefield] is preventing you from getting a variable that you need in another script, please
    set the desired variable to public outside of where it currently sits in the code. the use of a comma (,)
    saves space, but confuses some people. Using commas like public float var, var1; is the same 
    as saying public float var; over and over again.*/



    //FLOATS:

    //maxSpeed is the maximum speed your player can go. Play with it if results are undesireable.
    //currentSpeed is the current speed the player is going. Reference only.
    //speedModifier is the modifier to control the player's speed.
    //turnSpeedModifier is to control the sensitivity of the camera rotation.
    //jumpSpeedModifier is to control both how high and how fast the player jumps into the air. Note that the DRAG of the rigidbody (below)
    //should be how you control the descent.
    //offset X, Y, and Z are unused variables.
    //The camera object may show a Green squiggly line because we are only using this as reference in the script.
    [Header("Modifiers")]
        public float maxSpeed; public float currentSpeed,
            speedModifier, turnSpeedModifier, jumpSpeedModifier, defaultTurnSpeedMod = 300,
            mass = 0.5f, lift = 3f, drag = 2f, speedModp = 40f, runSpeed = 80f, zoomSpeed = 10;

        private float clipLength, attackLength;
        private bool running, attacking, sprint;

        Animation anim;

        public GameObject cameraOBJ;



        //BOOLS:

        //isMoving has not been implimented.
        //canMove is for reference only, so it will give you an error in the inspector.
        //grounded is for getting whether the player is on the ground or not. BE SURE TO SET YOUR FLOOR WITH A TAG OF "ground"!
        //freeCam determines whether or not the player is using the free look with the button "MoveCam" set in the editor.
        [Header("Booleans")]
        [SerializeField]
        private bool isMoving;
        public bool canMove = true;
        [SerializeField]
        public bool grounded, freeCam;



        [Header("Please Select Look Axis")]

        [Range(0, 2)] //slider with a range between 0 and 2 (inclusive)
        [Tooltip("Use the slider to select axis. 0=x,1=y,2=x&y")]
        public int axis; //set by the slider
        [Tooltip("Note: Changing these fields does nothing. They are for reference.")]
        public string[] axisIndex = { "X ONLY", "Y ONLY", "X AND Y" }; //reference for user in editor


        private bool XONLY, XANDY, YONLY;


        //Rigidbody and Vector3s:

        [Header("Required Data")]
        [SerializeField]
        private Rigidbody rb;
        private Vector3 tmpRotation;
        public float rotateSpeed = 100f, angleMax = 50f;
        private Vector3 initialVector = Vector3.forward;
        public GameObject footPole, footPoleLeft, footPoleRight, footPoleDefault, headJoint;

        void Awake()
        {
            // anim = transform.GetChild(0).GetComponent<Animation>();
            // clipLength = 2f;

            //get the rigidbody
            rb = GetComponent<Rigidbody>();
            //feel free to change the mass here if you need to.
            rb.mass = mass;

            axis = 2;

        }

        // Use this for initialization
        void Start()
        {

            initialVector = cameraOBJ.transform.position - transform.position;
            initialVector.y = 0;
            canMove = true;
        }
        void Update()
        {

            if (Input.GetButton("Sprint"))
            {
                sprint = true;
                speedModifier = runSpeed;
                maxSpeed = 10f;
            }

            else if (Input.GetButtonUp("Sprint")) { sprint = false; speedModifier = speedModp; maxSpeed = 5f; }
        }


        void FixedUpdate()
        {



            #region movement -- GET AXIS provides a positive or negative 1 if player is pressing. Direction is based on this.

            //get current speed for reference
            currentSpeed = rb.velocity.magnitude;


            if (canMove)
            {


                if (Input.GetAxis("Vertical") != 0 && grounded)
                {

                    //clipLength = anim.clip.length;


                    //test for input and max speed 
                    if (rb.velocity.magnitude <= maxSpeed)
                    {

                        rb.AddForce(Input.GetAxis("Vertical") * (transform.forward * speedModifier), ForceMode.Acceleration); //add force for forward/backward
                        running = true;
                    }
                    //if (sprint == true)
                    //{
                    //    GetComponent<Animator>().SetBool("running", true);
                    //    GetComponent<Animator>().SetBool("walking", false);
                    //    GetComponent<Animator>().SetBool("falling", false);
                    //}
                    //else
                    //{

                    //    GetComponent<Animator>().SetBool("running", false);
                    //    GetComponent<Animator>().SetBool("walking", true);
                    //    GetComponent<Animator>().SetBool("falling", false);

                    //}


                    //}
                    //else if (Input.GetAxis("Vertical") != 0 && !grounded)
                    //{
                    //    GetComponent<Animator>().SetBool("running", false);
                    //    GetComponent<Animator>().SetBool("walking", false);
                    //    GetComponent<Animator>().SetBool("falling", true);
                    //}

                    //else if (Input.GetAxis("Vertical") == 0 && grounded)
                    //{
                    //    GetComponent<Animator>().SetBool("running", false);
                    //    GetComponent<Animator>().SetBool("walking", false);
                    //    GetComponent<Animator>().SetBool("falling", false);

                    //}
                    //else if (Input.GetAxis("Vertical") == 0 && !grounded)
                    //{
                    //    GetComponent<Animator>().SetBool("running", false);
                    //    GetComponent<Animator>().SetBool("walking", false);
                    //    GetComponent<Animator>().SetBool("falling", true);
                    //}
                    else
                    {


                        //speed modifier is multiplied by the default of 2 in the variable drag. Use this to adjust drag.
                        rb.drag -= (Time.deltaTime * jumpSpeedModifier * drag);

                        //Ensure drag is not negative
                        if (rb.drag <= 0) rb.drag = 0f;

                    }// change the lift value for the amount of drag on lift-off

                    if (grounded)
                    {
                        rb.drag = lift;
                    }

                    ////test for input and max speed
                    //if (Input.GetAxis("Horizontal") != 0 && rb.velocity.magnitude <= maxSpeed)
                    //    rb.AddForce(Input.GetAxis("Horizontal") * (transform.right * speedModifier), ForceMode.Acceleration); //add force for sideways


                    // Jump is based on mass immensely. Play with the drag values below for best results.
                    if (Input.GetButtonDown("Jump") && grounded == true)
                    {

                        rb.AddForce(transform.up * jumpSpeedModifier, ForceMode.Impulse);
                    }


                }
                #endregion
            }
        }

    //test for ground
    void OnCollisionStay(Collision other)
    {
        if (other.collider.tag == "ground")
            StartCoroutine(setGrounded((other.collider.CompareTag("ground"))));

    }
    void OnCollisionExit(Collision other)
    {
        if (other.collider.tag == "ground")
            StartCoroutine(setGrounded((!other.collider.CompareTag("ground"))));
    }

    IEnumerator setGrounded(bool g)
    {
        yield return new WaitForSeconds(0.01f);
        grounded = g;
    }
}

