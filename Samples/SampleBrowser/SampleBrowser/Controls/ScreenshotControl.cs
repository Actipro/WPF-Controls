using System.Windows.Documents;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Implements <see cref="Control"/> that renders a screenshot.
/// </summary>
public class ScreenshotControl : Control {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="ImageSource"/> property.
	/// </summary>
	public static readonly DependencyProperty ImageSourceProperty
		= DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(ScreenshotControl), new FrameworkPropertyMetadata(defaultValue: null));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ScreenshotControl() {
		DefaultStyleKey = typeof(ScreenshotControl);

		Loaded += OnLoaded;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnLoaded(object sender, RoutedEventArgs e) {
		// Remove the control if it's not in the root window
		if (DataContext is not ApplicationViewModel
			&& Parent is InlineUIContainer container
			&& container.Parent is Paragraph para
			&& para.Parent is FlowDocument document
		) {
			document.Blocks.Remove(para);
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The image source.
	/// </summary>
	public ImageSource? ImageSource {
		get => (ImageSource)GetValue(ImageSourceProperty);
		set => SetValue(ImageSourceProperty, value);
	}

}
