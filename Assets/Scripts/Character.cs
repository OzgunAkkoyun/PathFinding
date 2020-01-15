using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Character : MonoBehaviour
{
    PathCanvas path;
    PathCreator pc;
    GetInputs getInputs;
    CameraShake cam;

    float distacetraveled;
    float speed = 1.0f;
    private Animator characterAnim;
    Vector3 inputVector;
    bool isAnimStarted;
    void Start()
    {
        path = FindObjectOfType<PathCanvas>();
        pc = FindObjectOfType<PathCreator>();
        characterAnim = GetComponent<Animator>();
        getInputs = FindObjectOfType<GetInputs>();
        cam = FindObjectOfType<CameraShake>();

        inputVector = transform.localPosition;
        //StartCoroutine("Jump");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator ExecuteAnimation()
    {
        for (int i = 0; i < getInputs.inputs.Count; i++)
        {
            if (getInputs.inputs[i] == KeyCode.LeftArrow)
            {
                inputVector.x-=100;
            }
            else if (getInputs.inputs[i] == KeyCode.RightArrow)
            {
                inputVector.x+=100;
            }
            else if (getInputs.inputs[i] == KeyCode.UpArrow)
            {
                inputVector.y+=100;
            }
            Debug.Log("Path: "+path.node[i].localPosition);
            Debug.Log("Input: "+inputVector);
            if (inputVector != path.node[i].localPosition)//Input is not correct
            {
                cam.TriggerShake();
            }

            if (isAnimStarted) yield break; // exit function
            isAnimStarted = true;
            for (float t = 0f; t < 1f; t += Time.deltaTime * 1f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, inputVector, t);
                yield return null;
            }
            transform.localPosition = inputVector;
            isAnimStarted = false;
        }
    }

    IEnumerator Jump()
    {
        for (float t = 0f; t < 1f; t += Time.deltaTime * 1f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.5f,1.5f,1.5f), t);
            yield return null;
        }

        for (float t = 1f; t < 0f; t -= Time.deltaTime * 1f)
        {
            transform.localScale = Vector3.Lerp(new Vector3(1.5f, 1.5f, 1.5f),transform.localScale, t);
            yield return null;
        }
    }
}
