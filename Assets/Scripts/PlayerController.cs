using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum AudioType { Jump, Walk, Land};
public class PlayerController : MonoBehaviour
{
    #region SerializeFields
    [Header("Movement")]
    [SerializeField] private float m_speed;
    [SerializeField] private Transform m_outOfBoundsDown;

    [Header("Jump")]
    [SerializeField] private float m_jumpingPower;
    [SerializeField] private Transform m_groundCheck;
    [SerializeField] private float m_groundCheckRadius;
    [SerializeField] LayerMask m_groundLayer;
    [SerializeField] private float m_landingVelocity =-3f;

    [Header("Death")]
    [SerializeField] private GameObject m_youDiedScreen;

    [Header("Skip")]
    [SerializeField] private Transform[] m_waypoints;

    [Header("SFX")]
    [SerializeField] private AudioSource m_audioSourceWalk;
    [SerializeField] private AudioSource m_audioSourceJump;
    [SerializeField] private AudioSource m_audioSourceLand;
    [SerializeField] private AudioClip m_playerJumpStone;
    [SerializeField] private AudioClip m_playerLandStone;
    [SerializeField] private AudioClip m_playerJumpGas;
    [SerializeField] private AudioClip m_playerLandGas;
    [SerializeField] private AudioClip[] m_playerWalkStone;
    [SerializeField] private AudioClip[] m_playerWalkGas;
    #endregion

    #region Private Variables
    private static PlayerController m_instance;

    private float m_horizontal;
    private Animator m_anim;
    private Rigidbody2D m_rigidbody;
    private bool m_isFacingRight = true;
    private int m_currentWaypoint =0;

    private string m_planetTag ="Planet";

    private PlanetType m_planetType;
    #endregion

    #region Properties
    public static PlayerController Instance { get => m_instance; set => m_instance = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();

        SetIsJumpingFalse();

        m_instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        m_rigidbody.velocity = new Vector2(m_horizontal * m_speed, m_rigidbody.velocity.y);

        if(m_rigidbody.velocity.x > 0.1f || m_rigidbody.velocity.x < -0.1f && IsGrounded())
        {
            m_anim.SetBool("walking", true);
        }
        else
        {
            m_anim.SetBool("walking", false);
        }

        if (!m_isFacingRight && m_horizontal > 0f)
        {
            Flip();
        }
        else if (m_isFacingRight && m_horizontal < 0f)
        {
            Flip();
        }

        if (transform.position.y < m_outOfBoundsDown.position.y)
        {
            //you died anim
            m_youDiedScreen.SetActive(true);
        }
    }

    #region Actions
    public void OnMove(InputAction.CallbackContext ctx)
    {
        m_horizontal = ctx.ReadValue<Vector2>().x;
    }
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && IsGrounded())
        {
            m_anim.SetBool("jumping", true);
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, m_jumpingPower);
        }
    }

    public void OnEscape(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            SceneManager.LoadScene(0); //Loads Menu Scene
    }

    public void OnSkip(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && m_currentWaypoint<m_waypoints.Length -1)
        {
            m_currentWaypoint++;
            transform.position = m_waypoints[m_currentWaypoint].position;
        }
    }
    #endregion

    #region Helper
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(m_groundCheck.position, m_groundCheckRadius, m_groundLayer);
    }

    private void Flip()
    {
        m_isFacingRight = !m_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == m_planetTag)
        {
            m_planetType = collision.GetComponent<Planet>().Type;

            if(m_rigidbody.velocity.y<m_landingVelocity)
            {
                PlaySFX(AudioType.Land);
            }
        }
    }
    #endregion

    #region AnimationEvents
    public void SetIsJumpingFalse()
    {
        m_anim.SetBool("jumping", false);
    }

    public void PlaySFX(AudioType audiotype)
    {
        switch (audiotype)
        {
            case AudioType.Jump:
                if(m_planetType == PlanetType.Gas)
                    m_audioSourceJump.clip = m_playerJumpGas;
                else
                    m_audioSourceJump.clip = m_playerJumpStone;

                m_audioSourceJump.Play();
                break;
            case AudioType.Walk:
                if(!IsGrounded())
                {
                    break;
                }

                if(m_planetType == PlanetType.Gas)
                {
                    int random = Random.Range(0, m_playerWalkGas.Length);
                    m_audioSourceWalk.clip = m_playerWalkGas[random];
                    m_audioSourceWalk.Play();
                }
                else
                {
                    int random = Random.Range(0, m_playerWalkStone.Length);
                    m_audioSourceWalk.clip = m_playerWalkStone[random];
                    m_audioSourceWalk.Play();
                }
                break;
            case AudioType.Land:
                if (m_planetType == PlanetType.Gas)
                    m_audioSourceLand.clip = m_playerLandGas;
                else
                    m_audioSourceLand.clip = m_playerLandStone;

                m_audioSourceLand.Play();
                break;
        }

    }
    #endregion
}
