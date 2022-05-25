using UnityEngine;
using System.Collections.Generic;
using UnitySpriteCutter;
[RequireComponent( typeof( LineRenderer ) )]
public class LinecastCutter : MonoBehaviour {

	public LayerMask layerMask;

	bool mouseUp = false;
	Vector2 mouseStart;
	Vector2 mouseEnd;
	void Update() {

		if ( Input.GetMouseButtonDown( 0 ) ) {
			mouseStart = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		}

		mouseEnd = Camera.main.ScreenToWorldPoint( Input.mousePosition );

		if ( Input.GetMouseButtonUp( 0 ) ) {
			//Vector2 mouseEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mouseUp = true;
			//LinecastCut( mouseStart, mouseEnd, layerMask.value );
		}
	}

    private void FixedUpdate()
    {
        if (mouseUp)
        {
            mouseUp = false;
            LinecastCut(mouseStart, mouseEnd, layerMask.value);
        }
    }

    void LinecastCut( Vector2 lineStart, Vector2 lineEnd, int layerMask = Physics2D.AllLayers ) {

		List<GameObject> gameObjectsToCut = new List<GameObject>();
		RaycastHit2D[] hits = Physics2D.LinecastAll( lineStart, lineEnd, layerMask );
		foreach ( RaycastHit2D hit in hits ) {
			
			if ( HitCounts( hit) && lineStart != lineEnd) {
				//Debug.Log(hit.transform.localScale);
				//Debug.Log("LineStart " + lineStart);
				//Debug.Log("Line End " + lineEnd);
				var distance = hit.point;
				Debug.Log(distance);

				//Debug.Log(distance);
				if (hit.transform.gameObject != null)
                {

					//Debug.Log("cutted");
					//Debug.Log("LineStart " + lineStart);
					gameObjectsToCut.Add(hit.transform.gameObject);
					//Debug.Log("Line End " + lineEnd);
                }
				
			}
		}
       
		foreach ( GameObject go in gameObjectsToCut ) {
			SpriteCutterOutput output = SpriteCutter.Cut( new SpriteCutterInput() {
				lineStart = lineStart,
				lineEnd = lineEnd,
				gameObject = go,
				gameObjectCreationMode = SpriteCutterInput.GameObjectCreationMode.CUT_OFF_ONE,
			} );

			if ( output != null && output.secondSideGameObject != null ) {
				Rigidbody2D newRigidbody = output.secondSideGameObject.AddComponent<Rigidbody2D>();
				newRigidbody.velocity = output.firstSideGameObject.GetComponent<Rigidbody2D>().velocity;
			}
		}
	}

	bool HitCounts( RaycastHit2D hit ) {
		return ( hit.transform.GetComponent<SpriteRenderer>() != null ||
		         hit.transform.GetComponent<MeshRenderer>() != null );
	}

}
