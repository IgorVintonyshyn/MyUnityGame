using System.Collections;
using UnityEngine;

public class DiamondCollectable : MonoBehaviour
{
    [SerializeField] Animator animator;



    private void Start()
    {
        animator = transform.parent.Find("Model").GetComponent<Animator>();
        
    }
    public void OnTriggerEnter(Collider other)
    {
        UIEvents collector = other.GetComponent<UIEvents>();
        if (collector != null)
        {
            StartCoroutine(CollectAndDestroy(collector));
        }
    }

    private IEnumerator CollectAndDestroy(UIEvents collector)
    {
        transform.GetComponent<SphereCollider>().enabled = false;
        collector.DiamondCollect();
        animator.SetTrigger("Collect");
        yield return new WaitForSeconds(1f);
        Destroy(transform.parent.gameObject);
    }
}
