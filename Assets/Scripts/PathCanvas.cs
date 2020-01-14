using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PathCreation.BezierPath;

public class PathCanvas : MonoBehaviour
{
    public GameObject transformObjectInstantiate;
    public GameObject roadInstantiate;
    public GameObject car;
    public GameObject destination;
    private GameObject tempRoad;
    public List<Transform> node = new List<Transform>();
    
    PathCreator pc;
    PathSpriteSelect spriteSelection;
    float distacetraveled;
    private Vector3 zeroVector = new Vector3(0, 100f, 0);

    public bool isLeft, isRight = true;
    private bool closedLoop = false;
    private int onlyLeftOrRight = -1;
    public int levelStepSize;

    [HideInInspector]
    public string lastDirection, tempDirection, roadSpite;

    void Awake()
    {
        pc = GetComponent<PathCreator>();
        spriteSelection = GetComponent<PathSpriteSelect>();
        CreateNode();

        // Create a new bezier path from the waypoints.
        /*BezierPath bezierPath = new BezierPath(node, closedLoop, PathSpace.xy);
        pc.bezierPath.AutoControlLength = 0.01f;
        pc.bezierPath = bezierPath;*/

    }

    void CreateNode()
    {
        int i = 0;
        do
        {
            var verticalOrHorizantal = Random.Range(0, 2); //0 is horizantal 1 is Vertical

            if (i == 0)
            {
                lastDirection = "Up";
            }
            else
            {
               
                if (node[i - 1].localPosition.y == destination.transform.localPosition.y) //if y position is equal to destination, road can be created only horizantal
                {
                    verticalOrHorizantal = 0;
                }

                if (Mathf.Floor(verticalOrHorizantal) == 0)
                {
                    var leftOrRight = 0;

                    if (node[i - 1].localPosition.y == destination.transform.localPosition.y)//if y position is equal to destination, road can be created only horizantall
                    {
                        if (node[i - 1].localPosition.x - destination.transform.localPosition.x < 0)// and the node is left or right from to destination created only left or right
                        {
                            leftOrRight = 1;
                            isRight = true;
                        }
                        else if (node[i - 1].localPosition.x - destination.transform.localPosition.x > 0)
                        {
                            leftOrRight = 0;
                            isLeft = true;
                        }
                    }
                    else
                    {
                        if (lastDirection == "Up")
                        {
                            leftOrRight = Random.Range(0, 2); //0 is Left 1 is Right
                        }
                        else if (lastDirection == "Left")
                        {
                            leftOrRight = 0;
                        }
                        else
                        {
                            leftOrRight = 1;
                        }
                    }

                    if (Mathf.Floor(leftOrRight) == 0 && isLeft)
                    {
                        isRight = false;
                        zeroVector.x-=100;
                        lastDirection = "Left";

                    }
                    else if (Mathf.Floor(leftOrRight) == 1 && isRight)
                    {
                        isLeft = false;
                        zeroVector.x+=100;
                        lastDirection = "Right";
                    }
                }
                else
                {
                    isLeft = true;
                    isRight = true;
                    zeroVector.y+=100;
                    lastDirection = "Up";
                }
            }

            if (destination.transform.localPosition.x - zeroVector.x > levelStepSize)
            {
                zeroVector.x+=100;
                isLeft = false;
                lastDirection = "Up";
                zeroVector.y+=100;
            }
            else if (destination.transform.localPosition.x - zeroVector.x < -levelStepSize)
            {
                zeroVector.x-=100;
                isRight = false;
                lastDirection = "Up";
                zeroVector.y+=100;
            }

            GameObject nodes = Instantiate(transformObjectInstantiate, zeroVector, transformObjectInstantiate.transform.rotation);
            nodes.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            nodes.gameObject.transform.parent = GameObject.Find("Road").transform;
            node.Add(nodes.transform);

            GameObject road = Instantiate(roadInstantiate, zeroVector, transformObjectInstantiate.transform.rotation);
            road.name = i.ToString();
            road.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            road.transform.parent = GameObject.Find("Road").gameObject.transform;

            if (tempDirection == "Up" && lastDirection == "Left")
            {
                roadSpite = "UpLeft";
            }
            else if (tempDirection == "Up" && lastDirection == "Right")
            {
                roadSpite = "UpRight";
            }
            else if (tempDirection == "Left" && lastDirection == "Left")
            {
                roadSpite = "LeftLeft";
            }
            else if (tempDirection == "Left" && lastDirection == "Up")
            {
                roadSpite = "LeftUp";
            }
            else if (tempDirection == "Right" && lastDirection == "Right")
            {
                roadSpite = "RightRight";
            }
            else if (tempDirection == "Right" && lastDirection == "Up")
            {
                roadSpite = "RightUp";
            }
            else if (tempDirection == "Up" && lastDirection == "Up")
            {
                roadSpite = "UpUp";
            }

            if (i != 0)
            {
                tempRoad.GetComponent<Image>().sprite = spriteSelection.RoadSpriteSelect(roadSpite);
            }

            tempRoad = road;
            tempDirection = lastDirection;
            i++;

            if (i > 50)
            {
                break;
            }

        } while (node[i - 1].transform.localPosition.x != destination.transform.localPosition.x || node[i - 1].transform.localPosition.y != destination.transform.localPosition.y);
    }
    
}
