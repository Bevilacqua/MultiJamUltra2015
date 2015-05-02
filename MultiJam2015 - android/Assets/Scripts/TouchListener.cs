using UnityEngine;
using System.Collections;

public class TouchListener : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

        if (Input.touchCount > 0)
        {
            if(gameObject.GetComponent<PlayerBlock>() != null)
            {
                if (Input.touches[0].phase == TouchPhase.Began && checkTouch(Input.touches[0].position)) gameObject.GetComponent<PlayerBlock>().OnTouched();
            }
            else
            {
                if (Input.touches[0].phase == TouchPhase.Began && checkTouch(Input.touches[0].position)) gameObject.GetComponent<CenterNode>().OnTouched();
            }
        }

        if (Input.GetMouseButtonDown(0) && checkTouch(Input.mousePosition)) gameObject.GetComponent<PlayerBlock>().OnTouched();

	}

    public bool checkTouch(Vector3 position)
    {
        Vector3 worldLocation = Camera.main.ScreenToWorldPoint(position);
        Collider2D collision = Physics2D.OverlapPoint(worldLocation);

        if (collision == null) return false;

        if (collision.gameObject.Equals(gameObject))
        {
            return true;
        }
        else return false;
    }

    
}
