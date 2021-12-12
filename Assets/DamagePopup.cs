using System.Collections;
using System.Collections.Generic;


using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] Transform floatingText;
    public void Create(Vector3 position, int damageAmount)
    {
        Transform damagePopupTransform = Instantiate(floatingText, position, Quaternion.identity);

        floatingText floatingTextComp = damagePopupTransform.GetComponent<floatingText>();

        floatingTextComp.Setup(damageAmount);
    }
}
