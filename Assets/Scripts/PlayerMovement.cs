using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Assignables")]
    [SerializeField] private Transform _playerCam;
    [SerializeField] private Transform _orientation;
    [SerializeField] private LayerMask _groundMask;
    
    private Rigidbody _rigidBody;

    [Header("Look Stats")]
    [SerializeField] private float _xSense = 400.0f;
    [SerializeField] private float _ySense = 400.0f;
    
    [Header("Movement Stats")]
    [SerializeField] private float _walkSpeed = 2500;
    [SerializeField] private float _sprintSpeed = 7000;
    [SerializeField] private float _maxSpeed = 20;
    [SerializeField] private float _groundDrag = 0.175f;
    [SerializeField] private float _airDrag = 0.095f;
    [SerializeField] private float _maxSlopeAngle = 50f;
    [SerializeField] private float _airMoveMult = 0.5f;

    private float threshold = 0.01f;
    private bool _isGrounded;
    private bool _canJump = true;

    [Header("Jump Stats")]
    [SerializeField] private float _jumpCooldown = 0.25f;
    [SerializeField] private float _jumpForce = 750f;
    [SerializeField] private int _maxAirJumps = 1;

    private int airJumpsLeft;
    
    /* Inputs */
    private float _xInput, _zInput;
    private bool _isJumping, _isSprinting;
    private float verticalLook;

    void Awake() {
        _rigidBody = GetComponent<Rigidbody>();
    }
    
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate() {
        UpdateMovement();
    }

    private void Update() {
        CheckInput();
        MouseLook();
    }

    private void CheckInput() {
        _xInput = Input.GetAxisRaw("Horizontal");
        _zInput = Input.GetAxisRaw("Vertical");
        _isJumping = Input.GetButton("Jump");
        _isSprinting = Input.GetKey(KeyCode.LeftShift);
    }

    private void UpdateMovement() {
        // Reset Jumps
        if(_isGrounded)
            airJumpsLeft = _maxAirJumps;

        //Find actual velocity relative to where player is looking
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;

        //Counteract sliding and sloppy movement
        CounterMovement(_xInput, _zInput, mag);
        
        //If holding jump && ready to jump, then jump
        if (_canJump && _isJumping) 
            Jump();
        
        //If speed is larger than maxspeed, cancel out the input so you don't go over max speed
        if (_xInput > 0 && xMag > _maxSpeed) _xInput = 0;
        if (_xInput < 0 && xMag < -_maxSpeed) _xInput = 0;
        if (_zInput > 0 && yMag > _maxSpeed) _zInput = 0;
        if (_zInput < 0 && yMag < -_maxSpeed) _zInput = 0;
        
        // Movement in air
        float movementMult = 1.0f;
        if (!_isGrounded) 
            movementMult = _airMoveMult;

        float moveSpeed = _walkSpeed;
        if(_isSprinting && _isGrounded)
            moveSpeed = _sprintSpeed;

        //Apply forces to move player
        _rigidBody.AddForce(_orientation.forward * _zInput * moveSpeed * Time.deltaTime * movementMult);
        _rigidBody.AddForce(_orientation.right * _xInput * moveSpeed * Time.deltaTime * movementMult);
    }

    private void Jump() {
        if(!_canJump) return;

        if(airJumpsLeft <= 0){
            airJumpsLeft = 0;
            return;
        }

        if(!_isGrounded)
            airJumpsLeft--;
            
        _canJump = false;

        //If jumping while falling, reset y velocity.
        Vector3 vel = _rigidBody.velocity;
        if (_rigidBody.velocity.y < 0.5f)
            _rigidBody.velocity = new Vector3(vel.x, 0, vel.z);
        else if (_rigidBody.velocity.y > 0) 
            _rigidBody.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

        //Add jump forces
        _rigidBody.AddForce(Vector2.up * _jumpForce * 1.5f);
        
        Invoke(nameof(ResetJump), _jumpCooldown);
    }
    
    private void ResetJump() {
        _canJump = true;
    }
    
    private void MouseLook() {
        float mouseX = Input.GetAxis("Mouse X") * _xSense * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _ySense * Time.fixedDeltaTime;

        //Find current look rotation
        Vector3 rot = _playerCam.transform.localRotation.eulerAngles;
        float horizontalLook = rot.y + mouseX;
        
        //Rotate, and also make sure we dont over- or under-rotate.
        verticalLook -= mouseY;
        verticalLook = Mathf.Clamp(verticalLook, -90f, 90f);

        //Perform the rotations
        _playerCam.transform.localRotation = Quaternion.Euler(verticalLook, horizontalLook, 0);
        _orientation.transform.localRotation = Quaternion.Euler(0, horizontalLook, 0);
    }

    private void CounterMovement(float x, float y, Vector2 mag) {
        if (_isJumping) return;

        float moveDrag = _groundDrag;
        if(!_isGrounded)
            moveDrag = _airDrag;

        //Counter movement
        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0)) {
            _rigidBody.AddForce(_walkSpeed * _orientation.transform.right * Time.deltaTime * -mag.x * moveDrag);
        }
        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0)) {
            _rigidBody.AddForce(_walkSpeed * _orientation.transform.forward * Time.deltaTime * -mag.y * moveDrag);
        }
        
        //Limit diagonal running. This will also cause a full stop if sliding fast and un-crouching, so not optimal.
        if (Mathf.Sqrt((Mathf.Pow(_rigidBody.velocity.x, 2) + Mathf.Pow(_rigidBody.velocity.z, 2))) > _maxSpeed) {
            float fallspeed = _rigidBody.velocity.y;
            Vector3 n = _rigidBody.velocity.normalized * _maxSpeed;
            _rigidBody.velocity = new Vector3(n.x, fallspeed, n.z);
        }
    }

    public Vector2 FindVelRelativeToLook() {
        float lookAngle = _orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(_rigidBody.velocity.x, _rigidBody.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = _rigidBody.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);
        
        return new Vector2(xMag, yMag);
    }

    private bool IsFloor(Vector3 v) {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < _maxSlopeAngle;
    }

    private bool cancellingGrounded;
    
    /// <summary>
    /// Handle ground detection
    /// </summary>
    private void OnCollisionStay(Collision other) {
        //Make sure we are only checking for walkable layers
        int layer = other.gameObject.layer;
        if (_groundMask != (_groundMask | (1 << layer))) return;

        //Iterate through every collision in a physics update
        for (int i = 0; i < other.contactCount; i++) {
            Vector3 normal = other.contacts[i].normal;
            //FLOOR
            if (IsFloor(normal)) {
                _isGrounded = true;
                cancellingGrounded = false;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        //Invoke ground/wall cancel, since we can't check normals with CollisionExit
        float delay = 3f;
        if (!cancellingGrounded) {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    private void StopGrounded() {
        _isGrounded = false;
    }

    public void AddMaxAirJumps(int num)
    {
        this._maxAirJumps += num;
    }
    
}
