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

    ResourceManager manager;

    float x;

    Vector3 pos;
    GameObject playerGO;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Body").GetComponent<ResourceManager>();
        pos = transform.position;
        x = Time.time;
        playerGO = GameObject.Find("Player");
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

        pos += transform.up * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.right * Mathf.Sin(x * frequency) * magnitude;
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
