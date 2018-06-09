using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    public int maxNumber;
    public float spawnTime;
    public Transform prefab;
    private float timeFromLastSpawned = 0;
    private List<Vector3> finalPoints = new List<Vector3>();
    private List<Vector3> targetPoints = new List<Vector3>();

	// Use this for initialization
	void Start () {
        finalPoints.Add(new Vector3(0,0,-15.70f));
        finalPoints.Add(new Vector3(0,0,15.70f));
        finalPoints.Add(new Vector3(15.70f,0,0));
        finalPoints.Add(new Vector3(-15.70f,0,0));

        targetPoints.Add(new Vector3(0, 0, -14.70f));
        targetPoints.Add(new Vector3(0, 0, 14.70f));
        targetPoints.Add(new Vector3(14.70f, 0, 0));
        targetPoints.Add(new Vector3(-14.70f, 0, 0));
    }
	
	// Update is called once per frame
	void Update () {
		if (getLambSpawnedNumber() < maxNumber && timeFromLastSpawned >= spawnTime)
        {
            Random rnd = new Random();
            int start = Random.Range(0,finalPoints.Count-1);
            int end;
            do
            {
                end = Random.Range(0, finalPoints.Count - 1);
            }
            while (end == start);
            Transform lamb = Instantiate(prefab, finalPoints[start], Quaternion.identity);
            Pathfinding pf = lamb.GetComponent<Pathfinding>();
            pf.finalPoint = finalPoints[end];
            pf.target = targetPoints[end];
            //USARE SOLO UN PUNTO E MODIFICARE LA GRIGLIA (?)
        }
	}

    private int getLambSpawnedNumber()
    {
        int n = 0;
        foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>())
            if (go.layer == 14)
                n++;
        return n;
    }
}
