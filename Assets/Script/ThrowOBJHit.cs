using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowOBJHit : MonoBehaviour
{
    [SerializeField] BoxCollider2D hitBox;
    [SerializeField] List<GameObject> collidersInTrigger = new List<GameObject>();

    bool isCritHit = false, isHit = false;

    public void SetHitBoxOn()
    {
        hitBox.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        collidersInTrigger.Add(other.gameObject);
    }

    public IEnumerator CheckColliderList()
    {
        foreach (var collider in collidersInTrigger)
        {
            if (collider.CompareTag("CritHit"))
            {
                isCritHit = true;
                isHit = false;
                break;
            }
            if (!isCritHit)
            {
                if (collider.CompareTag("Hit"))
                {
                    isHit = true;
                }
            }
        }
        if (isHit)
        {
            Debug.Log("Hit");
        }
        if (isCritHit)
        {
            Debug.Log("CritHit");
        }
        GameManager.Instance.FilpTurn();
        Destroy(this.gameObject);
        yield return null;
    }
}
