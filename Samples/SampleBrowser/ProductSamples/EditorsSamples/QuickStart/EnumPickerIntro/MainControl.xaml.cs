using ActiproSoftware.ProductSamples.EditorsSamples.Common;
using ActiproSoftware.SampleBrowser;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.EnumPickerIntro;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : ProductItemControl {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="EnumWithFlags"/> property.
	/// </summary>
	public static readonly DependencyProperty EnumWithFlagsProperty
		= DependencyProperty.Register(nameof(EnumWithFlags), typeof(EnumWithFlags?), typeof(MainControl), new PropertyMetadata(defaultValue: Common.EnumWithFlags.None));

	/// <summary>
	/// Defines the <see cref="EnumWithoutFlags"/> property.
	/// </summary>
	public static readonly DependencyProperty EnumWithoutFlagsProperty
		= DependencyProperty.Register(nameof(EnumWithoutFlags), typeof(EnumWithoutFlags?), typeof(MainControl), new PropertyMetadata(defaultValue: Common.EnumWithoutFlags.None));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		DataContext = this;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// An enumeration value that has the flags attribute.
	/// </summary>
	/// <value>
	/// The default value is <see cref="EnumWithFlags.None"/>.
	/// </value>
	public EnumWithFlags? EnumWithFlags {
		get => (EnumWithFlags?)GetValue(EnumWithFlagsProperty);
		set => SetValue(EnumWithFlagsProperty, value);
	}

	/// <summary>
	/// An enumeration value that does not have the flags attribute.
	/// </summary>
	/// <value>
	/// The default value is <see cref="EnumWithoutFlags.None"/>.
	/// </value>
	public EnumWithoutFlags? EnumWithoutFlags {
		get => (EnumWithoutFlags?)GetValue(EnumWithoutFlagsProperty);
		set => SetValue(EnumWithoutFlagsProperty, value);
	}

}
