using UnityEngine;

public class BallController : MonoBehaviour
{
    // Topun hareket gücü
    public float movementForce = 10f;
    // Mouse ile ivmelendirme gücü
    public float boostForce = 20f;

    // Rigidbody referansý
    private Rigidbody rb;

    private void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
    }

    // Fizik tabanlý daha iyi bir update
    private void FixedUpdate()
    {
        // Klavye W,A,S,D girdilerini alýyor
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Topun yerel koordinatlarýnda hareket yönü oluþturuyoruz.
        // transform.right: sað yön, transform.forward: ileri yön
        Vector3 force = (transform.right * horizontal + transform.forward * vertical) * movementForce;

        // Rigidbody'ye kuvvet uygulayarak topu hareket ettiriyoruz
        rb.AddForce(force);
    }

    // Topa týklandýðýnda çaðrýlan metod (OnMouseDown otomatik olarak tetiklenir)
    private void OnMouseDown()
    {
        // Topa ileri yönde anlýk (impulse) kuvvet uygularýz.
        // Bu kuvvet public boostForce deðiþkeni ile ayarlanabilir.
        rb.AddForce(transform.forward * boostForce, ForceMode.Impulse);
    }
}

