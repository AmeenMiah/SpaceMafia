using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestSwordEnemy : MonoBehaviour
{
    public readonly static HashSet<ClosestSwordEnemy> Pool = new HashSet<ClosestSwordEnemy>();

    private void OnEnable()
    {
        ClosestSwordEnemy.Pool.Add(this);
    }

    private void OnDisable()
    {
        ClosestSwordEnemy.Pool.Remove(this);
    }



    public static ClosestSwordEnemy FindClosestEnemy(Vector3 pos)
    {
        ClosestSwordEnemy result = null;
        float dist = float.PositiveInfinity;
        var e = ClosestSwordEnemy.Pool.GetEnumerator();
        while (e.MoveNext())
        {
            float d = (e.Current.transform.position - pos).sqrMagnitude;
            if (d < dist)
            {
                result = e.Current;
                dist = d;
            }
        }
        return result;
    }

}
