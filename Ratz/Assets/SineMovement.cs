using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SineMovement : MonoBehaviour
{

    [SerializeField]
    public float moveSpeed = 5f;

    [SerializeField]
    public float frequency = 20f;

    [SerializeField]
    public float magnitude = 0.5f;

    ResourceManager manager;

    float x;

    Vector3 pos, localScale;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Body").GetComponent<ResourceManager>();
        pos = transform.position;
        x=Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        MoveRight();
        if (manager.madOn)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((transform.gameObject.CompareTag("spear") == false))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
