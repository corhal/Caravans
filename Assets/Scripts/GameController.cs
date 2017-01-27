using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public TextAsset ItemsTable;
	public static GameController Instance;

	// System.Enum.Parse (typeof(Speaker), strings [2, i]);

	List<Item> items;
	public List<Item> Items { get { return items; } }

	void Awake() {
		Instance = this;
		items = new List<Item> ();
		UnpackItems (ItemsTable);
	}

	void UnpackItems (TextAsset csvTable) {
		string[,] strings = CSVReader.SplitCsvGrid (csvTable.text);
		for (int i = 1; i < strings.GetLength(1) - 1; i++) { // Х - хардкодий
			string itemName = strings [0, i];
			int itemWeight = System.Int32.Parse (strings [1, i]);
			int itemCost = System.Int32.Parse (strings [2, i]);

			items.Add(new Item(itemName, itemWeight, itemCost));
		}
	}
}
