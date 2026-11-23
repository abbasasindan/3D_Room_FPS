using UnityEngine;

public class First_Script : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] Vector3 value;
    void Start()
    {
        Debug.Log("Ready");
        transform.position = Vector3.zero;
    }

    void Update()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x+ value.x,0,0) ;
        
    }
}
