using UnityEngine;
using System.Collections;
using UnityEditor;
/// <summary>
/// Moves fish in the playfair scene
/// </summary>
public class Fish : MonoBehaviour {

    Vector2 originalPos;
    public int Range = 10;
    float leftBound = 0;
    float rightBound = 0;
    float direction = .1f;
	// Use this for initialization
	void Start () {
        originalPos = this.transform.position;
        ComputeBounds();
	}
    void ComputeBounds()
    {
        leftBound = this.transform.position.x - Range;
        rightBound = this.transform.position.x + Range;
    }
	void FixedUpdate()
    {
        if (direction > 0 && this.transform.position.x < rightBound)
        {
            Move(direction);
        }

        else if (direction < 0 && this.transform.position.x > leftBound)
        {
            Move(direction);
        }
        else
        {
            direction *= -1;
            Move(direction);
            this.transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
        }
    }
	// Update is called once per frame
	void Update () {
        
    }
    void Move(float direction)
    {
        this.transform.position = new Vector3(this.transform.position.x + direction, this.transform.position.y, this.transform.position.z);
    }
    void OnDrawGizmos()
    { 
        Vector3 position = this.transform.position;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(originalPos.x - Range, originalPos.y,-10), new Vector3(originalPos.x + Range, originalPos.y, -10));
    }
}
