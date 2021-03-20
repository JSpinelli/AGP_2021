using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader: MonoBehaviour
{
    public bool load;
    public string sceneToLoad;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (load)
            {
                SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.UnloadSceneAsync(sceneToLoad);
            }
        }
    }
}
