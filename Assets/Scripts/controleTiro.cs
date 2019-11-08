using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleTiro : MonoBehaviour
{
    
    public float speed = 10;

    public int direction = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x + (speed * Time.deltaTime * direction);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
