using UnityEngine;


public class LineSmoothing : MonoBehaviour
{

    public LineRenderer lineRenderer; 

    void Start()
    {
        // �������� �����������
        lineRenderer.useWorldSpace = true;
        lineRenderer.numCapVertices = 5;   
        lineRenderer.numCornerVertices = 5;
    }


}
