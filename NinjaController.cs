using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 direction = transform.position - other.transform.position; // ninjanýn bulunduðu poziyondan mermininkini çýkar
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject); // mermi yok olsun

            if (transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale<1) // çarpýþan nesnenin gravitysi 0 sa
            {
                ninjayiYokEt();
            }

            GetComponent<Rigidbody2D>().AddForce(new Vector2 (direction.x>0 ?1 :-1,direction.y>0 ?.3f :-.3f),ForceMode2D.Impulse); 
            // vector x 0 dan büyükse 1 deðilse -1. vector y 0 dan büyükse .3f deðilse -.3f.  tek seferlik güç uygula
            // mermi soldan geldiyse saða doðru soldan geldiyse saða doðru bir zýplama hareketi
        }
    }
        void ninjayiYokEt()
    {
        gameObject.tag = "Untagged";

        foreach (Transform item in transform)
        {
            item.GetComponent<Rigidbody2D>().gravityScale = 1f; // mermi ninjanýn neresine gelirse oranýn gravitysi 1 olsun
        }
    }
}
