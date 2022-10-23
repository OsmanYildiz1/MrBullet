using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 direction = transform.position - other.transform.position; // ninjan�n bulundu�u poziyondan mermininkini ��kar
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject); // mermi yok olsun

            if (transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale<1) // �arp��an nesnenin gravitysi 0 sa
            {
                ninjayiYokEt();
            }

            GetComponent<Rigidbody2D>().AddForce(new Vector2 (direction.x>0 ?1 :-1,direction.y>0 ?.3f :-.3f),ForceMode2D.Impulse); 
            // vector x 0 dan b�y�kse 1 de�ilse -1. vector y 0 dan b�y�kse .3f de�ilse -.3f.  tek seferlik g�� uygula
            // mermi soldan geldiyse sa�a do�ru soldan geldiyse sa�a do�ru bir z�plama hareketi
        }
    }
        void ninjayiYokEt()
    {
        gameObject.tag = "Untagged";

        foreach (Transform item in transform)
        {
            item.GetComponent<Rigidbody2D>().gravityScale = 1f; // mermi ninjan�n neresine gelirse oran�n gravitysi 1 olsun
        }
    }
}
