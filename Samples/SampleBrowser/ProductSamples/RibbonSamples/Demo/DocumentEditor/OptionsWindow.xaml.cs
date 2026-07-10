namespace ActiproSoftware.ProductSamples.RibbonSamples.Demo.DocumentEditor;

/// <summary>
/// Provides the options window for this sample.
/// </summary>
public partial class OptionsWindow : Window {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="Ribbon"/> property.
	/// </summary>
	public static readonly DependencyProperty RibbonProperty
		= DependencyProperty.Register(nameof(Ribbon), typeof(Windows.Controls.Ribbon.Ribbon), typeof(OptionsWindow), new FrameworkPropertyMetadata(defaultValue: null));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="ribbon">The ribbon being customized.</param>
	public OptionsWindow(Windows.Controls.Ribbon.Ribbon ribbon) {
		Ribbon = ribbon;

		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when the button is clicked.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnOKButtonClick(object sender, RoutedEventArgs e) {
		// Save changes to QAT customization
		customizeQat.Save();

		DialogResult = true;
		Close();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnClosing(CancelEventArgs e) {
		if (DialogResult != true) {
			// Cancel changes from QAT customization
			customizeQat.Cancel();
		}

		base.OnClosing(e);
	}

	/// <inheritdoc/>
	protected override void OnInitialized(EventArgs e) {
		base.OnInitialized(e);

		// Assign the ribbon
		customizeQat.Ribbon = Ribbon;
	}

	/// <summary>
	/// The ribbon that is being customized.
	/// </summary>
	public Windows.Controls.Ribbon.Ribbon? Ribbon {
		get => (Windows.Controls.Ribbon.Ribbon)GetValue(RibbonProperty);
		set => SetValue(RibbonProperty, value);
	}

}
