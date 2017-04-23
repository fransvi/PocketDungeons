using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour {

    [SerializeField]
    private Sprite[] _healthBar;
    [SerializeField]
    private Image _currentHealthSprite;
    [SerializeField]
    private Sprite[] _consumableSprites;
    [SerializeField]
    private Sprite[] _mainWeaponSprites;
    [SerializeField]
    private Sprite[] _offWeaponSprites;
    [SerializeField]
    private Sprite[] _slotSprites;
    [SerializeField]
    private Image _current1Slot;
    [SerializeField]
    private Image _current2Slot;
    [SerializeField]
    private Image _current3Slot;
    [SerializeField]
    private Image _current4Slot;
    [SerializeField]
    private Image _keySlot;
    [SerializeField]
    private Text _coinsText;

    [SerializeField]
    private GameObject _playerController;
    private GameObject _playerManager;


    public void SetPlayerStats(GameObject player, GameObject inventory)
    {
        _playerController = player;
        _playerManager = inventory;
        
    }

	// Update is called once per frame
	void Update () {
        int hp = _playerController.GetComponent<PlayerController>().GetHealth();
        _currentHealthSprite.overrideSprite = _healthBar[hp];

        

        bool hasPotion = _playerManager.GetComponent<PlayerInventory>().getHasPotion();
        bool hasKey = _playerManager.GetComponent<PlayerInventory>().getHasKey1();
        float goldAmount = _playerManager.GetComponent<PlayerInventory>().GetCurrentGold();
        int mainWeapon = _playerManager.GetComponent<PlayerInventory>().GetCurrentMainWeapon();
        int offWeapon = _playerManager.GetComponent<PlayerInventory>().GetCurrentOffWeapon();


        _coinsText.text = "x" + goldAmount;
        //TODO A button what do?
        _current1Slot.gameObject.SetActive(false);

        if(offWeapon == 0)
        {
            _current3Slot.gameObject.SetActive(false);
        }
        else if(offWeapon == 1)
        {
            _current3Slot.gameObject.SetActive(true);
            _current3Slot.sprite = _offWeaponSprites[1];
        }
        else if(offWeapon == 2)
        {
            _current3Slot.gameObject.SetActive(true);
            _current3Slot.sprite = _offWeaponSprites[2];
        }
        else if(offWeapon == 3)
        {
            _current3Slot.gameObject.SetActive(true);
            _current3Slot.sprite = _offWeaponSprites[3];
        }

        if(mainWeapon == 0)
        {
            _current2Slot.gameObject.SetActive(true);
            _current2Slot.sprite = _mainWeaponSprites[0];
        }
        else if(mainWeapon == 1)
        {
            _current2Slot.gameObject.SetActive(true);
            _current2Slot.sprite = _mainWeaponSprites[1];
        }
        if (hasPotion)
        {
            _current4Slot.gameObject.SetActive(true);
            _current4Slot.sprite = _consumableSprites[0];
        }
        else
        {
            _current4Slot.gameObject.SetActive(false);
        }
        if(hasKey)
        {
            _keySlot.gameObject.SetActive(true);
        }
        else
        {
            _keySlot.gameObject.SetActive(false);
        }
		
	}
}
