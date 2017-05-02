using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
    [SerializeField]
    private float _health;
    [SerializeField]
    private float _knockbackForce;
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private GameObject _hurtPoint;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private int _meleeDamage;
    [SerializeField]
    private bool _facingRight;
    [SerializeField]
    private GameObject _deathAnim;
    [SerializeField]
    private GameObject _stunnedAnim;
    private Rigidbody2D _rigidBody;
    private Color _color;
    private float _viewDistance = 5f;
    private Transform _center;
    private float _minDistance = 0.05f;
    private bool _stunned;

    // Use this for initialization
    void Start() {
        _stunned = false;
        _rigidBody = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(_player.GetComponent<CapsuleCollider2D>(), GetComponent<BoxCollider2D>());
        _color = gameObject.GetComponent<Renderer>().material.color;
        Physics2D.IgnoreLayerCollision(10, 16, true);
        Physics2D.IgnoreLayerCollision(10, 0, true);
        _player = GameObject.FindWithTag("Player");
        _center = transform.Find("CenterPoint");
    }

    // Update is called once per frame
    void Update() {

        float distance = Vector2.Distance(transform.position, _player.transform.position);
        if (distance < _viewDistance && distance > _minDistance && !_stunned)
        {
            MeleeAttack();
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, 3 * Time.deltaTime);
        }

        if (_health <= 0)
        {
            Die();
        }
        if (_rigidBody.velocity.y > 0 && !_facingRight)
        {
            //Flip();
        }
        else if (_rigidBody.velocity.y< 0 && _facingRight)
        {
            //Flip();
        }



    }
    //Placeholder for hurt anim
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

    private void Flip()
    {
        // Switch player sprite heading
        _facingRight = !_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    public void TakeDamage(float damage)
    {
        StartCoroutine(HurtAnim());
        if (transform.position.x < _player.transform.position.x)
        {
            //knock vasempaan
            _rigidBody.velocity = Vector2.zero;
            _rigidBody.AddForce(new Vector2(-_knockbackForce, _knockbackForce));
        }
        else if (transform.position.x > _player.transform.position.x)
        {
            //knock oikealle
            _rigidBody.velocity = Vector2.zero;
            _rigidBody.AddForce(new Vector2(_knockbackForce, _knockbackForce));
        }
        _health -= damage;
    }

    public void MaceStun()
    {
        StartCoroutine(Stunned());
    }
    IEnumerator Stunned()
    {
        _stunned = true;
        _stunnedAnim.SetActive(true);
        yield return new WaitForSeconds(2f);
        _stunnedAnim.SetActive(false);
        _stunned = false;
    }
    private void MeleeAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.05f, LayerMask.GetMask("Default"));
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
        GameObject poof = Instantiate(_deathAnim, _center.position, _center.rotation);
        Destroy(gameObject);
        Destroy(poof, 0.5f);
    }
}
