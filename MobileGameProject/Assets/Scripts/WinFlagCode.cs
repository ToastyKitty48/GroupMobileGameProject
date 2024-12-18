using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinFlagCode : MonoBehaviour

{
    [SerializeField] private float hitboxSizeX = 0.5f;
    [SerializeField] private float hitboxSizeY = 0.5f;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3 (hitboxSizeX, hitboxSizeY, 0));
    }
}
