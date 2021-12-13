using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Input;
using ActiproSoftware.Windows.Themes;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.SharedSamples.QuickStart.UserPromptOverlayIntro {

	/// <summary>
	/// Provides the main window for this sample.
	/// </summary>
	public partial class MainWindow {

		private ICommand closePromptCommand;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainWindow</c> class.
		/// </summary>
		public MainWindow() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Closes the overlay prompt without responding.
		/// </summary>
		private void ClosePrompt() {
			if (WindowChrome.GetIsOverlayVisible(this)) {
				// Get the UserPromptControl displayed
				var userPromptControl = userPromptContentControl.Content as UserPromptControl;
				if (userPromptControl == null)
					return;

				// Get the result which should be applied when closing the prompt
				var closeResult = userPromptControl.CloseResult.GetValueOrDefault();

				// Quit if a close result is not defined (which indicates the prompt cannot be closed without responding)
				if (closeResult == UserPromptStandardResult.None)
					return;

				// Set the result
				userPromptControl.Result = closeResult;
			}
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private async void OnShowPromptButtonClick(object sender, RoutedEventArgs e) {

			// Create the prompt to be displayed
			var userPromptControl = new UserPromptControl() {
				Header = "This is a warning message.",
				Content = "This prompt acts like a modal dialog by blocking interaction with the application until the prompt is addressed.",
				StandardButtons = UserPromptStandardButtons.OKCancel,
				DefaultResult = UserPromptStandardResult.Cancel,
				CornerRadius = new CornerRadius(5),
				BorderThickness = new Thickness(1),
				StandardStatusImage = UserPromptStandardImage.Warning,
			};

			// Show the prompt and handle the result
			var result = await ShowPrompt(userPromptControl);
			ThemedMessageBox.Show($"The following result was selected:  {result}", "Result");
		}

		/// <summary>
		/// Show the prompt as an overlay.
		/// </summary>
		/// <param name="userPromptControl">The user prompt control to display.</param>
		/// <returns>A <see cref="Task"/> of type <see cref="UserPromptStandardResult"/> which, when completed, will define the result of interacting with the user prompt.</returns>
		private async Task<UserPromptStandardResult> ShowPrompt(UserPromptControl userPromptControl) {
			// NOTE: This sample assumes the overlay is not being used for any other purpose. For
			//		 more complicated applications that those using a Ribbon Backstage, more coordination
			//		 is required to ensure the proper overlay is displayed and traditional dialog-based
			//		 prompts may be preferred.

			// Immediately hide any existing overlay
			WindowChrome.SetOverlayAnimationKinds(this, OverlayAnimationKinds.None);
			WindowChrome.SetIsOverlayVisible(this, false);

			// Populate the overlay with the UserPromptControl
			userPromptContentControl.Content = userPromptControl ?? throw new ArgumentNullException(nameof(userPromptControl));

			// Hide the overlay when a response is registered
			userPromptControl.Responding += (s, e) => {
				WindowChrome.SetIsOverlayVisible(this, false);
			};

			// Show the overlay
			WindowChrome.SetOverlayAnimationKinds(this, OverlayAnimationKinds.Fade);
			WindowChrome.SetIsOverlayVisible(this, true);

			// Wait for the overlay to close
			await Task.Run(async () => {
				var isOpen = false;
				do {
					await Task.Delay(250);
					await this.Dispatcher.InvokeAsync(() => {
						isOpen = WindowChrome.GetIsOverlayVisible(this);
					});

				} while (isOpen);
			});

			// Read the result
			if (!Enum.TryParse(userPromptControl.Result?.ToString(), out UserPromptStandardResult result))
				result = UserPromptStandardResult.None;

			// Clear the user prompt control
			userPromptContentControl.Content = null;

			return result;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the <see cref="ICommand"/> which is executed to close the overlay prompt without responding.
		/// </summary>
		public ICommand ClosePromptCommand {
			get {
				if (closePromptCommand == null)
					closePromptCommand = new DelegateCommand<object>((p) => ClosePrompt());
				return closePromptCommand;
			}
		}

	}

}