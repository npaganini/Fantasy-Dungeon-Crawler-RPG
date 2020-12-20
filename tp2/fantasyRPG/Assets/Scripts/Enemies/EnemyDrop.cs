using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    protected BuffItem TypeOfDrop;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(GetComponent<Rigidbody>());
    }
}
