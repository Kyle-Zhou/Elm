using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    Vector2 viewPortSize;
    Camera cam;

    public float viewPortFactor;

    Vector3 targetPosition;
    private Vector3 currentVeloctiy;
    public float followDuration;
    public float maximumFollowSpeed;

    public Transform player;

    Vector2 distance;
    private bool wieldingSword;
    public float Radius = 1.5f;
    float Dist;
    Vector3 MousePos1, MousePos2, ScreenMouse, MouseOffset;
    public Vector3 lastPosition;

    private void Start()
    {
        wieldingSword = false;
        cam = Camera.main;
        targetPosition = player.position;

    }

    private void FixedUpdate()
    {
        if (!wieldingSword) { 
            viewPortSize = (cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) - cam.ScreenToWorldPoint(Vector2.zero)) * viewPortFactor;

            distance = player.position - transform.position;

            if (Mathf.Abs(distance.x) > viewPortSize.x / 2)
            {
                targetPosition.x = Mathf.Clamp(player.position.x - (viewPortSize.x / 2 * Mathf.Sign(distance.x)), -30f, 5.25f);
                //targetPosition.x = (player.position.x - (viewPortSize.x / 2 * Mathf.Sign(distance.x)));

            }

            if (Mathf.Abs(distance.y) > viewPortSize.y / 2)
            {
                targetPosition.y = Mathf.Clamp(player.position.y - (viewPortSize.y / 2 * Mathf.Sign(distance.y)), -15.5f , 4f);
                //targetPosition.y = (player.position.y - (viewPortSize.y / 2 * Mathf.Sign(distance.y)));
            }

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition - new Vector3(0, 0, 10), ref currentVeloctiy, followDuration, maximumFollowSpeed);
        } else
        {
            Dist = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(player.position.x, player.position.y));
            MousePos1 = Input.mousePosition;

            ScreenMouse = cam.ScreenToWorldPoint(new Vector3(MousePos1.x, MousePos1.y, transform.position.z));
            MouseOffset = ScreenMouse - player.position;
            MousePos2 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -transform.position.z));

            //if (Dist > Radius)
            //{
            //    var norm = MouseOffset.normalized;
            //   transform.position = new Vector3(norm.x * Radius + player.position.x, norm.y * Radius + player.position.y, transform.position.z);
            //} else {
            //transform.position = new Vector3((MousePos2.x - player.position.x) / 3.0f + player.position.x, (MousePos2.y - player.position.y) / 3.0f + player.position.y, transform.position.z);
            maximumFollowSpeed = 5;
            followDuration = 0.4f;
            transform.position = Vector3.SmoothDamp(
                transform.position,
                new Vector3(Mathf.Clamp((MousePos2.x - player.position.x) / 4.0f + player.position.x, -30f, 5.25f), Mathf.Clamp((MousePos2.y - player.position.y) / 4.0f + player.position.y, -15.5f, 4f), 0) - new Vector3(0, 0, 10),
                ref currentVeloctiy, followDuration, maximumFollowSpeed);

            //}
        }
        

    }



    private void OnDrawGizmos()
    {
        Color c = Color.red;
        c.a = 0.3f;
        Gizmos.color = c;

        Gizmos.DrawCube(transform.position, viewPortSize);

    }

    public void SetWield(bool ws)
    {
        wieldingSword = ws;
    }

}
