using UnityEngine;
using System.Collections;

public class TouchListener : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

	    if(Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began && checkTouch(Input.touches[0].position)) gameObject.GetComponent<PlayerBlock>().OnTouched();
        }

        if (Input.GetMouseButtonDown(0) && checkTouch(Input.mousePosition)) gameObject.GetComponent<PlayerBlock>().OnTouched();

	}

    private bool checkTouch(Vector3 position)
    {
        Vector3 worldLocation = Camera.main.ScreenToWorldPoint(position);
        if (Physics2D.OverlapPoint(worldLocation))
        {
            return true;
        }
        else return false;
    }

    
}
