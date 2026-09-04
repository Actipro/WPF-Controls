using System.ComponentModel.DataAnnotations;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridExplicitProperties;

/// <summary>
/// Represents a test object for demonstration purposes.
/// </summary>
public class TestObject : ObservableObjectBase {

	private Size _recommendedSize;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	[Display(GroupName = "Defined on TestObject", ShortName = "Recommended size", Description = "This property is defined on the TestObject class instance bound to PropertyGrid.DataObject.")]
	public Size RecommendedSize {
		get => _recommendedSize;
		set => SetProperty(ref _recommendedSize, value);
	}

}
