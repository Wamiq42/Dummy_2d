using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInBounds : MonoBehaviour
{
    private Bounds boundary;
    private Renderer renderer2D;
    private Vector3 movePosition;
    private Vector3 minX, minY;
    private Vector3 maxX, maxY;
    private Vector2 boundsMIN;
    private Vector2 boundsMAX;
    private bool isDragging = false;
    

    [SerializeField]private GameObject go;

    private void Awake()
    {
        movePosition = transform.position;
            renderer2D = go.GetComponent<SpriteRenderer>();
        boundsMIN = new Vector2(renderer2D.bounds.min.x, renderer2D.bounds.min.y);
        boundsMAX = new Vector2(renderer2D.bounds.max.x, renderer2D.bounds.max.y);
        Debug.Log(boundsMIN);
        Debug.Log(boundsMAX);
        Debug.Log(renderer2D.bounds);
        boundary = renderer2D.bounds;
       // SetMinMaxPoly2D();

    }
    private void Update()
    {
        Vector2 mousePosition = (Camera.main.ScreenToWorldPoint((Input.mousePosition) - transform.position));
        mousePosition.x = Mathf.Clamp(mousePosition.x, boundsMIN.x, boundsMAX.x);
        mousePosition.y = Mathf.Clamp(mousePosition.y, boundsMIN.y, boundsMAX.y);



        if (isDragging && go.GetComponent<Collider2D>().OverlapPoint(mousePosition))
        {
            transform.position = Vector3.Lerp(transform.position, mousePosition, 0.5f);

            
        }
        //Vector2 mousePosition = (Camera.main.ScreenToWorldPoint((Input.mousePosition) - transform.position));
        //Vector3 bestPoint = transform.position;
        //float bestDistance = float.MaxValue;
        //foreach (var item in go.GetComponent<PolygonCollider2D>().points)
        //{
        //    float currentDistance = Vector3.Distance(item, transform.position);
        //    if (currentDistance < bestDistance)
        //    {
        //        bestDistance = currentDistance;
        //        bestPoint = item;
        //    }
        //}


        //if (isDragging)
        //{
        //    //transform.position = bestPoint;
        //    transform.position = Vector3.Lerp(transform.position, mousePosition, 0.5f);
        //}
        //MovementInBoundary();
    }




    
    private void LateUpdate()
    {


        //Vector2 mousePosition = (Camera.main.ScreenToWorldPoint((Input.mousePosition) - transform.position));
        //mousePosition.x = Mathf.Clamp(mousePosition.x, boundsMIN.x, boundsMAX.x);
        //mousePosition.y = Mathf.Clamp(mousePosition.y, boundsMIN.y, boundsMAX.y);



        //if (isDragging && go.GetComponent<Collider2D>().OverlapPoint(mousePosition))
        //{
        //    transform.position = Vector3.Lerp(transform.position, mousePosition, 0.5f);
        //}


















        //Vector3 movePosition = new Vector3(RoundToNearestHalfSafer(go.transform.position.x + Input.GetAxis("Horizontal")),
        //    RoundToNearestHalfSafer(go.transform.position.y + Input.GetAxis("Vertical")));
        //movePosition = new Vector3(transform.position.x + Input.GetAxis("Horizontal"),
        //    transform.position.y + Input.GetAxis("Vertical"));


        //Debug.Log("Min" + boundary.min);
        ////Debug.Log("Max" + boundary.max);
        //float xAxis = Mathf.Clamp(mousePosition.x, bounds.x, bounds.x * -1);
        //float yAxis = Mathf.Clamp(mousePosition.y, bounds.y, bounds.y * -1);
        //mousePosition = new Vector2(xAxis, yAxis);

        //Debug.Log("Center " + boundary.center);
        //Debug.Log("Extent" + boundary.extents);
        //Debug.Log("Size" + boundary.size);




    }

    private void OnMouseDown()
    {
        isDragging = true;
    }
    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        transform.position = movePosition;
    }
    void SetMinMaxPoly2D()
    {
        minX = new Vector3(Mathf.Infinity, 0, 0);
        minY = new Vector3(0, Mathf.Infinity, 0);
        maxX = new Vector3(-Mathf.Infinity, 0,0);
        maxY = new Vector3(0, -Mathf.Infinity,0);

        var poly = go.GetComponent<PolygonCollider2D>();
        for (int i = 0; i < poly.pathCount; i++)
        {
            var path = poly.GetPath(i);
            foreach (var item in path)
            {
                var v = transform.TransformPoint(item);
                if (v.x < minX.x)
                    minX = v;
                if (v.y < minY.y)
                    minY = v;
                if (v.x > maxX.x)
                    maxX = v;
                if (v.y > maxY.y)
                    maxY = v;
            }
        }
        
        var z = transform.position.z;
        minX.z = z;
        maxX.z = z;
        minY.z = z;
        maxY.z = z;
    }

















    //void MovementInBoundary()
    //{
    //    //Debug.Log("MovementInBoundary");
    //    float horizontalInput = Input.GetAxis("Horizontal");
    //    float verticalInput = Input.GetAxis("Vertical");

    //    Vector3 movePosition = new Vector3(transform.position.x *horizontalInput, transform.position.y * verticalInput);
    //    //Debug.Log("MovementPosition" + movePosition);
    //    Debug.Log("boundary min " + renderer2D.bounds.size);

    //    float xAxis = Mathf.Clamp(movePosition.x, boundary.min.x, boundary.max.x);
    //    float yAxis = Mathf.Clamp(movePosition.y, boundary.min.y, boundary.max.y);
    //    Debug.Log("x-axis" + xAxis);
    //    Debug.Log("y-axis" + yAxis);

    //    Vector2 direction = new Vector2(xAxis, yAxis);
    //    //Debug.Log("Direction" + direction);
    //    transform.Translate(direction);
    //}


    //public float RoundToNearestHalfSafer(float a)
    //{
    //    return a = a - (a % 0.5f);
    //}
}
