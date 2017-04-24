using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_King : MonoBehaviour {

	Collider2D[] colliders;
	Color _color;

	public int _meleeDamage;
	public float _health;
	public GameObject _deathAnim;

	// Use this for initialization
	void Start () {
		_color = gameObject.GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {

		if (_health <= 0)
		{
			Die();
		}

		colliders = Physics2D.OverlapCircleAll(transform.position, 1f, LayerMask.GetMask("Default"));
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				if (colliders[i].gameObject.GetComponent<PlayerController>())
				{
					colliders[i].gameObject.GetComponent<PlayerController>().Hurt(_meleeDamage);
				}
			}
		}
	}


	private void Die()
	{
		GameObject poof = Instantiate(_deathAnim, transform.position, transform.rotation);
		Destroy(gameObject);
		Destroy(poof, 0.5f);
	}

	IEnumerator HurtAnim()
	{
		Renderer rend = gameObject.GetComponent<Renderer>();
		for (int i = 0; i < 2; i++)
		{
			rend.material.color = new Color(1f, 1f, 1f, 0f);
			yield return new WaitForSeconds(0.1f);
			rend.material.color = _color;
			yield return new WaitForSeconds(0.1f);
		}
	}

	public void TakeDamage(float damage)
	{
		StartCoroutine(HurtAnim());
		_health -= damage;
	}
}
