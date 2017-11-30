using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {
    public float minGroundNormalY = 0.65f;
    public float gravityModifier = 1f;

    public bool grounded = false;

    protected Vector2 targetVelocity;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected Vector2 groundNormal;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] rayBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float minMoveDist = 0.001f;
    protected const float shellRadius = 0.01f;

    private void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }


    void Start () {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
        Initialize();
	}
	
	// Update is called once per frame
	void Update () {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
	}

    protected virtual void Initialize(){
    }

    protected virtual void ComputeVelocity() {
    }

    void FixedUpdate(){
        grounded = false;
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;
        Vector2 deltaPosition = velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        Vector2 movement = moveAlongGround * deltaPosition.x;
        Move(movement, false);
        movement = Vector2.up * deltaPosition.y;
        Move(movement, true);
    }

    private void Move(Vector2 movement, bool yMovement)
    {
        float dist = movement.magnitude;
        if (dist > minMoveDist) {
            int count = rb2d.Cast(movement, contactFilter,rayBuffer, dist + shellRadius);
            if (count == 0) {
                groundNormal = Vector2.up;
            }
            hitBufferList.Clear();
            for (int i = 0; i < count; i++) {
                hitBufferList.Add(rayBuffer[i]);
            }
            //check for grounding
            for (int i = 0; i < hitBufferList.Count; i++) {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY) {
                    grounded = true;
                    if (yMovement) {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0) {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                dist = modifiedDistance < dist ? modifiedDistance : dist;
            }
        }


        rb2d.position = rb2d.position + movement.normalized * dist;
    }
}
