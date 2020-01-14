using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PathCreation.BezierPath;

public class PathInstantiate : MonoBehaviour
{
    public GameObject transformObjectInstantiate;
    public GameObject roadInstantiate;
    private GameObject tempRoad;
    public GameObject destination;
    
    [HideInInspector]
    public List<Transform> node = new List<Transform>();

    PathCreator pc;
    PathSpriteSelect spriteSelection;

    private Vector3 zeroVector = new Vector3(0, -4f, 0);

    public bool isLeft,isRight = true;
    private bool closedLoop = false;
    private int onlyLeftOrRight =-1;
    public int levelStepSize;

    [HideInInspector]
    public string lastDirection, tempDirection,roadSpite;
    
    void Start()
    {
        pc = GetComponent<PathCreator>();
        spriteSelection = GetComponent<PathSpriteSelect>();
        CreateNode();

        // Create a new bezier path from the waypoints.
        BezierPath bezierPath = new BezierPath(node, closedLoop, PathSpace.xy);
        pc.bezierPath.AutoControlLength = 0.01f; 
        pc.bezierPath = bezierPath;
    }

    void CreateNode()
    {
        int i = 0;
        do
        {
            var verticalOrHorizantal = Random.Range(0, 2); //0 is horizantal 1 is Vertical

            if (i == 0 )
            {
                lastDirection = "Up";
            }
            else
            {
                if (node[i - 1].position.y == destination.transform.position.y) //if y position is equal to destination, road can be only created in horizantal
                {
                    verticalOrHorizantal = 0;
                }

                if (Mathf.Floor(verticalOrHorizantal) == 0)
                {
                    var leftOrRight = 0;

                    if (node[i - 1].position.y == destination.transform.position.y)//if y position is equal to destination, road can be only created in horizantal
                    {
                        if (node[i - 1].position.x - destination.transform.position.x < 0)// and the node is left or right from to destination only created left or right
                        {
                            leftOrRight = 1;
                            isRight = true;
                        }
                        else if (node[i - 1].position.x - destination.transform.position.x > 0)
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
                        zeroVector.x--;
                        lastDirection = "Left";

                    }
                    else if (Mathf.Floor(leftOrRight) == 1 && isRight)
                    {
                        isLeft = false;
                        zeroVector.x++;
                        lastDirection = "Right";
                    }
                }
                else
                {
                    isLeft = true;
                    isRight = true;
                    zeroVector.y++;
                    lastDirection = "Up";
                    
                }
            }

            if (destination.transform.position.x - zeroVector.x > levelStepSize)
            {
                zeroVector.x++;
                isLeft = false;
                lastDirection = "Up";
                zeroVector.y++;
            }
            else if (destination.transform.position.x - zeroVector.x < -levelStepSize)
            {
                zeroVector.x--;
                isRight = false;
                lastDirection = "Up";
                zeroVector.y++;
            }

            node.Add(Instantiate(transformObjectInstantiate, zeroVector, transformObjectInstantiate.transform.rotation).transform);

            GameObject road = Instantiate(roadInstantiate, zeroVector, transformObjectInstantiate.transform.rotation);
            road.name = i.ToString();

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
                tempRoad.GetComponent<SpriteRenderer>().sprite = spriteSelection.RoadSpriteSelect(roadSpite);
            }

            tempRoad = road;
            tempDirection = lastDirection;
            i++;
            if (i > 50)
            {
                break;
            }
        } while (node[i-1].transform.position.x != destination.transform.position.x || node[i-1].transform.position.y != destination.transform.position.y);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
