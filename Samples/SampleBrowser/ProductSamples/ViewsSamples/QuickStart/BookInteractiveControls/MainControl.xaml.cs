using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.BookInteractiveControls {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		#region Dependency Properties

		/// <summary>
		/// Identifies the <see cref="AlertOpacity"/> dependency property. This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="AlertOpacity"/> dependency property.</value>
		public static readonly DependencyProperty AlertOpacityProperty = DependencyProperty.Register("AlertOpacity",
			typeof(double), typeof(MainControl), new PropertyMetadata(0.0));

		/// <summary>
		/// Identifies the <see cref="AlertText"/> dependency property. This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="AlertText"/> dependency property.</value>
		public static readonly DependencyProperty AlertTextProperty = DependencyProperty.Register("AlertText",
			typeof(string), typeof(MainControl), new PropertyMetadata(null));

		#endregion // Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Called when the <see cref="ListBox"/> selection has changed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void OnListBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
			ListBoxItem item = ((ListBox)sender).SelectedItem as ListBoxItem;
			this.AlertText = string.Format("You selected the '{0}' ListBox item", (item != null) ? item.Content : "<null>");
			this.ShowAlert();
		}

		/// <summary>
		/// Called when the <see cref="Button"/> has been clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnButtonClick(object sender, System.Windows.RoutedEventArgs e) {
			this.AlertText = "You clicked the Button";
			this.ShowAlert();
		}

		/// <summary>
		/// Called when the <see cref="ComboBox"/> selection has changed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void OnComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
			ComboBoxItem item = ((ComboBox)sender).SelectedItem as ComboBoxItem;
			this.AlertText = string.Format("You selected the '{0}' ComboBox item", (item != null) ? item.Content : "<null>");
			this.ShowAlert();
		}

		/// <summary>
		/// Called when a <see cref="RadioButton"/> has been checked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnRadioButtonChecked(object sender, RoutedEventArgs e) {
			this.AlertText = string.Format("You selected the '{0}' RadioButton", ((RadioButton)sender).Content);
			this.ShowAlert();
		}

		/// <summary>
		/// Called when the <see cref="Slider"/> value has changed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedPropertyChangedEventArgs&lt;System.Double&gt;"/> instance containing the event data.</param>
		private void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
			this.AlertText = string.Format("You selected '{0}' from the Slider", e.NewValue);
			this.ShowAlert();
		}

		/// <summary>
		/// Called when the <see cref="TextBox"/> text has changed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
		private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e) {
			this.AlertText = string.Format("You typed '{0}' into the TextBox", ((TextBox)sender).Text);
			this.ShowAlert();
		}

		/// <summary>
		/// Shows the alert text.
		/// </summary>
		private void ShowAlert() {
			DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames() {
				Duration = new Duration(TimeSpan.FromMilliseconds(3000))
			};
			animation.KeyFrames.Add(new LinearDoubleKeyFrame() {
				KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(150)),
				Value = 1.0
			});
			animation.KeyFrames.Add(new LinearDoubleKeyFrame() {
				KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(3000)),
				Value = 0.0
			});
			Storyboard.SetTarget(animation, this);
			Storyboard.SetTargetProperty(animation, new PropertyPath(AlertOpacityProperty));

			Storyboard storyboard = new Storyboard() {
				Duration = new Duration(TimeSpan.FromMilliseconds(3000))
			}; ;
			storyboard.Children.Add(animation);
			storyboard.Begin();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the opacity of the alert message.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// The opacity of the alert message.
		/// The default value is <c>0.0</c>.
		/// </value>
		public double AlertOpacity {
			get { return (double)this.GetValue(AlertOpacityProperty); }
			set { this.SetValue(AlertOpacityProperty, value); }
		}

		/// <summary>
		/// Gets or sets the text of the alert message.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// The text of the alert message.
		/// The default value is <c>null</c>.
		/// </value>
		public string AlertText {
			get { return (string)this.GetValue(AlertTextProperty); }
			set { this.SetValue(AlertTextProperty, value); }
		}
	}
}
