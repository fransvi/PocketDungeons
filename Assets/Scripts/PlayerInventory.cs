using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {


    [SerializeField]
    private GameObject[] _mWeapons;
    [SerializeField]
    private GameObject[] _sWeapons;
    [SerializeField]
    private GameObject _activeMainWeapon;


    private bool _hasKey1;
    private float _playerGold;
    private bool _hasHealthPotion;
    private GameObject _activeOffWeapon;

	// Use this for initialization
	void Start () {
        _playerGold = 0f;
        _hasHealthPotion = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
