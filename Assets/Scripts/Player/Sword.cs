using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject wielder;
    public float speed;

    public Vector2 startMarker;
    public Vector2 endMarker;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;


    private void Awake()
    {
        wielder = GameObject.FindGameObjectWithTag("Player");
        startMarker = new Vector2(wielder.transform.position.x, wielder.transform.position.y);
        endMarker = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker, endMarker);

        //transform.right = wielder.transform.position - transform.position;
    }

    private void Update()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector2.Lerp(startMarker, endMarker, fractionOfJourney);
    }
}
