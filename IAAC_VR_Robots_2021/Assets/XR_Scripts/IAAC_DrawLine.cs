﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAAC_DrawLine : MonoBehaviour
{
    public GameObject ObjectToFollow;
    public GameObject axisPrefab;
    public float distanceForward = 0.2f;
    public float lineDistance = 0.01f;
    public GameObject lineRendPrefab;
    public float lineWidth = 0.001f;

    private GameObject axisInstantiate;
    private Vector3 endPoint;
    private bool paintingOn = false;
    private bool newPaintVerticies = false;
    private Vector3 previousPosition;
    private List<Vector3> currVerticies;
    private Color doodleColor;
    private GameObject myDoodleLineRend;
    private LineRenderer doodleLine;

    void Start()
    {
        currVerticies = new List<Vector3>();
        doodleColor = Color.yellow;
    }

    void Update()
    {
        //This is for holding down touch and letting go on mobile
        /*
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)
            {
                myDoodleLineRend = Instantiate(lineRendPrefab, Vector3.zero, Quaternion.identity);
                myDoodleLineRend.transform.parent = geometryParent.transform;
                doodleLine = myDoodleLineRend.GetComponent<LineRenderer>();
                doodleLine.material.color = doodleColor;
                doodleLine.startWidth = laserWidth;
                doodleLine.endWidth = laserWidth;
                paintingOn = true;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                paintingOn = false;
                currVerticies = new List<Vector3>();
            }
        }
        */

        if (paintingOn)
        {
            Vector3 origin = ObjectToFollow.transform.position;
            Vector3 direction = ObjectToFollow.transform.TransformDirection(Vector3.forward);

            endPoint = origin + direction * distanceForward;


            if (newPaintVerticies)
            {
                if (currVerticies.Count > 0)
                {
                    doodleLine.positionCount = currVerticies.Count;

                    for (int i = 0; i < currVerticies.Count; i++)
                    {
                        doodleLine.SetPosition(i, currVerticies[i]);
                    }
                    newPaintVerticies = false;
                }
            }

        }

        if (Vector3.Distance(endPoint, previousPosition) > lineDistance)
        {
            if (paintingOn)
            {


                previousPosition = endPoint;
                newPaintVerticies = true;

                currVerticies.Add(endPoint);

                axisInstantiate = Instantiate(axisPrefab, endPoint, ObjectToFollow.transform.rotation);


                axisInstantiate.transform.RotateAround(axisInstantiate.transform.position, axisInstantiate.transform.right, 180);
                axisInstantiate.transform.localScale = new Vector3(.1f, .1f, .1f);
                axisInstantiate.transform.parent = myDoodleLineRend.transform;
            }
        }

    }
    public void ToggleLineStart()
    {
        paintingOn = !paintingOn;

        if (paintingOn)
        {
            myDoodleLineRend = Instantiate(lineRendPrefab, Vector3.zero, Quaternion.identity);
            doodleLine = myDoodleLineRend.GetComponent<LineRenderer>();
            doodleLine.material.color = doodleColor;
            doodleLine.startWidth = lineWidth;
            doodleLine.endWidth = lineWidth;
        }
        else
        {
            currVerticies = new List<Vector3>();
        }
    }

    public void LineStart()
    {
        paintingOn = true;

        myDoodleLineRend = Instantiate(lineRendPrefab, Vector3.zero, Quaternion.identity);
        doodleLine = myDoodleLineRend.GetComponent<LineRenderer>();
        doodleLine.material.color = doodleColor;
        doodleLine.startWidth = lineWidth;
        doodleLine.endWidth = lineWidth;
    }

    public void LineStop()
    {
        paintingOn = false;

        currVerticies = new List<Vector3>();

    }

    public void DoTheThing()
    {

    }
}