using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Config parameter
    [SerializeField] AudioClip destroySound;
    [SerializeField] GameObject blockSparkleVFX;
    [SerializeField] Sprite[] hitSprites;

    // Cached ref
    Level level;

    // State variables
    [SerializeField] int timesHit;  

    private void Start()
    {
        CountBreakableBlocks();
    }

    // Determines the number of breakable blocks in the level.
    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    // Determines what should be done when the ball comes in contact with one of the breakable blocks.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "Breakable")
        {
            HandleHit();
        }
    }

    // Determines if the block should be destroyed or if the next hit sprite should be displayed.
    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            BlockDestroyed();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    // Changes the current block sprite to next sprite in the array based on the number of hits.
    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if(hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.Log("Block sprite is missing from array" + gameObject.name);
        }

    }

    // Destroys the block and activates the block destroyed SFX/VFX.
    private void BlockDestroyed()
    {
        PlayBlockDestroySFX();
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerSparklesVFX();
    }

    // Adds to the player score when the block is destroyed and plays the destroyBlock SFX.
    private void PlayBlockDestroySFX()
    {
        FindObjectOfType<GameSession>().AddToScore();
        AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position);
    }

    // Activates the blockSparkleVFX on the destroyed block and destorys the VFX after 2 seconds.
    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparkleVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }
}
