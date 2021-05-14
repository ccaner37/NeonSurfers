using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform camTransform;
    public Vector3 Offset;
    public float SmoothTime = 0.3f;

    private float fov;
    public Camera cam;
    private float durationInSeconds = 40;
    private float currentTime;

    private Vector3 velocity = Vector3.zero;

    public Player player;

    public Renderer rend;
    public GameObject floor;

    float timeLeft;
    public Color targetColor;

    float factor = Mathf.Pow(2, 5);



    void Start()
    {
        rend = floor.GetComponent<Renderer>();

        cam = this.GetComponent<Camera>();
        Offset = camTransform.position - target.position;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = target.position + Offset;
        targetPosition = new Vector3(targetPosition.x, targetPosition.y, 0.76f);
        camTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);

        if (!player.gameRunning)
        {
            transform.LookAt(target);
            currentTime += Time.deltaTime / durationInSeconds;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 53, currentTime);


            //Random colors

            if (timeLeft <= Time.deltaTime)
            {
                // transition complete
                // assign the target color
                rend.material.SetColor("ColorColo2", targetColor);

                // start a new transition
                targetColor = new Color(Random.value * factor, Random.value * factor, Random.value * factor);
                timeLeft = 1.2f;
            }
            else
            {
                // transition in progress
                // calculate interpolated color
                rend.material.SetColor("ColorColo2", Color.Lerp(rend.material.GetColor("ColorColo2"), targetColor, Time.deltaTime / timeLeft));

                // update the timer
                timeLeft -= Time.deltaTime;
            }

            //rend.material.SetColor("ColorColo2", Color.red);

            //print(rend.sharedMaterial.GetColor("ColorColo2"));
        }
    }
}
