namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridNotifyParentProperty;

/// <summary>
/// Represents an expandable class with two properties.
/// </summary>
[TypeConverter(typeof(ChildObjectConverter))]
public class ChildObject {

	private string _willNotify = "Actipro";
	private string _willNotNotify = "Software";

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// A property that will notify it's parent property of changes.
	/// </summary>
	[DefaultValue("Actipro")]
	[Description("Changes to this property will be automatically reflected in the parent property.")]
	[NotifyParentProperty(true)]
	public string WillNotify {
		get => _willNotify;
		set => _willNotify = value;
	}

	/// <summary>
	/// A property that will not notify it's parent property of changes.
	/// </summary>
	[DefaultValue("Software")]
	[Description("Changes to this property will *not* be automatically reflected in the parent property.")]
	public string WillNotNotify {
		get => _willNotNotify;
		set => _willNotNotify = value;
	}

}
