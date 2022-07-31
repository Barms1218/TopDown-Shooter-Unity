using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectorUI : MonoBehaviour
{
    [SerializeField] GameObject[] weaponImages;

    public void ShowImage(GameObject weaponObject)
    {

    }

    private IEnumerator HideImage(GameObject objectToHide)
    {
        yield return new WaitForSeconds(1f);
        objectToHide.SetActive(false);
    }
}
