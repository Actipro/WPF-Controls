using ActiproSoftware.Extensions;
using ActiproSoftware.ProductSamples.SharedSamples.Common;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Extensions;
using ActiproSoftware.Windows.Input;
using ActiproSoftware.Windows.Media;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

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
		/// Configures a default <see cref="UserPromptBuilder"/> specifically for use with these samples.
		/// </summary>
		/// <param name="displayResult"><c>true</c> to display the result after it is selected; otherwise <c>false</c> and the result will not be displayed.</param>
		/// <returns>A <see cref="UserPromptBuilder"/>.</returns>
		private static UserPromptBuilder ConfigureUserPrompt(bool displayResult = false) {
			return UserPromptBuilder.Configure()
				.AfterShow((builder, result) => {
					if (displayResult) {
						// Notify the user of the response
						ThemedMessageBox.Show($"The following result was selected:  {result}", "Result");
					}
				});
		}

		/// <summary>
		/// Occurs when a hyperlink is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnGenericHyperlinkClick(object sender, RoutedEventArgs e) {
			ThemedMessageBox.Show("Use this event handler to respond to the hyperlink.", "Hyperlink Clicked");
		}

		/// <summary>
		/// Occurs when a hyperlink is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnOpenMessageSettingsHyperlinkClick(object sender, RoutedEventArgs e) {
			ThemedMessageBox.Show("Use this event handler to open a Settings dialog.", "Settings");
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
				result = ThemedMessageBox.Show(text, caption, button, image, defaultResult, builder => builder.WithHelpCommand(helpAction));

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
		private void OnUserPromptControlRespondingCancelWhenVerified(object sender, UserPromptResponseEventArgs e) {
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
		private static void ShowContextualHelp(object context) {
			ThemedMessageBox.Show($"Here is where you would show help for context: {context}.", "Help");
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

			// Do not assign button label or the explicit value (e.g., "Yes" or "No")
			// will prevent the Style from setting the button content.
			ConfigureUserPrompt(displayResult: true)
				.WithHeaderContent("Use any style for buttons.")
				.WithContent("This sample has applied a custom style to buttons that changes the overall shape and gives the Yes and No buttons a distinctive color scheme with non-default labels.")
				.WithStandardButtons(UserPromptStandardButtons.YesNo)
				.WithButtonStyle(FindResource("CustomUserPromptButtonStyle") as Style)
				.Show();
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

			ConfigureUserPrompt(displayResult: true)
				.WithHeaderContent("Programmatically cancel the response")
				.WithContent("An event is raised when a response is triggered for a prompt. The result can be changed or set to NULL to cancel the response.")
				.WithCheckBox("Check to cancel the response", isChecked: true)
				.WithStandardButtons(UserPromptStandardButtons.YesNo)
				.OnResponding((builder, args) => {
					if ((builder?.Instance is UserPromptControl userPromptControl) && (userPromptControl.IsChecked)) {
						// Cancel the response
						ThemedMessageBox.Show($"Cancelling response of '{args.Response}'", "Result Canceled");
						args.Cancel = true;
					}
				})
				.Show();
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

			ConfigureUserPrompt()
				.WithTitle("Copy to Clipboard")
				.WithHeaderContent("Copy details to the clipboard")
				.WithContent("When displayed as a dialog, the Copy command (CTRL+C on Windows) can be invoked to copy the details of the prompt to the clipboard. This sample demonstrates the clipboard functionality and how to customize the text that is placed on the clipboard for various UI elements.")
				.WithContentClipboardText("This content will be placed on the clipboard instead of the message.")
				.WithCheckBox("This checked state is reflected on the clipboard", isChecked: true)
				.WithFooterContent("Click 'Show Sample as Dialog' button and then press 'CTRL+C'")
				.WithFooterClipboardText($"The '{nameof(UserPromptControl.Footer)}' property is auto-converted to clipboard text, but the footer of this instance is configured to use this custom text instead.")
				.WithFooterImage(ImageProvider.Default.GetImageSource(SharedImageSourceKeys.Question))
				.WithExpandedInformationHeaderText("Show more", "Show less")
				.WithExpandedInformationContent($"Clipboard text can be customized for '{nameof(UserPromptControl.Header)}', '{nameof(UserPromptControl.Content)}', '{nameof(UserPromptControl.Footer)}', '{nameof(UserPromptControl.ButtonItems)}', '{nameof(UserPromptControl.ExpandedInformationContent)}', and '{nameof(UserPromptControl.CheckBoxContent)}'.")
				.WithExpandedInformationContentClipboardText($"Use the '{nameof(UserPromptControl.HeaderClipboardText)}', '{nameof(UserPromptControl.ContentClipboardText)}', '{nameof(UserPromptControl.FooterClipboardText)}', '{nameof(UserPromptControl.ButtonItemsClipboardText)}', '{nameof(UserPromptControl.ExpandedInformationContentClipboardText)}', and '{nameof(UserPromptControl.CheckBoxContentClipboardText)}' properties to explicitly set clipboard text.")
				.WithIsExpanded(true)
				.WithButton(UserPromptStandardResult.Yes)
				.WithButton(UserPromptStandardResult.No)
				.WithButton(_ => _
					.WithResult(UserPromptStandardResult.Cancel)
					.WithContentClipboardText("Quit")
				)
				.Show();
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

			ConfigureUserPrompt()
				.WithTitle("Custom Theme Prompt")
				.WithHeaderContent($"Themed {image.ToString().ToLower()} message")
				.WithContent($"The color scheme for this prompt has been adjusted to further emphasize the type of message based on the image used.")
				.WithStatusImage(image)
				.WithStandardButtons(UserPromptStandardButtons.OKCancel)
				.WithStatusImageTheme() // <-- Custom extension method to tint resources based on the status icon (e.g., error messages are red)
				.Show();
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

			ConfigureUserPrompt(displayResult: true)
				.WithHeaderContent("Each button can have its own command.")
				.WithContent("The default command for a button will notify the UserPromptControl of the response for that button, but you can define your own command instead. This sample demonstrates how to define custom commands by associating each button with a command that will confirm the response before submitting it.")
				.WithButton(UserPromptStandardResult.Yes, afterInitialize: builder => builder.WithCommand(command, builder.Instance))
				.WithButton(UserPromptStandardResult.No, afterInitialize: builder => builder.WithCommand(command, builder.Instance))
				.Show();
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

			var imageSource = ImageLoader.GetIcon("Save16.png");

			// NOTE: This sample utilizes an extension method on UserPromptButtonBuilder to
			//		 easily define an icon for use with a button.  Extension methods are a great
			//		 way to make common configurations reusable.


			ConfigureUserPrompt(displayResult: true)
				.WithHeaderContent("Full support for custom button content.")
				.WithContent("Buttons can have any content, including images. This sample shows images used as content and demonstrates changing the horizontal alignment of all buttons from right (default) to center.")
				.WithButton(_ => _
					.WithResult(UserPromptStandardResult.CustomButton + 1)
					.WithContent("_Left Image")
					.WithIcon(imageSource, HorizontalAlignment.Left)
				)
				.WithButton(_ => _
					.WithResult(UserPromptStandardResult.CustomButton + 2)
					.WithContent("_Right Image")
					.WithIcon(imageSource, HorizontalAlignment.Right)
					.UseAsDefaultResult()
				)
				.WithButton(_ => _
					.WithResult(UserPromptStandardResult.CustomButton + 3)
					.WithContent("_Center Image")
					.WithIcon(imageSource, HorizontalAlignment.Center)
				)
				.WithButtonItemsHorizontalAlignment(HorizontalAlignment.Center)
				.Show();
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

			// Build the footer hyperlink
			var hyperlink = new Hyperlink(new Run("here"));
			hyperlink.Click += OnGenericHyperlinkClick;

			ConfigureUserPrompt()
				.WithHeaderContent("Customize the footer content")
				.WithContent("A footer can be used to provide additional context for a prompt. This sample demonstrates using a hyperlink to provide contextual help.")
				.WithFooterContent(new TextBlock() {
					Inlines = {
						new Run("Click "),
						hyperlink,
						new Run(" for more information")
					}
				})
				.WithFooterImage(ImageProvider.Default.GetImageSource(SharedImageSourceKeys.Question))
				.WithButton(UserPromptStandardResult.OK)
				.Show();
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

			var statusText = new TextBlock() {
				Text = "Estimated time remaining:",
				Margin = new Thickness(0, 2, 0, 2)
			};

			var progressBar = new AnimatedProgressBar() {
				Margin = new Thickness(0, 5, 0, 0),
				Minimum = 0,
				Maximum = 100,
				Value = 0,
				Height = 20,
				State = OperationState.Normal
			};

			ConfigureUserPrompt(displayResult: true)
				// Setting any header background will align the status icon and header content
				.WithHeaderContent("Exporting Project (Sample Project)")
				.WithHeaderForeground(Colors.White)
				.WithHeaderBackground(
					new LinearGradientBrush(
						startColor: (Color)ColorConverter.ConvertFromString("#094C75"),
						endColor: (Color)ColorConverter.ConvertFromString("#066F5C"),
						angle: 0d)
				)
				.WithStatusImage(ImageLoader.GetIcon("Save32.png"))
				.WithStandardButtons(UserPromptStandardButtons.Cancel)
				.WithContent(new StackPanel() {
					Children = {
						new TextBlock() {
							Inlines = {
								new Run("to "),
								new Run("Project Templates") { FontWeight = FontWeights.Bold },
								new Run(@" (C:\Templates\ProjectTemplates)"),
							}
						},
						statusText,
						progressBar
					}
				})
				.WithCheckBoxContent("Check this box to simulate an exception")
				.WithWindowStartupLocation(WindowStartupLocation.CenterOwner)
				.WithAutoSize(true, minimumWidth: 400)
				.BeforeShow(builder => {
					// Do background work here while the dialog is shown
					Task.Run(() => {
						var totalTime = TimeSpan.FromSeconds(10);
						var startTaskTime = DateTime.Now;
						var isCompleted = false;
						var throwException = false;
						try {
							do {
								if (throwException)
									throw new ApplicationException("An error was encountered during export.");
								Thread.Sleep(100);
								var elapsedTime = DateTime.Now - startTaskTime;
								var remainingTime = totalTime - elapsedTime;
								var percentageComplete = ((elapsedTime.TotalSeconds / Math.Max(1, totalTime.TotalSeconds)) * 100);
								Dispatcher.InvokeAsync(() => {
									// Must interact with UI controls on the UI thread that owns them
									throwException = builder.Instance?.IsChecked == true;
									if (!throwException) {
										progressBar.Value = percentageComplete;
										statusText.Text = $"Estimated time remaining: {remainingTime.TotalSeconds.Round(RoundMode.Ceiling)} seconds";
										isCompleted = (progressBar.IsCompleted || (builder.Instance?.Result is not null));
									}
								});
							} while (!isCompleted);
							if (isCompleted) {
								// Assign a user prompt result to automatically close the prompt once the work is complete
								Dispatcher.InvokeAsync(() => {
									if ((builder.Instance is not null) && (builder.Instance.Result is null))
										builder.Instance.Result = builder.Instance.DefaultResult;
								});
							}
						}
						catch (Exception ex) {
							// Report the error and show the progress bar in an error state
							Dispatcher.InvokeAsync(() => {
								statusText.Text = "Error: " + ex.Message;
								progressBar.State = OperationState.Error;
							});
						}
					});
				})
				.Show();
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

			try {
				// Create an exception
				throw new InvalidOperationException("This exception thrown to demonstrate the exception prompt.");
			}
			catch (Exception ex) {
				// Show the prompt

				// NOTE: This sample utilizes a pre-defined extension method on UserPromptBuilder to easily
				//		 configure a user prompt for showing an exception.  Extension methods on
				//		 the UserPromptBuilder class are a great way to make common configurations
				//		 reusable.

				UserPromptBuilder.Configure()
					.ForException(ex, "Error processing request.")
					.Show();
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

			ConfigureUserPrompt()
				.WithHeaderContent("Overwrite file?")
				.WithStandardButtons(UserPromptStandardButtons.YesNoCancel)
				.WithDefaultResult(UserPromptStandardResult.Cancel)
				.WithStatusImage(UserPromptStandardImage.Question)
				.WithExpandedInformation("Show details", "Hide details", expandedContent: null)
				.WithAutoSize(true)
				.WithContent(instance => {
					// The 'WithContent' method has an overload that allows the UserPromptControl
					// being built to be passed into this delegate. This is necessary here
					// since some properties of the content have bindings to the UserPromptControl.

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
						},
					};
					animatedExpander.BindToProperty(AnimatedExpanderDecorator.IsExpandedProperty, instance, UserPromptControl.IsExpandedProperty, BindingMode.TwoWay);
					animatedExpander.BindToProperty(AnimatedExpanderDecorator.IsAnimationEnabledProperty, instance, UserPromptControl.IsAnimationEnabledProperty, BindingMode.OneWay);

					// Build the content
					return new StackPanel() {
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
				})
				.Show();
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

			ConfigureUserPrompt(displayResult: true)
				.WithHeaderContent("Built-in support for a 'Help' button")
				.WithContent("A built-in 'Help' button can be displayed prominently in the button tray that, when invoked, can handle displaying contextual help without closing the dialog.\r\n\r\nThe 'Help' button is not supported by the WPF MessageBox API, but is supported by Windows Forms and the Win32 MessageBox API's.")
				.WithStandardButtons(UserPromptStandardButtons.OK)
				.WithHelpCommand(this.ContextualHelpCommand, "SampleIntegratedHelp")
				.Show();
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
			var statusImageSource = ImageLoader.GetIcon("Actipro.ico", freeze: false);
			ImageProvider.SetCanAdapt(statusImageSource, false); // Keep original colors
			statusImageSource.Freeze();
			var footerImageSource = ImageLoader.GetIcon("Help16.png", freeze: false);
			ImageProvider.SetCanAdapt(footerImageSource, true); // Allow to adjust for dark themes
			footerImageSource.Freeze();

			ConfigureUserPrompt()
				.WithHeaderContent("Control which images adapt to dark theme.")
				.WithContent(@"This sample demonstrates how the ImageProvider can be used to control if images adapt to the dark theme. Adaption is turned on, by default, in this sample application, but images can individually opt in or out of adaption as desired.

The content icon is configured to not change in dark themes, but the footer icon will.")
				.WithStandardButtons(UserPromptStandardButtons.OK)
				.WithStatusImage(statusImageSource)
				.WithFooterImage(footerImageSource)
				.WithFooterContent(() => {
					var hyperlink = new Hyperlink(new Run("here"));
					hyperlink.SetValue(Hyperlink.CommandProperty, ((ApplicationViewModel)this.DataContext).NavigateViewToItemInfoCommand);
					hyperlink.SetValue(Hyperlink.CommandParameterProperty, "https://ActiproSoftware/ProductSamples/SharedSamples/QuickStart/DynamicImageIntro/MainControl");

					return new TextBlock() {
						Inlines = {
							new Run("Click "),
							hyperlink,
							new Run(" for more samples working with DynamicImage and ImageProvider.")
						}
					};
				})
				.Show();
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

			var result = ConfigureUserPrompt(displayResult: false)
				.WithHeaderContent("Use the checkbox to allow user feedback.")
				.WithContent("A common scenario is to allow the user to opt out of future prompts.")
				.WithCheckBoxContent("_Stop showing messages like this")
				.WithIsChecked(
					getter: () => standardCheckBoxSampleIsChecked,
					setter: (value) => standardCheckBoxSampleIsChecked = value
				)
				.WithStandardButtons(UserPromptStandardButtons.OK)
				.AfterShow((builder, result) => {
					if (standardCheckBoxSampleIsChecked)
						ThemedMessageBox.Show($"You selected '{result}' and elected not to show this message again.", "Result");
					else
						ThemedMessageBox.Show($"You selected '{result}' and will continue to see this message.", "Result");
				})
				.Show();
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

			ConfigureUserPrompt()
				.WithHeaderContent("The checkbox can support custom content.")
				.WithContent("The content of the checkbox can be customized. Here a right-aligned hyperlink has been added to demonstrate an option to open a settings dialog that configures which messages are displayed.")
				.WithStandardButtons(UserPromptStandardButtons.OK)
				.WithCheckBoxContent(() => {
					// Build the checkbox content
					var hyperlink = new Hyperlink(new Run("Open message settings"));
					hyperlink.Click += OnOpenMessageSettingsHyperlinkClick;

					var hyperlinkTextBlock = new TextBlock(hyperlink);
					hyperlinkTextBlock.SetValue(DockPanel.DockProperty, Dock.Right);
					
					return new DockPanel() {
						Children = {
							hyperlinkTextBlock,
							new AccessText() { Text = "_Stop showing messages like this"},
						}
					};
				})
				.Show();
		}

	}

}