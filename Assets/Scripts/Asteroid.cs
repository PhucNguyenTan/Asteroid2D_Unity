using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[1];
    public int size;
    public int minSize = 1;
    public int maxSize = 4;
    public float speed = 2f;
    public float health = 1000f;
    public float sizeBig = 5f;
    public float sizeMass = 5f;
    public Color hurtColor = new Color(255, 255, 255);

    public explosion explosionFab;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidBody;
    private float maxLifeTime = 30f;
    private Material matWhite;
    private Material matDefault;
    private Color defaultColor;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;

    // Start is called before the first frame update
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");
        _rigidBody = GetComponent<Rigidbody2D>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = _spriteRenderer.material;
    }

    // Update is called once per frame
    private void Start()
    {
        //_spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)]; //Random choosing sprite
        _spriteRenderer.sprite = sprites[0];
        transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f); //Random the rotation
    }

    private void Update()
    {
        
        
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidBody.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLifeTime);
    }

    private void ResetMaterial()
    {
        //_spriteRenderer.color = defaultColor;
        //_spriteRenderer.material = matDefault;
        _spriteRenderer.material.shader = shaderSpritesDefault;
        _spriteRenderer.color = Color.white;
    }

    private void WhiteMaterial()
    {
        _spriteRenderer.material.shader = shaderGUItext;
        _spriteRenderer.color = Color.white;
    }


    public void InitiateAsteroid()
    {
        switch (size)
        {
            case 1: // Normal
                health = 10f;
                break;
            case 2: // Tough
                health = 30f;
                break;
            case 3: // Normal double
                health = 50f;
                break;
            case 4: // Tough double
                health = 50f;
                break;
        }
        transform.localScale = Vector3.one * (float)size/sizeBig;
        _rigidBody.mass = (float)size/sizeMass;

    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            health -= 10f;
            //_spriteRenderer.material = matWhite;
            //_spriteRenderer.color = hurtColor;
            WhiteMaterial();
            Killed();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            health = 0.0f;
            Killed();
            Player player = collision.gameObject.GetComponent<Player>();
            player.GetHurt();
        }
    }

    private void Killed()
    {
        if (health < 0.1f)
        {
            if (size > 2)
            {
                CreateSplit(2, this.size);
            }
            explosion explosion = Instantiate(this.explosionFab, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
        else
        {
            Invoke("ResetMaterial", .1f);
        }
    }

    private void CreateSplit(int iSplitAmount, int iSize)
    {
        Asteroid[] allSplit = new Asteroid[iSplitAmount];
        for (int i = 0; i < iSplitAmount; i++){
            Vector2 newPosition = this.transform.position;
            newPosition += Random.insideUnitCircle * 0.5f;

            allSplit[i] = Instantiate(this, newPosition, this.transform.rotation);
            allSplit[i]._spriteRenderer.color = defaultColor;
            allSplit[i].size = iSize - 2;
            allSplit[i].InitiateAsteroid();
            allSplit[i].SetTrajectory(Random.insideUnitCircle.normalized);
            //allSplit[i]._rigidBody.AddForce(direction * this.speed);
        }
    }



}
