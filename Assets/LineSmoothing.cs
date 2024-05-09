using UnityEngine;


public class LineSmoothing : MonoBehaviour
{

    public LineRenderer lineRenderer; 

    void Start()
    {
        // Включаем сглаживание
        lineRenderer.useWorldSpace = true;
        lineRenderer.numCapVertices = 5;   
        lineRenderer.numCornerVertices = 5;
    }


}
