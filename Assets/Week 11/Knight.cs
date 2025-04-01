using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public float speed = 5;
    Animator animator;
    SpriteRenderer sr;
    bool canRun = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");
        float direction2 = Input.GetAxis("Vertical");
        sr.flipX = direction < 0;

        animator.SetFloat("speed", Mathf.Abs(direction));
        animator.SetFloat("speed", Mathf.Abs(direction2));

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
            canRun = false;
        }
        if (canRun)
        {

            transform.position += transform.right * direction * speed * Time.deltaTime;
            transform.position += transform.up * direction2 * speed * Time.deltaTime;
        }
    }
    public void AttackHasFinished()
    {
        Debug.Log("The attack just finished!");
        canRun = true;
    }
}
