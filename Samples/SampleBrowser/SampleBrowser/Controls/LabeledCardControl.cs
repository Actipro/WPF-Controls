namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Implements <see cref="Control"/> that renders its content in a card.
/// </summary>
[ContentProperty(nameof(Child))]
public class LabeledCardControl : Control {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="Child"/> property.
	/// </summary>
	public static readonly DependencyProperty ChildProperty
		= DependencyProperty.Register(nameof(Child), typeof(UIElement), typeof(LabeledCardControl), new FrameworkPropertyMetadata(defaultValue: null));

	/// <summary>
	/// Defines the <see cref="Label"/> property.
	/// </summary>
	public static readonly DependencyProperty LabelProperty
		= DependencyProperty.Register(nameof(Label), typeof(string), typeof(LabeledCardControl), new FrameworkPropertyMetadata(defaultValue: null));

	/// <summary>
	/// Defines the <see cref="LabelBackground"/> property.
	/// </summary>
	public static readonly DependencyProperty LabelBackgroundProperty
		= DependencyProperty.Register(nameof(LabelBackground), typeof(Brush), typeof(LabeledCardControl), new FrameworkPropertyMetadata(defaultValue: null));

	/// <summary>
	/// Defines the <see cref="Orientation"/> property.
	/// </summary>
	public static readonly DependencyProperty OrientationProperty
		= DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(LabeledCardControl), new FrameworkPropertyMetadata(defaultValue: Orientation.Vertical));

	/// <summary>
	/// Defines the <see cref="UseLowerContrast"/> property.
	/// </summary>
	public static readonly DependencyProperty UseLowerContrastProperty
		= DependencyProperty.Register(nameof(UseLowerContrast), typeof(bool), typeof(LabeledCardControl), new FrameworkPropertyMetadata(defaultValue: false));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public LabeledCardControl() {
		DefaultStyleKey = typeof(LabeledCardControl);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The child element.
	/// </summary>
	public UIElement? Child {
		get => (UIElement)GetValue(ChildProperty);
		set => SetValue(ChildProperty, value);
	}

	/// <summary>
	/// The text label.
	/// </summary>
	public string? Label {
		get => (string)GetValue(LabelProperty);
		set => SetValue(LabelProperty, value);
	}

	/// <summary>
	/// The text label background <see cref="Brush"/>.
	/// </summary>
	public Brush? LabelBackground {
		get => (Brush)GetValue(LabelBackgroundProperty);
		set => SetValue(LabelBackgroundProperty, value);
	}

	/// <summary>
	/// The layout orientation.
	/// </summary>
	public Orientation Orientation {
		get => (Orientation)GetValue(OrientationProperty);
		set => SetValue(OrientationProperty, value);
	}

	/// <summary>
	/// Indicates whether to use lower contrast colors for the background.
	/// </summary>
	public bool UseLowerContrast {
		get => (bool)GetValue(UseLowerContrastProperty);
		set => SetValue(UseLowerContrastProperty, value);
	}

}
