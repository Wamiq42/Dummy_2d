using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class MoveOnPath : MonoBehaviour
{
   // [SerializeField] private Transform[] routes;
   // private int routeToGo;
   // private float tParam;
   // private Vector2 playerPosition;
   //// private float speedModifier; 
   // Vector2 p0;
   // Vector2 p1;
   // Vector2 p2;
   // Vector2 p3;


    public PathCreator pathCreator;
   
    float distanceTravel;

    //workedSolution
    private bool isDragging = false;
    private bool onObject = false;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float spacing;


    private void Start()
    {
        transform.position = pathCreator.path.GetPointAtDistance(0);
        InstantiateGameObjectOnPath();

    }
    private void Update()
    {
        if (isDragging && onObject)
        {
            //MovementOnPath();
            Movement();
        }
    }
    private void OnMouseEnter()
    {
        onObject = true;
    }

    void OnMouseExit()
    {
        onObject = false;
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
    }
  
    void Movement()
    {
        //3rd workedSolution
        Vector2 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        Vector3 nearestWorldPositionOnPath = pathCreator.path.GetPointAtDistance(pathCreator.path.GetClosestDistanceAlongPath(worldPosition));
        Debug.Log("nearestWorldPositionOnPath" + nearestWorldPositionOnPath);
        //Debug.Log("Get neareast Point " + pathCreator.path.GetClosestPointOnPath(worldPosition));
        //Debug.Log("Get neareast Point " + pathCreator.path.GetClosestPointOnPath(nearestWorldPositionOnPath));

        Debug.Log(pathCreator.path.length);
        

        //transform.rotation = pathCreator.path.GetRotation(0);

        transform.position = nearestWorldPositionOnPath;













        //distanceTravel += speed + Time.deltaTime;
        //transform.position = pathCreator.path.GetPointAtDistance(distanceTravel);
        //Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
        //distanceTravel = pathCreator.path.GetClosestDistanceAlongPath(position);
        // distanceTravel += speed + Time.deltaTime;

        //position.x = Mathf.Clamp(position.x, pathCreator.path.bounds.min.x, pathCreator.path.bounds.max.x);
        //position.y = Mathf.Clamp(position.y, pathCreator.path.bounds.min.y, pathCreator.path.bounds.max.y);


        //transform.Translate(position * Time.deltaTime);
    }




    void InstantiateGameObjectOnPath()
    {
        float dst = 0;

        while (dst < pathCreator.path.length)
        {
            Vector3 point = pathCreator.path.GetPointAtDistance(dst);
            //Quaternion rot = pathCreator.path.GetRotationAtDistance(dst);
            Instantiate(prefab, point, Quaternion.identity,pathCreator.gameObject.transform);
            dst += spacing;
        }
    }








    //void MovementOnPath()
    //{


    //    //tParam += Time.deltaTime * speedModifier;
    //    //tParam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    playerPosition = Mathf.Pow(1 - tParam, 3) * p0 
    //        + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1
    //        + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 
    //        + Mathf.Pow(tParam, 3) * p3;

    //    transform.position = playerPosition;
    //}
    private void OnMouseDown()
    {
        //p0 = routes[routeToGo].GetChild(0).position;
        //p1 = routes[routeToGo].GetChild(1).position;
        //p2 = routes[routeToGo].GetChild(2).position;
        //p3 = routes[routeToGo].GetChild(3).position;
        isDragging = true;
        onObject = true;
    }
    private void OnMouseUp()
    {
        isDragging = false;
        onObject = false;
        //transform.position = playerPosition;
        //tParam = 0f;
        //routeToGo += 1;
        //if (routeToGo > routes.Length - 1)
        //{
        //    routeToGo = 0;
        //}
    }
}
