using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotateSpeed = 100f;

    Transform gunarm; // silahý tutan kolun konusumu

    Transform pos_0, pos_1; // linerenderer posizyonu için
    LineRenderer lineRenderer;

    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    private void Awake()
    {
        gunarm = GameObject.Find("gunarm").transform; // oyundaki objeye ulaþmak için
        pos_0 = GameObject.Find("pos_0").transform; // oyundaki objeye ulaþmak için
        pos_1 = GameObject.Find("pos_1").transform; // oyundaki objeye ulaþmak için
        lineRenderer = GameObject.Find("gun").GetComponent<LineRenderer>(); // gun içindeki linerenderara ulaþ
        lineRenderer.enabled = false; // oyun baþlayýnca lazer yanmasýn

    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            KoluHareketEttir();
        }
        if (Input.GetMouseButtonUp(0))
        {
            mermiFirlat();   
        }
    }

    void KoluHareketEttir()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gunarm.position;// gerçek dünyadaki týkladýðýmýz noktayý göstermesi için.
        float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg; // y'nin x'e bölümünden gelen açýyý radyan cinsiden açýya çevirdik.
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward); // vermiþ olduðumuz açýyý quaterninona çevirdik.

        gunarm.rotation = Quaternion.Slerp(gunarm.rotation,rotation,rotateSpeed*Time.deltaTime); // bu fonksitonla yumuþak bir geçiþ saðladýk

        lineRenderer.enabled = true; // niþan alýnca lazer yansýn
        lineRenderer.SetPosition(0, pos_0.position);    // lazerin poziyoson baþlangýcý
        lineRenderer.SetPosition(1, pos_1.position);    // lazerin pozisyon bitiþi
    }
    void mermiFirlat()
    {
        lineRenderer.enabled = false; // lazer kaybolsun
        GameObject bullet = Instantiate(bulletPrefab,pos_0.position, Quaternion.identity);

        if (transform.localScale.x>0) // karakter saða dönükse
        {
            bullet.GetComponent<Rigidbody2D>().AddForce(pos_0.right * bulletSpeed, ForceMode2D.Impulse);
            //saða doðru bak bir kere kuvvet ekle
        }
        else
        {
            bullet.GetComponent<Rigidbody2D>().AddForce(-pos_0.right * bulletSpeed, ForceMode2D.Impulse);
            //sola doðru bak bir kere kuvvet ekle
        }
        

        Destroy(bullet, 2f);
    }
}
