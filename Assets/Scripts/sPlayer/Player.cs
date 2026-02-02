using System;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    Rigidbody2D rBody;
    float axisH = 0.0f;

    //bool atRight = false;
    //bool atLeft = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rBody = this.GetComponent<Rigidbody2D>();

        //atRight = false;
        //atLeft = false;
    }

    // Update is called once per frame
    void Update()
    {

        axisH = Input.GetAxisRaw("Horizontal");

        //if (Input.GetKeyDown(KeyCode.RightArrow)) {  atRight = true; }
        //if (Input.GetKeyUp(KeyCode.RightArrow)) {  atRight = false; }

        //if (Input.GetKeyDown(KeyCode.LeftArrow)) {  atLeft = true; }
        //if (Input.GetKeyUp(KeyCode.LeftArrow)) {  atLeft = false; }

        //if (!atRight) { Console.WriteLine("âEÇ™âüÇ≥ÇÍÇƒÇ¢Ç‹Ç∑"); }
        //if (atLeft) { Console.WriteLine("ç∂Ç™âüÇ≥ÇÍÇƒÇ¢Ç‹Ç∑"); }
    }

    private void FixedUpdate()
    {
        rBody.linearVelocity = new Vector2(axisH * 3.0f, rBody.linearVelocity.y);
    }
}
