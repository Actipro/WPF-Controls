namespace ActiproSoftware.ProductSamples.GridsSamples.Common;

/// <summary>
/// Provides a tree node model implementation for a fruit.
/// </summary>
public class FruitTreeNodeModel : TreeNodeModel {

	private string? _colorCategory;
	private string? _leadingProducer;
	private int? _servingCalories;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The color category.
	/// </summary>
	public string? ColorCategory {
		get => _colorCategory;
		set => SetProperty(ref _colorCategory, value);
	}

	/// <summary>
	/// The leading producer.
	/// </summary>
	public string? LeadingProducer {
		get => _leadingProducer;
		set => SetProperty(ref _leadingProducer, value);
	}

	/// <summary>
	/// The serving calories.
	/// </summary>
	public int? ServingCalories {
		get => _servingCalories;
		set => SetProperty(ref _servingCalories, value);
	}

}
