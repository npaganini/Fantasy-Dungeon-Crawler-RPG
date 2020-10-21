using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public CharacterController cc;
    public Animator anmCtrl;
    public float speed = 2f;
    private float moveLeftRightValue;
    private float moveUpDownValue;

    void Start()
    {
        cc = GetComponent<CharacterController>();

        anmCtrl = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
        UpdatePosition();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }

    void ReadInput()
    {
        moveLeftRightValue = ActionMapper.GetMoveHorizontal();
        moveUpDownValue = ActionMapper.GetMoveVertical();

    }

    void UpdatePosition()
    {
        float dt = Time.deltaTime;
        Vector2 movDir = new Vector2(moveLeftRightValue, moveUpDownValue);
        movDir = movDir.normalized;
        float spd = speed * movDir.magnitude;
        anmCtrl.SetFloat("Speed_f", spd);
        float dx = dt * spd * moveLeftRightValue;
        float dz = dt * spd * moveUpDownValue;
        cc.Move(new Vector3(dx, 0f, dz));

        /*
        if (Mathf.Abs(moveLeftRightValue) > 0.01f || Mathf.Abs(moveUpDownValue) > 0.01f)
        {
            if(movDir.magnitude > 0.001f)
                transform.forward = new Vector3(dx, 0f, dz);
        }*/
    }
}
