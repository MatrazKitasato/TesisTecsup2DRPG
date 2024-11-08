using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] bool isConditionFullfilled = false;
    [SerializeField] int numberRoom;
    
    void Start()
    {
        numberRoom = SceneManager.GetActiveScene().buildIndex;
    }
    public void ConditionFullfilled()
    {
        isConditionFullfilled = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isConditionFullfilled)
        {
            SceneManager.LoadScene(numberRoom + 1);
        }
    }
}
