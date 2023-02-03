using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private float unitsPerSeconds = 20f;
    private Rigidbody rb;
    
    #endregion

    #region Init
    private void Awake()
        => rb = GetComponent<Rigidbody>();

    #endregion

    #region Methods
    void FixedUpdate()
    {
        float horizontalValue = Input.GetAxis("Horizontal");
        Vector3 force = Vector3.right * horizontalValue * unitsPerSeconds;

        rb.AddForce(force);
    }
    
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        /*BoxCollider boxCollider = GetComponent<BoxCollider>();
        Bounds bounds = boxCollider.bounds;
        
        float minX = bounds.min.x;
        float maxX = bounds.max.x;

        float contactX = ball.contacts[0].point.x;
        
        Debug.Log($"{minX} - {maxX} - {contactX}");

        // TODO: check if rotation 60 or -60 if the sphere is more on the right or on the left
        
        Quaternion rotation = Quaternion.Euler(0,0,-60);
        Vector3 bounceDirection = rotation * Vector3.up;
        
        /*Rigidbody rb = ball.rigidbody;
        rb.AddForce(new Vector3(acceleration,acceleration,acceleration), ForceMode.Acceleration);*/
    }
}
