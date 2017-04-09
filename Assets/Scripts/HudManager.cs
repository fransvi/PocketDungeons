using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour {

    [SerializeField]
    private Sprite[] _healthBar;
    [SerializeField]
    private Image _currentHealthSprite;

    private GameObject _playerController;

	// Use this for initialization
	void Start () {
        _playerController = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        int hp = _playerController.GetComponent<PlayerController>().GetHealth();
        _currentHealthSprite.overrideSprite = _healthBar[hp];
		
	}
}
