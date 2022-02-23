using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    public float moveSpeed = 0.1f;
	public JoystickHandler jsMovement;
    public float upForce;
    public float upForceb;
    public static int coins = 0;
    public Text coinsText1;
    public static Text coinsText;

	private Rigidbody2D rb;
	private CapsuleCollider2D capsuleCollider2d;
	private Vector3 direction;
	private float xMin,xMax,yMin,yMax;
    private bool Touching = false;
	
    private void Awake()
    {
        capsuleCollider2d = transform.GetComponent<CapsuleCollider2D>();
    }

	void Update () 
    {
		
		direction = jsMovement.InputDirection;

        coinsText = coinsText1;
		
		if(direction.magnitude != 0){
		
			transform.position += direction * moveSpeed;
			//transform.position = new Vector3(Mathf.Clamp(transform.position.x,xMin,xMax),Mathf.Clamp(transform.position.y,yMin,yMax),0f);

		}

        IsGrounded();

	}	

    public void Jump()
    {
        if(Touching == true)
        {
            rb.velocity = new Vector2(rb.velocity.y, upForce);
        }
    }

	
        
	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		/*xMax = 10;
		xMin = -10; 
		yMax = 5;
		yMin = -5;*/
	}

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet"))
        {
            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
        }

        if(other.gameObject.CompareTag("coin"))
        {
            coins++;
            Destroy(other.gameObject);
            coinsText.text = coins.ToString("0");
        }

        if(other.gameObject.CompareTag("enemy"))
        {
            Destroy(other.gameObject);
            rb.velocity = new Vector2(rb.velocity.y, upForceb);
        }

        if(other.gameObject.CompareTag("end"))
        {
            SceneManager.LoadScene(1);
        }
    }

	private bool IsGrounded()
	{
        float extraHeightText = .1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider2d.bounds.center, capsuleCollider2d.bounds.size, 0f, Vector2.down, extraHeightText, groundLayerMask);
        Color rayColor;
        if(raycastHit.collider != null)
        {
            rayColor = Color.green;
            Touching = true;
        }
        else
        {
            rayColor = Color.red;
            Touching = false;
        }
        Debug.DrawRay(capsuleCollider2d.bounds.center + new Vector3(capsuleCollider2d.bounds.extents.x, 0), Vector2.down * (capsuleCollider2d.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(capsuleCollider2d.bounds.center - new Vector3(capsuleCollider2d.bounds.extents.x, 0), Vector2.down * (capsuleCollider2d.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(capsuleCollider2d.bounds.center - new Vector3(capsuleCollider2d.bounds.extents.x, capsuleCollider2d.bounds.extents.y + extraHeightText), Vector2.right * (capsuleCollider2d.bounds.extents.x * 2), rayColor);
        return raycastHit.collider != null;
	}

    
}	