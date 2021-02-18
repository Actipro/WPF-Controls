using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Views;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.BookEvents {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

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
		/// Appends a message to the events <see cref="ListBox"/>.
		/// </summary>
		/// <param name="text">The text to append.</param>
		private void AppendMessage(string text) {
			if (this.eventsListBox == null)
				return;

			ListBoxItem item = new ListBoxItem() { Content = text };
			this.eventsListBox.Items.Add(item);
			this.eventsListBox.SelectedItem = item;
			this.Dispatcher.BeginInvoke((Action)(() => {
				this.eventsListBox.ScrollIntoView(item);
			}));
		}

		/// <summary>
		/// Fades the instructional text into view.
		/// </summary>
		/// <param name="textBlock">The text block to fade in.</param>
		private void FadeInInstructionalText(TextBlock textBlock) {
			DoubleAnimation animation = new DoubleAnimation() {
				To = 1.0,
				Duration = new Duration(TimeSpan.FromMilliseconds(250))
			};
			Storyboard.SetTarget(animation, textBlock);
			Storyboard.SetTargetProperty(animation, new PropertyPath(FrameworkElement.OpacityProperty));

			Storyboard storyboard = new Storyboard();
			storyboard.Children.Add(animation);
			storyboard.Begin();
		}

		/// <summary>
		/// Fades the instructional text out of view.
		/// </summary>
		/// <param name="textBlock">The text block to fade out.</param>
		private void FadeOutInstructionalText(TextBlock textBlock) {
			DoubleAnimation animation = new DoubleAnimation() {
				To = 0.0,
				Duration = new Duration(TimeSpan.FromMilliseconds(100))
			};
			Storyboard.SetTarget(animation, textBlock);
			Storyboard.SetTargetProperty(animation, new PropertyPath(FrameworkElement.OpacityProperty));

			Storyboard storyboard = new Storyboard();
			storyboard.Children.Add(animation);
			storyboard.Begin();
		}


		/// <summary>
		/// Called when the clear list <see cref="Button"/> has been clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnClearListClick(object sender, RoutedEventArgs e) {
			this.eventsListBox.Items.Clear();
		}

		/// <summary>
		/// Invoked when the page curl has been activated.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="BookPageRoutedEventArgs"/> instance containing the event data.</param>
		private void OnPageCurlActivated(object sender, BookPageRoutedEventArgs e) {
			this.AppendMessage("PageCurlActivated");

			this.curlInstructions.Text = string.Format("Click and drag the corner to the {0} side to turn the page",
				(e.Face == BookPageFace.Back) ? "right" : "left");
			this.curlInstructions.HorizontalAlignment = (e.Face == BookPageFace.Back) ? HorizontalAlignment.Left : HorizontalAlignment.Right;
			this.curlInstructions.TextAlignment = (e.Face == BookPageFace.Back) ? TextAlignment.Left : TextAlignment.Right;
			this.FadeInInstructionalText(this.curlInstructions);
		}

		/// <summary>
		/// Invoked when the page curl is activating.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="CancelBookPageRoutedEventArgs"/> instance containing the event data.</param>
		private void OnPageCurlActivating(object sender, CancelBookPageRoutedEventArgs e) {
			this.AppendMessage("PageCurlActivating");

			if (this.cancelPageCurlActivatingCheckBox != null) {
				e.Cancel = (bool)this.cancelPageCurlActivatingCheckBox.IsChecked;
				e.Handled = true;
			}
		}

		/// <summary>
		/// Invoked when the page curl has deactivated.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="BookPageRoutedEventArgs"/> instance containing the event data.</param>
		private void OnPageCurlDeactivated(object sender, BookPageRoutedEventArgs e) {
			this.AppendMessage("PageCurlDeactivated");
			this.FadeOutInstructionalText(this.curlInstructions);
		}

		/// <summary>
		/// Invoked when the page has flipped.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="BookPageRoutedEventArgs"/> instance containing the event data.</param>
		private void OnPageFlipped(object sender, BookPageRoutedEventArgs e) {
			this.AppendMessage("PageFlipped");
			this.FadeOutInstructionalText(this.flipInstructions);
		}

		/// <summary>
		/// Invoked when the page is flipping.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="CancelBookPageRoutedEventArgs"/> instance containing the event data.</param>
		private void OnPageFlipping(object sender, CancelBookPageRoutedEventArgs e) {
			this.AppendMessage("PageFlipping");
			this.FadeOutInstructionalText(this.curlInstructions);

			if (this.cancelPageFlippingCheckBox != null) {
				e.Cancel = (bool)this.cancelPageFlippingCheckBox.IsChecked;
				e.Handled = true;
			}

			if (!e.Cancel) {
				this.flipInstructions.Text = string.Format("Now drag the corner to the {0} of the binding to complete the page turn",
					(e.Face == BookPageFace.Back) ? "right" : "left");
				this.FadeInInstructionalText(this.flipInstructions);
			}
		}

		/// <summary>
		/// Invoked when the selected view has changed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PropertyChangedRoutedEventArgs&lt;System.Int32&gt;"/> instance containing the event data.</param>
		private void OnSelectedViewChanged(object sender, PropertyChangedRoutedEventArgs<int> e) {
			this.AppendMessage("SelectedViewChanged");
		}

	}
}
