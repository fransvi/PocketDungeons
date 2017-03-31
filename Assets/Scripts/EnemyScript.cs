using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
    [SerializeField]
    private float _health;
    [SerializeField]
    private float _knockbackForce;
    [SerializeField]
    private Transform _player;

    private Rigidbody2D _rigidBody;
    private Color _color;


    // Use this for initialization
    void Start() {
        _rigidBody = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(_player.GetComponent<BoxCollider2D>(), GetComponent<CircleCollider2D>());
        _color = gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update() {
        if (_health <= 0)
        {
            Die();
        }

    }
    //Placeholder for hurt anim
    IEnumerator HurtAnim()
    {
        Renderer rend = gameObject.GetComponent<Renderer>();
        rend.material.color = Color.white;
        yield return new WaitForSeconds(0.10f);
        rend.material.color = _color;
    }


    public void TakeDamage(float damage)
    {
        StartCoroutine(HurtAnim());
        if (transform.position.x < _player.position.x)
        {
            //knock vasempaan
            _rigidBody.AddForce(new Vector2(-_knockbackForce, _knockbackForce));
        }
        else if (transform.position.x > _player.position.x)
        {
            //knock oikealle
            _rigidBody.AddForce(new Vector2(_knockbackForce, _knockbackForce));
        }
        _health -= damage;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
