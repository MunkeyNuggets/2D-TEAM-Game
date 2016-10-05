using UnityEngine;
using System.Collections;

public class projectileDragging : MonoBehaviour {

    public float maxStretch;
    public LineRenderer catapultLineFrount;
    public LineRenderer catapultLineBack;
    public Rigidbody2D rb;
    public Transform catapult;

    private Ray rayToMouse;
    private Ray leftCatapultToProjectile;
    private float maxStretchSqr;
    private float circleRadius;
    private bool clickedOn;
    private Vector2 prevVelocity;
    private SpringJoint2D spring;
    private CircleCollider2D circle;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spring = GetComponent<SpringJoint2D>();
        circle = GetComponent<CircleCollider2D>();
      
    }

    //code is broken as all fuck. Will fix. I hope..
	void Start () {
        LineRendererSetup();
        rayToMouse = new Ray(catapult.position, Vector3.zero);
        leftCatapultToProjectile = new Ray(catapultLineFrount.transform.position, Vector3.zero);
        maxStretchSqr = maxStretch * maxStretch;
 
        circleRadius = circle.radius;
	}
	

	void Update () {

        if (clickedOn)
            Dragging();

        if (spring != null)
        {
            if (!rb.isKinematic && prevVelocity.sqrMagnitude > rb.velocity.sqrMagnitude)
            {
                Destroy(spring);
                rb.velocity = prevVelocity;
            }
            if (!clickedOn)
            {
                prevVelocity = rb.velocity;

                LineRendererUpdate();
            }
        }
        else
        {
            catapultLineFrount.enabled = false;
            catapultLineBack.enabled = false;
        }

	}

        void LineRendererSetup()
    {
        //Setting the first segment to the transform.position of the catapult
        catapultLineFrount.SetPosition(0, catapultLineFrount.transform.position);
        catapultLineBack.SetPosition(0, catapultLineBack.transform.position);
        //set the sorting layer to foreground
        catapultLineFrount.sortingLayerName = "Foreground";
        catapultLineBack.sortingLayerName = "Foreground";
        //set the order in layers
        catapultLineFrount.sortingOrder = 3;
        catapultLineBack.sortingOrder = 1;
    }

    void OnMouseDown()
    {
        spring.enabled = false;
        clickedOn = true;
    }

    void OnMouseUp()
    {
        spring.enabled = true;
        rb.isKinematic = false;
        clickedOn = false;
    }

    void Dragging()
    {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 catapultToMouse = mouseWorldPoint - catapult.position;

        if (catapultToMouse.sqrMagnitude > maxStretchSqr)
        {
            rayToMouse.direction = catapultToMouse;
            mouseWorldPoint = rayToMouse.GetPoint(maxStretch);
        }

        mouseWorldPoint.z = 0f;
        transform.position = mouseWorldPoint;
    }

   void LineRendererUpdate()
    {
        //finding the vector between the projectiles transform.postion to the catapults transform.position
        Vector2 catapultToProjectile = transform.position - catapultLineFrount.transform.position;
        leftCatapultToProjectile.direction = catapultToProjectile;
        Vector3 holdPoint = leftCatapultToProjectile.GetPoint(catapultToProjectile.magnitude + circleRadius);
        catapultLineFrount.SetPosition(1, holdPoint);
        catapultLineBack.SetPosition(1, holdPoint);
    }

}
