using ActiproSoftware.Windows.Themes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Represents a UI container that can be placed in an external sample's overview document to launch the sample.
	/// </summary>
	public class ExternalSampleLauncherUIContainer : BlockUIContainer {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ExternalSampleLauncherUIContainer"/> class.
		/// </summary>
		public ExternalSampleLauncherUIContainer() {
			this.Loaded += this.OnLoaded;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the control is loaded.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains data related to this event.</param>
		private void OnLoaded(object sender, RoutedEventArgs e) {
			var viewModel = this.ViewModel;
			if (viewModel != null) {
				// Create a button to open the external sample
				var button = new Button();

				button.ContentTemplate = Application.Current.TryFindResource("ExternalSampleLauncherButtonContentTemplate") as DataTemplate;
				button.HorizontalContentAlignment = HorizontalAlignment.Center;
				button.Margin = new Thickness(50, 10, 50, 10);
				button.MaxWidth = 600;
				button.Padding = new Thickness(30, 20, 30, 20);
				button.Style = Application.Current.TryFindResource("AccentButtonStyle") as Style;
				button.VerticalContentAlignment = VerticalAlignment.Center;

				ThemeProperties.SetCornerRadius(this, new CornerRadius(15));
				this.SetResourceReference(FontSizeProperty, AssetResourceKeys.ExtraLarge4FontSizeDoubleKey);

				button.CommandParameter = this;
				button.Command = viewModel.OpenExternalSampleCommand;

				this.Child = button;
			}
			else {
				// Remove the control if it's not in the root window
				var document = this.Parent as FlowDocument;
				if (document != null)
					document.Blocks.Remove(this);
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the view-model for this view.
		/// </summary>
		/// <value>The view-model for this view.</value>
		public ApplicationViewModel ViewModel => this.DataContext as ApplicationViewModel;

	}

}
