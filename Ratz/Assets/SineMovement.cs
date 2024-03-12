using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SineMovement : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    float frequency = 20f;

    [SerializeField]
    float magnitude = 0.5f;

    [SerializeField]
    bool musicManager;

    float x;

    Vector3 pos, localScale;
    // Start is called before the first frame update
    void Start()
    {
        musicManager = true;
        pos = transform.position;
        x=Time.time;
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
   
            pos += transform.right * Time.deltaTime * moveSpeed;
            transform.position = pos + transform.up * Mathf.Sin(x * frequency) * magnitude;
    }
}
