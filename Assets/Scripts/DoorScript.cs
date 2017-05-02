using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {


    [SerializeField]
    Sprite[] _doorStates;

    public bool _requiresKey;

    private int _doorState;
	// Use this for initialization
	void Start () {
        _doorState = 0;
	}
	
	// Update is called once per frame
	void Update () {
        
		if(_doorState == 0)
        {
            GetComponent<SpriteRenderer>().sprite = _doorStates[0];
        }else if(_doorState == 1)
        {
            GetComponent<SpriteRenderer>().sprite = _doorStates[1];
        }
	}

    public int GetDoorState()
    {
        return _doorState;
    }
    public bool GetRequiresKey()
    {
        return _requiresKey;
    }

    public void SetDoorState(int ds)
    {
        _doorState = ds;
    }
}
