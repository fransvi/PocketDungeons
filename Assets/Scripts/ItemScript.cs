using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {



    /*
    Item types:

    0: Health potion
    1: Large Coin
    2: Key1
    3: Bow
    4: Small Coin
    5: Bomb
    6: Shield

    */
    [SerializeField]
    GameObject[] _items;
    [SerializeField]
    private int _itemInt;

    GameObject _activeItem;

	// Use this for initialization
	void Start () {

        Physics2D.IgnoreLayerCollision(16, 16, true);
        Physics2D.IgnoreLayerCollision(16, 0, true);
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

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.01f, LayerMask.NameToLayer("Ground") | LayerMask.NameToLayer("Stairs") 
            | LayerMask.NameToLayer("Platform"));
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                Physics2D.gravity = Vector2.zero;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }

        }
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
