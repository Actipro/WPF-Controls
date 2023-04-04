using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using RibbonControls = ActiproSoftware.Windows.Controls.Ribbon.Controls;
using ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives;
using ActiproSoftware.Windows.Controls.Ribbon.Input;
using ActiproSoftware.Windows.Controls.Ribbon.UI;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Themes;
using ImageLoader = ActiproSoftware.SampleBrowser.ImageLoader;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.RibbonSamples.Demo.DocumentEditor {

	/// <summary>
	/// Represents an extended <see cref="RichTextBox"/> control.
	/// </summary>
	public class RichTextBoxExtended : RichTextBox {

        private MemoryStream	previewStream;

		#region Dependency Properties
		
		/// <summary>
		/// Identifies the <see cref="DocumentUri"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="DocumentUri"/> dependency property.</value>
		public static readonly DependencyProperty DocumentUriProperty = DependencyProperty.Register("DocumentUri", typeof(Uri), typeof(RichTextBoxExtended), new FrameworkPropertyMetadata(null, OnDocumentUriPropertyValueChanged));
		
		#endregion
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the <c>RichTextBoxExtended</c> class.
		/// </summary>
		static RichTextBoxExtended() {
			AcceptsReturnProperty.OverrideMetadata(typeof(RichTextBoxExtended), new FrameworkPropertyMetadata(true));
			AcceptsTabProperty.OverrideMetadata(typeof(RichTextBoxExtended), new FrameworkPropertyMetadata(true));
			HorizontalScrollBarVisibilityProperty.OverrideMetadata(typeof(RichTextBoxExtended), new FrameworkPropertyMetadata(ScrollBarVisibility.Hidden));
			VerticalScrollBarVisibilityProperty.OverrideMetadata(typeof(RichTextBoxExtended), new FrameworkPropertyMetadata(ScrollBarVisibility.Hidden));
		}

		/// <summary>
		/// Initializes an instance of the <c>RichTextBoxExtended</c> class.
		/// </summary>
		public RichTextBoxExtended() {
			// Set appearance
			this.Background = Brushes.White;
			this.BorderBrush = Brushes.Black;
			this.BorderThickness = new Thickness(1);
			this.Foreground = Brushes.Black;
			this.Document.Background = Brushes.Transparent;
			this.Document.Foreground = this.Foreground;

			ThemeProperties.SetUseBackgroundStates(this, false);
			ThemeProperties.SetUseBorderStates(this, false);

			// Assign a custom context menu
			if (!BrowserInteropHelper.IsBrowserHosted) {
				RibbonControls.ContextMenu contextMenu = new RibbonControls.ContextMenu();
				RibbonControls.Menu menu = new RibbonControls.Menu();
				contextMenu.Items.Add(menu);
				menu.Items.Add(new RibbonControls.Button(System.Windows.Input.ApplicationCommands.Undo) { KeyTipAccessText = "U" });
				menu.Items.Add(new RibbonControls.Button(System.Windows.Input.ApplicationCommands.Redo) { KeyTipAccessText = "R" });
				menu.Items.Add(new RibbonControls.Separator());
				menu.Items.Add(new RibbonControls.Button(System.Windows.Input.ApplicationCommands.Cut) { KeyTipAccessText = "T" });
				menu.Items.Add(new RibbonControls.Button(System.Windows.Input.ApplicationCommands.Copy) { KeyTipAccessText = "C" });
				menu.Items.Add(new RibbonControls.Button(System.Windows.Input.ApplicationCommands.Paste) { KeyTipAccessText = "P" });
				this.ContextMenu = contextMenu;

				// Attach a mini-toolbar to the context menu
				contextMenu.MiniToolBar = new ActiproSoftware.ProductSamples.RibbonSamples.Common.RichTextBoxMiniToolBar();

				// Attach to the context menu opening event
				this.ContextMenuOpening += OnContextMenuOpening;
			}

			// Add command bindings
            this.CommandBindings.Add(new CommandBinding(EditingCommands.AlignCenter, null, OnAlignCenterCanExecute));
            this.CommandBindings.Add(new CommandBinding(EditingCommands.AlignJustify, null, OnAlignJustifyCanExecute));
			this.CommandBindings.Add(new CommandBinding(EditingCommands.AlignLeft, null, OnAlignLeftCanExecute));
            this.CommandBindings.Add(new CommandBinding(EditingCommands.AlignRight, null, OnAlignRightCanExecute));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.ApplyBackground, OnApplyBackgroundExecute, OnApplyBackgroundCanExecute));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.ApplyDefaultBackground, OnApplyDefaultBackgroundExecute));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.ApplyDefaultForeground, OnApplyDefaultForegroundExecute));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.ApplyForeground, OnApplyForegroundExecute, OnApplyForegroundCanExecute));
			this.CommandBindings.Add(new CommandBinding(ApplicationCommands.ClearFormatting, OnClearFormattingExecute));
			this.CommandBindings.Add(new CommandBinding(ApplicationCommands.FontFamily, OnFontFamilyExecute, OnFontFamilyCanExecute));
			this.CommandBindings.Add(new CommandBinding(ApplicationCommands.FontSize, OnFontSizeExecute, OnFontSizeCanExecute));
			this.CommandBindings.Add(new CommandBinding(EditingCommands.ToggleBold, null, OnToggleBoldCanExecute));
			this.CommandBindings.Add(new CommandBinding(EditingCommands.ToggleItalic, null, OnToggleItalicCanExecute));
			this.CommandBindings.Add(new CommandBinding(ApplicationCommands.ToggleStrikethrough, OnToggleStrikethroughExecute, OnToggleStrikethroughCanExecute));
			this.CommandBindings.Add(new CommandBinding(EditingCommands.ToggleSubscript, null, OnToggleSubscriptCanExecute));
			this.CommandBindings.Add(new CommandBinding(EditingCommands.ToggleSuperscript, null, OnToggleSuperscriptCanExecute));
			this.CommandBindings.Add(new CommandBinding(EditingCommands.ToggleUnderline, null, OnToggleUnderlineCanExecute));

		}
				
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// COMMAND HANDLERS
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> needs to determine whether it can execute.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> that contains the event data.</param>
		private void OnAlignCenterCanExecute(object sender, CanExecuteRoutedEventArgs e) {
			ICheckableCommandParameter parameter = e.Parameter as ICheckableCommandParameter;
			if (parameter != null) {
				parameter.Handled = true;
				parameter.IsChecked = this.SelectionAlignCenter;
			}
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> needs to determine whether it can execute.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> that contains the event data.</param>
		private void OnAlignJustifyCanExecute(object sender, CanExecuteRoutedEventArgs e) {
			ICheckableCommandParameter parameter = e.Parameter as ICheckableCommandParameter;
			if (parameter != null) {
				parameter.Handled = true;
				parameter.IsChecked = this.SelectionAlignJustify;
			}
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> needs to determine whether it can execute.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> that contains the event data.</param>
		private void OnAlignLeftCanExecute(object sender, CanExecuteRoutedEventArgs e) {
			ICheckableCommandParameter parameter = e.Parameter as ICheckableCommandParameter;
			if (parameter != null) {
				parameter.Handled = true;
				parameter.IsChecked = this.SelectionAlignLeft;
			}
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> needs to determine whether it can execute.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> that contains the event data.</param>
		private void OnAlignRightCanExecute(object sender, CanExecuteRoutedEventArgs e) {
			ICheckableCommandParameter parameter = e.Parameter as ICheckableCommandParameter;
			if (parameter != null) {
				parameter.Handled = true;
				parameter.IsChecked = this.SelectionAlignRight;
			}
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> needs to determine whether it can execute.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> that contains the event data.</param>
		private void OnApplyBackgroundCanExecute(object sender, CanExecuteRoutedEventArgs e) {
			BrushValueCommandParameter parameter = e.Parameter as BrushValueCommandParameter;
			if ((parameter != null) && (!this.IsPreviewModeActive)) {
				parameter.UpdatedValue = this.SelectionBackground;
				parameter.Handled = true;
			}
			e.CanExecute = true;
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void OnApplyBackgroundExecute(object sender, ExecutedRoutedEventArgs e) {
			BrushValueCommandParameter parameter = e.Parameter as BrushValueCommandParameter;
			if (parameter != null) {
				switch (parameter.Action) {
					case ValueCommandParameterAction.CancelPreview:
						this.DeactivatePreviewMode(true);
						break;
					case ValueCommandParameterAction.Commit:
						this.DeactivatePreviewMode(false);
						this.SelectionBackground = parameter.Value;
						this.UpdateApplyDefaultBackgroundSmallImageSource(parameter.Value);
						break;
					case ValueCommandParameterAction.Preview:
						this.ActivatePreviewMode();
						this.SelectionBackground = parameter.PreviewValue;
						break;
				}
			}
			else {
				this.SelectionBackground = null;
				this.UpdateApplyDefaultBackgroundSmallImageSource(null);
			}
			e.Handled = true;
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void OnApplyDefaultBackgroundExecute(object sender, ExecutedRoutedEventArgs e) {
			this.SelectionBackground = ApplicationCommands.ApplyDefaultBackground.Tag as Brush;
			e.Handled = true;
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void OnApplyDefaultForegroundExecute(object sender, ExecutedRoutedEventArgs e) {
			this.SelectionForeground = ApplicationCommands.ApplyDefaultForeground.Tag as Brush;
			e.Handled = true;
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> needs to determine whether it can execute.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> that contains the event data.</param>
		private void OnApplyForegroundCanExecute(object sender, CanExecuteRoutedEventArgs e) {
			BrushValueCommandParameter parameter = e.Parameter as BrushValueCommandParameter;
			if ((parameter != null) && (!this.IsPreviewModeActive)) {
				parameter.UpdatedValue = this.SelectionForeground;
				parameter.Handled = true;
			}
			e.CanExecute = true;
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void OnApplyForegroundExecute(object sender, ExecutedRoutedEventArgs e) {
			BrushValueCommandParameter parameter = e.Parameter as BrushValueCommandParameter;
			if (parameter != null) {
				switch (parameter.Action) {
					case ValueCommandParameterAction.CancelPreview:
						this.DeactivatePreviewMode(true);
						break;
					case ValueCommandParameterAction.Commit:
						this.DeactivatePreviewMode(false);
						this.SelectionForeground= parameter.Value;
						this.UpdateApplyDefaultForegroundSmallImageSource(parameter.Value);
						break;
					case ValueCommandParameterAction.Preview:
						this.ActivatePreviewMode();
						this.SelectionForeground = parameter.PreviewValue;
						break;
				}
			}
			else {
				this.SelectionForeground = null;
				this.UpdateApplyDefaultForegroundSmallImageSource(null);
			}
			e.Handled = true;
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void OnClearFormattingExecute(object sender, ExecutedRoutedEventArgs e) {
			this.Selection.ClearAllProperties();
			e.Handled = true;
		}
		
		/// <summary>
		/// Occurs when the <see cref="DocumentUriProperty"/> value is changed.
		/// </summary>
		/// <param name="obj">The <see cref="DependencyObject"/> whose property is changed.</param>
		/// <param name="e">A <see cref="DependencyPropertyChangedEventArgs"/> that contains the event data.</param>
		private static void OnDocumentUriPropertyValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
			RichTextBoxExtended control = (RichTextBoxExtended)obj;
			try {
				control.Document = Application.LoadComponent(control.DocumentUri) as FlowDocument;
			}
			catch {}
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> needs to determine whether it can execute.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> that contains the event data.</param>
		private void OnFontFamilyCanExecute(object sender, CanExecuteRoutedEventArgs e) {
			FontFamilyValueCommandParameter parameter = e.Parameter as FontFamilyValueCommandParameter;
			if ((parameter != null) && (!this.IsPreviewModeActive)) {
				parameter.UpdatedValue = this.SelectionFontFamily;
				parameter.Handled = true;
			}
			e.CanExecute = true;
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void OnFontFamilyExecute(object sender, ExecutedRoutedEventArgs e) {
			FontFamilyValueCommandParameter parameter = e.Parameter as FontFamilyValueCommandParameter;
			if (parameter != null) {
				if ((parameter.Value != null) && (!RibbonControls.FontFamilyComboBox.IsValidFontFamilyName(parameter.Value.Source)))
					MessageBox.Show(String.Format("The font family '{0}' does not exist.", parameter.Value), "Invalid Font Family", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				else {
					switch (parameter.Action) {
						case ValueCommandParameterAction.CancelPreview:
							this.DeactivatePreviewMode(true);
							break;
						case ValueCommandParameterAction.Commit:
							this.DeactivatePreviewMode(false);
							this.SelectionFontFamily = parameter.Value;
							break;
						case ValueCommandParameterAction.Preview:
							this.ActivatePreviewMode();
							this.SelectionFontFamily = parameter.PreviewValue;
							break;
					}
				}
				e.Handled = true;
			}
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> needs to determine whether it can execute.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> that contains the event data.</param>
		private void OnFontSizeCanExecute(object sender, CanExecuteRoutedEventArgs e) {
			DoubleValueCommandParameter parameter = e.Parameter as DoubleValueCommandParameter;
			if ((parameter != null) && (!this.IsPreviewModeActive)) {
				parameter.UpdatedValue = this.SelectionFontSize;
				parameter.Handled = true;
			}
			e.CanExecute = true;
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void OnFontSizeExecute(object sender, ExecutedRoutedEventArgs e) {
			DoubleValueCommandParameter parameter = e.Parameter as DoubleValueCommandParameter;
			if (parameter != null) {
				if (parameter.ConversionException != null)
					MessageBox.Show(parameter.ConversionException.Message, "Invalid Font Size", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				else {
					switch (parameter.Action) {
						case ValueCommandParameterAction.CancelPreview:
							this.DeactivatePreviewMode(true);
							break;
						case ValueCommandParameterAction.Commit:
							this.DeactivatePreviewMode(false);
							this.SelectionFontSize = parameter.Value;
							break;
						case ValueCommandParameterAction.Preview:
							this.ActivatePreviewMode();
							this.SelectionFontSize = parameter.PreviewValue;
							break;
					}
				}
				e.Handled = true;
			}
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> needs to determine whether it can execute.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> that contains the event data.</param>
		private void OnToggleBoldCanExecute(object sender, CanExecuteRoutedEventArgs e) {
			ICheckableCommandParameter parameter = e.Parameter as ICheckableCommandParameter;
			if (parameter != null) {
				parameter.Handled = true;
				parameter.IsChecked = this.SelectionBold;
			}
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> needs to determine whether it can execute.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> that contains the event data.</param>
		private void OnToggleItalicCanExecute(object sender, CanExecuteRoutedEventArgs e) {
			ICheckableCommandParameter parameter = e.Parameter as ICheckableCommandParameter;
			if (parameter != null) {
				parameter.Handled = true;
				parameter.IsChecked = this.SelectionItalic;
			}
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> needs to determine whether it can execute.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> that contains the event data.</param>
		private void OnToggleStrikethroughCanExecute(object sender, CanExecuteRoutedEventArgs e) {
			ICheckableCommandParameter parameter = e.Parameter as ICheckableCommandParameter;
			if (parameter != null) {
				parameter.Handled = true;
				parameter.IsChecked = this.SelectionStrikethrough;
			}
			e.CanExecute = true;
			e.Handled = true;
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void OnToggleStrikethroughExecute(object sender, ExecutedRoutedEventArgs e) {
			this.SelectionStrikethrough = !this.SelectionStrikethrough;
			e.Handled = true;
		}
			
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> needs to determine whether it can execute.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> that contains the event data.</param>
		private void OnToggleSubscriptCanExecute(object sender, CanExecuteRoutedEventArgs e) {
			ICheckableCommandParameter parameter = e.Parameter as ICheckableCommandParameter;
			if (parameter != null) {
				parameter.Handled = true;
				parameter.IsChecked = this.SelectionSubscript;
			}
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> needs to determine whether it can execute.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> that contains the event data.</param>
		private void OnToggleSuperscriptCanExecute(object sender, CanExecuteRoutedEventArgs e) {
			ICheckableCommandParameter parameter = e.Parameter as ICheckableCommandParameter;
			if (parameter != null) {
				parameter.Handled = true;
				parameter.IsChecked = this.SelectionSuperscript;
			}
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> needs to determine whether it can execute.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> that contains the event data.</param>
		private void OnToggleUnderlineCanExecute(object sender, CanExecuteRoutedEventArgs e) {
			ICheckableCommandParameter parameter = e.Parameter as ICheckableCommandParameter;
			if (parameter != null) {
				parameter.Handled = true;
				parameter.IsChecked = this.SelectionUnderline;
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Coerces a setting to a nullable boolean value.
		/// </summary>
		/// <param name="value">The setting value.</param>
		/// <param name="trueValue">The true value.</param>
		/// <returns>A nullable boolean value.</returns>
		private bool? CoerceBooleanValue(object value, object trueValue) {
			if (value == null)
				return null;
			else if (value.Equals(trueValue))
				return true;
			else if (value == DependencyProperty.UnsetValue)
				return null;
			else
				return false;
		}
		
		/// <summary>
		/// Occurs when the context menu is opening.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="ContextMenuEventArgs"/> that contains the event data.</param>
		private void OnContextMenuOpening(object sender, ContextMenuEventArgs e) {
			this.UpdateSpellCheckContextMenuItems();
		}
			
		/// <summary>
		/// Updates the <see cref="ImageSource"/> for the <c>ApplicationCommands.ApplyDefaultForeground</c> command.
		/// </summary>
		/// <param name="brush">The <see cref="Brush"/> to set as default.</param>
		private void UpdateApplyDefaultForegroundSmallImageSource(Brush brush) {
			// The default brush is stored in the Tag, quit if it is already there
			if (ApplicationCommands.ApplyDefaultForeground.Tag == brush)
				return;

			// Store the brush in the Tag
			ApplicationCommands.ApplyDefaultForeground.Tag = brush;

			// Create a DrawingImage
			DrawingImage image = new DrawingImage();

			DrawingGroup group = new DrawingGroup();
			image.Drawing = group;

			ImageDrawing imageDrawing = new ImageDrawing(ImageLoader.GetIcon("FontColor16.png"), new Rect(0, 0, 16, 16));
			group.Children.Add(imageDrawing);

			GeometryDrawing geomDrawing = new GeometryDrawing();
			geomDrawing.Brush = (brush ?? Brushes.Transparent);
			group.Children.Add(geomDrawing);
			RectangleGeometry rectGeom = new RectangleGeometry(new Rect(0, 12, 16, 4));
			geomDrawing.Geometry = rectGeom;
			ImageProvider.SetCanAdapt(geomDrawing, false);

			ApplicationCommands.ApplyDefaultForeground.ImageSourceSmall = image;
		}

		/// <summary>
		/// Updates the <see cref="ImageSource"/> for the <c>ApplicationCommands.ApplyDefaultBackground</c> command.
		/// </summary>
		/// <param name="brush">The <see cref="Brush"/> to set as default.</param>
		private void UpdateApplyDefaultBackgroundSmallImageSource(Brush brush) {
			// The default brush is stored in the Tag, quit if it is already there
			if (ApplicationCommands.ApplyDefaultBackground.Tag == brush)
				return;

			// Store the brush in the Tag
			ApplicationCommands.ApplyDefaultBackground.Tag = brush;

			// Create a DrawingImage
			DrawingImage image = new DrawingImage();

			DrawingGroup group = new DrawingGroup();
			image.Drawing = group;

			ImageDrawing imageDrawing = new ImageDrawing(ImageLoader.GetIcon("TextHighlightColor16.png"), new Rect(0, 0, 16, 16));
			group.Children.Add(imageDrawing);

			GeometryDrawing geomDrawing = new GeometryDrawing();
			geomDrawing.Brush = (brush ?? Brushes.Transparent);
			group.Children.Add(geomDrawing);
			RectangleGeometry rectGeom = new RectangleGeometry(new Rect(0, 12, 16, 4));
			geomDrawing.Geometry = rectGeom;
			ImageProvider.SetCanAdapt(geomDrawing, false);

			ApplicationCommands.ApplyDefaultBackground.ImageSourceSmall = image;
		}
		
		/// <summary>
		/// Updates the spell check context menu items.
		/// </summary>
		private void UpdateSpellCheckContextMenuItems() {
			// Process items for spell-checking if SpellCheck.IsEnabled = true
			if ((this.ContextMenu != null) && (this.ContextMenu.ItemsSource == null)) {
				RibbonControls.Menu menu = null;
				if (this.ContextMenu.Items.Count > 0) {
					// Get an existing menu
					menu = this.ContextMenu.Items[0] as RibbonControls.Menu;
					if ((menu != null) && (!"SpellingErrors".Equals(menu.Tag)))
						menu = null;
				}

				if (menu != null) {
					// Clear the items
					menu.Items.Clear();
				}
				else {
					// Create a new menu
					menu = new RibbonControls.Menu();
					menu.Tag = "SpellingErrors";
					this.ContextMenu.Items.Insert(0, menu);
				}

				// If spell check is enabled...
				if (this.SpellCheck.IsEnabled) {
					// Get the spelling error at the caret
					SpellingError error = this.GetSpellingError(this.CaretPosition);
					if (error != null) {
						// Add suggestion items
						foreach (string suggestion in error.Suggestions) {
							RibbonControls.Button button = new RibbonControls.Button();
							button.Command = EditingCommands.CorrectSpellingError;
							button.CommandParameter = suggestion;
							button.Label = suggestion;
							menu.Items.Add(button);
						}

						// Add separator
						if (menu.Items.Count > 0)
							menu.Items.Add(new RibbonControls.Separator());
					}
				}
				
				// Update visibility
				menu.Visibility = (this.SpellCheck.IsEnabled && (menu.Items.Count > 0) ? Visibility.Visible : Visibility.Collapsed);
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Activates preview mode.
		/// </summary>
		public void ActivatePreviewMode() {
			if (previewStream == null) {
				if (this.Selection.IsEmpty) {
					// When the selection is empty, we need to select something for the preview stream functionality to work correctly
					if (this.Selection.End != this.Selection.End.DocumentEnd)
						EditingCommands.SelectRightByCharacter.Execute(null, this);
					else if (this.Selection.Start != this.Selection.Start.DocumentStart)
						EditingCommands.SelectRightByCharacter.Execute(null, this);
				}

                previewStream = new MemoryStream();
	            this.Selection.Save(previewStream, DataFormats.Xaml);
			}
		}

		/// <summary>
		/// Deactivates preview mode.
		/// </summary>
		/// <param name="restoreOldSettings">Whether to restore the old settings.</param>
		public void DeactivatePreviewMode(bool restoreOldSettings) {
			if (previewStream != null) {
				if (restoreOldSettings)
					this.Selection.Load(previewStream, DataFormats.Xaml);
				previewStream.Dispose();
				previewStream = null;
			}
		}
		
		/// <summary>
		/// Gets or sets a <see cref="Uri"/> indicating the location of the <see cref="FlowDocument"/> to load.
		/// </summary>
		/// <value>A <see cref="Uri"/> indicating the location of the <see cref="FlowDocument"/> to load.</value>
		public Uri DocumentUri {
			get {
				return (Uri)this.GetValue(RichTextBoxExtended.DocumentUriProperty);
			}
			set {
				this.SetValue(RichTextBoxExtended.DocumentUriProperty, value);
			}
		}
		
		/// <summary>
		/// Loads the document text.
		/// </summary>
		/// <param name="text">The text to load.</param>
		public void LoadDocument(string text) {
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(text);
			writer.Flush();
			stream.Position = 0;
			TextRange range = new TextRange(this.Document.ContentStart, this.Document.ContentEnd);
            range.Load(stream, DataFormats.Rtf);
			stream.Close();
		}

		/// <summary>
		/// Gets whether preview mode is active.
		/// </summary>
		/// <value>
		/// <c>true</c> if preview mode is active; otherwise, <c>false</c>.
		/// </value>
		public bool IsPreviewModeActive {
			get {
				return (previewStream != null);
			}
		}

		/// <summary>
		/// Invoked when an unhandled <see cref="UIElement.MouseUp"/> routed event is raised on this element. 
		/// Implement this method to add class handling for this event. 
		/// </summary>
		/// <param name="e">The event data for the <see cref="UIElement.MouseUp"/> event.</param>
		protected override void OnMouseUp(MouseButtonEventArgs e) {
			// Call the base method
			base.OnMouseUp(e);

			// If a selection was just made with the mouse and we are not in an XBAP...
			if ((e.ChangedButton == MouseButton.Left) && (!this.Selection.IsEmpty) && (!BrowserInteropHelper.IsBrowserHosted)) {
				// Show the mini-toolbar
				MiniToolBarService.Show(new ActiproSoftware.ProductSamples.RibbonSamples.Common.RichTextBoxMiniToolBar(), 
					this, e.GetPosition(this));
			}
		}
		
		/// <summary>
		/// Called when the rendered size of a control changes. 
		/// </summary>
		/// <param name="sizeInfo">Specifies the size changes.</param>
		protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
			// Call the base method
			base.OnRenderSizeChanged(sizeInfo);

			// Adjust the document's page width (since there is a WPF bug when used within a parent ScrollViewer with horizontal scroll capabilities)
			if (this.Document != null)
				this.Document.PageWidth = this.ActualWidth - this.BorderThickness.Left - this.Padding.Left - this.BorderThickness.Right - this.Padding.Right;
		}

        /// <summary>
        /// Gets or sets whether the selected text is aligned center.
        /// </summary>
        /// <value>
        /// <c>true</c> if the selected text is aligned center; otherwise, <c>false</c>.
        /// </value>
        public bool? SelectionAlignCenter {
            get {
                return this.CoerceBooleanValue(this.Selection.GetPropertyValue(TextBlock.TextAlignmentProperty), TextAlignment.Center);
            }
            set {
                if ((value.HasValue) && (value.Value))
                    this.Selection.ApplyPropertyValue(TextBlock.TextAlignmentProperty, TextAlignment.Center );
            }
        }

        /// <summary>
        /// Gets or sets whether the selected text is aligned justify.
        /// </summary>
        /// <value>
        /// <c>true</c> if the selected text is aligned justify; otherwise, <c>false</c>.
        /// </value>
        public bool? SelectionAlignJustify {
            get {
                return this.CoerceBooleanValue(this.Selection.GetPropertyValue(TextBlock.TextAlignmentProperty), TextAlignment.Justify);
            }
            set {
                if ((value.HasValue) && (value.Value))
                    this.Selection.ApplyPropertyValue(TextBlock.TextAlignmentProperty, TextAlignment.Justify );
            }
        }
		
        /// <summary>
        /// Gets or sets whether the selected text is aligned left.
        /// </summary>
        /// <value>
        /// <c>true</c> if the selected text is aligned left; otherwise, <c>false</c>.
        /// </value>
        public bool? SelectionAlignLeft {
            get {
                return this.CoerceBooleanValue(this.Selection.GetPropertyValue(TextBlock.TextAlignmentProperty), TextAlignment.Left);
            }
            set {
                if ((value.HasValue) && (value.Value))
                    this.Selection.ApplyPropertyValue(TextBlock.TextAlignmentProperty, TextAlignment.Left );
            }
        }

        /// <summary>
        /// Gets or sets whether the selected text is aligned right.
        /// </summary>
        /// <value>
        /// <c>true</c> if the selected text is aligned right; otherwise, <c>false</c>.
        /// </value>
        public bool? SelectionAlignRight {
            get {
                return this.CoerceBooleanValue(this.Selection.GetPropertyValue(TextBlock.TextAlignmentProperty), TextAlignment.Right);
            }
            set {
                if ((value.HasValue) && (value.Value))
                    this.Selection.ApplyPropertyValue(TextBlock.TextAlignmentProperty, TextAlignment.Right );
            }
        }

		/// <summary>
		/// Gets or sets the selected background.
		/// </summary>
		/// <value>The selected background.</value>
		public Brush SelectionBackground {
			get {
				object value = this.Selection.GetPropertyValue(TextElement.BackgroundProperty);
				if (value == DependencyProperty.UnsetValue)
					return null;
				else
					return (Brush)value;
			}
			set {
				this.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets whether the selected text is bold.
		/// </summary>
		/// <value>
		/// <c>true</c> if the selected text is bold; otherwise, <c>false</c>.
		/// </value>
		public bool? SelectionBold {
			get {
				return this.CoerceBooleanValue(this.Selection.GetPropertyValue(TextElement.FontWeightProperty), FontWeights.Bold);
			}
			set {
				this.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, (value != false ? FontWeights.Bold : FontWeights.Normal));
			}
		}
		
		/// <summary>
		/// Gets or sets the selected font family.
		/// </summary>
		/// <value>The selected font family.</value>
		public FontFamily SelectionFontFamily {
			get {
				object value = this.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
				if (value == DependencyProperty.UnsetValue)
					return null;
				else
					return (FontFamily)value;
			}
			set {
				if (value != null)
					this.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets the selected font size.
		/// </summary>
		/// <value>The selected font size.</value>
		public double SelectionFontSize {
			get {
				object value = this.Selection.GetPropertyValue(TextElement.FontSizeProperty);
				if (value == DependencyProperty.UnsetValue)
					return double.NaN;
				else
					return (double)value;
			}
			set {
				if (!value.Equals(double.NaN)) 
					this.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets the selected foreground.
		/// </summary>
		/// <value>The selected foreground.</value>
		public Brush SelectionForeground {
			get {
				object value = this.Selection.GetPropertyValue(TextElement.ForegroundProperty);
				if (value == DependencyProperty.UnsetValue)
					return null;
				else
					return (Brush)value;
			}
			set {
				this.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, (value != null ? value : Brushes.Black));
			}
		}
		
		/// <summary>
		/// Gets or sets whether the selected text is italic.
		/// </summary>
		/// <value>
		/// <c>true</c> if the selected text is italic; otherwise, <c>false</c>.
		/// </value>
		public bool? SelectionItalic {
			get {
				return this.CoerceBooleanValue(this.Selection.GetPropertyValue(TextElement.FontStyleProperty), FontStyles.Italic);
			}
			set {
				this.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, (value != false ? FontStyles.Italic : FontStyles.Normal));
			}
		}
		
		/// <summary>
		/// Gets or sets whether the selected text has a strike-through.
		/// </summary>
		/// <value>
		/// <c>true</c> if the selected text has a strike-through; otherwise, <c>false</c>.
		/// </value>
		public bool? SelectionStrikethrough {
			get {
				return this.CoerceBooleanValue(this.Selection.GetPropertyValue(TextBlock.TextDecorationsProperty), TextDecorations.Strikethrough);
			}
			set {
				this.Selection.ApplyPropertyValue(TextBlock.TextDecorationsProperty, (value != false ? TextDecorations.Strikethrough : null));
			}
		}
		
		/// <summary>
		/// Gets or sets whether the selected text is subscript.
		/// </summary>
		/// <value>
		/// <c>true</c> if the selected text is subscript; otherwise, <c>false</c>.
		/// </value>
		public bool? SelectionSubscript {
			get {
				return this.CoerceBooleanValue(this.Selection.GetPropertyValue(Typography.VariantsProperty), FontVariants.Subscript);
			}
			set {
				this.Selection.ApplyPropertyValue(Typography.VariantsProperty, (value != false ? FontVariants.Subscript : FontVariants.Normal));
			}
		}

		/// <summary>
		/// Gets or sets whether the selected text is superscript.
		/// </summary>
		/// <value>
		/// <c>true</c> if the selected text is superscript; otherwise, <c>false</c>.
		/// </value>
		public bool? SelectionSuperscript {
			get {
				return this.CoerceBooleanValue(this.Selection.GetPropertyValue(Typography.VariantsProperty), FontVariants.Superscript);
			}
			set {
				this.Selection.ApplyPropertyValue(Typography.VariantsProperty, (value != false ? FontVariants.Superscript : FontVariants.Normal));
			}
		}

		/// <summary>
		/// Gets or sets whether the selected text is underlined.
		/// </summary>
		/// <value>
		/// <c>true</c> if the selected text is underlined; otherwise, <c>false</c>.
		/// </value>
		public bool? SelectionUnderline {
			get {
				return this.CoerceBooleanValue(this.Selection.GetPropertyValue(TextBlock.TextDecorationsProperty), TextDecorations.Underline);
			}
			set {
				this.Selection.ApplyPropertyValue(TextBlock.TextDecorationsProperty, (value != false ? TextDecorations.Underline : null));
			}
		}

	}
}