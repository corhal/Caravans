using System.Collections;
using System.Collections.Generic;

public class Storage {	

	int maxWeight;
	public int MaxWeight { get { return maxWeight; } }
	int currentWeight;
	public int CurrentWeight { get { return currentWeight; } }

	Dictionary<Item, int> storedAmountsByItems;
	public Dictionary<Item, int> StoredAmountsByItems { get { return storedAmountsByItems; } }

	public Storage (int maxWeight) {
		this.maxWeight = maxWeight;
		storedAmountsByItems = new Dictionary<Item, int> ();
	}

	public bool CheckIfCanGiveItem (Item item, int amount) {
		if (!storedAmountsByItems.ContainsKey(item)) {
			storedAmountsByItems.Add (item, 0);
		}
		if (storedAmountsByItems[item] >= amount ) {
			return true;
		}
		return false;
	}

	public bool CheckIfCanTakeItem (Item item, int amount) {
		if (!storedAmountsByItems.ContainsKey(item)) {
			storedAmountsByItems.Add (item, 0);
		}
		int freeWeight = maxWeight - currentWeight;
		int itemsWeight = item.Weight * amount;
		if (freeWeight >= itemsWeight) {
			return true;
		}
		return false;
	}

	public void TakeItem (Item item, int amount) {
		if (!CheckIfCanTakeItem (item, amount)) {
			return;
		}
		storedAmountsByItems [item] += amount;
		int itemsWeight = item.Weight * amount;
		currentWeight += itemsWeight;
	}

	public void GiveItem (Item item, int amount) {
		if (!CheckIfCanGiveItem (item, amount)) {
			return;
		}
		storedAmountsByItems [item] -= amount;
		int itemsWeight = item.Weight * amount;
		currentWeight -= itemsWeight;
	}
}
