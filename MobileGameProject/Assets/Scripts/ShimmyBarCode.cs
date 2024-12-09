using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShimmyBarCode : MonoBehaviour
{
    [SerializeField] public Transform leftCheckPoint; // Transforms for shimmy bar L & R points
    [SerializeField] public Transform rightCheckPoint;
    [SerializeField] private float pointRadi = 0.2f;
    void OnDrawGizmos()
    {
        // Draw a gizmo for the shimmy check in the editor
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
            Debug.Log("Grabbed bar");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Let go of bar");
    }
}
