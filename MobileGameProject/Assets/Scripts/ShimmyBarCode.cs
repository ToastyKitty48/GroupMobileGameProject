using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShimmyBarCode : MonoBehaviour
{
    [SerializeField] public Transform leftCheckPoint; // Transform for ground check position
    [SerializeField] public Transform rightCheckPoint; // Transform for ground check position
    [SerializeField] private float pointRadi = 0.2f;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    void OnDrawGizmos()
    {
        // Draw a gizmo for the ground check in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rightCheckPoint.position, pointRadi);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(leftCheckPoint.position, pointRadi);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerMovementAndAC playerMove = collision.gameObject.GetComponent<PlayerMovementAndAC>();
            playerMove.leftCheckPointPos = leftCheckPoint;
            playerMove.rightCheckPointPos = rightCheckPoint;
        }
    }
}
