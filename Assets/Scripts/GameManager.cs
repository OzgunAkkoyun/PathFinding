using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Character character;
    GetInputs getInputs;
    PathInstantiate pi;

    void Start()
    {
        getInputs = FindObjectOfType<GetInputs>();
        character = FindObjectOfType<Character>();
        pi = FindObjectOfType<PathInstantiate>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            character.StartCoroutine( "ExecuteAnimation" );
        }
    }
    
}
