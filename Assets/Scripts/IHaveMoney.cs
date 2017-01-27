using UnityEngine;
using System.Collections;

public interface IHaveMoney {

	int Money{ get;}

	void SpendMoney (int amount);

	void TakeMoney (int amount);
}
