using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Circle2DPointGenerator : MonoBehaviour
{
    public float Radius = 1.0f;
    public int NumPoints = 32;

    EdgeCollider2D EdgeCollider;
    float CurrentRadius = 0.0f;

    public float ThetaScale = 0.01f;
    private int Size;
    private LineRenderer LineDrawer;
    private float Theta = 0f;
    public bool DebugDrawEditor = false;
    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start()
    {
        LineDrawer = GetComponent<LineRenderer>();
        CreateCircle();
    }

    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {

        // If the radius or point count has changed, update the circle
        if (NumPoints != EdgeCollider.pointCount || CurrentRadius != Radius)
        {
            CreateCircle();
        }
    }

    /// <summary>
    /// Creates the circle.
    /// </summary>
    void CreateCircle()
    {
        Theta = 0f;
        Size = (int)((1f / ThetaScale) + 1f);
        LineDrawer.SetVertexCount(Size);
        for (int i = 0; i < Size; i++)
        {
            Theta += (2.0f * Mathf.PI * ThetaScale);
            float x = Radius * Mathf.Cos(Theta);
            float y = Radius * Mathf.Sin(Theta);
            LineDrawer.SetPosition(i, new Vector3(x, y, 0));
        }
        Vector2[] edgePoints = new Vector2[NumPoints + 1];
        EdgeCollider = GetComponent<EdgeCollider2D>();

        for (int loop = 0; loop <= NumPoints; loop++)
        {
            float angle = (Mathf.PI * 2.0f / NumPoints) * loop;
            edgePoints[loop] = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
        }

        EdgeCollider.points = edgePoints;
        CurrentRadius = Radius;
    }
    /// <summary>
    /// Renders shape of circle in the editor outside of runtime
    /// </summary>
    void OnDrawGizmos()
    {
        if(DebugDrawEditor)
        {
            DebugDraw();
        }
    }
    void DebugDraw()
    {
        Gizmos.color = Color.green;
        for(int i = 0; i < EdgeCollider.points.Length; i++)
        {
            Gizmos.DrawLine(gameObject.transform.position, EdgeCollider.points[i]);
        }
    }
}