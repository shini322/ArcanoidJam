using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gosling : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private Rigidbody2D rb;
    private HashSet<Block> blocksWasGetDamage = new ();
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartMove(List<Transform> points, Transform center)
    {
        rb = GetComponent<Rigidbody2D>();
        Transform point = points[Random.Range(0, points.Count)];
        transform.position = point.position;
        rb.linearVelocity = (center.position - point.position).normalized * speed;
        StartCoroutine(DeathCoroutine());
        audioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Block block))
        {
            if (blocksWasGetDamage.Contains(block))
            {
                return;
            }

            block.GetDamage();
            blocksWasGetDamage.Add(block);
        }
    }

    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}