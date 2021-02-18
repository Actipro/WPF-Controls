using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Navigation;
using ActiproSoftware.Windows.Controls.Navigation.Serialization;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Themes;
using ActiproSoftware.Windows.Serialization;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.NavigationBarLayout {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private NavigationBarLayoutSerializer layoutSerializer;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			this.Loaded += new RoutedEventHandler(OnLoaded);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates the layout serializer.
		/// </summary>
		private void CreateLayoutSerializer() {
			// Create a layout serializer that supports custom data
			if (this.layoutSerializer == null) {
				this.layoutSerializer = new NavigationBarLayoutSerializer();
				this.layoutSerializer.ObjectSerialized += new EventHandler<ItemSerializationEventArgs>(this.OnObjectSerialized);
				this.layoutSerializer.CustomTypes.Add(typeof(CustomData));  // Register the custom data type
			}
		}

		/// <summary>
		/// Occurs when the control is loaded.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLoaded(object sender, RoutedEventArgs e) {
			// Initialize the layout textbox
			this.SaveLayout();
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLoadLayoutButtonClick(object sender, RoutedEventArgs e) {
			this.CreateLayoutSerializer();
			this.layoutSerializer.LoadFromString(layoutTextBox.Text, navBar);

			// If you wish to read the custom data from the layout, attach to the NavigationBarLayoutSerializer.ObjectDeserialized event
			// using a handler like OnObjectSerialized but one that has code to read the custom data instead of write it
		}
		
		/// <summary>
		/// Occurs when an object is serialized into the layout.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ItemSerializationEventArgs"/> that contains the event data.</param>
		private void OnObjectSerialized(object sender, ItemSerializationEventArgs e) {
			// Store some custom data in the layout... this is called every time an object is serialized
			if (e.Node is XmlNavigationBarLayout) {
				CustomData data = new CustomData();
				data.StringValue = "Custom data object in root layout element, injected in this sample's code-behind";
				e.Node.Tag = data;
			}
			else if (e.Item == mailPane)
				e.Node.Tag = "Custom data for Mail pane only";
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSaveLayoutButtonClick(object sender, RoutedEventArgs e) {
			this.SaveLayout();
		}

		/// <summary>
		/// Saves the layout to a <see cref="TextBox"/>.
		/// </summary>
		private void SaveLayout() {
			this.CreateLayoutSerializer();
			this.layoutTextBox.Text = layoutSerializer.SaveToString(navBar);
		}
	}
}