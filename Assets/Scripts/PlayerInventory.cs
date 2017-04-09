using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    [SerializeField]
    private GameObject[] _mWeapons;
    [SerializeField]
    private GameObject[] _oWeapons;

    [SerializeField]
    private GameObject _activeMainWeapon;
    private bool _hasKey1;
    private float _playerGold;
    private bool _hasHealthPotion;
    private GameObject _activeOffWeapon;
    private GameObject _playerController;


    private bool _hasBow;

	// Use this for initialization
	void Start () {
        _playerGold = 0f;
        _hasKey1 = false;
        _hasHealthPotion = false;
        _hasBow = false;
        _playerController = GameObject.Find("Player");
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}


    public void gainGold(float val)
    {
        this._playerGold += val;
    }

    public void loseGold(float val)
    {
        if(_playerGold - val >= 0)
        {
            _playerGold -= val;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Gold: "+_playerGold);
        GUI.Label(new Rect(10, 30, 100, 20), "Key1: " + _hasKey1);
        GUI.Label(new Rect(10, 50, 100, 20), "Pot: " + _hasHealthPotion);
        GUI.Label(new Rect(10, 70, 100, 20), "Bow: " + _hasBow);
    }


    public void gainKey1()
    {
        this._hasKey1 = true;
    }

    public bool getHasKey1()
    {
        return _hasKey1;
    }
    public bool getHasPotion()
    {
        return _hasHealthPotion;
    }

    public void useHealthPotion()
    {
        this._hasHealthPotion = false;
    }

    public void gainHealthPotion()
    {
        this._hasHealthPotion = true;
    }
    // Placeholders for weapons
    public bool getHasBow()
    {
        return _hasBow;
    }

    public void gainBow()
    {
        _hasBow = true;
    }


    public void setMw(GameObject go)
    {
        foreach(GameObject g in _mWeapons)
        {
            if(g == go)
            {
                _activeMainWeapon = g;
            }
        }

    }

    public void setOw(GameObject go)
    {
        foreach (GameObject g in _oWeapons)
        {
            if (g == go)
            {
                _activeMainWeapon = g;
            }
        }

    }


    public GameObject getMw()
    {
        return _activeMainWeapon;
    }

    public GameObject getOw()
    {
        return _activeOffWeapon;
    }
}
