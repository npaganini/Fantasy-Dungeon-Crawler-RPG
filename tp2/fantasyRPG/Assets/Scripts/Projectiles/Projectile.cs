using UnityEngine;

public abstract class Projectile : PoolItem
{
    public new Camera camera;
    protected Rigidbody Rb;
    public Vector3 DirectionVector;
    protected const float MaxTimeStuck = 2.5f;
    protected float TimeStuck = 0;

    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position ,DirectionVector) > .1 )
        {
            float step =  20 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, DirectionVector, step);
        }
        else
        {
            Stuck();
        }
    }

    public abstract void OnTriggerEnter(Collider col);

    protected abstract void Stuck();
}
