using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sineMovementUp : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed;

    [SerializeField]
    public float frequency;

    [SerializeField]
    public float magnitude;

    [SerializeField]
    public bool musicManager;

    float x;

    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        musicManager = true;
        pos = transform.position;
        x = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        MoveRight();
        if (musicManager)
        {
            x += Time.deltaTime;
        }
        else
        {
            x -= Time.deltaTime;
        }
    }

    void MoveRight()
    {

        pos += transform.up * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.right * Mathf.Sin(x * frequency) * magnitude;
    }
}
