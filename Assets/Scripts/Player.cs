using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float speed = 10f;

    private float fHorizontalNav;
    private float fVerticalNav;
    private float playerHealth;
    private BoxCollider2D _boxCollide;
    private SpriteRenderer _spriteRenderer;
    private enum PlayerState { Normal, InvisibleDamaged}

    public bullet bulletFab;

    

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollide = GetComponent<BoxCollider2D>();
        playerHealth = 30.0f;
    }

    // Update is called once per frame
    void Update()
    {
        fHorizontalNav = Input.GetAxis("Horizontal");
        fVerticalNav = Input.GetAxis("Vertical");
        transform.position += new Vector3(fHorizontalNav/100f,fVerticalNav/100f, 0f);

        //No idea how this shit work, lol
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    private void Shoot() {
        bullet bullet = Instantiate(this.bulletFab, this.transform.position, this.transform.rotation);
        bullet.Project(transform.up);
    }

    public void GetHurt()
    {
        playerHealth -= 10.0f;
        StartCoroutine(FlashingHurt());

    }

    private void SetSpriteColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    IEnumerator FlashingHurt()
    {
        _boxCollide.enabled = false;
        for (int n = 0; n < 6; n++)
        {
            SetSpriteColor(Color.red);
            yield return new WaitForSeconds(0.1f); // ??? Still not sure what this thing work
            SetSpriteColor(Color.white);
            yield return new WaitForSeconds(0.1f);    
        }
        _boxCollide.enabled = true;

        /* Old not working way
         * _boxCollide.enabled = false;
        Invoke("SpriteWhite", .1f);
        Invoke("SpriteRed", .2f);

        yield return new WaitForSeconds(3);
        _boxCollide.enabled = true;
        SpriteWhite();*/
    }

    
}
