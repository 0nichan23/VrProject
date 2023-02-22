using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpellHand : MonoBehaviour
{
    [SerializeField] private StatSheet _playerStats;
    GameManager GM => GameManager.instance;

    ObjectPooler OP => ObjectPooler.Instance;

    public SOspell CastSpell(string tag, float acc)
    {
        SOspell spell = GM.Spells.Find(x => x.Gesture == tag);
        if (spell == null) return null;
        if (spell.Accuracy > acc) return null;

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
        return spell;
    }

    void ShootFireball()
    {
        OP.SpawnFromPool("Fireball", transform.position, transform.rotation);
    }

    void ShootLightning()
    {
        GameObject lightning =OP.SpawnFromPool("Lightning", transform.position, transform.rotation);
        lightning.GetComponent<TurnOff>().Use(10);
        lightning.transform.SetParent(transform);
    }

    void ShootIceSpike()
    {
        OP.SpawnFromPool("IceSpike", transform.position, transform.rotation);
    }

    void CreateBarrier()
    {
        GameObject Shield = OP.SpawnFromPool("Shield", _playerStats.transform.position, _playerStats.transform.rotation);
        Shield.GetComponent<TurnOff>().Use(10);
        Shield.GetComponentInChildren<VisualEffect>().SendEvent("OnPlay");

        Shield.transform.SetParent(_playerStats.transform);
        GameManager.instance.Player.Damageable.OnTakeDamge.AddListener(ZeroDamage);
    }

    private void ZeroDamage(Attack givenAttack)
    {
        givenAttack.Damage.AddMod(0f);
        StartCoroutine(RemoveBuff());
    }
    private IEnumerator RemoveBuff()
    {
        yield return new  WaitForSecondsRealtime(10);
        GameManager.instance.Player.Damageable.OnTakeDamge.RemoveListener(ZeroDamage);

    }
}
