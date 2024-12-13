using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float CameraFollowThreshold = 1f;
    [SerializeField] float CameraFollowSpeed =0.1f;
    GameObject Player;
    Rigidbody2D PlayerRB;
    float PlayerPos;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerRB = Player.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        PlayerPos = Player.transform.position.y;
        if (PlayerPos - transform.position.y > CameraFollowThreshold || PlayerPos - transform.position.y < CameraFollowThreshold * -1)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, PlayerPos, transform.position.z),CameraFollowSpeed);
        }
    }
}
