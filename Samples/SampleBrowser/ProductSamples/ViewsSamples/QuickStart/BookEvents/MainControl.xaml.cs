using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Views;
using System.Windows.Media.Animation;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.BookEvents;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

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

	private static void AnimateInstructionalTextOpacity(TextBlock textBlock, double targetOpacity, TimeSpan duration) {
		var animation = new DoubleAnimation() {
			To = targetOpacity,
			Duration = new Duration(duration)
		};
		Storyboard.SetTarget(animation, textBlock);
		Storyboard.SetTargetProperty(animation, new PropertyPath(OpacityProperty));

		var storyboard = new Storyboard();
		storyboard.Children.Add(animation);
		storyboard.Begin();
	}

	/// <summary>
	/// Appends a message to the events <see cref="ListBox"/>.
	/// </summary>
	/// <param name="text">The text to append.</param>
	private void AppendMessage(string text) {
		if (eventsListBox is null)
			return;

		var item = new ListBoxItem() { Content = text };
		eventsListBox.Items.Add(item);
		eventsListBox.SelectedItem = item;
		Dispatcher.BeginInvoke(() => {
			eventsListBox.ScrollIntoView(item);
		});
	}

	/// <summary>
	/// Fades the instructional text into view.
	/// </summary>
	/// <param name="textBlock">The text block to fade in.</param>
	private static void FadeInInstructionalText(TextBlock textBlock)
		=> AnimateInstructionalTextOpacity(textBlock, targetOpacity: 1.0, TimeSpan.FromMilliseconds(250));

	/// <summary>
	/// Fades the instructional text out of view.
	/// </summary>
	/// <param name="textBlock">The text block to fade out.</param>
	private static void FadeOutInstructionalText(TextBlock textBlock)
		=> AnimateInstructionalTextOpacity(textBlock, targetOpacity: 0.0, TimeSpan.FromMilliseconds(100));

	private void OnClearListClick(object sender, RoutedEventArgs e)
		=> eventsListBox.Items.Clear();

	private void OnPageCurlActivated(object sender, BookPageRoutedEventArgs e) {
		AppendMessage("PageCurlActivated");

		curlInstructions.Text = string.Format("Click and drag the corner to the {0} side to turn the page",
			(e.Face == BookPageFace.Back) ? "right" : "left");
		curlInstructions.HorizontalAlignment = (e.Face == BookPageFace.Back) ? HorizontalAlignment.Left : HorizontalAlignment.Right;
		curlInstructions.TextAlignment = (e.Face == BookPageFace.Back) ? TextAlignment.Left : TextAlignment.Right;
		FadeInInstructionalText(curlInstructions);
	}

	private void OnPageCurlActivating(object sender, CancelBookPageRoutedEventArgs e) {
		AppendMessage("PageCurlActivating");

		if (cancelPageCurlActivatingCheckBox is not null) {
			e.Cancel = cancelPageCurlActivatingCheckBox.IsChecked == true;
			e.Handled = true;
		}
	}

	private void OnPageCurlDeactivated(object sender, BookPageRoutedEventArgs e) {
		AppendMessage("PageCurlDeactivated");
		FadeOutInstructionalText(curlInstructions);
	}

	private void OnPageFlipped(object sender, BookPageRoutedEventArgs e) {
		AppendMessage("PageFlipped");
		FadeOutInstructionalText(flipInstructions);
	}

	private void OnPageFlipping(object sender, CancelBookPageRoutedEventArgs e) {
		AppendMessage("PageFlipping");
		FadeOutInstructionalText(curlInstructions);

		if (cancelPageFlippingCheckBox is not null) {
			e.Cancel = cancelPageFlippingCheckBox.IsChecked == true;
			e.Handled = true;
		}

		if (!e.Cancel) {
			flipInstructions.Text = string.Format("Now drag the corner to the {0} of the binding to complete the page turn",
				(e.Face == BookPageFace.Back) ? "right" : "left");
			FadeInInstructionalText(flipInstructions);
		}
	}

	private void OnSelectedViewChanged(object sender, PropertyChangedRoutedEventArgs<int> e)
		=> AppendMessage("SelectedViewChanged");

}
