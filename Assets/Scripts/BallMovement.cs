using UnityEngine;

public class BallController : MonoBehaviour
{
    // Topun hareket g�c�
    public float movementForce = 10f;
    // Mouse ile ivmelendirme g�c�
    public float boostForce = 20f;

    // Rigidbody referans�
    private Rigidbody rb;

    private void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
    }

    // Fizik tabanl� daha iyi bir update
    private void FixedUpdate()
    {
        // Klavye W,A,S,D girdilerini al�yor
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Topun yerel koordinatlar�nda hareket y�n� olu�turuyoruz.
        // transform.right: sa� y�n, transform.forward: ileri y�n
        Vector3 force = (transform.right * horizontal + transform.forward * vertical) * movementForce;

        // Rigidbody'ye kuvvet uygulayarak topu hareket ettiriyoruz
        rb.AddForce(force);
    }

    // Topa t�kland���nda �a�r�lan metod (OnMouseDown otomatik olarak tetiklenir)
    private void OnMouseDown()
    {
        // Topa ileri y�nde anl�k (impulse) kuvvet uygular�z.
        // Bu kuvvet public boostForce de�i�keni ile ayarlanabilir.
        rb.AddForce(transform.forward * boostForce, ForceMode.Impulse);
    }
}

