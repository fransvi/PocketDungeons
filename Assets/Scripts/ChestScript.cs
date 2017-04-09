using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour {


    [SerializeField]
    Sprite[] _chestStates;
    [SerializeField]
    private Transform _openPoint;
    [SerializeField]
    private GameObject[] _gameObjects;

    [SerializeField]
    private int _chestContainsInt;

    [SerializeField]
    private GameObject _chestContainsItem;

    [SerializeField]
    private int _chestContainsAmount;

    private int _chestState;


	// Use this for initialization
	void Start () {

        _chestContainsItem.GetComponent<ItemScript>().SetItemInt(_chestContainsInt);
		
	}
	
	// Update is called once per frame
	void Update () {
        if(_chestState == 0)
        {
            GetComponent<SpriteRenderer>().sprite = _chestStates[0];
        }else if(_chestState == 1)
        {
            GetComponent<SpriteRenderer>().sprite = _chestStates[1];
        }
		
	}

    public void OpenChest()
    {
        if(_chestState == 0)
        {
            for (int i = 0; i < _chestContainsAmount; i++)
            {
                Vector3 newPos = new Vector3(_openPoint.position.x, _openPoint.position.y, _openPoint.position.z - 2);
                GameObject go = Instantiate(_chestContainsItem, newPos, _openPoint.rotation);
                float itemForcey = Random.Range(150, 300);
                float itemForcex = Random.Range(150, 300);
                go.GetComponent<Rigidbody2D>().AddForce(new Vector2(itemForcex, itemForcey));
            }
            _chestState = 1;
        }

    }
}
