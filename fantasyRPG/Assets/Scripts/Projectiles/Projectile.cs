using UnityEngine;

public abstract class Projectile : PoolItem
{
    protected Rigidbody Rb;
    public Vector3 DirectionVector;
    protected bool Hit;

    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!Hit)
        {
            float step = 20 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, DirectionVector, step);
            DirectionVector = transform.position + (DirectionVector - transform.position).normalized * 1000.0f;    
        }
    }

    public abstract void OnTriggerEnter(Collider col);

    protected abstract void Stuck();
}
