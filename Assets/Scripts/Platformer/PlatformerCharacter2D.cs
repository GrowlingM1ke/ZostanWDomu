using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        public GameObject info;
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Animator b_Anim;            // Reference to brain Animator
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private bool m_brain_trigger;       // See if player is stepping on the brain trigger.
        public bool staminaDotyk = false;
        public bool staminaRuch = false;
        public Transform healthbar;
        public float stamina = 5;
        private bool switchControls = false;
        private string changeScene = null;
        private bool finishedLevel = false;
        private bool takenDamage = false;
        public float countdown = 0.7f;
        public float countdown2 = 0.3f;

        private Transform playerGraphics;	// Reference to the graphics so we can change direction

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            playerGraphics = transform.Find("Graphics");
            b_Anim = null;
            if (GameObject.FindGameObjectWithTag("brain") != null)
                b_Anim = GameObject.FindGameObjectWithTag("brain").gameObject.GetComponent<Animator>();

        }

        public void Start()
        {
            if (info == null)
                info = GameObject.Find("Information");

            if (SceneManager.GetActiveScene().name == "Tutorial_scene")
            {
                if (info.GetComponent<information>().hasFinished())
                {
                    GameObject portal = GameObject.Find("FINAL PORTAL");
                    portal.GetComponent<SpriteRenderer>().enabled = true;
                    portal.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (changeScene != null)
                {
                    if (changeScene == "Portal_sluch")
                    {
                        DontDestroyOnLoad(info);
                        SceneManager.LoadScene("Sluch_Level");
                    }
                    else if (changeScene == "Portal_dotyk")
                    {
                        DontDestroyOnLoad(info);
                        SceneManager.LoadScene("Dotyk_Level");
                    }

                    else if (changeScene == "Portal_wzrok")
                    {
                        DontDestroyOnLoad(info);
                        SceneManager.LoadScene("Level_wzrok");
                    }
                    else if (changeScene == "Portal_ruch")
                    {
                        DontDestroyOnLoad(info);
                        SceneManager.LoadScene("Ruch_Level");
                    }
                    else if (changeScene == "FINAL PORTAL")
                    {
                        DontDestroyOnLoad(info);
                        SceneManager.LoadScene("Ending_cutscena");
                    }

                }
            }


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (SceneManager.GetActiveScene().name == "Tutorial_scene")
                    SceneManager.LoadScene("Main_Menu");
                else
                    SceneManager.LoadScene("Tutorial_scene");
            }

        }

        private void FixedUpdate()
        {
            if (stamina > 5)
                stamina = 5f;

            if (stamina < 0.1f && staminaDotyk == true) {
                stamina = 5f;
                GameMaster.KillPlayer(gameObject.GetComponent<Player>());

            }

                if (finishedLevel)
            {
                countdown -= Time.deltaTime;
                if (countdown < 0)
                {
                    WinScene();
                }
            }

            if (takenDamage)
            {
                countdown2 -= Time.deltaTime;
                if (countdown2 < 0)
                {
                    GameMaster.KillPlayer(gameObject.GetComponent<Player>());
                }
            }

            

            m_Grounded = false;
            Mathf.Clamp(stamina, -1, 5);

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                    if (colliders[i].gameObject.tag == "brain_trigger")
                        m_brain_trigger = true;
                    else if (colliders[i].gameObject.tag == "music_trigger")
                        gameObject.GetComponent<FMODUnity.StudioEventEmitter>().SetParameter("Progress", colliders[i].gameObject.GetComponent<MusicTrigger>().progress);
                    else if (colliders[i].gameObject.tag == "music_trigger_wrong")
                        gameObject.GetComponent<FMODUnity.StudioEventEmitter>().SetParameter("WRONG WAY", colliders[i].gameObject.GetComponent<MusicWRONG>().wrongWay);
                    else if (colliders[i].gameObject.tag == "platform")
                    {
                        if (colliders[i].gameObject.GetComponent<PlatformTouch>() != null)
                        {
                            stamina += colliders[i].gameObject.GetComponent<PlatformTouch>().takeEffect() * Time.deltaTime;
                            healthbar.gameObject.GetComponent<HealthBar>().setHealth(stamina);
                        }
                    }
                    else if (colliders[i].gameObject.tag == "spikes" && !GetComponents<AudioSource>()[3].isPlaying)
                    {
                        GetComponents<AudioSource>()[3].Play();
                        takenDamage = true;
                    }

                }
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

            if (b_Anim != null)
                b_Anim.SetBool("Trigger", m_brain_trigger);

            // Stuff related to staminabar
            if (staminaRuch)
            {
                if (!switchControls)
                {
                    stamina -= Time.deltaTime;
                    healthbar.gameObject.GetComponent<HealthBar>().setHealth(stamina);
                    if (stamina < 0)
                    {
                        switchControls = !switchControls;
                    }
                }
                else
                {
                    stamina += Time.deltaTime;
                    healthbar.gameObject.GetComponent<HealthBar>().setHealth(stamina);
                    if (stamina > 5)
                    {
                        switchControls = !switchControls;
                    }
                }

            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "portal")
            {
                changeScene = collision.gameObject.name;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "portal")
                changeScene = null;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "portal")
            {
                if (GetComponents<AudioSource>()[2].isPlaying == false)
                    GetComponents<AudioSource>()[2].Play();
            }

            if (collision.gameObject.tag == "win")
            {
                if (SceneManager.GetActiveScene().name == "Sluch_Level")
                {
                    GameObject.Find("Information").GetComponent<information>().finishedSluch = 2;
                }
                else if (SceneManager.GetActiveScene().name == "Dotyk_Level")
                {
                    GameObject.Find("Information").GetComponent<information>().finishedDotyk = 2;
                }

                else if (SceneManager.GetActiveScene().name == "Level_wzrok")
                {
                    GameObject.Find("Information").GetComponent<information>().finishedWzrok = 2;
                }
                else if (SceneManager.GetActiveScene().name == "Ruch_Level")
                {
                    GameObject.Find("Information").GetComponent<information>().finishedRuch = 2;
                }

                GetComponents<AudioSource>()[2].Play();
                finishedLevel = true;

            }
        }





        public void Move(float move, bool crouch, bool jump)
        {
            if (switchControls)
                move = -move;

            if (move != 0f && !jump && GetComponent<AudioSource>().isPlaying == false && m_Grounded)
            {
                GetComponent<AudioSource>().volume = UnityEngine.Random.Range(0.3f, 0.6f);
                GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.8f, 1.1f);
                GetComponent<AudioSource>().Play();
            }

            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move * m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                       Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add Jump sound
                GetComponents<AudioSource>()[1].volume = UnityEngine.Random.Range(0.15f, 0.5f);
                GetComponents<AudioSource>()[1].pitch = UnityEngine.Random.Range(1.1f, 1.6f);
                GetComponents<AudioSource>()[1].Play();
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = playerGraphics.localScale;
            theScale.x *= -1;
            playerGraphics.localScale = theScale;
        }

        public IEnumerator Waiting(float time)
        {
            yield return new WaitForSeconds((float)time);
        }

        public void Fallen()
        {
            if (!takenDamage)
                countdown2 = 0.9f;
            takenDamage = true;
            if (!GetComponents<AudioSource>()[4].isPlaying)
                GetComponents<AudioSource>()[4].Play();
        }

        public void WinScene()
        {
            if (SceneManager.GetActiveScene().name == "Dotyk_Level")
                SceneManager.LoadScene("Dotyk_cutscena");
            else if (SceneManager.GetActiveScene().name == "Level_wzrok")
                SceneManager.LoadScene("Wzrok_cutscena");
            else if (SceneManager.GetActiveScene().name == "Ruch_Level")
                SceneManager.LoadScene("Ruch_cutscena");
            else if (SceneManager.GetActiveScene().name == "Sluch_Level")
                SceneManager.LoadScene("Sluch_cutscena");
        }
    }
}
