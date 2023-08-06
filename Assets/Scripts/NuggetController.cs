using UnityEngine;

public class NuggetController : GunController
{
    public Sprite fourShots;
    public Sprite threeShots;
    public Sprite twoShots;
    public Sprite oneShots;

    protected override void SetIdleSprite()
    {
        gunRenderer.sprite = CurrentAmmo switch
        {
            4 => fourShots,
            3 => threeShots,
            2 => twoShots,
            1 => oneShots,
            0 => emptySprite,
            _ => gunRenderer.sprite
        };
    }
}