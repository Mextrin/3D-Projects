using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWalk
{
    float Vertical(Entity entity);
    float Horizontal(Entity entity);
}
