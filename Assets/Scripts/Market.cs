using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Market : MonoBehaviour, IHaveMoney {

	public int InitialMoney;
	public int InitialMaxWeight;
	int money;
	public int Money { get { return money; } }

	Storage marketStorage;
	public Storage MarketStorage { get { return marketStorage; } }
	Merchant marketMerchant;
	public Merchant MarketMerchant { get { return marketMerchant; } }

	Dictionary<Item, float> costCoefsByItems;
	public Dictionary<Item, float> CostCoefsByItems { get { return costCoefsByItems; } }

	public void SpendMoney (int amount) {
		if (amount > money) {
			throw new UnityException (this + " tries to spend more money than it has! This should be checked on the spender system's side!");
		}
		money -= amount;
	}

	public void TakeMoney (int amount) {
		money += amount;
	}

	void Awake () {
		costCoefsByItems = new Dictionary<Item, float> ();
		marketStorage = new Storage (InitialMaxWeight);
		marketMerchant = new Merchant (marketStorage, this as IHaveMoney);
	}

	void Start () {
		foreach (var item in GameController.Instance.Items) {
			costCoefsByItems.Add(item, Random.Range(0.75f, 1.5f));
		}
		money = InitialMoney;
		foreach (var item in GameController.Instance.Items) {
			marketStorage.TakeItem (item, Random.Range (0, 1000));
		}
	}
}
