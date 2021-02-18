using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridDictionaryDataFactory {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private Dictionary<string, string> dictionary;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			this.InitializeData();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the default data and factory.
		/// </summary>
		private void InitializeData() {
			dictionary = new Dictionary<string, string>();
			dictionary.Add("Key1", "Value1");
			dictionary.Add("Key2", "Value2");
			dictionary.Add("Key3", "Value3");
			dictionary.Add("Key4", "Value4");
			dictionary.Add("Key5", "Value5");
			dictionary.Add("Key6", "Value6");

			propGrid.DataFactory = new DictionaryDataFactory<string, string>();
			propGrid.DataObject = dictionary;
		}

	}

}
