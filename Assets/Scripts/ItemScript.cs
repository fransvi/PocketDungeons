using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {



    /*
    Item types:
    0: Health potion
    1: Gold Coin
    2: Key1
    3: OWeapon Bow

    */
    [SerializeField]
    GameObject[] _items;
    [SerializeField]
    private int _itemInt;

    GameObject _activeItem;

	// Use this for initialization
	void Start () {

        Physics2D.IgnoreLayerCollision(16, 16, true);
        _activeItem = _items[_itemInt];
        for(int i = 0; i < _items.Length; i++)
        {
            if(_items[i] != _activeItem)
            {
                _items[i].SetActive(false);
            }
            else
            {
                _items[i].SetActive(true);
            }
        }
	}

    public int GetItemInt()
    {
        return _itemInt;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void SetItemInt(int i)
    {
        _itemInt = i;
    }
	
	// Update is called once per frame
	void Update () {
        _activeItem = _items[_itemInt];
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] != _activeItem)
            {
                _items[i].SetActive(false);
            }
            else
            {
                _items[i].SetActive(true);
            }
        }
    }
}
