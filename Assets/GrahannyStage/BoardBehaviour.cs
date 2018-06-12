using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehaviour : MonoBehaviour {

    public Transform platformPrefab;
    public float timeActive = 0;
    private bool active = false;
    public Transform otherBoard;
    private BoardBehaviour otherB;
    private ParticleSystem ps;
    ParticleSystem.MainModule settings;

    // Use this for initialization
    void Start () {
        otherB = otherBoard.GetComponent<BoardBehaviour>();
        settings = transform.GetChild(0).GetComponent<ParticleSystem>().main;
    }
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            timeActive += Time.deltaTime;
            settings.startColor = new ParticleSystem.MinMaxGradient(Color.green);
        }
        else if (otherB.getActive()) settings.startColor = new ParticleSystem.MinMaxGradient(Color.yellow);
        else settings.startColor = new ParticleSystem.MinMaxGradient(Color.red);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 9)
        {
            active = true;
            if (otherB.getActive())
                Instantiate(platformPrefab, new Vector3(0,10,0), Quaternion.identity);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == 9)
        {
            active = false;
            timeActive = 0;
        }
    }

    public bool getActive()
    {
        return active;
    }

    public void setActive(bool b)
    {
        active = b;
    }

    public float getActiveTime()
    {
        return timeActive;
    }
}
