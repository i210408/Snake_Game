using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakeHeadFollowPath : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public Transform pathParent;
    public GameObject tailPrefab;

    private Vector3[] pathNodes;
    private int currentNode = 0;

    private List<GameObject> tailSegments = new List<GameObject>();
    private int tailLength = 0;

    private void Start()
    {
        pathNodes = new Vector3[pathParent.childCount];
        for (int i = 0; i < pathParent.childCount; i++)
        {
            pathNodes[i] = pathParent.GetChild(i).position;
        }

        // Create the initial tail segment and set positions relative to the head
        GrowTail();
        GrowTail();
        GrowTail();

        StartCoroutine(MoveAlongPath());
    }

    private IEnumerator MoveAlongPath()
    {
        Vector3 startPosition = pathNodes[currentNode];
        Vector3 endPosition = pathNodes[(currentNode + 1) % pathNodes.Length];

        Vector3 direction = (endPosition - startPosition).normalized;

        float travelled = 0.0f;
        float distance = Vector3.Distance(startPosition, endPosition);

        while (travelled < distance)
        {
            float interpolationValue = travelled / distance;
            transform.position = Vector3.Lerp(startPosition, endPosition, interpolationValue);

            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction) * Quaternion.Euler(0, 0, 180);

          

            travelled += moveSpeed * Time.deltaTime;
            yield return null;
        }

        currentNode = (currentNode + 1) % pathNodes.Length;
        StartCoroutine(MoveAlongPath());
    }

    // Method to grow the snake tail by adding a new segment
    private void GrowTail()
    {
        GameObject newTailSegment = Instantiate(tailPrefab);
        tailSegments.Add(newTailSegment);
        tailLength++;
    }


}
