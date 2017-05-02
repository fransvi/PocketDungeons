using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour {


    public int _moveAxis;
    private Vector3 _moveDir1;
    private Vector3 _moveDir2;
    public float _moveMultiplr;
    public bool _platformAtTop;
    public bool _platformAtBot;
    public float _moveTime;
    private bool _movingDown;
    private bool _movingUp;

	// Use this for initialization
	void Start () {
        if(_moveAxis == 0)
        {
            _moveDir1 = Vector3.up;
            _moveDir2 = Vector3.down;
        }else if(_moveAxis == 1)
        {
            _moveDir1 = Vector3.left;
            _moveDir2 = Vector3.right;
        }

        _movingDown = false;
        _movingUp = false;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (_platformAtTop)
        {       
            StartCoroutine(PlatTimerDown());                   
        }
        else if (_platformAtBot)
        {
            StartCoroutine(PlatTimerUp());                   
        }
        if (_movingDown)
        {
            transform.position += _moveDir2 * _moveMultiplr;
        }
        else if (_movingUp)
        {
            transform.position += _moveDir1 * _moveMultiplr;
        }
	}
    IEnumerator PlatTimerDown()
    {
        _movingDown = true;
        _platformAtTop = false;
        yield return new WaitForSeconds(_moveTime);
        _platformAtBot = true;
        _movingDown = false;
        _movingUp = true;
    }
    IEnumerator PlatTimerUp()
    {
        _movingUp = true;
        _platformAtBot = false;
        yield return new WaitForSeconds(_moveTime);
        _platformAtTop = true;
        _movingUp = false;
        _movingDown = true;
    }

}
