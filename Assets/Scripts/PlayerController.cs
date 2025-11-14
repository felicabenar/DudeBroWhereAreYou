using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody rb;
    public float speed =1000f;
    public float rotSpeed = 50f;
    public float jumpForce = 2f;
    public bool isGrounded = true;
    public Vector3 startPos = Vector3.zero;
    public Vector3 startRot = Vector3.zero;
    public Vector3 jump;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        startRot = transform.localEulerAngles;
        jump = new Vector3(0f, 2f, 0f);
    }



    void FixedUpdate()
    {

        float translation, rotation;

        translation = Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime;
        rotation = Input.GetAxis("Horizontal") * rotSpeed * Time.fixedDeltaTime;

        Quaternion turn = Quaternion.Euler(0f, rotation, 0f);

        // rb.MovePosition(rb.position + transform.forward * translation);
        rb.AddRelativeForce(Vector3.forward * translation);
        rb.MoveRotation(rb.rotation * turn);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }


    void Update() { }

    void OnCollisionEnter()
    {
        isGrounded = true;
    }

    void OnClick() { }
}
