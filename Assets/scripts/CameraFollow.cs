using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [Range(0, 1)]public float followRate;
    public float zDepth;

    private GameObject[] targets;

	// Use this for initialization
	void Start () {
        targets = GameObject.FindGameObjectsWithTag("Player");
        transform.position = GetFocusPoint();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position += (GetFocusPoint() - transform.position) * followRate;
	}


    private Vector3 GetFocusPoint()
    {
        Vector3 focus = new Vector3();
        int count = 0;

        foreach(GameObject target in targets)
            if(target != null)
            {
                focus += new Vector3(target.transform.position.x, target.transform.position.y, zDepth);
                count += 1;
            }

        focus /= count;

        float longestDistance = 10;

        foreach(GameObject target in targets)
        {
            if(target != null && (target.transform.position - focus).magnitude > longestDistance)
            {
                longestDistance = (target.transform.position - focus).magnitude;
            }
        }

        return new Vector3(focus.x, focus.y, -Mathf.Pow(longestDistance, 0.6f) * 3);
    }
}
