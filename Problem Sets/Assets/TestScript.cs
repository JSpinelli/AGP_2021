#if UNITY_EDITOR
#define ENABLE_LOGS
#endif
using UnityEngine;


public class TestScript : MonoBehaviour
{
    void Start()
    {
        Logger.Log("THIS IS A TEST");
    }
    
}
