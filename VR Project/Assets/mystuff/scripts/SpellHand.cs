using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHand : MonoBehaviour
{
    [SerializeField] private StatSheet _playerStats;
    GameManager GM => GameManager.instance;

    ObjectPooler OP => ObjectPooler.Instance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CastSpell(string tag, float acc)
    {
        SOspell spell = GM.Spells.Find(x => x.Gesture == tag);
        if (spell == null) return;
        if (spell.Accuracy > acc) return;

        _playerStats.UseAttack(spell.Cooldown);

        switch (spell.Type)
        {
            case SOspell.SpellType.Fireball:
                ShootFireball();
                break;
            case SOspell.SpellType.Lightning: 
                ShootLightning();
                break;
            case SOspell.SpellType.IceSpike:
                ShootIceSpike();
                break;
            case SOspell.SpellType.Shield:
                CreateBarrier();
                break;
        }
    }

    void ShootFireball()
    {
        OP.SpawnFromPool("Fireball", transform.position, transform.rotation);
    }

    void ShootLightning()
    {

    }

    void ShootIceSpike()
    {

    }

    void CreateBarrier()
    {

    }
}
