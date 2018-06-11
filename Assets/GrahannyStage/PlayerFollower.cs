using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour {

    private Transform player;
    public float speed;
    private bool following = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (following)
        {
            transform.LookAt(player.transform.position);
            transform.position += (player.transform.position - transform.position).normalized * speed * Time.deltaTime;
        }
    }

    public void ChangeTarget(Transform t)
    {
        player = t;
    }

    public void setFollowing(bool b)
    {
        following = b;
    }
}
