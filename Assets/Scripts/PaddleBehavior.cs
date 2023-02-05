using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private float unitsPerSeconds = 50;
    [SerializeField] private bool isLeftPaddle;
    private Rigidbody rb;
    
    #endregion

    #region Init
    private void Awake()
        => rb = GetComponent<Rigidbody>();

    #endregion

    #region Methods
    void FixedUpdate()
    {
        float leftValue = Input.GetAxis("LeftPaddle");
        float rightValue = Input.GetAxis("RightPaddle");
        
        Vector3 force = Vector3.right * (isLeftPaddle ? rightValue : leftValue) * unitsPerSeconds * Time.deltaTime;

        // change of direction
        if (rb.velocity.x < 0 && force.x > 0 || rb.velocity.x > 0 && force.x < 0)
            rb.velocity = Vector3.zero;
        
        rb.velocity = force;
    }
    
    #endregion
}
