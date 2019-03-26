using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Config parameters
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minX = 1.25f;
    [SerializeField] float maxX = 14.75f;

    // Cache variables
    GameSession theGameSession;
    Ball theBall;

    // Sets the gameObject GameSession to the variable theGameSession.
    // Sets the gameObject Ball to the variable theBall.
    public void Start()
    {
        theGameSession = FindObjectOfType<GameSession>();
        theBall = FindObjectOfType<Ball>();
    }

    // Prevents the paddle from moving off the screen.
    void Update()
    {
        Vector3 paddlePos = new Vector3(transform.position.x, transform.position.y, -1f)
        {
            // Only allows the paddle to move between the minX and maxX varibales.
            x = Mathf.Clamp(GetXPos(), minX, maxX)
        };
        transform.position = paddlePos;
    }

    // Returns the value of the mouse's X position or the paddles X position depending if AutoPlay is enabled.
    private float GetXPos()
    {

        if (theGameSession.IsAutoPlayEnabled())
        {
            return theBall.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
