using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeWindow : MonoBehaviour {

	public Player PlayerTrader;

	public Text InfoLabel;
	public GameObject TradeElementPrefab;
	public GameObject Window;
	public Transform TradeElementsParent;
	public List<GameObject> TradeElementObjects;
	public List<TradeElement> TradeElements;

	void Awake () {
		TradeElementObjects = new List<GameObject> ();
		TradeElements = new List<TradeElement> ();
	}

	public void UpdateWindowInfo () {	
		Debug.Log (PlayerTrader);	
		InfoLabel.text = "Cargo: " + PlayerTrader.PlayerStorage.CurrentWeight + "/" + PlayerTrader.PlayerStorage.MaxWeight + "    $" + PlayerTrader.Money;
	}

	public void UpdateWindow (Market trader) {		
		UpdateWindowInfo ();
		foreach (var tradeElementObject in TradeElementObjects) {			
			Destroy (tradeElementObject);
		}
		TradeElementObjects.Clear ();
		TradeElements.Clear ();
		List<Item> items = new List<Item> ();
		foreach (var amountByItem in PlayerTrader.PlayerStorage.StoredAmountsByItems) {
			if (amountByItem.Value != 0 && !items.Contains(amountByItem.Key)) {
				items.Add (amountByItem.Key);
			}
		}
		foreach (var amountByItem in trader.MarketStorage.StoredAmountsByItems) {
			if (amountByItem.Value != 0 && !items.Contains(amountByItem.Key)) {
				items.Add (amountByItem.Key);
			}
		}
		foreach (var item in items) {
			GameObject newTradeElementObject = Instantiate (TradeElementPrefab, TradeElementsParent) as GameObject;
			newTradeElementObject.transform.localScale = Vector2.one;
			TradeElement newTradeElement = newTradeElementObject.GetComponent<TradeElement> ();
			newTradeElement.ItemToTrade = item;
			newTradeElement.PlayerTrader = PlayerTrader.PlayerMerchant;
			newTradeElement.TraderPlayer = PlayerTrader;
			newTradeElement.Trader = trader.MarketMerchant;
			newTradeElement.TraderMarket = trader;
			TradeElementObjects.Add (newTradeElementObject);
			TradeElements.Add (newTradeElement);
		}
	}
}
