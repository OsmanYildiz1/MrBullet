using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotateSpeed = 100f;

    Transform gunarm; // silah� tutan kolun konusumu

    Transform pos_0, pos_1; // linerenderer posizyonu i�in
    LineRenderer lineRenderer;

    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    private void Awake()
    {
        gunarm = GameObject.Find("gunarm").transform; // oyundaki objeye ula�mak i�in
        pos_0 = GameObject.Find("pos_0").transform; // oyundaki objeye ula�mak i�in
        pos_1 = GameObject.Find("pos_1").transform; // oyundaki objeye ula�mak i�in
        lineRenderer = GameObject.Find("gun").GetComponent<LineRenderer>(); // gun i�indeki linerenderara ula�
        lineRenderer.enabled = false; // oyun ba�lay�nca lazer yanmas�n

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
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gunarm.position;// ger�ek d�nyadaki t�klad���m�z noktay� g�stermesi i�in.
        float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg; // y'nin x'e b�l�m�nden gelen a��y� radyan cinsiden a��ya �evirdik.
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward); // vermi� oldu�umuz a��y� quaterninona �evirdik.

        gunarm.rotation = Quaternion.Slerp(gunarm.rotation,rotation,rotateSpeed*Time.deltaTime); // bu fonksitonla yumu�ak bir ge�i� sa�lad�k

        lineRenderer.enabled = true; // ni�an al�nca lazer yans�n
        lineRenderer.SetPosition(0, pos_0.position);    // lazerin poziyoson ba�lang�c�
        lineRenderer.SetPosition(1, pos_1.position);    // lazerin pozisyon biti�i
    }
    void mermiFirlat()
    {
        lineRenderer.enabled = false; // lazer kaybolsun
        GameObject bullet = Instantiate(bulletPrefab,pos_0.position, Quaternion.identity);

        if (transform.localScale.x>0) // karakter sa�a d�n�kse
        {
            bullet.GetComponent<Rigidbody2D>().AddForce(pos_0.right * bulletSpeed, ForceMode2D.Impulse);
            //sa�a do�ru bak bir kere kuvvet ekle
        }
        else
        {
            bullet.GetComponent<Rigidbody2D>().AddForce(-pos_0.right * bulletSpeed, ForceMode2D.Impulse);
            //sola do�ru bak bir kere kuvvet ekle
        }
        

        Destroy(bullet, 2f);
    }
}
