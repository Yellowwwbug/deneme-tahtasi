using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    // �arp��ma sonucu k�r�lmay� tetikleyecek minimum kuvvet
    public float breakForceThreshold = 5f;
    // K�r�lm�� duvar prefab��; bu prefab, duvar k�r�ld���nda ortaya ��kacak par�alanm�� haldir.
    public GameObject fracturedWallPrefab;
    

    private bool isBroken = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isBroken)
            return;

        // Topun tag'�n�n "Ball" oldu�undan emin olun
        if (collision.gameObject.CompareTag("Ball"))
        {
            // �arp��ma kuvveti (relativeVelocity) e�ik de�erin �zerine ��karsa
            if (collision.relativeVelocity.magnitude >= breakForceThreshold)
            {
                BreakWall(collision);
            }
        }
    }

    private void BreakWall(Collision collision)
    {
        isBroken = true;
        // K�r�lm�� duvar prefab��n�, orijinal duvar�n pozisyonunda instantiate et
        GameObject fractured = Instantiate(fracturedWallPrefab, transform.position, transform.rotation);

        // �ste�e ba�l�: K�r�lm�� par�alar�n daha ger�ek�i da��lmas� i�in kuvvet ekleyebilirsin
        foreach (Rigidbody rb in fractured.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 forceDir = rb.transform.position - collision.contacts[0].point;
            rb.AddForce(forceDir.normalized * collision.relativeVelocity.magnitude, ForceMode.Impulse);
        }

        // Orijinal duvar� sahneden kald�r
        Destroy(gameObject);
    }
}
