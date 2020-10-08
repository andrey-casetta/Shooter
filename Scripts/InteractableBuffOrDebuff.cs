using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBuffOrDebuff : MonoBehaviour
{
    private Plants plantScript;
    private Insects insectScript;

    private enum InteractableType
    {
        //Planta que cura: ao interagir com ela, instantaneamente ela solta uma fumaça que possui o poder de restaurar  a vida do player enquanto ele estiver dentro dela.
        HealPlant,

        //Planta de Stun: ao interagir com ela, após 2 segundos ela solta um gás que irá atordoar quem tiver dentro por 3 segundos (player ou inimigos), o gás dura 3 segundos.
        StunPlant,

        //“Planta” Espinho: ao encostar nela o player irá ter sangramento.
        ThornPlant,

        //Planta Energia: ao interagir o player absorve a energia da planta e converte em energia própria (munição).
        EnergyPlant,

        //Planta Armadura: ao interagir com ela, instantaneamente ela solta uma densa fumaça que gruda na pele e aumenta as propriedades de defesa do player.
        ArmorPlant,

        //Planta Dano Crítico: ao interagir com ela, instantaneamente ela solta um gás que entra nos olhos e aumenta a chance de dar um disparo crítico.
        CriticalDamagePlant,

        //Planta Veneno: Ao interagir com ela, após 2 segundos ela solta um gás venenoso que irá envenenar o player, o gás dura 4,5 segundos e acumula novo dano a cada 0.9 segundos.
        PoisonPlant,

        //Planta de Fumaça: ao interagir com ela, instantaneamente libera uma densa fumaça encobrindo quem é que esteja nela, aumenta a chance de miss em 50%.
        SmokePlant,

        //Inseto Stun: Inseto que anda de um lado para o outro, ao acertar ele ou interagir com ele, após 2 segundos explode causando stun de 3 seg na área ao redor.
        StunInsect,

        //Inseto Veneno:  Inseto que anda de um lado para o outro, ao acertar ele ou interagir com ele, após 2 segundos explode causando envenenamento severo na área ao redor.
        PoisonInsect,

        //Inseto Sangramento:  Inseto que anda de um lado para o outro, ao acertar ele ou interagir com ele, após 2 segundos explode soltando espinhos e causando sangramento em uma área ao redor.
        BleedInsect,

        //Inseto Fogo:  Inseto que anda de um lado para o outro, ao acertar ele ou interagir com ele, após 2 segundos explode causando Fire severo em uma área ao redor.
        FireInsect
    }

    [SerializeField]
    private InteractableType _interactableType;

    void Start()
    {
        plantScript = GetComponent<Plants>();
        insectScript = GetComponent<Insects>();
    }

    void Update()
    {
    }

    private void ApplyPlantEffect(InteractableType type)
    {
        switch (_interactableType)
        {
            case InteractableType.HealPlant:
                HealEffect();
                break;

            case InteractableType.StunPlant:
                StunEffect();
                break;

            case InteractableType.ThornPlant:
                ThornEffect();
                break;

            case InteractableType.EnergyPlant:
                EnergyEffect();
                break;

            case InteractableType.ArmorPlant:
                ArmorEffect();
                break;

            case InteractableType.CriticalDamagePlant:
                CriticalDamageEffect();
                break;

            case InteractableType.PoisonPlant:
                PoisonEffect();
                break;

            case InteractableType.SmokePlant:
                SmokeEffect();
                break;

            case InteractableType.StunInsect:
                StunInsect();
                break;

            case InteractableType.PoisonInsect:
                PoisonInsect();
                break;

            case InteractableType.BleedInsect:
                BleedInsect();
                break;

            case InteractableType.FireInsect:
                FireInsect();
                break;
        }
    }

    private void HealEffect()
    {
        plantScript.HealEffect();
    }

    private void StunEffect()
    {
        plantScript.StunEffect();
    }

    private void ThornEffect()
    {

    }
    private void EnergyEffect()
    {

    }
    private void ArmorEffect()
    {

    }
    private void CriticalDamageEffect()
    {

    }
    private void PoisonEffect()
    {

    }
    private void SmokeEffect()
    {

    }
    private void StunInsect()
    {

    }
    private void PoisonInsect()
    {

    }
    private void BleedInsect()
    {

    }
    private void FireInsect()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.T))
                ApplyPlantEffect(_interactableType);
        }
    }
}
