using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
    //Animaattori
    //11
    public Animator CharacterAnimator;

    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _jumpCooldown;
    [SerializeField]
    private float _attackCooldown;
    [SerializeField]
    private bool _airControl;
    [SerializeField]
    private LayerMask _whatIsGround;
    [SerializeField]
    private LayerMask _whatIsPlatform;
    [SerializeField]
    private LayerMask _whatIsSpikes;
    [SerializeField]
    private LayerMask _whatIsLadder;
    [SerializeField]
    private LayerMask _enemyLayerMask;
    [SerializeField]
    private LayerMask _whatIsItem;
    [SerializeField]
    private LayerMask _whatIsDoor;
    [SerializeField]
    private LayerMask _whatIsChest;
    [SerializeField]
    private LayerMask _whatIsStairs;
    [SerializeField]
    private LayerMask _whatIsLever;
    [SerializeField]
    private float _meleeRadius;
    [SerializeField]
    private float _meleeDamage;
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private float _knockbackForce;
    [SerializeField]
    private Transform _ladderCheck;
    [SerializeField]
    private GameObject _bullet;

    private int _health;
    private bool _jumpOnCooldown = false;
    private float _horizontalMove = 0;
    private bool _jump = false;
    private Transform _groundCheck;
    private Transform _meleeCheck;
    private Transform _headCheck;

    private GameObject _playerManager;
    private bool _attackOnCooldown = false;

    private GameManager _gameManager;
    private Transform _weaponSwing;
    [SerializeField]
    private float _slopeFriction;

    private bool _drawingBow;
    private float _bowForce;
    private bool _onLadder;
    private Rigidbody2D _rigidBody;
    private bool _invulnurable;
    const float _groundedRadius = .1f;
    private bool _grounded;
    private float tempSpeed;
    private bool _facingRight;
    private bool _isTakingDamage;
    private bool _onPlatform;
    private bool _isJumping = false;
    private Vector2 _gravity;
    private Color _color;

    void Start () {
        _meleeCheck = transform.Find("MeleeCheck");
        _groundCheck = transform.Find("GroundCheck");
        _ladderCheck = transform.Find("LadderCheck");
        _weaponSwing = transform.Find("Swing");
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _health = _maxHealth;
        _invulnurable = false;
        _onLadder = false;
        _weaponSwing.gameObject.SetActive(false);
        _gravity = Physics2D.gravity;
        tempSpeed = _maxSpeed;
        _playerManager = GameObject.Find("PlayerManager");
        Physics2D.IgnoreLayerCollision(0, 16, true);
        Physics2D.IgnoreLayerCollision(0, 18, true);
        //Animaattori
        _color = gameObject.GetComponent<Renderer>().material.color;
        CharacterAnimator = GetComponent<Animator>();
    }

    //KB Controls
    void Update()
    {


        //Counter velocity on ramps
        float angle;
        RaycastHit2D[] hits = new RaycastHit2D[2];
        //cast ray downwards
        int h = Physics2D.RaycastNonAlloc(transform.position, -Vector2.up, hits);
        if (h > 1)
        {
            angle = Mathf.Abs(Mathf.Atan2(hits[1].normal.x, hits[1].normal.y) * Mathf.Rad2Deg); //get angle
            if (angle > 30)
            {

                _maxSpeed = tempSpeed / 2;
            }
            else
            {
                _maxSpeed = tempSpeed;
            }
        }


            /*
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
            */
            // Nuoli alas
            // Go down platforms

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _jump = true;
                //JumpDown();
                //Jump-animaatio

            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _jump = false;
                //JumpUp();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (!_attackOnCooldown)
                {
                    Attack();
                    CharacterAnimator.Play("Attack");
                }

            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                UsePotion();
                //TODO
            }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUpItem();
            OpenDoor();
        }
        if (Input.GetKey(KeyCode.R))
        {
            _bowForce += 1;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            UseOffWeapon();
            _bowForce = 0;
        }
}

    private void OpenDoor()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, 0.50f, _whatIsDoor);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {

                
                if (_playerManager.GetComponent<PlayerInventory>().getHasKey1())
                {
                    colliders[i].gameObject.GetComponent<DoorScript>().SetDoorState(1);
                    
                }
                if (colliders[i].gameObject.GetComponent<DoorScript>().GetDoorState() == 1)
                {
                    _gameManager.LoadMenu();
                }
            }
        }

        Collider2D[] chests = Physics2D.OverlapCircleAll(_groundCheck.position, 0.50f, _whatIsChest);
        for (int i = 0; i < chests.Length; i++)
        {
            if (chests[i].gameObject != gameObject)
            {

                    chests[i].gameObject.GetComponent<ChestScript>().OpenChest();
            }
        }

        Collider2D[] levers = Physics2D.OverlapCircleAll(_groundCheck.position, 0.50f, _whatIsLever);
        for (int i = 0; i < levers.Length; i++)
        {
            if (levers[i].gameObject != gameObject)
            {
                if (!levers[i].gameObject.GetComponent<LeverScript>().GetSpikesRaising() && !levers[i].gameObject.GetComponent<LeverScript>().GetSpikesLowering())
                {
                    if (levers[i].gameObject.GetComponent<LeverScript>().GetLeverState() == 0)
                    {
                        levers[i].gameObject.GetComponent<LeverScript>().SetLeverState(1);
                    }
                    else if (levers[i].gameObject.GetComponent<LeverScript>().GetLeverState() == 1)
                    {
                        levers[i].gameObject.GetComponent<LeverScript>().SetLeverState(0);
                    }
                }
            }
        }
    }

    private void UseOffWeapon()
    {
        //Bow
        if (_playerManager.GetComponent<PlayerInventory>().getHasBow())
        {
            GameObject go = Instantiate(_bullet, _meleeCheck.position, _meleeCheck.rotation);
            if (_facingRight)
            {
                go.GetComponent<BulletScript>().createBullet(true, _bowForce);
            }
            else
            {
                go.GetComponent<BulletScript>().createBullet(false, _bowForce);
            }

            Destroy(go, 3.0f);
        }
    }

    private void PickUpItem()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, 0.5f, _whatIsItem);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
       
                int itemInt = colliders[i].gameObject.GetComponent<ItemScript>().GetItemInt();
                //_gravity = Physics2D.gravity;
                if (itemInt == 0)
                {
                    _playerManager.GetComponent<PlayerInventory>().gainHealthPotion();
                }
                else if(itemInt == 1)
                {
                    _playerManager.GetComponent<PlayerInventory>().gainGold(5f);
                }
                else if(itemInt == 2)
                {
                    _playerManager.GetComponent<PlayerInventory>().gainKey1();
                }
                else if(itemInt == 3)
                {
                    _playerManager.GetComponent<PlayerInventory>().gainBow();
                }
                else if(itemInt == 4)
                {
                    _playerManager.GetComponent<PlayerInventory>().gainGold(1f);
                }
                colliders[i].gameObject.GetComponent<ItemScript>().Die();
            }

        }
    }

    void FixedUpdate () {

        //Ignore platform when jumping on it
        _horizontalMove = Input.GetAxis("Horizontal");

        if (transform.GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            Physics2D.IgnoreLayerCollision(0, 9, true);
        }
        else if(Input.GetAxis("Vertical") == -1)
        {
            Physics2D.IgnoreLayerCollision(0, 9, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(0, 9, false);
        }

        //old gravity code
        //_rigidBody.AddForce(_gravity * _rigidBody.mass);
        _grounded = false;
        //Checks for ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, _groundedRadius, _whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                //_gravity = Physics2D.gravity;
                _grounded = true;
            }
 
        }
        Collider2D[] stairs = Physics2D.OverlapCircleAll(_groundCheck.position, 0.2f, _whatIsStairs);
        for (int i = 0; i < stairs.Length; i++)
        {
            if (stairs[i].gameObject != gameObject)
            {
                _grounded = true;
                
            }

        }

        Collider2D[] ladders = Physics2D.OverlapCircleAll(_ladderCheck.position, _groundedRadius, _whatIsLadder);
        for (int i = 0; i < ladders.Length; i++)
        {
            if (ladders[i].gameObject != gameObject)
            {
                _onLadder = true;
            }
            

        }
        if(ladders.Length < 1)
        {
            _onLadder = false;
        }


        //TODO better hit decetion to spikes
        /*
        Collider2D[] hurtPoints = Physics2D.OverlapCircleAll(_groundCheck.position, _groundedRadius, _whatIsSpikes);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                //_gravity = Physics2D.gravity;
                _grounded = true;
            }

        }
        */

        if (_onLadder)
        {
            Physics2D.gravity = Vector2.zero;
            _rigidBody.velocity = new Vector2(0, 0);
            if (Input.GetAxis("Vertical") > 0)
            {
                transform.Translate(0, 4 * Time.deltaTime, 0);
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                transform.Translate(0, -4 * Time.deltaTime, 0);
            }
        }
        else
        {
            Physics2D.gravity = _gravity;

        }

        Move(_horizontalMove, _jump);



    }

    public void Move(float move, bool jump)
    {
        if (_grounded || _airControl)
        {
            if (_grounded && move != 0)
            {
                //CharacterAnimator.SetBool("Walk", true);
                CharacterAnimator.Play("Walk");
            }
            else if (_isJumping)
            {
                CharacterAnimator.Play("Jump");
            }
            else if (!_grounded && !_isJumping)
            {
                CharacterAnimator.Play("Fall");
            }
            //else if (_onLadder) NEEDS ENTRY ANIMATION WHEN IT IS CLIMBING THE LADDERS!!
            //{
            //    CharacterAnimator.SetTrigger("Entry");
            //}
            else
            {
                CharacterAnimator.Play("Idle");
                //CharacterAnimator.SetBool("Walk", false);
            }


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
   
            if (_grounded && _jump && !_jumpOnCooldown && !_invulnurable)
            {
                _grounded = false;
                //_rigidBody.isKinematic = true;
               // RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f, _whatIsStairs);
               // _rigidBody.velocity = new Vector2(_rigidBody.velocity.x - (hit.normal.x * _slopeFriction), _rigidBody.velocity.y);
                _rigidBody.AddForce(new Vector2(0f, _jumpForce));
                StartCoroutine(JumpCooldown());
                //_rigidBody.isKinematic = false;
            }

        }

    }

    public void UsePotion()
    {
        if (_playerManager.GetComponent<PlayerInventory>().getHasPotion())
        {
            if (_health == 20)
            {
                //Do nothing
            }
            else if (_health + 10 > 20)
            {
                _health = 20;
                _playerManager.GetComponent<PlayerInventory>().useHealthPotion();
            }
            else
            {
                _health += 10;
                _playerManager.GetComponent<PlayerInventory>().useHealthPotion();
            }
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

    //Player takes damage;
    public void Hurt(int damage)
    {
        if (_health - damage > 0f && !_invulnurable)
        {
            _health -= damage;

            _rigidBody.AddForce(new Vector2(-_knockbackForce, _knockbackForce));
            StartCoroutine(Flicker(5));
            StartCoroutine(InvulnTimer());

        }
        else
        {
            Die();
        }
    }

    IEnumerator DeathAnimation()
    {
        //TODO play death animation and load Gameover/retry screen
        yield return new WaitForSeconds(1f);
    }

    private void Die()
    {
        StartCoroutine(DeathAnimation());
    }


    IEnumerator InvulnTimer()
    {
        _invulnurable = true;
        yield return new WaitForSeconds(1f);
        _invulnurable = false;
    }

    IEnumerator Flicker(int times)
    {

        //TODO play hurt animation
        Renderer rend = gameObject.GetComponent<Renderer>();
        for (int i = 0; i < times; i++)
        {
            rend.material.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(0.1f);
            rend.material.color = _color;
            yield return new WaitForSeconds(0.1f);
        }
  
     
    }

    //Placeholder for attack anims
    IEnumerator AttackAnim()
    {
        _weaponSwing.gameObject.SetActive(true);
        //CharacterAnimator.Play("Attack");
        _attackOnCooldown = true;
        yield return new WaitForSeconds(_attackCooldown);
        _weaponSwing.gameObject.SetActive(false);
        _attackOnCooldown = false;
    }

    IEnumerator JumpCooldown()
    {
        _jumpOnCooldown = true;
        _isJumping = true;
        yield return new WaitForSeconds(_jumpCooldown);
        _isJumping = false;
        _jumpOnCooldown = false;

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


    public int GetHealth()
    {
        return _health;
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
