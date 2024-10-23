using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] bool isPlayerInRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerInRange = true;
        Debug.Log("Se puede iniciar dialogo");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerInRange = false;
        Debug.Log("No se puede iniciar dialogo");
    }
}
