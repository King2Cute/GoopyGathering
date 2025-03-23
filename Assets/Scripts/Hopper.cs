using UnityEngine;

namespace Assets.Scripts
{
    public class Hopper : LockedCamPlayerMovement
    {
        [Header("Hopper Jump Force")]
        [SerializeField]
        float minJumpForce = 2f;
        [SerializeField]
        float maxJumpForce = 20f;

        private Animator anim;

        private float jumpPressure = 0f;

        protected override void Start()
        {
            base.Start();
            jumpPressure = 0f;
            anim = GetComponent<Animator>();
        }

        protected override void DoJump()
        {
            if (isGrounded)
            {
                if (Input.GetKey(jump))
                {
                    if (jumpPressure < maxJumpForce)
                    {
                        jumpPressure += Time.deltaTime * 10f;
                    }
                    else
                    {
                        jumpPressure = maxJumpForce;
                    }
                    anim.SetFloat("JumpPressure", jumpPressure + minJumpForce);
                }
                else
                {
                    if (jumpPressure > 0f)
                    {
                        jumpPressure += minJumpForce;
                        rb.velocity = new Vector3(jumpPressure/10f, jumpPressure, 0f);
                        jumpPressure = 0f;
                        isGrounded = false;
                        anim.SetFloat("JumpPressure", 0f);
                        anim.SetBool("IsGround", isGrounded);
                    }
                }
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("ground"))
            {
                isGrounded = true;
                anim.SetBool("IsGround", isGrounded);
            }
        }
    }
}
