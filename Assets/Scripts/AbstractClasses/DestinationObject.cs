using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestinationObject : MonoBehaviour, IRightClickableUp
{

    [SerializeField] ECommandType commandType;
    [SerializeField] private Transform destinationTransform;
    [SerializeField] protected Renderer entityRenderer;
    [SerializeField] protected Coroutine highlightCoroutine;

    protected virtual void Start()
    {
        GameManager.instance.onTacticalView += Highlight;
        GameManager.instance.onFreeState += BackToNormal;
    }

    public ECommandType CommandType { get => commandType;}
    public Transform DestinationTransform { get => destinationTransform;}

    public virtual void RightClicked(PlayerController player, Vector3 position)
    {
        player.SantaSelected.onAddMovementCommand(new Vector3(DestinationTransform.position.x, 0, DestinationTransform.position.z), DestinationTransform.position.y, this);
        //player.SantaSelected.onAddMovementCommand(new Vector3(transform.position.x, 0, transform.position.z), transform.position.y, this);
    }

    protected virtual void Highlight()
    {
        if (highlightCoroutine != null) 
        { 
            StopCoroutine(highlightCoroutine);
            highlightCoroutine = null;
        } 
        entityRenderer.material.SetFloat(Constants.SHADER_BOOLEAN_HIGHLIGHT_NAME, 1f);
    }

    protected virtual void BackToNormal()
    {
        entityRenderer.material.SetFloat(Constants.SHADER_BOOLEAN_HIGHLIGHT_NAME, 0.0f);
    }

    public void TemporaryHighlight()
    {
        Debug.Log("Debug Temporary");
        if (entityRenderer.material.GetFloat(Constants.SHADER_BOOLEAN_HIGHLIGHT_NAME) == 0) highlightCoroutine = StartCoroutine(CoroutineHighlight());
    }

    private IEnumerator CoroutineHighlight()
    {
        Highlight();
        yield return new WaitForSeconds(GameManager.instance.HighlightInfosTime);
        BackToNormal();
        highlightCoroutine = null;
    }

    protected virtual void OnDestroy()
    {
        GameManager.instance.onTacticalView -= Highlight;
        GameManager.instance.onFreeState -= BackToNormal;
    }

    public abstract void EntityArrivedAtDestinationObject(SantaController santa);
}
