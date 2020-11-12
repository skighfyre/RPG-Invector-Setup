using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField]
    GameObject ScoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ScoreBoard.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Y))
        {
            ScoreBoard.SetActive(false);
        }
        
    }
}
