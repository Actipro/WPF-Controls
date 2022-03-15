using ActiproSoftware.ProductSamples.SharedSamples.Common;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Input;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Themes;
using ActiproSoftware.Windows.Themes.Generation;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.SharedSamples.QuickStart.UserPromptIntro {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private ICommand	contextualHelpCommand;
		private bool		standardCheckBoxSampleIsChecked = false;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Applies a custom appearance to the given <paramref name="userPromptControl"/> based on the context
		/// associated with the status image (e.g. red for error).
		/// </summary>
		/// <param name="userPromptControl">The control to customize.</param>
		private void ApplyImageBasedAppearance(UserPromptControl userPromptControl) {

			ColorFamilyName colorFamilyName;
			switch (userPromptControl.StandardStatusImage) {
				case UserPromptStandardImage.Error:
					colorFamilyName = ColorFamilyName.Red;
					break;
				case UserPromptStandardImage.Information:
					colorFamilyName = ColorFamilyName.Blue;
					break;
				case UserPromptStandardImage.Warning:
					colorFamilyName = ColorFamilyName.Orange;
					break;
				case UserPromptStandardImage.Question:
					colorFamilyName = ColorFamilyName.Indigo;
					break;
				default:
					// Nothing to apply
					return;
			}

			// Adjust assets based on theme
			var themeDefinition = ThemeManager.GetThemeDefinition(ThemeManager.CurrentTheme);
			var colorPalette = new ColorPalette(themeDefinition);
			var colorRamp = colorPalette.GetColorRamp(colorFamilyName);

			var lightestColorFamilyBrush = new SolidColorBrush(colorRamp[ShadeName.Lightest].Color);
			var lighterColorFamilyBrush = new SolidColorBrush(colorRamp[ShadeName.Lighter].Color);
			var lightColorFamilyBrush = new SolidColorBrush(colorRamp[ShadeName.Light].Color);
			var litColorFamilyBrush = new SolidColorBrush(colorRamp[ShadeName.Lit].Color);
			var baseColorFamilyBrush = new SolidColorBrush(colorRamp[ShadeName.Base].Color);
			var dimColorFamilyBrush = new SolidColorBrush(colorRamp[ShadeName.Dim].Color);
			var darkerColorFamilyBrush = new SolidColorBrush(colorRamp[ShadeName.Darker].Color);

			// Set direct properties on the User Prompt Control
			userPromptControl.Foreground = Brushes.Black;
			userPromptControl.Background = lightestColorFamilyBrush;
			userPromptControl.TrayForeground = Brushes.Black;
			userPromptControl.TrayBackground = lighterColorFamilyBrush;
			userPromptControl.BorderBrush = lightColorFamilyBrush;
			userPromptControl.HeaderForeground = darkerColorFamilyBrush;

			//
			// Customize the assets used by buttons to change appearance in all states without style-based triggers
			//

			// Normal
			userPromptControl.Resources.Add(AssetResourceKeys.ButtonForegroundNormalBrushKey, Brushes.Black);
			userPromptControl.Resources.Add(AssetResourceKeys.ButtonBackgroundNormalBrushKey, lightColorFamilyBrush);
			userPromptControl.Resources.Add(AssetResourceKeys.ButtonBorderNormalBrushKey, litColorFamilyBrush);

			// Defaulted/Focused
			userPromptControl.Resources.Add(AssetResourceKeys.ButtonForegroundDefaultedBrushKey, Brushes.Black);
			userPromptControl.Resources.Add(AssetResourceKeys.ButtonBackgroundDefaultedBrushKey, lightColorFamilyBrush);
			userPromptControl.Resources.Add(AssetResourceKeys.ButtonBorderDefaultedBrushKey, dimColorFamilyBrush);

			// Hover
			userPromptControl.Resources.Add(AssetResourceKeys.ButtonForegroundHoverBrushKey, Brushes.Black);
			userPromptControl.Resources.Add(AssetResourceKeys.ButtonBackgroundHoverBrushKey, litColorFamilyBrush);
			userPromptControl.Resources.Add(AssetResourceKeys.ButtonBorderHoverBrushKey, dimColorFamilyBrush);

			// Checked
			userPromptControl.Resources.Add(AssetResourceKeys.ButtonForegroundCheckedBrushKey, Brushes.Black);
			userPromptControl.Resources.Add(AssetResourceKeys.ButtonBackgroundCheckedBrushKey, lightColorFamilyBrush);
			userPromptControl.Resources.Add(AssetResourceKeys.ButtonBorderCheckedBrushKey, dimColorFamilyBrush);

			// Pressed
			userPromptControl.Resources.Add(AssetResourceKeys.ButtonForegroundPressedBrushKey, Brushes.Black);
			userPromptControl.Resources.Add(AssetResourceKeys.ButtonBackgroundPressedBrushKey, baseColorFamilyBrush);
			userPromptControl.Resources.Add(AssetResourceKeys.ButtonBorderPressedBrushKey, darkerColorFamilyBrush);

			// Disabled
			userPromptControl.Resources.Add(AssetResourceKeys.ContainerForegroundLowDisabledBrushKey, new SolidColorBrush(Color.FromArgb(200, 0, 0, 0))); // Black with transparency
			userPromptControl.Resources.Add(AssetResourceKeys.ButtonBackgroundDisabledBrushKey, lightestColorFamilyBrush);
			userPromptControl.Resources.Add(AssetResourceKeys.ButtonBorderDisabledBrushKey, lightColorFamilyBrush);

		}

		/// <summary>
		/// Creates a <see cref="Button"/> control.
		/// </summary>
		/// <param name="result">The standard result to be associated with the button.</param>
		/// <param name="text">The text, if any, to be used as the button label.</param>
		/// <param name="imageSource">The source, if any, of an image to be placed within the button.</param>
		/// <param name="imageAlignment">The alignment to be used for any images added to the button.</param>
		/// <param name="isDefault"><c>true</c> if the button should be configured as the default button.</param>
		/// <returns>A new <see cref="Button"/> control.</returns>
		private Button CreateButton(UserPromptStandardResult result, string text = null, ImageSource imageSource = null, HorizontalAlignment imageAlignment = HorizontalAlignment.Left, bool isDefault = false) {

			// Initialize the button
			var button = new Button() { IsDefault = isDefault };

			// Set the attached property for the result associated with the button
			UserPromptControl.SetButtonResult(button, result);

			if (imageSource is null) {
				// No image, so set the context to any text that was provided
				button.Content = text;
			}
			else {
				var image = new DynamicImage() {
					Source = imageSource,
					Height = 16,
					Width = 16,
					SnapsToDevicePixels = true,
					UseLayoutRounding = true,
				};
				if (text is null) {
					// Use only the image as content
					button.Content = image;
				}
				else {
					// Combine the image and text as content
					var accessText = new AccessText() { Text = text };

					switch (imageAlignment) {
						case HorizontalAlignment.Left:
							image.VerticalAlignment = VerticalAlignment.Center;
							image.Margin = new Thickness(0, 0, 4, 0);
							accessText.VerticalAlignment = VerticalAlignment.Center;
							button.Content = new StackPanel {
								Orientation = Orientation.Horizontal,
								Children = { image, accessText }
							};
							break;

						case HorizontalAlignment.Right:
							image.VerticalAlignment = VerticalAlignment.Center;
							image.Margin = new Thickness(4, 0, 0, 0);
							accessText.VerticalAlignment = VerticalAlignment.Center;
							button.Content = new StackPanel {
								Orientation = Orientation.Horizontal,
								Children = { accessText, image }
							};
							break;

						case HorizontalAlignment.Center:
							image.HorizontalAlignment = HorizontalAlignment.Center;
							image.Margin = new Thickness(0, 0, 0, 4);
							accessText.HorizontalAlignment = HorizontalAlignment.Center;
							button.Content = new StackPanel {
								Orientation = Orientation.Vertical,
								HorizontalAlignment = HorizontalAlignment.Center,
								Children = { image, accessText }
							};
							break;

						default:
							throw new NotSupportedException();
					}
				}
			}

			return button;
		}

		/// <summary>
		/// Occurs when a hyperlink is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnGenericHyperlinkClick(object sender, RoutedEventArgs e) {
			ThemedMessageBox.Show("Use this event handler to respond to the hyperlink.", "Hyperlink Clicked", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		/// <summary>
		/// Occurs when a hyperlink is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnOpenMessageSettingsHyperlinkClick(object sender, RoutedEventArgs e) {
			ThemedMessageBox.Show("Use this event handler to open a Settings dialog.");
		}

		/// <summary>
		/// Occurs when a button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSaveChangesAndExitButtonClick(object sender, RoutedEventArgs e) {
			ThemedMessageBox.Show("Use this event handler to save changes.");
		}

		/// <summary>
		/// Occurs when a button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnShowNativeMessageBoxButtonClick(object sender, RoutedEventArgs e) {
			var text = messageBoxText.Text;
			if (string.IsNullOrEmpty(text))
				text = null;
			var caption = messageBoxCaption.Text;
			if (string.IsNullOrEmpty(caption))
				caption = null;
			var button = (MessageBoxButton)((ComboBoxItem)messageBoxButton.SelectedItem).Tag;
			var image = (MessageBoxImage)((ComboBoxItem)messageBoxImage.SelectedItem).Tag;
			var defaultResult = (MessageBoxResult)((ComboBoxItem)messageBoxDefaultResult.SelectedItem).Tag;

			var result = MessageBox.Show(text, caption, button, image, defaultResult);
			messageBoxResult.Text = result.ToString();
		}

		/// <summary>
		/// Occurs when a button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnShowThemedMessageBoxButtonClick(object sender, RoutedEventArgs e) {
			var text = messageBoxText.Text;
			if (string.IsNullOrEmpty(text))
				text = null;
			var caption = messageBoxCaption.Text;
			if (string.IsNullOrEmpty(caption))
				caption = null;
			var button = (MessageBoxButton)((ComboBoxItem)messageBoxButton.SelectedItem).Tag;
			var image = (MessageBoxImage)((ComboBoxItem)messageBoxImage.SelectedItem).Tag;
			var defaultResult = (MessageBoxResult)((ComboBoxItem)messageBoxDefaultResult.SelectedItem).Tag;
			var helpAction = (addHelpButton.IsChecked == true)
				? (Action)(() => ShowContextualHelp("MyContext"))
				: null;

			MessageBoxResult result;

			// Cache the current image provider
			var originalImageProvider = ImageProvider.Default;
			try {
				if (useSystemImages.IsChecked == true) {
					// This sample temporarily changes the ImageProvider to one that provides SystemIcons.
					// In most scenarios, any changes to ImageProvider.Default would be assigned during
					// application startup and last the duration of the application session.
					ImageProvider.Default = SystemIconsImageProvider.Instance;
				}

				// Show the MessageBox
				result = ThemedMessageBox.Show(text, caption, button, image, defaultResult, helpAction);

			}
			finally {
				// Restore the original image provider
				if (useSystemImages.IsChecked == true)
					ImageProvider.Default = originalImageProvider;
			}

			messageBoxResult.Text = result.ToString();
		}

		/// <summary>
		/// Occurs when a response is provided for a user prompt control.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="UserPromptResponseEventArgs"/> that contains the event data.</param>
		private void OnUserPromptControlRespondingCancelWhenVerifieid(object sender, UserPromptResponseEventArgs e) {
			if ((sender is UserPromptControl userPromptControl) && (userPromptControl.IsChecked)) {
				// Cancel the response
				ThemedMessageBox.Show($"Cancelling response of '{e.Response}'", "Result Canceled");
				e.Cancel = true;
			}
		}

		/// <summary>
		/// Displays contextual help.
		/// </summary>
		/// <param name="context">The context to be used when determining the help to be provided.</param>
		private void ShowContextualHelp(object context) {
			ThemedMessageBox.Show($"Here is where you would show help for context: {context}.", "Help");
		}

		/// <summary>
		/// Shows a user prompt as a modal dialog.
		/// </summary>
		/// <param name="userPromptControl">The control which defines the prompt to be displayed.</param>
		/// <param name="title">The title of the window. When <c>null</c>, a default title may be assigned.</param>
		/// <param name="initializeAction">An optional action which will be called to initialize the <see cref="UserPromptWindow"/> before it is displayed.</param>
		/// <returns>One of the <see cref="UserPromptStandardResult"/> values indicating the user's response to the prompt.</returns>
		private UserPromptStandardResult ShowDialog(UserPromptControl userPromptControl, bool displayResult = false, string title = "Actipro WPF Controls", Action<UserPromptWindow> initializeAction = null) {
			if (double.IsNaN(userPromptControl.Width)) {
				// This QuickStart uses a width of 500 for prompts only to be consistent with how the
				// prompts are configured in XAML (where each sample is the same size). In practical
				// applications setting a MaxWidth would be more appropriate so that smaller messages
				// are not unnecessarily wide.
				userPromptControl.Width = 500;
			}

			// Show the prompt and record the result
			Window owner = null; // Use default
			var result = UserPromptWindow.ShowDialog(userPromptControl, title, owner, initializeAction);

			if (displayResult) {
				// Notify the user of the reponse
				ThemedMessageBox.Show($"The following result was selected:  {result}", "Result");
			}

			return result;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the command used for displaying contextual help.
		/// </summary>
		public ICommand ContextualHelpCommand {
			get {
				if (contextualHelpCommand == null) {
					// Configure the command used for contextual help
					contextualHelpCommand = new DelegateCommand<object>(p => ShowContextualHelp(p));
				}
				return contextualHelpCommand;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// DIALOG SAMPLE PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when a button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSampleShowDialogButtonStylesClick(object sender, RoutedEventArgs e) {
			//
			// SAMPLE: Use any style for buttons
			//

			// Initialize the prompt
			var userPromptControl = new UserPromptControl() {
				Header = "Use any style for buttons.",
				Content = "This sample has applied a custom style to buttons that changes the overall shape and gives the Yes and No buttons a distinctive color scheme with non-default labels.",
				StandardButtons = UserPromptStandardButtons.YesNo,
				ButtonStyle=(Style)FindResource("CustomUserPromptButtonStyle"),
			};

			ShowDialog(userPromptControl, displayResult: true);
		}

		/// <summary>
		/// Occurs when a button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSampleShowDialogCancelResponseClick(object sender, RoutedEventArgs e) {
			//
			// SAMPLE: Cancel response
			//

			// Initialize the prompt
			var userPromptControl = new UserPromptControl() {
				Header = "Programmatically cancel the response",
				Content = "An event is raised when a response is triggered for a prompt. The result can be changed or set to NULL to cancel the response.",
				CheckBoxContent = "Check to cancel the response",
				IsChecked = true,
				StandardButtons = UserPromptStandardButtons.YesNo,
			};
			userPromptControl.Responding += OnUserPromptControlRespondingCancelWhenVerifieid;

			ShowDialog(userPromptControl, displayResult: true);
		}

		/// <summary>
		/// Occurs when a button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSampleShowDialogCopyDetailsToClipboardClick(object sender, RoutedEventArgs e) {
			//
			// SAMPLE: Copy to clipboard
			//

			// Initialize the prompt
			var userPromptControl = new UserPromptControl() {
				Header = "Copy details to the clipboard",
				Content = "When displayed as a dialog, the Copy command (CTRL+C) can be invoked to copy the details of the prompt to the clipboard. This sample demonstrates the clipboard functionality and how to customize the text that is placed on the clipboard for various UI elements.",
				Footer = "Click 'Show Sample as Dialog' button and then press 'CTRL+C'",
				FooterClipboardText = "The 'Content' property is auto-converted to clipboard text, but the footer is configured to use this custom text instead.",
				FooterImageSource = ImageProvider.Default.GetImageSource(SharedImageSourceKeys.Question),
				ExpandedInformationCollapsedHeaderText = "Show more",
				ExpandedInformationExpandedHeaderText = "Show less",
				ExpandedInformationContent = "Clipboard text can be customized for 'Header', 'Content', 'Footer', 'ButtonItems', 'ExpandedInformationContent', and 'CheckBoxContent'.",
				ExpandedInformationContentClipboardText = "Use the 'HeaderClipboardText', 'ContentClipboardText', 'FooterClipboardText', 'ButtonItemsClipboardText', 'ExpandedInformationContentClipboardText', and 'CheckBoxContentClipboardText' properties to explicitly set clipboard text.",
				IsExpanded = true,
				CheckBoxContent= "This checked state is reflected on the clipboard",
				IsChecked = true,
				StandardButtons = UserPromptStandardButtons.YesNoCancel,
			};

			ShowDialog(userPromptControl, title: "Copy to Clipboard");
		}

		/// <summary>
		/// Occurs when a button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSampleShowDialogCustomAppearanceClick(object sender, RoutedEventArgs e) {
			//
			// SAMPLE: Customize the appearance of a prompt based on the type of message displayed
			//

			// Determine which image is used based on the button that was clicked
			UserPromptStandardImage image;
			if (sender == customizedAppearanceInformationButton)
				image = UserPromptStandardImage.Information;
			else if (sender == customizedAppearanceQuestionButton)
				image = UserPromptStandardImage.Question;
			else if (sender == customizedAppearanceWarningButton)
				image = UserPromptStandardImage.Warning;
			else if (sender == customizedAppearanceErrorButton)
				image = UserPromptStandardImage.Error;
			else
				throw new NotImplementedException();

			// Initialize the prompt
			var userPromptControl = new UserPromptControl() {
				Header = $"Themed {image.ToString().ToLower()} message",
				Content = $"The color scheme for this prompt has been adjusted to further emphasize the type of message based on the image used.",
				StandardStatusImage = image,
				StandardButtons = UserPromptStandardButtons.OKCancel,
			};

			// Customize the appearance of UserPromptControl
			ApplyImageBasedAppearance(userPromptControl);

			// Define callback to customize the UserPromptWindow before display
			var customizeWindow = (Action<UserPromptWindow>)((window) => {
				// Override theme assets for the window title bar foreground to always use colors that match the custom theme (even when in a dark theme)
				window.Resources.Add(AssetResourceKeys.AlternateWindowTitleBarForegroundActiveBrushKey, new SolidColorBrush(Color.FromRgb(17, 17, 17)));
				window.Resources.Add(AssetResourceKeys.AlternateWindowTitleBarForegroundInactiveBrushKey, new SolidColorBrush(Color.FromRgb(112, 113, 113)));
			});

			ShowDialog(userPromptControl, false, "Custom Theme Prompt", customizeWindow);
		}

		/// <summary>
		/// Occurs when a button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSampleShowDialogCustomButtonCommandsClick(object sender, RoutedEventArgs e) {
			//
			// SAMPLE: Each button can have its own command
			//

			// Define the custom command to be associated with each button
			var command = new ConfirmationCommand();

			// Define "Yes" button
			var yesButton = new Button() {
				Command = command,
				Content = "_Yes"
			};
			yesButton.CommandParameter = yesButton;
			UserPromptControl.SetButtonResult(yesButton, UserPromptStandardResult.Yes);

			// Define "No" button
			var noButton = new Button() {
				Command = command,
				Content = "_No"
			};
			noButton.CommandParameter = noButton;
			UserPromptControl.SetButtonResult(noButton, UserPromptStandardResult.No);

			// Initialize the prompt
			var userPromptControl = new UserPromptControl() {
				Header = "Each button can have its own command.",
				Content = "The default command for a button will notify the UserPromptControl of the response for that button, but you can define your own command instead. This sample demonstrates how to define custom commands by associating each button with a command that will confirm the response before submitting it.",
				ButtonItems = new Button[] {
					yesButton,
					noButton
				},
			};

			ShowDialog(userPromptControl, displayResult: true);
		}

		/// <summary>
		/// Occurs when a button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSampleShowDialogCustomButtonContentClick(object sender, RoutedEventArgs e) {
			//
			// SAMPLE: Use any content for buttons
			//

			var imageSource = new BitmapImage(new Uri("pack://application:,,,/SampleBrowser;component/Images/Icons/Save16.png"));
			var userPromptControl = new UserPromptControl() {
				Header = "Full support for custom button content.",
				Content = "Buttons can have any content, including images. This sample shows images used as content and demonstrates changing the horizontal alignment of all buttons from right (default) to center.",
				ButtonItems = new Button[] {
					CreateButton(UserPromptStandardResult.CustomButton, "_Left Image", imageSource, HorizontalAlignment.Left),
					CreateButton(UserPromptStandardResult.CustomButton, "_Right Image", imageSource, HorizontalAlignment.Right, isDefault: true),
					CreateButton(UserPromptStandardResult.CustomButton, "_Center Image", imageSource, HorizontalAlignment.Center),
				},
				ButtonItemsHorizontalAlignment = HorizontalAlignment.Center,
			};
			ShowDialog(userPromptControl);
		}

		/// <summary>
		/// Occurs when a button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSampleShowDialogCustomFooterContentClick(object sender, RoutedEventArgs e) {
			//
			// SAMPLE: Customize the footer content
			//

			// Initialize the prompt
			var userPromptControl = new UserPromptControl() {
				Header = "Customize the footer content",
				Content = "A footer can be used to provide additional context for a prompt. This sample demonstrates using a hyperlink to provide contextual help.",
				FooterImageSource = ImageProvider.Default.GetImageSource(SharedImageSourceKeys.Question),
				StandardButtons = UserPromptStandardButtons.OK,
			};

			// Build the footer
			var hyperlink = new Hyperlink(new Run("here"));
			hyperlink.Click += OnGenericHyperlinkClick;
			userPromptControl.Footer = new TextBlock() {
				Inlines = {
					new Run("Click "),
					hyperlink,
					new Run(" for more information")
				}
			};

			ShowDialog(userPromptControl);
		}

		/// <summary>
		/// Occurs when a button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSampleShowDialogCustomHeaderAndContentClick(object sender, RoutedEventArgs e) {
			//
			// SAMPLE: Customize the header and content
			//

			// Initialize the prompt
			var userPromptControl = new UserPromptControl() {
				Header = "Exporting Project (Sample Project)",
				HeaderForeground = new SolidColorBrush(Colors.White),
				StatusImageSource = new BitmapImage(new Uri("pack://application:,,,/SampleBrowser;component/Images/Icons/Save32.png")),
				StandardButtons = UserPromptStandardButtons.Cancel,
			};

			// Setting any header background will align the status icon and header content
			userPromptControl.HeaderBackground = new LinearGradientBrush(
				startColor: (Color)ColorConverter.ConvertFromString("#094C75"),
				endColor: (Color)ColorConverter.ConvertFromString("#066F5C"),
				angle: 0d);

			// Build the content
			userPromptControl.Content = new StackPanel() {
				Children = {
					new TextBlock() {
						Inlines = {
							new Run("to "),
							new Run("Project Templates") { FontWeight = FontWeights.Bold },
							new Run(@" (C:\Templates\ProjectTemplates)"),
						}
					},
					new TextBlock() {
						Text = "Estimated time remaining: 1 minutes",
						Margin = new Thickness(0, 2, 0, 2)
					},
					new AnimatedProgressBar() {
						Margin = new Thickness(0, 5, 0, 0),
						Minimum = 0,
						Maximum = 100,
						Value = 25,
						Height = 20,
						State = OperationState.Normal
					},
				}
			};

			ShowDialog(userPromptControl);
		}

		/// <summary>
		/// Occurs when a button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSampleShowDialogExpandedInformationErrorClick(object sender, RoutedEventArgs e) {
			//
			// SAMPLE: Expanded information error
			//

			// NOTE: This sample utilizes a helper class defined within this sample project to
			//		 demonstrate a common approach for making it easy to utilize even complex
			//		 prompts from anywhere in an application.

			try {
				// Create an exception
				throw new InvalidOperationException("This exception thrown to demonstrate the exception prompt.");
			}
			catch (Exception ex) {
				// Show the prompt
				UserPromptHelper.ShowException(ex, "Error processing request.");
			}

		}

		/// <summary>
		/// Occurs when a button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSampleShowDialogExpandedInformationInContentClick(object sender, RoutedEventArgs e) {
			//
			// SAMPLE: Use expanded information to toggle details directly within content
			//

			// Initialize the prompt
			var userPromptControl = new UserPromptControl() {
				Header = "Overwrite file?",
				StandardButtons = UserPromptStandardButtons.YesNoCancel,
				DefaultResult = UserPromptStandardResult.Cancel,
				StandardStatusImage = UserPromptStandardImage.Question,
				ExpandedInformationCollapsedHeaderText = "Show details",
				ExpandedInformationExpandedHeaderText = "Hide details",
			};

			// Build the details that will only be visible when expanded
			var animatedExpander = new AnimatedExpanderDecorator() {
				Child = new StackPanel() {
					Children = {
						new TextBlock() {
							Text = "Original File:",
							FontWeight = FontWeights.Bold,
							Margin = new Thickness(0, 10, 0, 5),
						},
						new StackPanel() {
							Margin = new Thickness(10, 0, 0, 0),
							Children = {
								new TextBlock() { Text = "File size: 88 MB" },
								new TextBlock() { Text = "Last modified: October 26, 1985" },
							}
						},
						new TextBlock() {
							Text = "Replace With:",
							FontWeight = FontWeights.Bold,
							Margin = new Thickness(0, 10, 0, 5),
						},
						new StackPanel() {
							Margin = new Thickness(10, 0, 0, 0),
							Children = {
								new TextBlock() { Text = "File size: 1.21 GB" },
								new TextBlock() { Text = "Last modified: October 21, 2015" },
							}
						},
					}
				}				
			};
			animatedExpander.SetBinding(AnimatedExpanderDecorator.IsExpandedProperty,
				new Binding(nameof(UserPromptControl.IsExpanded)) { Source = userPromptControl });
			animatedExpander.SetBinding(AnimatedExpanderDecorator.IsAnimationEnabledProperty,
				new Binding(nameof(UserPromptControl.IsAnimationEnabled)) { Source = userPromptControl });

			// Build the content
			userPromptControl.Content = new StackPanel() {
				Children = {
					new TextBlock() {
						TextWrapping = TextWrapping.Wrap,
						Inlines = {
							new Run("A file named "),
							new Run("DeLorean.txt") { FontStyle = FontStyles.Italic},
							new Run(" already exists in this location. Do you want to overwrite the file?"),
						}
					},
					animatedExpander,
				}
			};

			ShowDialog(userPromptControl);
		}

		/// <summary>
		/// Occurs when a button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSampleShowDialogHelpButtonClick(object sender, RoutedEventArgs e) {
			//
			// SAMPLE: Built-in support for a 'Help' button
			//

			// Initialize the prompt
			var userPromptControl = new UserPromptControl() {
				Header = "Built-in support for a 'Help' button",
				Content = "A built-in 'Help' button can be displayed prominently in the button tray that, when invoked, can handle displaying contextual help without closing the dialog.\r\n\r\nThe 'Help' button is not supported by the WPF MessageBox API, but is supported by Windows Forms and the Win32 MessageBox API's.",
				StandardButtons = UserPromptStandardButtons.OK | UserPromptStandardButtons.Help,
				HelpCommand = this.ContextualHelpCommand,
				HelpCommandParameter = "SampleIntegratedHelp",
			};

			ShowDialog(userPromptControl, displayResult: true);
		}

		/// <summary>
		/// Occurs when a button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSampleShowDialogImageAdaptionClick(object sender, RoutedEventArgs e) {
			//
			// SAMPLE: Images can automatically adapt to dark themes or keep the original image
			//

			// Initialize the images
			var statusImageSource = new BitmapImage(new Uri("pack://application:,,,/SampleBrowser;component/Images/Icons/Actipro.ico"));
			ImageProvider.SetCanAdapt(statusImageSource, false); // Keep original colors
			var footerImageSource = new BitmapImage(new Uri("pack://application:,,,/SampleBrowser;component/Images/Icons/Help16.png"));
			ImageProvider.SetCanAdapt(footerImageSource, true); // Allow to adjust for dark themes

			// Initialize the prompt
			var userPromptControl = new UserPromptControl() {
				Header = "Control which images adapt to dark theme.",
				Content = @"This sample demonstrates how the ImageProvider can be used to control if images adapt to the dark theme. Adaption is turned on, by default, in this sample application, but images can individually opt in or out of adaption as desired.

The content icon is configured to not change in dark themes, but the footer icon will.",
				StandardButtons = UserPromptStandardButtons.OK,
				StatusImageSource = statusImageSource,
				FooterImageSource = footerImageSource,
			};

			// Build the footer content
			var hyperlink = new Hyperlink(new Run("here"));
			hyperlink.SetValue(Hyperlink.CommandProperty, ((ApplicationViewModel)this.DataContext).NavigateViewToItemInfoCommand);
			hyperlink.SetValue(Hyperlink.CommandParameterProperty, "https://ActiproSoftware/ProductSamples/SharedSamples/QuickStart/DynamicImageIntro/MainControl");
			userPromptControl.Footer = new TextBlock() {
				Inlines = {
					new Run("Click "),
					hyperlink,
					new Run(" for more samples working with DynamicImage and ImageProvider.")
				}
			};

			ShowDialog(userPromptControl);
		}

		/// <summary>
		/// Occurs when a button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSampleShowDialogStandardCheckBoxClick(object sender, RoutedEventArgs e) {
			//
			// SAMPLE: Standard checkbox
			//

			// Check if the user has suppressed the message
			if (standardCheckBoxSampleIsChecked) {
				var overrideResult = ThemedMessageBox.Show("You indicated you didn't want to see these messages. Show message anyway?", "Show Message", MessageBoxButton.YesNo, MessageBoxImage.Question);
				if (overrideResult == MessageBoxResult.No)
					return;
			}

			// Initialize the prompt
			var userPromptControl = new UserPromptControl() {
				Header = "Use the checkbox to allow user feedback.",
				Content = "A common scenario is to allow the user to opt out of future prompts.",
				CheckBoxContent = "_Stop showing messages like this",
				StandardButtons = UserPromptStandardButtons.OK,
				IsChecked = standardCheckBoxSampleIsChecked,
			};

			// Display the prompt
			var result = ShowDialog(userPromptControl);

			if (userPromptControl.IsChecked) {
				// Flag that the message should not be displayed again
				standardCheckBoxSampleIsChecked = userPromptControl.IsChecked;
				ThemedMessageBox.Show($"You selected '{result}' and selected not to show this message again.", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			else {
				ThemedMessageBox.Show($"You selected '{result}' and will continue to see this message.", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		/// <summary>
		/// Occurs when a button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSampleShowDialogCheckBoxCustomContentClick(object sender, RoutedEventArgs e) {
			//
			// SAMPLE: Checkbox custom content
			//

			// Initialize the prompt
			var userPromptControl = new UserPromptControl() {
				Header = "The checkbox can support custom content.",
				Content = "The content of the checkbox can be customized. Here a right-aligned hyperlink has been added to demonstrate an option to open a settings dialog that configures which messages are displayed.",
				StandardButtons = UserPromptStandardButtons.OK,
			};

			// Build the checkbox content
			var hyperlink = new Hyperlink(new Run("Open message settings"));
			hyperlink.Click += OnOpenMessageSettingsHyperlinkClick;
			var hyperlinkTextBlock = new TextBlock(hyperlink);
			hyperlinkTextBlock.SetValue(DockPanel.DockProperty, Dock.Right);
			userPromptControl.CheckBoxContent = new DockPanel() {
				Children = {
					hyperlinkTextBlock,
					new AccessText() { Text = "_Stop showing messages like this"},
				}
			};

			ShowDialog(userPromptControl);
		}

	}

}