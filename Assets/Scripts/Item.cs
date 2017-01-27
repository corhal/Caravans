
public class Item {

	string name;
	public string Name { get { return name; } }

	int weight;
	public int Weight { get { return weight; } }

	int baseCost;
	public int BaseCost { get { return baseCost; } }

	public Item (string name, int weight, int baseCost) {
		this.name = name;
		this.weight = weight;
		this.baseCost = baseCost;
	}
}
