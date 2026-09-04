using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.HighlightingStyleViewer;

/// <summary>
/// Interaction logic for xaml
/// </summary>
public partial class HighlightingStyleEditor : UserControl {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="ClassificationType"/> property.
	/// </summary>
	public static readonly DependencyProperty ClassificationTypeProperty
		= DependencyProperty.Register(nameof(ClassificationType), typeof(IClassificationType), typeof(HighlightingStyleEditor), new FrameworkPropertyMetadata(defaultValue: null, OnPropertyChangedForRefresh));

	/// <summary>
	/// Defines the <see cref="HighlightingStyleRegistry"/> property.
	/// </summary>
	public static readonly DependencyProperty HighlightingStyleRegistryProperty
		= DependencyProperty.Register(nameof(HighlightingStyleRegistry), typeof(IHighlightingStyleRegistry), typeof(HighlightingStyleEditor), new FrameworkPropertyMetadata(defaultValue: null, OnPropertyChangedForRefresh));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public HighlightingStyleEditor() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when a property is changed that needs to refresh the control.
	/// </summary>
	/// <param name="obj">The <see cref="DependencyObject"/> whose property is changed.</param>
	/// <param name="e">The event data.</param>
	private static void OnPropertyChangedForRefresh(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
		var control = (HighlightingStyleEditor)obj;

		// Get the style to edit
		var style = (control.ClassificationType is not null)
			? control.HighlightingStyleRegistry?[control.ClassificationType]
			: null;

		// Update the data context
		control.DataContext = style;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="IClassificationType"/> for which to edit an <see cref="IHighlightingStyle"/>.
	/// </summary>
	public IClassificationType? ClassificationType {
		get => (IClassificationType)GetValue(ClassificationTypeProperty);
		set => SetValue(ClassificationTypeProperty, value);
	}

	/// <summary>
	/// The <see cref="IHighlightingStyleRegistry"/> to use.
	/// </summary>
	public IHighlightingStyleRegistry? HighlightingStyleRegistry {
		get => (IHighlightingStyleRegistry)GetValue(HighlightingStyleRegistryProperty);
		set => SetValue(HighlightingStyleRegistryProperty, value);
	}

}
