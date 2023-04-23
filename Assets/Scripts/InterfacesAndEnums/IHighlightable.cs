using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHighlightable
{
    protected abstract void Highlight();
    protected abstract void BackToNormal();
}
