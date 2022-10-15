using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMoveState : EntityStateBase
{
    private EntityStateID _entityStateID = EntityStateID.Move;
    public override EntityStateID EntityStateID => _entityStateID;
    //get object unit

    public override void OnStateEnter()
    {
        //choose animation
        //add some events
    }

    public override void OnUpdate()
    {
        //update animation 
        //update move
        //SetDestination to goal 
    }
    public override void OnStateExit()
    {
        //minus some events
    }
}
