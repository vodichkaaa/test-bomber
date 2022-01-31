using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _moveSpeed = 0.05f;

    [SerializeField]
    private Transform movePoint = null;

    [SerializeField]
    private LayerMask _whatStopsMovement = 0;

    public Animator animator = null;
    
    private readonly Vector3 _moveRight = new Vector3(0.78f, 0f, 0f);
    private readonly Vector3 _moveLeft = new Vector3(-0.78f, 0f, 0f);
    private readonly Vector3 _moveUp = new Vector3(0.09f, 0.91f, 0f);
    private readonly Vector3 _moveDown = new Vector3(-0.09f, -0.91f, 0f);

    private Vector2 _movement = Vector2.zero;

    private void Start()
    {
        movePoint.parent = null;
        
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, _moveSpeed);

        _movement.x = SimpleInput.GetAxisRaw("Horizontal");
        _movement.y = SimpleInput.GetAxisRaw("Vertical");

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (_movement.x > 0f)
            {
                if(!Physics2D.OverlapCircle(movePoint.position + _moveRight, .2f, _whatStopsMovement))
                {
                    movePoint.position += _moveRight;
                }
            }

            else if (_movement.y > 0f)
            {
                if(!Physics2D.OverlapCircle(movePoint.position + _moveUp, .2f, _whatStopsMovement))
                {
                    movePoint.position += _moveUp;
                }
            }
            
            else if (_movement.y < 0f)
            {
                if(!Physics2D.OverlapCircle(movePoint.position + _moveDown, .2f, _whatStopsMovement))
                {
                    movePoint.position += _moveDown;
                }
            }
            
            else if (_movement.x < 0f)
            {
                if(!Physics2D.OverlapCircle(movePoint.position + _moveLeft, .2f, _whatStopsMovement))
                {
                    movePoint.position += _moveLeft;
                }
                
            }
            
            animator.SetFloat("Horizontal", _movement.x);
            animator.SetFloat("Vertical", _movement.y);
            animator.SetFloat("Speed", _movement.sqrMagnitude);
            
        }
    }
}
