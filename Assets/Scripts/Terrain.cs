using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour, IDamagable
{
    [SerializeField] Sprite[] spritesLevelOfDamage;

    int life;
    int indexLevelOfDamage;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        life = spritesLevelOfDamage.Length;
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeSprite(indexLevelOfDamage);
    }

    public void Damage(int amount)
    {
        life -= amount;
        indexLevelOfDamage = spritesLevelOfDamage.Length - life;
        //indexLevelOfDamage+=(int)amount;

        if (indexLevelOfDamage < spritesLevelOfDamage.Length)
        {
            ChangeSprite(indexLevelOfDamage);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeSprite(int index)
    {
        if (!spriteRenderer)
        {
            Start();
        }

        spriteRenderer.sprite = spritesLevelOfDamage[index];
    }
}
