using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public static PlayerAbilities instance;
    public GameObject bullet;
    public Transform fireDest;
    public Transform fireDestUp;
    public float shootTime;
    private float stime = 0f;
    private bool canShoot = true;
    private bool isDead = false;
    public float fireSpeed = 10f;
    [SerializeField] protected Animator animator;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead == false) {
            if (Input.GetButtonDown("Fire1") && canShoot) {
                Transform direction = fireDest;
                if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") > 0)
                    direction = fireDestUp;
                GameObject proj = Instantiate(bullet, direction.position, direction.rotation) as GameObject;
                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                rb.velocity = direction.transform.right * fireSpeed;
                canShoot = false;
                animator.SetBool("Shooting", true);
                Invoke("ChangeBool", 0.5f);

            }

            if (!canShoot) {
                stime += Time.deltaTime;
                if (stime >= shootTime) {
                    canShoot = true;
                    stime = 0f;
                }
            }
        }
    }

    private void ChangeBool()
    {
        animator.SetBool("Shooting", false);

    }

    public void SetIsDead(bool value) {
        isDead = value;
    }
}