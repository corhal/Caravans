using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IHaveMoney {

	int money;
	public int Money { get { return money; } }

	public int InitialMoney;
	public int InitialMaxWeight;

	Storage playerStorage;
	public Storage PlayerStorage { get { return playerStorage; } }
	Merchant playerMerchant;
	public Merchant PlayerMerchant { get { return playerMerchant; } }

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
		playerStorage = new Storage (InitialMaxWeight);
		playerMerchant = new Merchant (playerStorage, this as IHaveMoney);
	}

	void Start () {
		money = InitialMoney;
		foreach (var item in GameController.Instance.Items) {
			playerStorage.TakeItem (item, Random.Range (0, 10));
		}
	}
}
