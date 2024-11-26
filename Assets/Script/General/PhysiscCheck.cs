using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysiscCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;
    [Header("基础属性")]
    public bool manual;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public Vector2 bottomOffset;
    public float checkRadius;
    public LayerMask groundlayer;
    public LayerMask oneWayPlatformLayer;
    [Header("bool")]
    public bool isGround;
    public bool isOneWayPlatform;
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
        //check one Way
        isOneWayPlatform = Physics2D.OverlapCircle((Vector2)transform.position+ bottomOffset, checkRadius, oneWayPlatformLayer);
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
