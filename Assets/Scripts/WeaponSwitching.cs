using System.Collections;
using System.Collections.Generic;
using InputSystem;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon;
    public StarterAssetsInputs input;

    // Start is called before the first frame update
    private void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(input.weaponSwitch);
        switch (input.weaponSwitch)
        {
            case > 0f:
                selectedWeapon++;
                selectedWeapon %= transform.childCount - 1;
                SelectWeapon();
                break;

            case < 0f:
                selectedWeapon--;
                if(selectedWeapon < 0) selectedWeapon += transform.childCount - 1;
                SelectWeapon();
                break;
        }
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(i++ == selectedWeapon);
        }
    }
}