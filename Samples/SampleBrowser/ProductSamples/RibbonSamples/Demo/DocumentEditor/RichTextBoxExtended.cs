using ActiproSoftware.Windows.Controls.Ribbon.Input;
using ActiproSoftware.Windows.Controls.Ribbon.UI;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Themes;
using System.Windows.Documents;
using ImageLoader = ActiproSoftware.SampleBrowser.ImageLoader;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
using RibbonControls = ActiproSoftware.Windows.Controls.Ribbon.Controls;

namespace ActiproSoftware.ProductSamples.RibbonSamples.Demo.DocumentEditor;

/// <summary>
/// Represents an extended <see cref="RichTextBox"/> control.
/// </summary>
public class RichTextBoxExtended : RichTextBox {

	private MemoryStream? _previewStream;

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="DocumentUri"/> property.
	/// </summary>
	public static readonly DependencyProperty DocumentUriProperty
		= DependencyProperty.Register(nameof(DocumentUri), typeof(Uri), typeof(RichTextBoxExtended), new FrameworkPropertyMetadata(defaultValue: null, OnDocumentUriPropertyValueChanged));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static RichTextBoxExtended() {
		AcceptsReturnProperty.OverrideMetadata(typeof(RichTextBoxExtended), new FrameworkPropertyMetadata(true));
		AcceptsTabProperty.OverrideMetadata(typeof(RichTextBoxExtended), new FrameworkPropertyMetadata(true));
		HorizontalScrollBarVisibilityProperty.OverrideMetadata(typeof(RichTextBoxExtended), new FrameworkPropertyMetadata(ScrollBarVisibility.Hidden));
		VerticalScrollBarVisibilityProperty.OverrideMetadata(typeof(RichTextBoxExtended), new FrameworkPropertyMetadata(ScrollBarVisibility.Hidden));
	}

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public RichTextBoxExtended() {
		// Set appearance
		Background = Brushes.White;
		BorderBrush = Brushes.Black;
		BorderThickness = new Thickness(1);
		Foreground = Brushes.Black;
		Document.Background = Brushes.Transparent;
		Document.Foreground = Foreground;

		// Force Ideal formatting because Display formatting at mixed DPI (e.g. 100% primary monitor, 150% secondary monitor)
		//   could cause RichTextBox to crash after switching monitors and scrolling documents with wrapped lines; especially if a MaxWidth
		//   was assigned to the RichTextBox or one of its parent containers
		TextOptions.SetTextFormattingMode(this, TextFormattingMode.Ideal);

		ThemeProperties.SetUseBackgroundStates(this, false);
		ThemeProperties.SetUseBorderStates(this, false);

		// Assign a custom context menu
		var contextMenu = new RibbonControls.ContextMenu();
		var menu = new RibbonControls.Menu();
		contextMenu.Items.Add(menu);
		menu.Items.Add(new RibbonControls.Button(System.Windows.Input.ApplicationCommands.Undo) { KeyTipAccessText = "U" });
		menu.Items.Add(new RibbonControls.Button(System.Windows.Input.ApplicationCommands.Redo) { KeyTipAccessText = "R" });
		menu.Items.Add(new RibbonControls.Separator());
		menu.Items.Add(new RibbonControls.Button(System.Windows.Input.ApplicationCommands.Cut) { KeyTipAccessText = "T" });
		menu.Items.Add(new RibbonControls.Button(System.Windows.Input.ApplicationCommands.Copy) { KeyTipAccessText = "C" });
		menu.Items.Add(new RibbonControls.Button(System.Windows.Input.ApplicationCommands.Paste) { KeyTipAccessText = "P" });
		ContextMenu = contextMenu;

		// Attach a mini-toolbar to the context menu
		contextMenu.MiniToolBar = new Common.RichTextBoxMiniToolBar();

		// Attach to the context menu opening event
		ContextMenuOpening += OnContextMenuOpening;

		// Add command bindings
		CommandBindings.Add(new CommandBinding(EditingCommands.AlignCenter, null, OnAlignCenterCanExecute));
		CommandBindings.Add(new CommandBinding(EditingCommands.AlignJustify, null, OnAlignJustifyCanExecute));
		CommandBindings.Add(new CommandBinding(EditingCommands.AlignLeft, null, OnAlignLeftCanExecute));
		CommandBindings.Add(new CommandBinding(EditingCommands.AlignRight, null, OnAlignRightCanExecute));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.ApplyBackground, OnApplyBackgroundExecute, OnApplyBackgroundCanExecute));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.ApplyDefaultBackground, OnApplyDefaultBackgroundExecute));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.ApplyDefaultForeground, OnApplyDefaultForegroundExecute));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.ApplyForeground, OnApplyForegroundExecute, OnApplyForegroundCanExecute));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.ClearFormatting, OnClearFormattingExecute));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.FontFamily, OnFontFamilyExecute, OnFontFamilyCanExecute));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.FontSize, OnFontSizeExecute, OnFontSizeCanExecute));
		CommandBindings.Add(new CommandBinding(EditingCommands.ToggleBold, null, OnToggleBoldCanExecute));
		CommandBindings.Add(new CommandBinding(EditingCommands.ToggleItalic, null, OnToggleItalicCanExecute));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.ToggleStrikethrough, OnToggleStrikethroughExecute, OnToggleStrikethroughCanExecute));
		CommandBindings.Add(new CommandBinding(EditingCommands.ToggleSubscript, null, OnToggleSubscriptCanExecute));
		CommandBindings.Add(new CommandBinding(EditingCommands.ToggleSuperscript, null, OnToggleSuperscriptCanExecute));
		CommandBindings.Add(new CommandBinding(EditingCommands.ToggleUnderline, null, OnToggleUnderlineCanExecute));

	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Coerces a setting to a nullable boolean value.
	/// </summary>
	/// <param name="value">The setting value.</param>
	/// <param name="trueValue">The true value.</param>
	/// <returns>A nullable boolean value.</returns>
	private static bool? CoerceBooleanValue(object value, object trueValue) {
		if (value is null)
			return null;
		else if (value.Equals(trueValue))
			return true;
		else if (value == DependencyProperty.UnsetValue)
			return null;
		else
			return false;
	}

	private void OnAlignCenterCanExecute(object sender, CanExecuteRoutedEventArgs e) {
		var parameter = e.Parameter as ICheckableCommandParameter;
		if (parameter is not null) {
			parameter.Handled = true;
			parameter.IsChecked = SelectionAlignCenter;
		}
	}

	private void OnAlignJustifyCanExecute(object sender, CanExecuteRoutedEventArgs e) {
		var parameter = e.Parameter as ICheckableCommandParameter;
		if (parameter is not null) {
			parameter.Handled = true;
			parameter.IsChecked = SelectionAlignJustify;
		}
	}

	private void OnAlignLeftCanExecute(object sender, CanExecuteRoutedEventArgs e) {
		var parameter = e.Parameter as ICheckableCommandParameter;
		if (parameter is not null) {
			parameter.Handled = true;
			parameter.IsChecked = SelectionAlignLeft;
		}
	}

	private void OnAlignRightCanExecute(object sender, CanExecuteRoutedEventArgs e) {
		var parameter = e.Parameter as ICheckableCommandParameter;
		if (parameter is not null) {
			parameter.Handled = true;
			parameter.IsChecked = SelectionAlignRight;
		}
	}

	private void OnApplyBackgroundCanExecute(object sender, CanExecuteRoutedEventArgs e) {
		var parameter = e.Parameter as BrushValueCommandParameter;
		if ((parameter is not null) && !IsPreviewModeActive) {
			parameter.UpdatedValue = SelectionBackground;
			parameter.Handled = true;
		}
		e.CanExecute = true;
	}

	private void OnApplyBackgroundExecute(object sender, ExecutedRoutedEventArgs e) {
		var parameter = e.Parameter as BrushValueCommandParameter;
		if (parameter is not null) {
			switch (parameter.Action) {
				case ValueCommandParameterAction.CancelPreview:
					DeactivatePreviewMode(true);
					break;
				case ValueCommandParameterAction.Commit:
					DeactivatePreviewMode(false);
					SelectionBackground = parameter.Value;
					UpdateApplyDefaultBackgroundSmallImageSource(parameter.Value);
					break;
				case ValueCommandParameterAction.Preview:
					ActivatePreviewMode();
					SelectionBackground = parameter.PreviewValue;
					break;
			}
		}
		else {
			SelectionBackground = null;
			UpdateApplyDefaultBackgroundSmallImageSource(null);
		}
		e.Handled = true;
	}

	private void OnApplyDefaultBackgroundExecute(object sender, ExecutedRoutedEventArgs e) {
		SelectionBackground = ApplicationCommands.ApplyDefaultBackground.Tag as Brush;
		e.Handled = true;
	}

	private void OnApplyDefaultForegroundExecute(object sender, ExecutedRoutedEventArgs e) {
		SelectionForeground = ApplicationCommands.ApplyDefaultForeground.Tag as Brush;
		e.Handled = true;
	}

	private void OnApplyForegroundCanExecute(object sender, CanExecuteRoutedEventArgs e) {
		var parameter = e.Parameter as BrushValueCommandParameter;
		if ((parameter is not null) && (!IsPreviewModeActive)) {
			parameter.UpdatedValue = SelectionForeground;
			parameter.Handled = true;
		}
		e.CanExecute = true;
	}

	private void OnApplyForegroundExecute(object sender, ExecutedRoutedEventArgs e) {
		var parameter = e.Parameter as BrushValueCommandParameter;
		if (parameter is not null) {
			switch (parameter.Action) {
				case ValueCommandParameterAction.CancelPreview:
					DeactivatePreviewMode(true);
					break;
				case ValueCommandParameterAction.Commit:
					DeactivatePreviewMode(false);
					SelectionForeground = parameter.Value;
					UpdateApplyDefaultForegroundSmallImageSource(parameter.Value);
					break;
				case ValueCommandParameterAction.Preview:
					ActivatePreviewMode();
					SelectionForeground = parameter.PreviewValue;
					break;
			}
		}
		else {
			SelectionForeground = null;
			UpdateApplyDefaultForegroundSmallImageSource(null);
		}
		e.Handled = true;
	}

	private void OnClearFormattingExecute(object sender, ExecutedRoutedEventArgs e) {
		Selection.ClearAllProperties();
		e.Handled = true;
	}

	private void OnContextMenuOpening(object sender, ContextMenuEventArgs e)
		=> UpdateSpellCheckContextMenuItems();

	private static void OnDocumentUriPropertyValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
		var control = (RichTextBoxExtended)obj;
		try {
			control.Document = Application.LoadComponent(control.DocumentUri) as FlowDocument;
		}
		catch { }
	}

	private void OnFontFamilyCanExecute(object sender, CanExecuteRoutedEventArgs e) {
		var parameter = e.Parameter as FontFamilyValueCommandParameter;
		if ((parameter is not null) && !IsPreviewModeActive) {
			parameter.UpdatedValue = SelectionFontFamily;
			parameter.Handled = true;
		}
		e.CanExecute = true;
	}

	private void OnFontFamilyExecute(object sender, ExecutedRoutedEventArgs e) {
		var parameter = e.Parameter as FontFamilyValueCommandParameter;
		if (parameter is not null) {
			if ((parameter.Value is not null) && (!RibbonControls.FontFamilyComboBox.IsValidFontFamilyName(parameter.Value.Source)))
				MessageBox.Show(string.Format("The font family '{0}' does not exist.", parameter.Value), "Invalid Font Family", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			else {
				switch (parameter.Action) {
					case ValueCommandParameterAction.CancelPreview:
						DeactivatePreviewMode(true);
						break;
					case ValueCommandParameterAction.Commit:
						DeactivatePreviewMode(false);
						SelectionFontFamily = parameter.Value;
						break;
					case ValueCommandParameterAction.Preview:
						ActivatePreviewMode();
						SelectionFontFamily = parameter.PreviewValue;
						break;
				}
			}
			e.Handled = true;
		}
	}

	private void OnFontSizeCanExecute(object sender, CanExecuteRoutedEventArgs e) {
		var parameter = e.Parameter as DoubleValueCommandParameter;
		if ((parameter is not null) && (!IsPreviewModeActive)) {
			parameter.UpdatedValue = SelectionFontSize;
			parameter.Handled = true;
		}
		e.CanExecute = true;
	}

	private void OnFontSizeExecute(object sender, ExecutedRoutedEventArgs e) {
		var parameter = e.Parameter as DoubleValueCommandParameter;
		if (parameter is not null) {
			if (parameter.ConversionException is not null)
				MessageBox.Show(parameter.ConversionException.Message, "Invalid Font Size", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			else {
				switch (parameter.Action) {
					case ValueCommandParameterAction.CancelPreview:
						DeactivatePreviewMode(true);
						break;
					case ValueCommandParameterAction.Commit:
						DeactivatePreviewMode(false);
						SelectionFontSize = parameter.Value;
						break;
					case ValueCommandParameterAction.Preview:
						ActivatePreviewMode();
						SelectionFontSize = parameter.PreviewValue;
						break;
				}
			}
			e.Handled = true;
		}
	}

	private void OnToggleBoldCanExecute(object sender, CanExecuteRoutedEventArgs e) {
		var parameter = e.Parameter as ICheckableCommandParameter;
		if (parameter is not null) {
			parameter.Handled = true;
			parameter.IsChecked = SelectionBold;
		}
	}

	private void OnToggleItalicCanExecute(object sender, CanExecuteRoutedEventArgs e) {
		var parameter = e.Parameter as ICheckableCommandParameter;
		if (parameter is not null) {
			parameter.Handled = true;
			parameter.IsChecked = SelectionItalic;
		}
	}

	private void OnToggleStrikethroughCanExecute(object sender, CanExecuteRoutedEventArgs e) {
		var parameter = e.Parameter as ICheckableCommandParameter;
		if (parameter is not null) {
			parameter.Handled = true;
			parameter.IsChecked = SelectionStrikethrough;
		}
		e.CanExecute = true;
		e.Handled = true;
	}

	private void OnToggleStrikethroughExecute(object sender, ExecutedRoutedEventArgs e) {
		SelectionStrikethrough = !SelectionStrikethrough;
		e.Handled = true;
	}

	private void OnToggleSubscriptCanExecute(object sender, CanExecuteRoutedEventArgs e) {
		var parameter = e.Parameter as ICheckableCommandParameter;
		if (parameter is not null) {
			parameter.Handled = true;
			parameter.IsChecked = SelectionSubscript;
		}
	}

	private void OnToggleSuperscriptCanExecute(object sender, CanExecuteRoutedEventArgs e) {
		var parameter = e.Parameter as ICheckableCommandParameter;
		if (parameter is not null) {
			parameter.Handled = true;
			parameter.IsChecked = SelectionSuperscript;
		}
	}

	private void OnToggleUnderlineCanExecute(object sender, CanExecuteRoutedEventArgs e) {
		var parameter = e.Parameter as ICheckableCommandParameter;
		if (parameter is not null) {
			parameter.Handled = true;
			parameter.IsChecked = SelectionUnderline;
		}
	}

	/// <summary>
	/// Updates the <see cref="ImageSource"/> for the <c>ApplicationCommands.ApplyDefaultForeground</c> command.
	/// </summary>
	/// <param name="brush">The <see cref="Brush"/> to set as default.</param>
	private static void UpdateApplyDefaultForegroundSmallImageSource(Brush? brush) {
		// The default brush is stored in the Tag, quit if it is already there
		if (ApplicationCommands.ApplyDefaultForeground.Tag == brush)
			return;

		// Store the brush in the Tag
		ApplicationCommands.ApplyDefaultForeground.Tag = brush;

		// Create a DrawingImage
		var image = new DrawingImage();

		var group = new DrawingGroup();
		image.Drawing = group;

		var imageDrawing = new ImageDrawing(ImageLoader.GetIcon("FontColor16.png"), new Rect(0, 0, 16, 16));
		group.Children.Add(imageDrawing);

		var geomDrawing = new GeometryDrawing {
			Brush = (brush ?? Brushes.Transparent)
		};
		group.Children.Add(geomDrawing);
		var rectGeom = new RectangleGeometry(new Rect(0, 12, 16, 4));
		geomDrawing.Geometry = rectGeom;
		ImageProvider.SetCanAdapt(geomDrawing, false);

		ApplicationCommands.ApplyDefaultForeground.ImageSourceSmall = image;
	}

	/// <summary>
	/// Updates the <see cref="ImageSource"/> for the <c>ApplicationCommands.ApplyDefaultBackground</c> command.
	/// </summary>
	/// <param name="brush">The <see cref="Brush"/> to set as default.</param>
	private static void UpdateApplyDefaultBackgroundSmallImageSource(Brush? brush) {
		// The default brush is stored in the Tag, quit if it is already there
		if (ApplicationCommands.ApplyDefaultBackground.Tag == brush)
			return;

		// Store the brush in the Tag
		ApplicationCommands.ApplyDefaultBackground.Tag = brush;

		// Create a DrawingImage
		var image = new DrawingImage();

		var group = new DrawingGroup();
		image.Drawing = group;

		var imageDrawing = new ImageDrawing(ImageLoader.GetIcon("TextHighlightColor16.png"), new Rect(0, 0, 16, 16));
		group.Children.Add(imageDrawing);

		var geomDrawing = new GeometryDrawing {
			Brush = (brush ?? Brushes.Transparent)
		};
		group.Children.Add(geomDrawing);
		var rectGeom = new RectangleGeometry(new Rect(0, 12, 16, 4));
		geomDrawing.Geometry = rectGeom;
		ImageProvider.SetCanAdapt(geomDrawing, false);

		ApplicationCommands.ApplyDefaultBackground.ImageSourceSmall = image;
	}

	/// <summary>
	/// Updates the spell check context menu items.
	/// </summary>
	private void UpdateSpellCheckContextMenuItems() {
		// Process items for spell-checking if SpellCheck.IsEnabled = true
		if (ContextMenu is { ItemsSource: null }) {
			RibbonControls.Menu? menu = null;
			if (ContextMenu.Items.Count > 0) {
				// Get an existing menu
				menu = ContextMenu.Items[0] as RibbonControls.Menu;
				if ((menu is not null) && (!"SpellingErrors".Equals(menu.Tag)))
					menu = null;
			}

			if (menu is not null) {
				// Clear the items
				menu.Items.Clear();
			}
			else {
				// Create a new menu
				menu = new RibbonControls.Menu {
					Tag = "SpellingErrors"
				};
				ContextMenu.Items.Insert(0, menu);
			}

			// If spell check is enabled...
			if (SpellCheck.IsEnabled) {
				// Get the spelling error at the caret
				var error = GetSpellingError(CaretPosition);
				if (error is not null) {
					// Add suggestion items
					foreach (var suggestion in error.Suggestions) {
						var button = new RibbonControls.Button {
							Command = EditingCommands.CorrectSpellingError,
							CommandParameter = suggestion,
							Label = suggestion
						};
						menu.Items.Add(button);
					}

					// Add separator
					if (menu.Items.Count > 0)
						menu.Items.Add(new RibbonControls.Separator());
				}
			}

			// Update visibility
			menu.Visibility = (SpellCheck.IsEnabled && (menu.Items.Count > 0)) ? Visibility.Visible : Visibility.Collapsed;
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Activates preview mode.
	/// </summary>
	public void ActivatePreviewMode() {
		if (_previewStream is null) {
			if (Selection.IsEmpty) {
				// When the selection is empty, we need to select something for the preview stream functionality to work correctly
				if (Selection.End != Selection.End.DocumentEnd)
					EditingCommands.SelectRightByCharacter.Execute(null, this);
				else if (Selection.Start != Selection.Start.DocumentStart)
					EditingCommands.SelectRightByCharacter.Execute(null, this);
			}

			_previewStream = new MemoryStream();
			Selection.Save(_previewStream, DataFormats.Xaml);
		}
	}

	/// <summary>
	/// Deactivates preview mode.
	/// </summary>
	/// <param name="restoreOldSettings">Whether to restore the old settings.</param>
	public void DeactivatePreviewMode(bool restoreOldSettings) {
		if (_previewStream is not null) {
			if (restoreOldSettings)
				Selection.Load(_previewStream, DataFormats.Xaml);
			_previewStream.Dispose();
			_previewStream = null;
		}
	}

	/// <summary>
	/// A <see cref="Uri"/> indicating the location of the <see cref="FlowDocument"/> to load.
	/// </summary>
	public Uri? DocumentUri {
		get => (Uri)GetValue(DocumentUriProperty);
		set => SetValue(DocumentUriProperty, value);
	}

	/// <summary>
	/// Loads the document text.
	/// </summary>
	/// <param name="text">The text to load.</param>
	public void LoadDocument(string text) {
		var stream = new MemoryStream();
		var writer = new StreamWriter(stream);
		writer.Write(text);
		writer.Flush();
		stream.Position = 0;
		var range = new TextRange(Document.ContentStart, Document.ContentEnd);
		range.Load(stream, DataFormats.Rtf);
		stream.Close();
	}

	/// <summary>
	/// Indicates whether preview mode is active.
	/// </summary>
	public bool IsPreviewModeActive
		=> (_previewStream is not null);

	/// <inheritdoc/>
	protected override void OnMouseUp(MouseButtonEventArgs e) {
		base.OnMouseUp(e);

		// If a selection was just made with the mouse...
		if ((e.ChangedButton == MouseButton.Left) && (!Selection.IsEmpty)) {
			// Show the mini-toolbar
			MiniToolBarService.Show(new Common.RichTextBoxMiniToolBar(), this, e.GetPosition(this));
		}
	}

	/// <inheritdoc/>
	protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
		base.OnRenderSizeChanged(sizeInfo);

		// Adjust the document's page width (since there is a WPF bug when used within a parent ScrollViewer with horizontal scroll capabilities)
		if (Document is not null)
			Document.PageWidth = (ActualWidth - BorderThickness.Left - Padding.Left - BorderThickness.Right - Padding.Right);
	}

	/// <summary>
	/// Indicates whether the selected text is aligned center.
	/// </summary>
	public bool? SelectionAlignCenter {
		get => CoerceBooleanValue(Selection.GetPropertyValue(TextBlock.TextAlignmentProperty), TextAlignment.Center);
		set {
			if (value == true)
				Selection.ApplyPropertyValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
		}
	}

	/// <summary>
	/// Indicates whether the selected text is aligned justify.
	/// </summary>
	public bool? SelectionAlignJustify {
		get => CoerceBooleanValue(Selection.GetPropertyValue(TextBlock.TextAlignmentProperty), TextAlignment.Justify);
		set {
			if (value == true)
				Selection.ApplyPropertyValue(TextBlock.TextAlignmentProperty, TextAlignment.Justify);
		}
	}

	/// <summary>
	/// Indicates whether the selected text is aligned left.
	/// </summary>
	public bool? SelectionAlignLeft {
		get => CoerceBooleanValue(Selection.GetPropertyValue(TextBlock.TextAlignmentProperty), TextAlignment.Left);
		set {
			if (value == true)
				Selection.ApplyPropertyValue(TextBlock.TextAlignmentProperty, TextAlignment.Left);
		}
	}

	/// <summary>
	/// Indicates whether the selected text is aligned right.
	/// </summary>
	public bool? SelectionAlignRight {
		get => CoerceBooleanValue(Selection.GetPropertyValue(TextBlock.TextAlignmentProperty), TextAlignment.Right);
		set {
			if (value == true)
				Selection.ApplyPropertyValue(TextBlock.TextAlignmentProperty, TextAlignment.Right);
		}
	}

	/// <summary>
	/// The selected background.
	/// </summary>
	public Brush? SelectionBackground {
		get {
			var value = Selection.GetPropertyValue(TextElement.BackgroundProperty);
			return (value == DependencyProperty.UnsetValue)
				? null
				: (Brush)value;
		}
		set => Selection.ApplyPropertyValue(TextElement.BackgroundProperty, value);
	}

	/// <summary>
	/// Indicates whether the selected text is bold.
	/// </summary>
	public bool? SelectionBold {
		get => CoerceBooleanValue(Selection.GetPropertyValue(TextElement.FontWeightProperty), FontWeights.Bold);
		set => Selection.ApplyPropertyValue(TextElement.FontWeightProperty, (value != false ? FontWeights.Bold : FontWeights.Normal));
	}

	/// <summary>
	/// The selected font family.
	/// </summary>
	public FontFamily? SelectionFontFamily {
		get {
			var value = Selection.GetPropertyValue(TextElement.FontFamilyProperty);
			return (value == DependencyProperty.UnsetValue)
				? null
				: (FontFamily)value;
		}
		set {
			if (value is not null)
				Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, value);
		}
	}

	/// <summary>
	/// The selected font size.
	/// </summary>
	public double SelectionFontSize {
		get {
			var value = Selection.GetPropertyValue(TextElement.FontSizeProperty);
			if (value == DependencyProperty.UnsetValue)
				return double.NaN;
			else
				return (double)value;
		}
		set {
			if (!value.Equals(double.NaN))
				Selection.ApplyPropertyValue(TextElement.FontSizeProperty, value);
		}
	}

	/// <summary>
	/// The selected foreground.
	/// </summary>
	public Brush? SelectionForeground {
		get {
			var value = Selection.GetPropertyValue(TextElement.ForegroundProperty);
			return (value == DependencyProperty.UnsetValue)
				? null
				: (Brush)value;
		}
		set => Selection.ApplyPropertyValue(TextElement.ForegroundProperty, (value is not null ? value : Brushes.Black));
	}

	/// <summary>
	/// Indicates whether the selected text is italic.
	/// </summary>
	public bool? SelectionItalic {
		get => CoerceBooleanValue(Selection.GetPropertyValue(TextElement.FontStyleProperty), FontStyles.Italic);
		set => Selection.ApplyPropertyValue(TextElement.FontStyleProperty, (value != false ? FontStyles.Italic : FontStyles.Normal));
	}

	/// <summary>
	/// Indicates whether the selected text has a strike-through.
	/// </summary>
	public bool? SelectionStrikethrough {
		get => CoerceBooleanValue(Selection.GetPropertyValue(TextBlock.TextDecorationsProperty), TextDecorations.Strikethrough);
		set => Selection.ApplyPropertyValue(TextBlock.TextDecorationsProperty, (value != false ? TextDecorations.Strikethrough : null));
	}

	/// <summary>
	/// Indicates whether the selected text is subscript.
	/// </summary>
	public bool? SelectionSubscript {
		get => CoerceBooleanValue(Selection.GetPropertyValue(Typography.VariantsProperty), FontVariants.Subscript);
		set => Selection.ApplyPropertyValue(Typography.VariantsProperty, (value != false ? FontVariants.Subscript : FontVariants.Normal));
	}

	/// <summary>
	/// Indicates whether the selected text is superscript.
	/// </summary>
	public bool? SelectionSuperscript {
		get => CoerceBooleanValue(Selection.GetPropertyValue(Typography.VariantsProperty), FontVariants.Superscript);
		set => Selection.ApplyPropertyValue(Typography.VariantsProperty, (value != false ? FontVariants.Superscript : FontVariants.Normal));
	}

	/// <summary>
	/// Indicates whether the selected text is underlined.
	/// </summary>
	public bool? SelectionUnderline {
		get => CoerceBooleanValue(Selection.GetPropertyValue(TextBlock.TextDecorationsProperty), TextDecorations.Underline);
		set => Selection.ApplyPropertyValue(TextBlock.TextDecorationsProperty, (value != false ? TextDecorations.Underline : null));
	}

}
