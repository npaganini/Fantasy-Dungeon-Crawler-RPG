using UnityEngine;

public abstract class EnemyDrop : MonoBehaviour
{
    protected BuffItem TypeOfDrop;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(GetComponent<Rigidbody>());
    }

    public BuffItem GetTypeOfDrop()
    {
        return TypeOfDrop;
    }
}
