using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerShoot : MonoBehaviour
{
    Animator flowerAni;
    // Start is called before the first frame update
    void Start()
    {
        flowerAni = GetComponent<Animator>();
    }

    private void OnEnable() {
        Actions.OnFlowerShoot += shootFlower;
    }

    private void OnDisable() {
        Actions.OnFlowerShoot -= shootFlower;
    }

    void shootFlower(){
        StartCoroutine(OnFlowerShoot());
    }

    IEnumerator OnFlowerShoot(){
        flowerAni.SetBool("FlowerShooting", true);
        yield return new WaitForSeconds(0.5f);
        flowerAni.SetBool("FlowerShooting", false);
    }
}
