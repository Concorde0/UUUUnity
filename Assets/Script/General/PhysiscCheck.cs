using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysiscCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;
    [Header("¼ì²â²ÎÊý")]
    public bool manual;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public Vector2 bottomOffset;
    public float checkRadius;
    public LayerMask groundlayer;
    [Header("×´Ì¬")]
    public bool isGround;
    public bool touchLeftWall;
    public bool touchRightWall;
    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();

        if (!manual)
        {
            rightOffset = new Vector2((coll.bounds.size.x + coll.offset.x) / 2, coll.bounds.size.y / 2);
            leftOffset = new Vector2(-rightOffset.x, rightOffset.y);

        }
    }
    private void Update()
    {
        Check();
    }
    public void Check()
    {
        //check ground
        isGround =  Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset,checkRadius,groundlayer);
        //check wall
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRadius, groundlayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRadius, groundlayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRadius);
    }
}
