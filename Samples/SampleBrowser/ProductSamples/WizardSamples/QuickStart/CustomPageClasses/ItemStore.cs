namespace ActiproSoftware.ProductSamples.WizardSamples.QuickStart.CustomPageClasses;

/// <summary>
/// Represents a storage object for items.
/// </summary>
public class ItemStore {

	/// <summary>
	/// The index of the current item.
	/// </summary>
	public int CurrentIndex { get; set; }

	/// <summary>
	/// The collection of items.
	/// </summary>
	public List<Item> Items { get; } = [];

}
