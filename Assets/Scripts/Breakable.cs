using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    
    public float breakForceThreshold = 5f;
   
    public GameObject fracturedWallPrefab;
    

    private bool isBroken = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isBroken)
            return;

        
        if (collision.gameObject.CompareTag("Ball"))
        {
            
            if (collision.relativeVelocity.magnitude >= breakForceThreshold)
            {
                BreakWall(collision);
            }
        }
    }

    private void BreakWall(Collision collision)
    {
        isBroken = true;
    
        GameObject fractured = Instantiate(fracturedWallPrefab, transform.position, transform.rotation);

      
        foreach (Rigidbody rb in fractured.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 forceDir = rb.transform.position - collision.contacts[0].point;
            rb.AddForce(forceDir.normalized * collision.relativeVelocity.magnitude, ForceMode.Impulse);
        }

        // Orijinal duvarý sahneden kaldýr
        Destroy(gameObject);
    }
}
