using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 6.0f;
    private float jumpHeight = 2.5f;
    private float gravityValue = -9.81f;

    public AnimControl animControl;

   public int health = 2;

    bool gotoLeft;
    bool gotoRight;

    float wallDistance = 5.25f;

    public bool gameRunning = true;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if(gameRunning)
        {

            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector3 move = new Vector3(1, 0, 0);
            controller.Move(move * Time.deltaTime * playerSpeed);

            gameObject.transform.forward = move;

            // Changes the height position of the player..
            if (Input.GetButtonDown("Jump") && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            if (gotoLeft)
            {
                move = new Vector3(1, 0, 2.25f);
                controller.Move(move * Time.deltaTime * playerSpeed);

                gameObject.transform.forward = move;
            }

            if (gotoRight)
            {
                move = new Vector3(1, 0, -2.25f);
                controller.Move(move * Time.deltaTime * playerSpeed);

                gameObject.transform.forward = move;
            }


            // DOnt go outside of the map

            Vector3 pos = transform.position;

            if (transform.position.z < -3.30f)
            {
                pos.z = -3.30f;
            }
            else if (transform.position.z > wallDistance)
            {
                pos.z = wallDistance;
            }

            transform.position = pos;

        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Block")
        {
            if (health == 1)
            {
            //    Destroy(hit.gameObject);
             //   health -= health;
                animControl.Die();
                gameRunning = false;
                StartCoroutine("RestartGame");
            }
            Destroy(hit.gameObject);
            health -= 1;
            playerSpeed = 3.5f;
            animControl.Block();
            StartCoroutine("Heal");
        }
        else if (hit.gameObject.tag == "FinishPoint")
        {
            gameRunning = false;
            animControl.ChangeDirection();
        }
    }

    public void Jump()
    {
        if (groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
    }

    public void GoLeft()
    {
        gotoLeft = true;
        StartCoroutine("StopGotoLeft");
    }

    public void GoRight()
    {
        gotoRight = true;
        StartCoroutine("StopGotoRight");
    }

    public void SlideCollision()
    {
        controller.height = 2.15f;
        controller.center = new Vector3(0, 0.839999974f, 0);
        StartCoroutine("SlideTimer");
    }

    IEnumerator StopGotoRight()
    {
        yield return new WaitForSeconds(0.30f);
        gotoRight = false;
    }

    IEnumerator StopGotoLeft()
    {
        yield return new WaitForSeconds(0.30f);
        gotoLeft = false;
    }

    IEnumerator SlideTimer()
    {
        yield return new WaitForSeconds(1.2f);
        controller.center = new Vector3(0, 2.4f, 0);
        controller.height = 5.37f;
        controller.radius = 0.95f;
    }

    IEnumerator Heal()
    {
        yield return new WaitForSeconds(5.30f);
        health = 2;
        playerSpeed = 6.1f;
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5.30f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

