using UnityEngine;

public class PositionRendererSorter : MonoBehaviour
{
    [SerializeField]
    private int sortingOrderBase = 100;
    [SerializeField]
    private int offset = 0;
    [SerializeField]
    private bool runOnlyOnce = false;
    private float timer;
    private float timerMax = .01f;
    private Renderer myRenderer;

    private void Awake()
    {
        
        myRenderer = gameObject.GetComponent<Renderer>();
    }

    private void LateUpdate()
    {
        timer -= Time.deltaTime;
        if(timer<= 0f)
        {
            timer = timerMax;
            myRenderer.sortingOrder = (int)(100 * (sortingOrderBase - transform.localPosition.y - offset / 100f));
            if (runOnlyOnce)
            {
                Destroy(this);
            }
        }
    }
}
