using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testlerp : MonoBehaviour
{

    [SerializeField] private Vector3 goalPos;
    [SerializeField] private float speed;
    private float current, target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0)) target = target ==0 ? 0 : 1;

        //current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);

        //transform.position = Vector3.Lerp (Vector3.zero, )
    }
}
