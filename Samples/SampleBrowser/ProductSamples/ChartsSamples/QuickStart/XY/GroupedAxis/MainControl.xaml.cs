namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.GroupedAxis;

public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		FruitData.Add(new Fruit("Apple", "Red", calories: 52));
		FruitData.Add(new Fruit("Avocado", "Green", calories: 160));
		FruitData.Add(new Fruit("Strawberry", "Red", calories: 46));
		FruitData.Add(new Fruit("Grape", "Green", calories: 114));
		FruitData.Add(new Fruit("Watermelon", "Red", calories: 92));
		FruitData.Add(new Fruit("Banana", "Yellow", calories: 94));
		FruitData.Add(new Fruit("Pineapple", "Yellow", calories: 76));
		FruitData.Add(new Fruit("Orange", "Orange", calories: 86));
		FruitData.Add(new Fruit("Grapefruit", "Orange", calories: 82));
		FruitData.Add(new Fruit("Lemon", "Yellow", calories: 17));
		FruitData.Add(new Fruit("Lime", "Green", calories: 16));
		FruitData.Add(new Fruit("Pear", "Green", calories: 98));
		FruitData.Add(new Fruit("Plum", "Purple", calories: 36));

		GroupByFirstLetter = (a, b) => ((string)a)[0].Equals(((string)b)[0]);
		LabelWithFirstLetter = a => ((string)a).Substring(0, 1);
		SortAlphabetically = (a, b) => ((string)a).ToLower()[0] - ((string)b).ToLower()[0];
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The fruit data.
	/// </summary>
	public ObservableCollection<Fruit> FruitData { get; } = [];

	/// <summary>
	/// A grouping function.
	/// </summary>
	public Func<object, object, bool> GroupByFirstLetter { get; }

	/// <summary>
	/// A labeling function.
	/// </summary>
	public Func<object, string> LabelWithFirstLetter { get; }

	/// <summary>
	/// A sorting function.
	/// </summary>
	public Func<object, object, int> SortAlphabetically { get; }

}
