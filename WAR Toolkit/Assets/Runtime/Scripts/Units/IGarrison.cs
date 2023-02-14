using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGarrison
{

}

public interface ICapturable
{
    int OwnerPlayerIndex { get; }
}
