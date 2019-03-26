using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // Parameters
    [SerializeField] int breakableBlocks; // Serialized for debugging

    // Cached ref
    SceneLoader sceneLoader;

    // Sets the gameObject SceneLoader to the variable sceneLoader.
    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    // Determines how many breakable blocks there are in the scene by adding one to breakableBlocks.
    public void CountBlocks()
    {
        breakableBlocks++;
    }

    // Decrease the value of breakableBlock and loads next scene if their are no more blocks to destroy.
    public void BlockDestroyed()
    {
        breakableBlocks--;
        if(breakableBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
