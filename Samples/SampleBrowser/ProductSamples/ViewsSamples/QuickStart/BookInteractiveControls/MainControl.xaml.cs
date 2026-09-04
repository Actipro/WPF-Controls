using System.Windows.Media.Animation;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.BookInteractiveControls;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="AlertOpacity"/> property.
	/// </summary>
	public static readonly DependencyProperty AlertOpacityProperty
		= DependencyProperty.Register(nameof(AlertOpacity), typeof(double), typeof(MainControl), new PropertyMetadata(defaultValue: 0.0));

	/// <summary>
	/// Defines the <see cref="AlertText"/> property.
	/// </summary>
	public static readonly DependencyProperty AlertTextProperty
		= DependencyProperty.Register(nameof(AlertText), typeof(string), typeof(MainControl), new PropertyMetadata(defaultValue: null));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnListBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
		var item = ((ListBox)sender).SelectedItem as ListBoxItem;
		AlertText = string.Format("You selected the '{0}' ListBox item", item?.Content ?? "<null>");
		ShowAlert();
	}

	private void OnButtonClick(object sender, RoutedEventArgs e) {
		AlertText = "You clicked the Button";
		ShowAlert();
	}

	private void OnComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
		var item = ((ComboBox)sender).SelectedItem as ComboBoxItem;
		AlertText = string.Format("You selected the '{0}' ComboBox item", item?.Content ?? "<null>");
		ShowAlert();
	}

	private void OnRadioButtonChecked(object sender, RoutedEventArgs e) {
		AlertText = string.Format("You selected the '{0}' RadioButton", ((RadioButton)sender).Content);
		ShowAlert();
	}

	private void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
		AlertText = string.Format("You selected '{0}' from the Slider", e.NewValue);
		ShowAlert();
	}

	private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e) {
		AlertText = string.Format("You typed '{0}' into the TextBox", ((TextBox)sender).Text);
		ShowAlert();
	}

	private void ShowAlert() {
		var animation = new DoubleAnimationUsingKeyFrames() {
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

		var storyboard = new Storyboard() {
			Duration = new Duration(TimeSpan.FromMilliseconds(3000))
		};
		storyboard.Children.Add(animation);
		storyboard.Begin();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The opacity of the alert message.
	/// </summary>
	/// <value>
	/// The default value is <c>0.0</c>.
	/// </value>
	public double AlertOpacity {
		get => (double)GetValue(AlertOpacityProperty);
		set => SetValue(AlertOpacityProperty, value);
	}

	/// <summary>
	/// The text of the alert message.
	/// </summary>
	/// <value>
	/// The default value is <c>null</c>.
	/// </value>
	public string? AlertText {
		get => (string)GetValue(AlertTextProperty);
		set => SetValue(AlertTextProperty, value);
	}

}
