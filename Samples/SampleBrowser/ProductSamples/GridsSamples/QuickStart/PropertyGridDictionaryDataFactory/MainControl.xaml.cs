namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridDictionaryDataFactory;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private Dictionary<string, string>? _dictionary;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		InitializeData();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the default data and factory.
	/// </summary>
	private void InitializeData() {
		_dictionary = new Dictionary<string, string> {
			{ "Key1", "Value1" },
			{ "Key2", "Value2" },
			{ "Key3", "Value3" },
			{ "Key4", "Value4" },
			{ "Key5", "Value5" },
			{ "Key6", "Value6" }
		};

		propGrid.DataFactory = new DictionaryDataFactory<string, string>();
		propGrid.DataObject = _dictionary;
	}

}
