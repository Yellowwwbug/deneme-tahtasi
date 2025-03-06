using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    // Çarpýþma sonucu kýrýlmayý tetikleyecek minimum kuvvet
    public float breakForceThreshold = 5f;
    // Kýrýlmýþ duvar prefab’ý; bu prefab, duvar kýrýldýðýnda ortaya çýkacak parçalanmýþ haldir.
    public GameObject fracturedWallPrefab;
    

    private bool isBroken = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isBroken)
            return;

        // Topun tag'ýnýn "Ball" olduðundan emin olun
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Çarpýþma kuvveti (relativeVelocity) eþik deðerin üzerine çýkarsa
            if (collision.relativeVelocity.magnitude >= breakForceThreshold)
            {
                BreakWall(collision);
            }
        }
    }

    private void BreakWall(Collision collision)
    {
        isBroken = true;
        // Kýrýlmýþ duvar prefab’ýný, orijinal duvarýn pozisyonunda instantiate et
        GameObject fractured = Instantiate(fracturedWallPrefab, transform.position, transform.rotation);

        // Ýsteðe baðlý: Kýrýlmýþ parçalarýn daha gerçekçi daðýlmasý için kuvvet ekleyebilirsin
        foreach (Rigidbody rb in fractured.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 forceDir = rb.transform.position - collision.contacts[0].point;
            rb.AddForce(forceDir.normalized * collision.relativeVelocity.magnitude, ForceMode.Impulse);
        }

        // Orijinal duvarý sahneden kaldýr
        Destroy(gameObject);
    }
}
