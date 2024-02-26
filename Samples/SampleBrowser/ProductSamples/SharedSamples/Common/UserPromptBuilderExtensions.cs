using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Themes;
using ActiproSoftware.Windows.Themes.Generation;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.SharedSamples.Common {

	/// <summary>
	/// Defines extension methods for <see cref="UserPromptBuilder"/>.
	/// </summary>
	public static class UserPromptBuilderExtensions {

		/// <summary>
		/// Configures the builder to customize resources on the <see cref="UserPromptControl"/> with a <see cref="Hue"/>
		/// that corresponds to the intent of the given status image (e.g., red hue for error messages).
		/// </summary>
		/// <param name="builder">The builder to configure.</param>
		/// <param name="statusImage">The status image whose intent will be used to determine the <see cref="Hue"/>.</param>
		/// <returns>The builder, for use with method-chaining.</returns>
		/// <exception cref="InvalidOperationException" />
		public static UserPromptBuilder WithStatusImageTheme(this UserPromptBuilder builder, UserPromptStandardImage? statusImage = null) {
			Brush windowBorderActiveBrush = null;
			return builder.AfterBuild(_ => {

				var userPromptControl = builder.Instance!;

				statusImage ??= userPromptControl.StandardStatusImage;

				ColorFamilyName colorFamilyName;
				switch (statusImage) {
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

				// Initialize brushes to be used for window resources
				windowBorderActiveBrush = darkerColorFamilyBrush;
			})
			.AfterInitializeWindow(window => {
				// Callback to customize the UserPromptWindow before display
				// Override theme assets for the window title bar foreground to always use colors that match the custom theme (even when in a dark theme)
				window.Resources.Add(AssetResourceKeys.AlternateWindowTitleBarForegroundActiveBrushKey, new SolidColorBrush(Color.FromRgb(17, 17, 17)));
				window.Resources.Add(AssetResourceKeys.AlternateWindowTitleBarForegroundInactiveBrushKey, new SolidColorBrush(Color.FromRgb(112, 113, 113)));
				if (windowBorderActiveBrush is not null)
					window.Resources.Add(AssetResourceKeys.WindowBorderActiveBrushKey, windowBorderActiveBrush);
			});
		}

		/// <summary>
		/// Configures the content of a <see cref="UserPromptControl"/> button instance to include an icon.
		/// </summary>
		/// <param name="builder">The builder to configure.</param>
		/// <param name="image">The icon.</param>
		/// <param name="imageAlignment">The alignment of the icon.</param>
		/// <returns>The builder, for use with method-chaining.</returns>
		/// <exception cref="NotSupportedException">Thrown for unsupported image alignment values.</exception>
		public static UserPromptButtonBuilder WithIcon(this UserPromptButtonBuilder builder, ImageSource image, HorizontalAlignment imageAlignment) {
			return builder.AfterBuild(_ => {
				if (image is not null
					&& builder.Instance is Button button) {

					var existingContent = button.Content as FrameworkElement;
					if (existingContent is null
						&& button.Content is string label) {

						existingContent = new AccessText() { Text = label };
					}

					var imageControl = new DynamicImage() {
						Source = image,
						Height = 16,
						Width = 16,
						UseLayoutRounding = true,
					};

					if (existingContent is null) {
						// No content, so image will be centered without additional content
						imageControl.HorizontalAlignment = HorizontalAlignment.Center;
						imageControl.VerticalAlignment = VerticalAlignment.Center;
						button.Content = imageControl;
					}
					else {
						// Remove existing content from button before adding as new child
						button.Content = null;

						// Arrange the image next to the content based on image alignment
						double spacing = 4;
						switch (imageAlignment) {
							case HorizontalAlignment.Left:
								imageControl.VerticalAlignment = VerticalAlignment.Center;
								imageControl.Margin = new Thickness(0, 0, spacing, 0);
								existingContent.VerticalAlignment = VerticalAlignment.Center;
								button.Content = new StackPanel {
									Orientation = Orientation.Horizontal,
									Children = { imageControl, existingContent }
								};
								break;

							case HorizontalAlignment.Right:
								imageControl.VerticalAlignment = VerticalAlignment.Center;
								imageControl.Margin = new Thickness(spacing, 0, 0, 0);
								existingContent.VerticalAlignment = VerticalAlignment.Center;
								button.Content = new StackPanel {
									Orientation = Orientation.Horizontal,
									Children = { existingContent, imageControl }
								};
								break;

							case HorizontalAlignment.Center:
								imageControl.HorizontalAlignment = HorizontalAlignment.Center;
								imageControl.Margin = new Thickness(0, 0, 0, spacing);
								existingContent.HorizontalAlignment = HorizontalAlignment.Center;
								button.Content = new StackPanel {
									Orientation = Orientation.Vertical,
									HorizontalAlignment = HorizontalAlignment.Center,
									Children = { imageControl, existingContent }
								};
								break;

							default:
								throw new NotSupportedException();
						}
					}

				}
			});
		}

	}

}
