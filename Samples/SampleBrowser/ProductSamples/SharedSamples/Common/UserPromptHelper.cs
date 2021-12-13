using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Input;
using ActiproSoftware.Windows.Themes;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.SharedSamples.Common {

	/// <summary>
	/// Defines helper methods for use with <see cref="UserPromptWindow"/>.
	/// </summary>
	internal static class UserPromptHelper {

		//
		// Helper classes like this are a great way to make it easy to create and display custom user prompts.
		//

		/// <summary>
		/// Displays a user prompt with details about an exception.
		/// </summary>
		/// <param name="ex">The exception to be displayed.</param>
		public static void ShowException(Exception ex) {
			if (ex == null)
				throw new ArgumentNullException(nameof(ex));
			ShowException(ex, ex.GetType().Name);
		}

		/// <summary>
		/// Displays a user prompt with details about an exception.
		/// </summary>
		/// <param name="ex">The exception to be displayed.</param>
		/// <param name="header">The header instruction of the prompt.</param>
		public static void ShowException(Exception ex, string header) {
			if (ex == null)
				throw new ArgumentNullException(nameof(ex));

			// Read the stack trace
			var stackTrace = ex.ToString();

			// Initialize the prompt
			var userPromptControl = new UserPromptControl() {
				Header = header,
				Content = ex.Message,
				ExpandedInformationCollapsedHeaderText = "Show details",
				ExpandedInformationExpandedHeaderText = "Hide details",
				ExpandedInformationContentClipboardText = stackTrace,
				StandardButtons = UserPromptStandardButtons.OK,
				StandardStatusImage = UserPromptStandardImage.Error,
				Width = 500,
			};

			// Build the hyperlink to copy stack trace to the clipboard
			var copyStackTraceLink = new TextBlock(
				new Hyperlink(new Run("Copy to Clipboard")) {
					Command = new DelegateCommand<object>(p => Clipboard.SetText(stackTrace))
				}) {
			};
			copyStackTraceLink.SetValue(DockPanel.DockProperty, Dock.Right);

			// Build the expanded information with the stack trace
			userPromptControl.ExpandedInformationContent = new StackPanel() {
				Children = {
					new DockPanel() {
						HorizontalAlignment = HorizontalAlignment.Stretch,
						LastChildFill = true,
						Children = {
							copyStackTraceLink,
							new TextBlock(new Run("Stack Trace:")) {
								FontWeight = FontWeights.Bold,
								Margin = new Thickness(0, 0, 0, 3),
								HorizontalAlignment = HorizontalAlignment.Left,
							},
						},
					},
					new TextBox() {
						IsReadOnly = true,
						MaxLines = 5,
						Background = (Brush)userPromptControl.FindResource(AssetResourceKeys.ContainerBackgroundLowBrushKey),
						HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
						VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
						Text = stackTrace,
					},
				}
			};

			// Show the prompt
			UserPromptWindow.ShowDialog(userPromptControl, title: "Error");
		}

	}

}
