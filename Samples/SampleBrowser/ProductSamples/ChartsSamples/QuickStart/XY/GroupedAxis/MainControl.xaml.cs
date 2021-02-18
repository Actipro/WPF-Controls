using System;
using System.Collections.ObjectModel;

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.GroupedAxis {

	public partial class MainControl {

		private readonly ObservableCollection<Fruit> fruitData = new ObservableCollection<Fruit>();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			FruitData.Add(new Fruit("Apple", "Red", 52));
			FruitData.Add(new Fruit("Avocado", "Green", 160));
			FruitData.Add(new Fruit("Strawberry", "Red", 46));
			FruitData.Add(new Fruit("Grape", "Green", 114));
			FruitData.Add(new Fruit("Watermelon", "Red", 92));
			FruitData.Add(new Fruit("Banana", "Yellow", 94));
			FruitData.Add(new Fruit("Pineapple", "Yellow", 76));
			FruitData.Add(new Fruit("Orange", "Orange", 86));
			FruitData.Add(new Fruit("Grapefruit", "Orange", 82));
			FruitData.Add(new Fruit("Lemon", "Yellow", 17));
			FruitData.Add(new Fruit("Lime", "Green", 16));
			FruitData.Add(new Fruit("Pear", "Green", 98));
			FruitData.Add(new Fruit("Plum", "Purple", 36));

			GroupByFirstLetter = (a, b) => ((string)a)[0].Equals(((string)b)[0]);
			LabelWithFirstLetter = a => ((string) a).Substring(0, 1);
			SortAlphabetically = (a, b) => ((string) a).ToLower()[0] - ((string) b).ToLower()[0];
		}

		#endregion OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the fruit data.
		/// </summary>
		/// <value>The fruit data.</value>
		public ObservableCollection<Fruit> FruitData {
			get { return fruitData; }
		}

		public Func<object, object, bool> GroupByFirstLetter { get; set; }
		public Func<object, object, int> SortAlphabetically { get; set; }
		public Func<object, string> LabelWithFirstLetter { get; set; }

		#endregion PUBLIC PROCEDURES
	}
}
