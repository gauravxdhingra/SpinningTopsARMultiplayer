using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public Joystick joystick;
    public float speed=2f;
    private Vector3 velocityVector=Vector3.zero;//Initial Velocity
    private Rigidbody rb;
    public float maximumVelocityChange=4f;
    public float tiltAmount=10;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Joystick input
        float _xMovementInput = joystick.Horizontal;
        float _zMovementInput = joystick.Vertical;

        // Calc velocity vector
        Vector3 _movementHorizontal = transform.right * _xMovementInput;
        Vector3 _movementVertical = transform.forward * _zMovementInput;

        // Final movement velocity vector
        Vector3 _movementVelocityVector = (_movementHorizontal+_movementVertical ).normalized * speed;

        // Apply movement
        Move(_movementVelocityVector);

           transform.rotation= Quaternion.Euler(joystick.Vertical*speed*tiltAmount, 0, -1*joystick.Horizontal*speed*tiltAmount);
    }

    void Move(Vector3 movementVelocityVector){
         velocityVector = movementVelocityVector;
    }

    private void FixedUpdate() {
        if(velocityVector!=Vector3.zero){

            // get rigidbody's current velocity
            Vector3 velocity=rb.velocity;
            Vector3 velocityChange = (velocityVector-velocity);
        

        velocityChange.x =Mathf.Clamp(velocityChange.x,-maximumVelocityChange, maximumVelocityChange );
        velocityChange.z =Mathf.Clamp(velocityChange.z,-maximumVelocityChange, maximumVelocityChange );

        velocityChange.y=0f;

        rb.AddForce(velocityChange,ForceMode.Acceleration);

        }
    }
}