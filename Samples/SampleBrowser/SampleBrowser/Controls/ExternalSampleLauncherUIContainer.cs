using ActiproSoftware.Windows.Themes;
using System.Windows.Documents;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Represents a UI container that can be placed in an external sample's overview document to launch the sample.
/// </summary>
public class ExternalSampleLauncherUIContainer : BlockUIContainer {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ExternalSampleLauncherUIContainer() {
		Loaded += OnLoaded;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnLoaded(object sender, RoutedEventArgs e) {
		if (ViewModel is { } viewModel) {
			// Create a button to open the external sample
			var button = new Button {
				ContentTemplate = Application.Current.TryFindResource("ExternalSampleLauncherButtonContentTemplate") as DataTemplate,
				HorizontalContentAlignment = HorizontalAlignment.Center,
				Margin = new Thickness(50, 10, 50, 10),
				MaxWidth = 600,
				Padding = new Thickness(30, 20, 30, 20),
				Style = Application.Current.TryFindResource("AccentButtonStyle") as Style,
				VerticalContentAlignment = VerticalAlignment.Center
			};

			ThemeProperties.SetCornerRadius(this, new CornerRadius(15));
			SetResourceReference(FontSizeProperty, AssetResourceKeys.ExtraLarge4FontSizeDoubleKey);

			button.CommandParameter = this;
			button.Command = viewModel.OpenExternalSampleCommand;

			Child = button;
		}
		else {
			// Remove the control if it's not in the root window
			if (Parent is FlowDocument document)
				document.Blocks.Remove(this);
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The view-model for this view.
	/// </summary>
	public ApplicationViewModel? ViewModel
		=> DataContext as ApplicationViewModel;

}
