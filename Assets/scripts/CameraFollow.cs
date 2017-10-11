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

        foreach(GameObject target in targets)
            if(target != null)
                focus += new Vector3(target.transform.position.x, target.transform.position.y, zDepth);
        
        focus /= targets.Length + 1;

        if(targets.Length <= 1)
            return focus;
        else
        {
            float distance = (targets[0].transform.position - targets[1].transform.position).magnitude;
            return new Vector3(focus.x, focus.y, Mathf.Pow(distance, 0.2f) * -10f);
        }
    }
}
