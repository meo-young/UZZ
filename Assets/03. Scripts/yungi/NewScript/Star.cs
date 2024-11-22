using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Star : MonoBehaviour
{
    ParticleSystem particleSystem;
    Vector2 direction;
    public float moveSpeed = 0.1f;
    public float minSize = 0.1f;
    public float maxSize = 0.3f;
    public float sizeSpeed = 1f;

    public Color[] colors;
    public float colorSpeed = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();

        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        float size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector2(size, size);

        particleSystem.startColor = colors[Random.Range(0, colors.Length)];


    }

    // Update is called once per frame
    void Update()
    {
        
        
        transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero,Time.deltaTime * sizeSpeed);
        //sprite.color = colors[Random.Range(0, colors.Length)];

        Color color = particleSystem.startColor;
        color.a = Mathf.Lerp(particleSystem.startColor.a, 0, Time.deltaTime * colorSpeed);
        particleSystem.startColor = color;

        if (particleSystem.startColor.a <= 0.01f)
            Destroy(gameObject);
        
    
    }
}
