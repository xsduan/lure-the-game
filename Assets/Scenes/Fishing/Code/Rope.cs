//For more information, please see the following video:
//https://www.youtube.com/watch?v=FcnvwtyxLds

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Rope : MonoBehaviour
{

    #region inspector variables

    [SerializeField] float maxSegmentLength = 0.25f; //Maximum distance between two points
    [SerializeField] float minSegmentLength = 0.01f; //Minimum distance between two points

    public int maxPointCount = 20;
    [HideInInspector] public int pointCount;

    [SerializeField] float lineWidth = 0.1f; //How thick the line should be
    public Transform startObject; //The transform where the rope should start (probably representing the fishing line)
    public Transform endObject; //The transform where the rope should end (probably representing the hook)

    #endregion


    #region internal vars


    private LineRenderer lineRenderer;
    private List<RopePoint> ropePoints = new List<RopePoint>();
    private float ropeSegmentLength;

    #endregion



    void Awake()
    {
        pointCount = maxPointCount;
        ropeSegmentLength = maxSegmentLength;
        lineRenderer = GetComponent<LineRenderer>();
        Vector3 startPoint = startObject.position;

        for (int i = 0; i < pointCount; i++)
        {
            ropePoints.Add(new RopePoint(startPoint));
            startPoint.y -= ropeSegmentLength;
        }
    }

    void LateUpdate()
    {
        SimulateRope();
        DrawRope();
    }

    void DrawRope()
    {
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            ropePositions[i] = ropePoints[i].posNow;
        }
        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }

    private void SimulateRope()
    {

        for (int i = 0; i < pointCount; i++)
        {
            RopePoint point = ropePoints[i];
            Vector3 velocity = (point.posNow - point.posOld); //calculates velocity by subtracting old position from current position
            point.posOld = point.posNow;
            point.posNow += velocity;
            point.posNow += Physics.gravity * Time.deltaTime;
            ropePoints[i] = point;

        }
        for(int i = 0; i < 50; i++)
        {
            AddConstraints();
        }
        
    }

    //Physics constraints
    #region constraints

    public void AddConstraints()
    {
        ConstrainStartingPosition();
        ConstrainEndingPosition();
        ConstrainDistance();
    }


    //Makes sure the first point is always located at the base
    public void ConstrainStartingPosition()
    {
        RopePoint point = ropePoints[0];
        point.posNow = startObject.position;
        ropePoints[0] = point;
    }

    //Makes sure the last point is always located at the hook
    public void ConstrainEndingPosition()
    {
        RopePoint point = ropePoints[pointCount - 1];
        point.posNow = endObject.position;
        ropePoints[pointCount - 1] = point;
    }

    //Makes sure no two points can be any more than a certain distance from each other.
    public void ConstrainDistance()
    {
        for (int i = 0; i < pointCount - 1; i++)
        {
            RopePoint firstPoint = ropePoints[i];
            RopePoint secondPoint = ropePoints[i + 1];

            float dist = (firstPoint.posNow - secondPoint.posNow).magnitude; //distance between points
            float error = Mathf.Abs(dist - ropeSegmentLength); //how much larger or smaller the distance between points is vs. the current segment length
            Vector3 changeDirection = Vector3.zero;

            if(dist > ropeSegmentLength)
            {
                changeDirection = (firstPoint.posNow - secondPoint.posNow).normalized;
            }
            else if(dist < ropeSegmentLength)
            {
                changeDirection = (secondPoint.posNow - firstPoint.posNow).normalized;
            }
            Vector3 changeAmount = changeDirection * error;
            if(i != 0)
            {
                firstPoint.posNow -= changeAmount * 0.5f;
                ropePoints[i] = firstPoint;
                secondPoint.posNow += changeAmount * 0.5f;
                ropePoints[i + 1] = secondPoint;
            }
            else
            {
                secondPoint.posNow += changeAmount;
                ropePoints[i+1] = secondPoint;
            }

        }
    }
    #endregion

    public void TurnOn()
    {
        lineRenderer.enabled = true;
    }

    public void TurnOff()
    {
        lineRenderer.enabled = false;
    }
    public void ResetPointCount()
    {
        pointCount = maxPointCount;
    }

    public struct RopePoint
    {
        public Vector3 posNow;
        public Vector3 posOld;

        public RopePoint(Vector3 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }
}
