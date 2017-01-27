using System.Collections;

public class Merchant {

	Storage storage;
	IHaveMoney wallet;

	public Merchant (Storage storage, IHaveMoney wallet) {
		this.storage = storage;
		this.wallet = wallet;
	}

	public bool CheckIfCanBuyItem (Item item, int amount) {
		int itemsCost = item.BaseCost * amount;
		if (wallet.Money >= itemsCost && storage.CheckIfCanTakeItem (item, amount)) {
			return true;
		}
		return false;
	}

	public bool CheckIfCanSellItem (Item item, int amount) {
		if (storage.CheckIfCanGiveItem (item, amount)) {
			return true;
		}
		return false;
	}

	public void BuyItem (Item item, int amount) {
		if (!CheckIfCanBuyItem (item, amount)) {
			return;
		}
		int itemsCost = item.BaseCost * amount;
		wallet.SpendMoney (itemsCost);
		storage.TakeItem (item, amount);
	}

	public void SellItem (Item item, int amount) {
		if (!CheckIfCanSellItem (item, amount)) {
			return;
		}
		int itemsCost = item.BaseCost * amount;
		wallet.TakeMoney (itemsCost);
		storage.GiveItem (item, amount);
	}
}
