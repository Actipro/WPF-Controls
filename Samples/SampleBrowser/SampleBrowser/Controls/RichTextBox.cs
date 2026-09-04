using ActiproSoftware.Windows.Themes;
using System.Windows.Documents;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Represents a <see cref="RichTextBox"/> that has some default property settings for use in the sample browser.
/// </summary>
public class RichTextBox : System.Windows.Controls.RichTextBox {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="DocumentUri"/> property.
	/// </summary>
	public static readonly DependencyProperty DocumentUriProperty
		= DependencyProperty.Register(nameof(DocumentUri), typeof(Uri), typeof(RichTextBox), new FrameworkPropertyMetadata(defaultValue: null, OnDocumentUriPropertyValueChanged));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static RichTextBox() {
		AcceptsReturnProperty.OverrideMetadata(typeof(RichTextBox), new FrameworkPropertyMetadata(true));
		AcceptsTabProperty.OverrideMetadata(typeof(RichTextBox), new FrameworkPropertyMetadata(true));
		HorizontalScrollBarVisibilityProperty.OverrideMetadata(typeof(RichTextBox), new FrameworkPropertyMetadata(ScrollBarVisibility.Visible));
		VerticalScrollBarVisibilityProperty.OverrideMetadata(typeof(RichTextBox), new FrameworkPropertyMetadata(ScrollBarVisibility.Visible));
	}

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public RichTextBox() {
		Style = (Style)FindResource(SharedResourceKeys.TextBoxBaseStyleKey);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private static void OnDocumentUriPropertyValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
		var control = (RichTextBox)obj;
		try {
			control.Document = Application.LoadComponent(control.DocumentUri) as FlowDocument;
		}
		catch { }
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// A <see cref="Uri"/> indicating the location of the <see cref="FlowDocument"/> to load.
	/// </summary>
	public Uri? DocumentUri {
		get => (Uri)GetValue(DocumentUriProperty);
		set => SetValue(DocumentUriProperty, value);
	}

	/// <inheritdoc/>
	protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
		base.OnRenderSizeChanged(sizeInfo);

		// Adjust the document's page width
		if (Document is { } document) {
			document.PageWidth = Math.Max(1,
				ActualWidth - BorderThickness.Left - Padding.Left - BorderThickness.Right - Padding.Right - SystemParameters.VerticalScrollBarWidth
			);
		}
	}

}
