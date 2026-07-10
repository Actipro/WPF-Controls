namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Represents an <see cref="ItemsControl"/> that renders a control gallery in a sample.
/// </summary>
public class SampleGalleryControl : ItemsControl {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="Label"/> property.
	/// </summary>
	public static readonly DependencyProperty LabelProperty
		= DependencyProperty.Register(nameof(Label), typeof(string), typeof(SampleGalleryControl), new FrameworkPropertyMetadata(defaultValue: "GALLERY"));

	/// <summary>
	/// Defines the <see cref="UseLowerContrast"/> property.
	/// </summary>
	public static readonly DependencyProperty UseLowerContrastProperty
		= DependencyProperty.Register(nameof(UseLowerContrast), typeof(bool), typeof(SampleGalleryControl), new FrameworkPropertyMetadata(defaultValue: true));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SampleGalleryControl() {
		DefaultStyleKey = typeof(SampleGalleryControl);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The label.
	/// </summary>
	public string? Label {
		get => (string)GetValue(LabelProperty);
		set => SetValue(LabelProperty, value);
	}

	/// <inheritdoc/>
	protected override void PrepareContainerForItemOverride(DependencyObject element, object item) {
		// Adjust the margin between children
		if (element is FrameworkElement container) {
			container.Margin = new Thickness(0, 0, -Padding.Right, -Padding.Bottom);
			container.VerticalAlignment = VerticalAlignment.Top;
		}

		// Pass along contrast setting
		if (element is LabeledCardControl card)
			card.UseLowerContrast = UseLowerContrast;

		base.PrepareContainerForItemOverride(element, item);
	}

	/// <summary>
	/// Indicates whether to use lower contrast colors for the contained card backgrounds.
	/// </summary>
	public bool UseLowerContrast {
		get => (bool)GetValue(UseLowerContrastProperty);
		set => SetValue(UseLowerContrastProperty, value);
	}

}
