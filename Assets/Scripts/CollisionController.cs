using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    GameObject slicedGameObject;
    
  

    private void Start()
    {
        slicedGameObject = Resources.Load<GameObject>("SlicedPrefabs/SlicedCircle") as GameObject;
    }
   
  

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Slicer>() != null)
        {
            Instantiate(slicedGameObject, transform.position, GetDirection(collision.transform));

            
            Destroy(gameObject);
        }
    }

    Quaternion GetDirection(Transform transform)
    {
        Vector2 direction = (transform.position - this.transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward,direction);
        return rotation;
    }
}
