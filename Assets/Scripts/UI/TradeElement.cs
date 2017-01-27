using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TradeElement : MonoBehaviour {

	public Text ItemLabel;
	public Text ItemWeightLabel;
	public Text CostLabel;
	public Text PlayersAmountLabel;
	public Text TradersAmountLabel;

	public Text TradeLabel;

	public InputField AmountInput;

	public Market TraderMarket;
	public Player TraderPlayer;
	public Merchant Trader;
	public Merchant PlayerTrader;

	public Item ItemToTrade;
	public int AmountToTrade;
	public int CostPerUnit;

	public void SetAmountFromInputField () {
		float floatAmount = Utility.StringToFloat (AmountInput.text); // very unsafe
		AmountToTrade = (int)floatAmount;
		UpdateElement ();
	}

	void Start () {
		Debug.Log (TraderMarket);
		CostPerUnit = (int)((float) ItemToTrade.BaseCost * TraderMarket.CostCoefsByItems [ItemToTrade]);
		CostLabel.text = "$" + CostPerUnit;
		ItemLabel.text = ItemToTrade.Name;
		ItemWeightLabel.text = ItemToTrade.Weight.ToString ();
		UpdateElement ();
	}

	public void ChangeInput (int amount) {
		float floatAmount = 0;
		if (AmountInput.text != "") {
			floatAmount = Utility.StringToFloat (AmountInput.text);
		}
		int num = (int)floatAmount;
		num += amount;
		AmountInput.text = num.ToString ();
		SetAmountFromInputField ();
	}

	public void Trade () {
		if (AmountToTrade >= 0) {
			Buy (AmountToTrade);
		} else {
			Sell (-AmountToTrade);
		}
		UpdateElement ();
	}

	void Buy (int amount) {
		if (PlayerTrader.CheckIfCanBuyItem (ItemToTrade, amount) && Trader.CheckIfCanSellItem (ItemToTrade, amount)) {
			PlayerTrader.BuyItem (ItemToTrade, amount);
			Trader.SellItem (ItemToTrade, amount);
		}
	}

	void Sell (int amount) {
		if (PlayerTrader.CheckIfCanSellItem(ItemToTrade, amount) && Trader.CheckIfCanBuyItem(ItemToTrade, amount)) {
			PlayerTrader.SellItem (ItemToTrade, amount);
			Trader.BuyItem (ItemToTrade, amount);
		}
	}

	void UpdateElement () {
		PlayersAmountLabel.text = TraderPlayer.PlayerStorage.StoredAmountsByItems [ItemToTrade].ToString();
		if (AmountToTrade >= 0) {
			TradeLabel.text = "Buy";
		} else {
			TradeLabel.text = "Sell";
		}

		// dirty hax time

		GetComponentInParent<TradeWindow> ().UpdateWindowInfo ();
	}
}
