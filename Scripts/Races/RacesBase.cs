using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacesBase : MonoBehaviour
{
	private enum TypesOfRaces
	{
		Aliens,
		Beasts,
		Humans,
		Machines,
		Undead
	}
	/*
	 * 
	Aliens: 
	Fraquezas: Nada
	Forças: Choque, Slow, Sangramento, Veneno, Stun, Fire = 50% menos dano.

		Feras:
	Fraquezas: Sangramento e Stun = 20 % mais dano
	Forças: Penetração = 100 % (Imune)

		Humanos:
	Fraquezas: Penetração = 5 % mais dano
	Forças: Holly = 100 % (Imune)

		Machines:
	Fraquezas: Fire = 40 % mais dano
	Forças: Veneno, sangramento, vampirismo, stun e Holly = 100 % (Imune)

		Undead: 
	Fraquezas: Fire = 10 % mais dano e Penetração = 20 % mais dano
	Forças: Veneno, Vampirismo e Sangramento = 100 % (Imune).
	*/

	[SerializeField]
	private TypesOfRaces RaceType;

    // Start is called before the first frame update
    void Start()
    {
		switch (RaceType)
		{
			case TypesOfRaces.Aliens:
				SetAlienDefaults();
				break;

			case TypesOfRaces.Beasts:
				SetBeastsDefaults();
				break;

			case TypesOfRaces.Humans:
				SetHumanDefaults();
				break;

			case TypesOfRaces.Machines:
				SetMachinesDefaults();
				break;

			case TypesOfRaces.Undead:
				SetundeadDefaults();
				break;
		}
    }

	private void SetundeadDefaults()
	{
		this.gameObject.AddComponent<Undead>();
	}

	private void SetMachinesDefaults()
	{
		this.gameObject.AddComponent<Machine>();
	}

	private void SetHumanDefaults()
	{
		this.gameObject.AddComponent<Human>();
	}

	private void SetBeastsDefaults()
	{
		this.gameObject.AddComponent<Beast>();
	}

	private void SetAlienDefaults()
	{
		this.gameObject.AddComponent<Alien>();
	}
}
