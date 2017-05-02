using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private bool _airControl;
    [SerializeField]
    private LayerMask _whatIsGround;
    [SerializeField]
    private LayerMask _whatIsPlatform;
    [SerializeField]
    private LayerMask _enemyLayerMask;
    [SerializeField]
    private float _jumpCooldown;
    [SerializeField]
    private float _meleeRadius;
    [SerializeField]
    private float _meleeDamage;

    private float _horizontalMove = 0;
    private bool _jump = false;
    private bool _fallingThrough = false;
    private Transform _groundCheck;
    private Transform _meleeCheck;
    private Transform _headCheck;

    //Placeholder

    private Transform _weaponSwing;
    private Rigidbody2D _rigidBody;
    const float _groundedRadius = .2f;
    private bool _grounded;
    private bool _facingRight;
    private bool _onPlatform;
    private bool _isJumping;
    private Vector2 _gravity;

	void Start () {
        _meleeCheck = transform.Find("MeleeCheck");
        _groundCheck = transform.Find("GroundCheck");
        _weaponSwing = transform.Find("Swing");
        _rigidBody = GetComponent<Rigidbody2D>();
        _weaponSwing.gameObject.SetActive(false);
        _gravity = Physics2D.gravity;
	
	}

    //KB Controls
    void Update()
    {
        // float x = Input.GetAxis("Horizontal");

        // Nuoli vasempaan
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CancelInvoke();
            InvokeRepeating("DecreaseSpeed", 0, 0.05f);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            CancelInvoke();
            InvokeRepeating("StopMove", 0, 0.05f);
        }
        // Nuoli oikeaan
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CancelInvoke();
            InvokeRepeating("AddSpeed", 0, 0.05f);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            CancelInvoke();
            InvokeRepeating("StopMove", 0, 0.05f);
        }
        // Nuoli alas
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            
            //TODO
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            _fallingThrough = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpDown();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            JumpUp();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            //TODO
        }
    }

    void FixedUpdate () {
        _rigidBody.AddForce(_gravity * _rigidBody.mass);
        _grounded = false;
        //Checks for ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, _groundedRadius, _whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                _gravity = Physics2D.gravity;
                _grounded = true;
            }
 
        }
        //Checks for platforms
        GameObject platform;
        Collider2D[] platforms = Physics2D.OverlapCircleAll(_groundCheck.position, _groundedRadius, _whatIsPlatform);
        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i].gameObject != gameObject)
            {
                _gravity = Physics2D.gravity;
                _grounded = true;
                _onPlatform = true;
                platform = platforms[i].gameObject;
            }

        }
        Move(_horizontalMove, _jump);
    }

    public void Move(float move, bool jump)
    {
        if (_grounded || _airControl)
        {
            //Horizontal force
            _rigidBody.velocity = new Vector2(move * _maxSpeed, _rigidBody.velocity.y);

            //Direction flip
            if (move > 0 && !_facingRight)
            {
                Flip();
            }
            else if (move < 0 && _facingRight)
            {
                Flip();
            }
        }
        //Vertical force
        if (_grounded && _jump)
        {
            _grounded = false;
            _rigidBody.AddForce(new Vector2(0f, _jumpForce));
        }
        if (_onPlatform && _fallingThrough)
        {

        }
    }
    private void DropDown()
    {
        if (_onPlatform)
        {

        }
    }
    IEnumerator IgnorePlatform()
    {
        _grounded = false;

        yield return new WaitForSeconds(1);
    }

    private void Flip()
    {
        // Switch player sprite heading
        _facingRight = !_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    //Placeholder for attack anims
    IEnumerator AttackAnim()
    {
        _weaponSwing.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        _weaponSwing.gameObject.SetActive(false);
    }

    public void Attack()
    {
        StartCoroutine(AttackAnim());
        //Check if enemies hit
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_meleeCheck.position, _meleeRadius, _enemyLayerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                //Enemy hit
                colliders[i].gameObject.GetComponent<EnemyScript>().TakeDamage(_meleeDamage);
            }
        }
    }


    //Funktiot touch screen liikkumiselle
    public void AddSpeed()
    {
        if(_horizontalMove <= 1f)
        {
            if(_horizontalMove + 0.15f > 1f)
            {
                _horizontalMove = 1f;
            }
            else
            {
                _horizontalMove += 0.15f;
            }

        }
    }
    public void DecreaseSpeed()
    {
        if(_horizontalMove >= -1f)
        {
            if(_horizontalMove - 0.15f < -1f)
            {
                _horizontalMove = -1f;
            }
            else
            {
                _horizontalMove -= 0.15f;
            }

        }
    }
    public void StopMove()
    {
        if (_horizontalMove > 0f)
        {
            {
                if(_horizontalMove - 0.10f < 0f)
                {
                    _horizontalMove = 0f;
                }else if(_horizontalMove > 0f)
                {
                    _horizontalMove -= 0.10f;
                }
            }

        }
        else
        {
            if (_horizontalMove + 0.10f > 0f)
            {
                _horizontalMove = 0f;
            }
            else if (_horizontalMove < 0f)
            {
                _horizontalMove += 0.10f;
            }
        }
    }
    public void LeftMoveDown()
    {
        CancelInvoke();
        InvokeRepeating("DecreaseSpeed", 0, 0.05f);
    }
    public void RightMoveDown()
    {
        CancelInvoke();
        InvokeRepeating("AddSpeed", 0, 0.05f);
    }
    public void MoveUp()
    {
        CancelInvoke();
        InvokeRepeating("StopMove", 0, 0.05f);
    }
    public void JumpUp()
    {
        _gravity = Physics2D.gravity * 1.5f;
        _jump = false;
    }
    public void JumpDown()
    {
        _gravity = _gravity / 1.5f;
        _jump = true;
    }

}
