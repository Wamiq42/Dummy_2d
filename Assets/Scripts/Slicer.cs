using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slicer : MonoBehaviour
{
    bool isSlicing;
    Rigidbody2D slicerRigidbody;


    private void Start()
    {
        slicerRigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }


        if (isSlicing)
        {
            UpdateSlice();
        }
    }


    void UpdateSlice()
    {
        slicerRigidbody.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    void StartSlicing()
    {
        isSlicing = true;
        GetComponent<CircleCollider2D>().enabled = true;
    }
    void StopSlicing()
    {
        isSlicing = false;
        GetComponent<CircleCollider2D>().enabled = false;
    }
}
