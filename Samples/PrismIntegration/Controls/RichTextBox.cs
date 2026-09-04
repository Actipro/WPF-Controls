using System.Windows.Documents;

namespace ActiproSoftware.Windows.PrismIntegration.Controls;

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
		AcceptsReturnProperty.OverrideMetadata(typeof(RichTextBox), new FrameworkPropertyMetadata(defaultValue: true));
		AcceptsTabProperty.OverrideMetadata(typeof(RichTextBox), new FrameworkPropertyMetadata(defaultValue: true));
		HorizontalScrollBarVisibilityProperty.OverrideMetadata(typeof(RichTextBox), new FrameworkPropertyMetadata(defaultValue: ScrollBarVisibility.Visible));
		VerticalScrollBarVisibilityProperty.OverrideMetadata(typeof(RichTextBox), new FrameworkPropertyMetadata(defaultValue: ScrollBarVisibility.Visible));
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when the <see cref="DocumentUriProperty"/> value is changed.
	/// </summary>
	/// <param name="obj">The <see cref="DependencyObject"/> whose property is changed.</param>
	/// <param name="e">The event data.</param>
	private static void OnDocumentUriPropertyValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
		if (obj is RichTextBox control) {
			try {
				if (control.DocumentUri is { } documentUri)
					control.Document = Application.LoadComponent(documentUri) as FlowDocument;
			}
			catch { }
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="Uri"/> indicating the location of the <see cref="FlowDocument"/> to load.
	/// </summary>
	public Uri DocumentUri {
		get => (Uri)GetValue(DocumentUriProperty);
		set => SetValue(DocumentUriProperty, value);
	}

	/// <inheritdoc/>
	protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
		// Call the base method
		base.OnRenderSizeChanged(sizeInfo);

		// Adjust the document's page width (since there is a WPF bug when used within a parent ScrollViewer with horizontal scroll capabilities)
		if (Document is { } document) {
			var width = ActualWidth - BorderThickness.Left - Padding.Left - BorderThickness.Right - Padding.Right - SystemParameters.VerticalScrollBarWidth;
			document.PageWidth = Math.Max(1, width);
		}
	}

}
