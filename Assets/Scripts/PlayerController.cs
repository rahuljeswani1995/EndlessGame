using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxForwardSpeed;

    private int desiredLane = 1;
    public float laneDistance = 4;

    // amount of jump on pressing up arrow
    public float jumpForce;
    public float Gravity = -20;

    public Animator animator;
    private bool isSliding;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.isGameStarted)
            return;

        if (forwardSpeed < maxForwardSpeed)
            forwardSpeed += 0.1f * Time.deltaTime;


        animator.SetBool("isGameStarted", true);

        animator.SetBool("isGrounded", controller.isGrounded);

        // controller.Move(direction * Time.deltaTime);
        direction.z = forwardSpeed;

        // introduce gravity after every deltaTime interval (brings player back down on jumping)
            direction.y += Gravity * Time.deltaTime;

        // jump on pressing up key only when player is on ground (to avoid multiple jumps)
        if (controller.isGrounded)
        {
            // no gravity needed on ground
            //direction.y = -1;

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
        }
        //else
        //{

        //}

        if (Input.GetKeyDown(KeyCode.DownArrow) && !isSliding)
        {
            StartCoroutine(Slide());
        }


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if(desiredLane == 3)
            {
                desiredLane = 2;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }

        // Calculate next position
        Vector3 targetPostion = transform.position.z * transform.forward + transform.position.y * transform.up;

        if(desiredLane == 0)
        {
            targetPostion += Vector3.left * laneDistance;
        }
        else if(desiredLane == 2)
        {
            targetPostion += Vector3.right * laneDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPostion, 70 * Time.deltaTime);
        controller.center = controller.center;
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.isGameStarted)
            return;
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;
        yield return new WaitForSeconds(1.3f);
        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;
        animator.SetBool("isSliding", false);
        isSliding = false;
    }
}