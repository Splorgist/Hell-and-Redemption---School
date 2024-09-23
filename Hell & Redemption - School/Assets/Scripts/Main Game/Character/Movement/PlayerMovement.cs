using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Referrences")]
    public PlayerMovementStats MoveStats;
    [SerializeField] private Collider2D _feetColl;
    [SerializeField] private Collider2D _bodyColl;

    private Rigidbody2D _rb;

    private Vector2 _moveVelocity;
    private bool _isFacingRight;

    private RaycastHit2D _groundHit;
    private RaycastHit2D _headHit;
    private bool _isGrounded;
    private bool _bumpedHead;

    // Jump variables
    public float VerticleVelocity { get; private set;}
    private bool _isJumping;
    private bool _isFastFalling;
    private bool _isFalling;
    private float fastFallTime;
    private float _fastFallReleaseSpeed;
    private int _numberOfJumpsUsed;

    // Apex variables
    private float _apexPoint;
    private float _timePastApexThreshold;
    private bool _isPastApexThreshold;

    // Jump buffer variables
    private float _jumpBufferTimer;
    private bool _jumpReleasedDuringBuffer;

    // Coyote time variables
    private float _coyoteTimer;

    private void Awake()
    {
        _isFacingRight = true;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CountTimers();
        JumpChecks();
        Debug.Log(_numberOfJumpsUsed);
    }

    private void FixedUpdate()
    {
        CollisionChecks();
        Jump();

        if (_isGrounded){
            Move(MoveStats.GroundAcceleration, MoveStats.GroundDeceleration, InputManager.Movement);
        } else{
            Move(MoveStats.AirAcceleration, MoveStats.AirDeceleration, InputManager.Movement);
        }
    }

    private void Move(float acceleration, float deceleration, Vector2 moveInput)
    {
        if (moveInput != Vector2.zero){
            TurnCheck(moveInput);
            Vector2 targetVelocity = Vector2.zero;
            if (InputManager.RunIsHeld){
                targetVelocity = new Vector2(moveInput.x, 0f) * MoveStats.MaxRunSpeed;
            } else {
                targetVelocity = new Vector2(moveInput.x, 0f) * MoveStats.MaxWalkSpeed;
            }

            _moveVelocity = Vector2.Lerp(_moveVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
            _rb.velocity = new Vector2(_moveVelocity.x, _rb.velocity.y);
        }

        else if (moveInput == Vector2.zero){
            _moveVelocity = Vector2.Lerp(_moveVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
            _rb.velocity = new Vector2(_moveVelocity.x, _rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (_isJumping){

            if(_bumpedHead){
                _isFastFalling = true;
            }

            if (VerticleVelocity >= 0f){
                _apexPoint = Mathf.InverseLerp(MoveStats.InitialJumpVelocity, 0f, VerticleVelocity);

                if (_apexPoint > MoveStats.ApexThreshold){

                    if (!_isPastApexThreshold){
                        _isPastApexThreshold = true;
                        _timePastApexThreshold = 0f;
                    }

                    if (_isPastApexThreshold){
                        _timePastApexThreshold += Time.fixedDeltaTime;

                        if (_timePastApexThreshold < MoveStats.ApexHangTime){
                            VerticleVelocity = 0f;
                        }
                       
                        else {
                            VerticleVelocity = -0.01f;
                        }
                    }
                }

                else{
                    VerticleVelocity += MoveStats.Gravity * Time.fixedDeltaTime;

                    if (_isPastApexThreshold){
                        _isPastApexThreshold = false;
                    }
                }
            }

            else if (!_isFastFalling){
                VerticleVelocity += MoveStats.Gravity * MoveStats.GravityOnReleaseMultiplier * Time.fixedDeltaTime;
            }

            else if (VerticleVelocity < 0f){
                if (!_isFalling){
                    _isFalling = true;
                }
            }
        }

        if (_isFastFalling){

            if (fastFallTime >= MoveStats.TimeForUpwardsCancel){
                VerticleVelocity += MoveStats.Gravity * MoveStats.GravityOnReleaseMultiplier * Time.fixedDeltaTime;
            }

            else if (fastFallTime < MoveStats.TimeForUpwardsCancel){
                VerticleVelocity = Mathf.Lerp(_fastFallReleaseSpeed, 0f, (fastFallTime / MoveStats.TimeForUpwardsCancel));
            }

            fastFallTime += Time.fixedDeltaTime;
        }

        if (!_isGrounded && !_isJumping){
            if (!_isFalling){
                _isFalling = true;
            }

            VerticleVelocity += MoveStats.Gravity * Time.fixedDeltaTime;
        }

        VerticleVelocity = Mathf.Clamp(VerticleVelocity, -MoveStats.MaxFallSpeed, 50f);

        _rb.velocity = new Vector2(_rb.velocity.x, VerticleVelocity);
    }

    private void BumpedHead()
    {
        Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, _bodyColl.bounds.max.y);
        Vector2 boxCastSize = new Vector2(_feetColl.bounds.size.x * MoveStats.HeadWidth, MoveStats.HeadDetectionRayLength);

        _headHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.up, MoveStats.HeadDetectionRayLength, MoveStats.GroundLayer);

        if (_headHit.collider != null){
            _bumpedHead = true;
        }

        else {
            _bumpedHead = false;
        }
    }

    private void JumpChecks()
    {
        if (InputManager.JumpWasPressed){
            _jumpBufferTimer = MoveStats.JumpBufferTime;
            _jumpReleasedDuringBuffer = false;
        }

        if (InputManager.JumpWasReleased){

            if (_jumpBufferTimer > 0f){
                _jumpReleasedDuringBuffer = true;
            }

            if (_isJumping && VerticleVelocity > 0f){

                if (_isPastApexThreshold){
                    _isPastApexThreshold = false;
                    _isFastFalling = true;
                    fastFallTime = MoveStats.TimeForUpwardsCancel;
                    VerticleVelocity = 0f;
                }

                else {
                    _isFastFalling = true;
                    _fastFallReleaseSpeed = VerticleVelocity;
                }
            }
        }

        if (_jumpBufferTimer > 0f && !_isJumping && (_isGrounded || _coyoteTimer > 0f)){
            InitiateJump(1);

            if (_jumpReleasedDuringBuffer){
                _isFastFalling = true;
                _fastFallReleaseSpeed = VerticleVelocity;
            }
        }

        else if (_jumpBufferTimer > 0f && _isJumping && _numberOfJumpsUsed < MoveStats.NumberOfJumpsAllowed){
            _isFastFalling = false;
            InitiateJump(1);
        }

        else if (_jumpBufferTimer > 0f && _isFalling && _numberOfJumpsUsed < MoveStats.NumberOfJumpsAllowed - 1){
            InitiateJump(2);
            _isFastFalling = false;
        }

        if ((_isJumping || _isFalling) && _isGrounded && VerticleVelocity <= 0f){
            _isJumping = false;
            _isFalling = false;
            _isFastFalling = false;
            fastFallTime = 0f;
            _isPastApexThreshold = false;
            _numberOfJumpsUsed = 0;

            VerticleVelocity = Physics2D.gravity.y;
        }
    }

    private void OnDrawGizmos()
    {
        if (MoveStats.ShowWalJumpArc){
            DrawJumpArc(MoveStats.MaxWalkSpeed, Color.white);
        }


        if (MoveStats.ShowRunJumpArc){
            DrawJumpArc(MoveStats.MaxRunSpeed, Color.red);
        }
    }

    private void InitiateJump(int _jumpCount)
    {
        if (!_isJumping){
            _isJumping = true;
        }

        _jumpBufferTimer = 0f;
        _numberOfJumpsUsed += _jumpCount;
        VerticleVelocity = MoveStats.InitialJumpVelocity;
    }

    private void TurnCheck(Vector2 moveInput)
    {
        if (_isFacingRight && moveInput.x < 0){
            Turn(false);
        } else if (! _isFacingRight && moveInput.x > 0){
            Turn(true);
        }
    }

    private void Turn(bool turnRight)
    {
        if (turnRight){
            _isFacingRight = true;
            transform.Rotate(0f, 180f, 0f);
        } else {
            _isFacingRight = false;
            transform.Rotate(0f, -180f, 0f);
        }
    }

    private void IsGrounded()
    {
        Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, _feetColl.bounds.min.y);
        Vector2 boxCastSize = new Vector2(_feetColl.bounds.size.x, MoveStats.GroundDetectionRayLength);

        _groundHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.down, MoveStats.GroundDetectionRayLength, MoveStats.GroundLayer);

        if (_groundHit.collider != null){
            _isGrounded = true;
        } else {
            _isGrounded = false;
        }
    }

    private void CollisionChecks()
    {
        IsGrounded();
        BumpedHead();
    }

    private void CountTimers()
    {
        _jumpBufferTimer -= Time.deltaTime;

        if (!_isGrounded){
            _coyoteTimer -= Time.deltaTime;
        }else {
            _coyoteTimer = MoveStats.JumpCoyoteTime;
        }
    }

    private void DrawJumpArc(float moveSpeed, Color gizmoColor)
    {
        Vector2 startPosition = new Vector2(_feetColl.bounds.center.x, _feetColl.bounds.min.y);
        Vector2 previousPosition = startPosition;
        float speed = 0f;

        if (MoveStats.DrawRight){
            speed = moveSpeed;
        }

        else {
                speed = -moveSpeed;
        }

        Vector2 velocity = new Vector2(speed, MoveStats.InitialJumpVelocity);

        Gizmos.color = gizmoColor;

        float timeStep = 2 * MoveStats.TimeTillJumpApex / MoveStats.ArcResolution;

        for (int i = 0; i < MoveStats.VisualisationSteps; i++){
            float simulationTime = i * timeStep;
            Vector2 displacement;
            Vector2 drawPoint;

            if (simulationTime < MoveStats.TimeTillJumpApex){
                displacement = velocity * simulationTime + 0.5f * new Vector2(0, MoveStats.Gravity) * simulationTime * simulationTime;
            }

            else if (simulationTime < MoveStats.TimeTillJumpApex + MoveStats.ApexHangTime){
                float apexTime = simulationTime - MoveStats.TimeTillJumpApex;
                displacement = velocity * MoveStats.TimeTillJumpApex + 0.5f * new Vector2(0, MoveStats.Gravity) * MoveStats.TimeTillJumpApex * MoveStats.TimeTillJumpApex;
                displacement += new Vector2(speed, 0) * apexTime;
            }

            else {
                float descendTime = simulationTime - (MoveStats.TimeTillJumpApex + MoveStats.ApexHangTime);
                displacement = velocity * MoveStats.TimeTillJumpApex + 0.5f * new Vector2(0, MoveStats.Gravity) * MoveStats.TimeTillJumpApex * MoveStats.TimeTillJumpApex;
                displacement += new Vector2(speed, 0) * descendTime + 0.5f * new Vector2(0, MoveStats.Gravity) * descendTime * descendTime;
            }

            drawPoint = startPosition + displacement;

            if (MoveStats.StopOnCollision){
                RaycastHit2D hit = Physics2D.Raycast(previousPosition, drawPoint - previousPosition, Vector2.Distance(previousPosition, drawPoint), MoveStats.GroundLayer);

                if (hit.collider != null){
                    Gizmos.DrawLine(previousPosition, hit.point);
                    break;
                }
            }

            Gizmos.DrawLine(previousPosition, drawPoint);
            previousPosition = drawPoint;
        }
    }
}
