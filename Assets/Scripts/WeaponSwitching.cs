using InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon;
    public StarterAssetsInputs input;
    public Image cursorImage;

    // Start is called before the first frame update
    private void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    private void Update()
    {
        if(input.switchWeapon.Equals(0)) return;

        ChangeWeapon(input.switchWeapon > 0);
        SelectWeapon();
    }

    private void ChangeWeapon(bool up)
    {
        int numWeapons = transform.childCount;

        if(up)
        {
            selectedWeapon++;
            selectedWeapon %= numWeapons;
        }
        else
        {
            selectedWeapon--;
            if(selectedWeapon < 0) selectedWeapon += numWeapons;
        }
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if(i == selectedWeapon)
            {
                GunController weaponController = weapon.GetComponent<GunController>();
                if(weaponController)
                    cursorImage.sprite = weaponController.cursor;
            }

            weapon.gameObject.SetActive(i++ == selectedWeapon);
        }
    }
}