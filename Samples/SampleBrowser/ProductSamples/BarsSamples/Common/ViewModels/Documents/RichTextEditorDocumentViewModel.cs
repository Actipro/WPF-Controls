using ActiproSoftware.Extensions;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Data;
using ActiproSoftware.Windows.Input;
using System.Windows.Documents;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common;

/// <summary>
/// Represents a view model for a document of richly-formatted text.
/// </summary>
/// <param name="barManager">The <see cref="Common.BarManager"/> to be associated with the view model.</param>
/// <param name="document">The <see cref="FlowDocument"/> of rich text to be managed by the view model.</param>
public class RichTextEditorDocumentViewModel(BarManager barManager, FlowDocument document) : DocumentViewModel(barManager) {

	private static Color DefaultFontBackgroundPickerColor = Colors.Yellow;
	private static Color DefaultFontForegroundPickerColor = Colors.Red;

	private const double FontSizeChangeSmallStepThreshold = 12.0;

	private RichTextBoxExtended.PreviewModeState _previewMode;
	private RichTextStyle _selectionTextStyle = new();

	private ICommand? _decreaseFontSizeCommand;
	private ICommand? _increaseFontSizeCommand;
	private ICommand? _insertSymbolCommand;
	private ICommand? _setFontColorCommand;
	private ICommand? _setFontFamilyCommand;
	private ICommand? _setFontSizeCommand;
	private ICommand? _setNumberingCommand;
	private ICommand? _setTextAlignmentCommand;
	private ICommand? _setTextHighlightColorCommand;
	private ICommand? _setTextStyleCommand;
	private ICommand? _setUnderlineCommand;
	private ICommand? _stopHighlightingCommand;
	private ICommand? _toggleBoldCommand;
	private ICommand? _toggleItalicCommand;
	private ICommand? _toggleNumberingCommand;
	private ICommand? _toggleStrikethroughCommand;
	private ICommand? _toggleUnderlineCommand;
	private ICommand? _unknownFontSizeCommand;

	// --------------------------------------------------------------------------------------------------
	// EVENTS
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Raised to request that the view activates preview mode.
	/// </summary>
	public event EventHandler? RequestActivatePreviewMode;

	/// <summary>
	/// Raised to request that the view discards the current state and exits preview model.
	/// </summary>
	public event EventHandler? RequestCancelPreviewMode;

	/// <summary>
	/// Raised to request that the view inserts text.
	/// </summary>
	public event EventHandler<string>? RequestInsertText;

	/// <summary>
	/// Raised to request that the view saves the current state and exits preview model.
	/// </summary>
	public event EventHandler? RequestSaveAndExitPreviewMode;

	/// <summary>
	/// Raised to request that the view clears all text highlights.
	/// </summary>
	public event EventHandler? RequestClearAllTextHighlights;

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Applies numbering to the current selection.
	/// </summary>
	/// <param name="viewModel">The view model which defines the number to apply.</param>
	private void ApplyNumbering(NumberingBarGalleryItemViewModel viewModel) {
		// Numbering is not supported by this application and this method stub is included
		//   only for demonstration purposes of how an application might implement
		//   applying a gallery item for numbering
		if (PreviewMode == RichTextBoxExtended.PreviewModeState.None) {
			MessageBox.Show(
				$"The numbering '{viewModel.Label}' is for user interface demonstration purposes only and no application functionality has been implemented for it.", "Numbering Not Implemented",
				MessageBoxButton.OK, MessageBoxImage.Information);

			// Since selecting one of the numbering gallery items will automatically change the selection to that
			//   item, make sure the view model selection is reset to reflect that numbering is not active
			BarManager.UpdateControlViewModelCheckedState(BarControlKeys.Numbering, () => false);
			if (BarManager.ControlViewModels[BarControlKeys.NumberingGallery] is BarGalleryViewModel numberingGalleryViewModel) {
				numberingGalleryViewModel.SelectItemByValueMatch<NumberingBarGalleryItemViewModel>(i =>
					i.Value == NumberingKind.None
				);
			}
		}
	}

	/// <summary>
	/// Applies a text style to the current selection.
	/// </summary>
	/// <param name="textStyle">The text style to apply.</param>
	private void ApplyTextStyle(TextStyle textStyle) {
		UpdateSelectionTextStyle(s => {
			s.Bold = textStyle.Bold;
			s.FontColor = textStyle.TextColor;
			s.FontFamilyName = textStyle.FontFamilyName;
			s.FontSize = textStyle.FontSize;
			s.Italic = textStyle.Italic;
			s.Underline = textStyle.Underline ? UnderlineKind.Underline : UnderlineKind.None;
		});
	}

	/// <summary>
	/// Applies an underline to the current selection.
	/// </summary>
	/// <param name="viewModel">The view model which defines the underline style to apply.</param>
	private void ApplyUnderline(UnderlineBarGalleryItemViewModel viewModel) {
		// Only standard underline is supported by this application and the other underline kinds
		//   are for demonstration purposes only
		if ((viewModel.Value == UnderlineKind.None) || (viewModel.Value == UnderlineKind.Underline)) {
			UpdateSelectionTextStyle(s => s.Underline = viewModel.Value);
		}
		else if (PreviewMode == RichTextBoxExtended.PreviewModeState.None) {
			// Provide feedback that the selected item is not supported
			MessageBox.Show(
				$"The underline '{viewModel.Label}' is for user interface demonstration purposes only and no application functionality has been implemented for it.", "Underline Not Implemented",
				MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}

	/// <summary>
	/// Raises the <see cref="RequestActivatePreviewMode"/> event.
	/// </summary>
	private void OnRequestActivatePreviewMode()
		=> RequestActivatePreviewMode?.Invoke(this, EventArgs.Empty);

	/// <summary>
	/// Raises the <see cref="RequestCancelPreviewMode"/> event.
	/// </summary>
	private void OnRequestCancelPreviewMode()
		=> RequestCancelPreviewMode?.Invoke(this, EventArgs.Empty);

	/// <summary>
	/// Raises the <see cref="RequestClearAllTextHighlights"/> event.
	/// </summary>
	private void OnRequestClearAllTextHighlights()
		=> RequestClearAllTextHighlights?.Invoke(this, EventArgs.Empty);

	/// <summary>
	/// Raises the <see cref="RequestInsertText"/> event.
	/// </summary>
	private void OnRequestInsertText(string? text)
		=> RequestInsertText?.Invoke(this, text ?? string.Empty);

	/// <summary>
	/// Raises the <see cref="RequestSaveAndExitPreviewMode"/> event.
	/// </summary>
	private void OnRequestSaveAndExitPreviewMode()
		=> RequestSaveAndExitPreviewMode?.Invoke(this, EventArgs.Empty);

	/// <summary>
	/// Updates the style of the current selection.
	/// </summary>
	/// <param name="action">The action to be performed against the current text style./param>
	private void UpdateSelectionTextStyle(Action<RichTextStyle> action) {
		// Ignore changes if text style if preview mode is active and selection is not available because
		//   it will not be possible to restore the original text style when preview is canceled
		if (PreviewMode == RichTextBoxExtended.PreviewModeState.ActiveWithoutSelection)
			return;

		if (action is not null) {
			var textStyle = SelectionTextStyle.Clone();
			action.Invoke(textStyle);
			SelectionTextStyle = textStyle;
		}
	}

	/// <summary>
	/// Updates relevant instances within <see cref="BarManager.ControlViewModels"/> based on the current selection's text style.
	/// </summary>
	private void UpdateBarControlViewModelsFromSelection()
		=> UpdateBarControlViewModelsFromSelection(SelectionTextStyle);

	/// <summary>
	/// Updates relevant instances within <see cref="BarManager.ControlViewModels"/> based on the given text style.
	/// </summary>
	/// <param name="textStyle">The current select's text style.</param>
	private void UpdateBarControlViewModelsFromSelection(RichTextStyle textStyle) {
		if (PreviewMode != RichTextBoxExtended.PreviewModeState.None)
			return;

		if (BarManager.ControlViewModels[BarControlKeys.FontColorPicker] is BarGalleryViewModel fontColorGalleryViewModel)
			fontColorGalleryViewModel.SelectItemByValueMatch<ColorBarGalleryItemViewModel>(i => i.Value == textStyle.FontColor);

		if ((BarManager.ControlViewModels[BarControlKeys.Font] is BarComboBoxViewModel fontFamilyComboBoxViewModel) && !(string.IsNullOrEmpty(textStyle.FontFamilyName)))
			fontFamilyComboBoxViewModel.SelectItemByTextMatch<FontFamilyBarGalleryItemViewModel>(i => i.Value ?? string.Empty, textStyle.FontFamilyName!);

		if ((BarManager.ControlViewModels[BarControlKeys.FontSize] is BarComboBoxViewModel fontSizeComboBoxViewModel) && !(double.IsNaN(textStyle.FontSize)))
			fontSizeComboBoxViewModel.SelectItemByValueMatch<FontSizeBarGalleryItemViewModel>(i => i.Value == textStyle.FontSize, i => i.Value.ToString(), textStyle.FontSize.ToString());

		if (BarManager.ControlViewModels[BarControlKeys.TextHighlightColorPicker] is BarGalleryViewModel textHighlightColorGalleryViewModel)
			textHighlightColorGalleryViewModel.SelectItemByValueMatch<ColorBarGalleryItemViewModel>(i => i.Value == textStyle.TextHighlightColor);

		if (BarManager.ControlViewModels[BarControlKeys.QuickStylesGallery] is BarGalleryViewModel textStyleGalleryViewModel) {
			textStyleGalleryViewModel.SelectItemByValueMatch<TextStyleBarGalleryItemViewModel>(i =>
				i.Value is not null
				&& i.Value.Bold == textStyle.Bold
				&& i.Value.TextColor == textStyle.FontColor
				&& i.Value.FontFamilyName == textStyle.FontFamilyName
				&& i.Value.FontSize == textStyle.FontSize
				&& i.Value.Italic == textStyle.Italic
				&& i.Value.Underline == (textStyle.Underline == UnderlineKind.Underline)
			);
		}

		BarManager.UpdateControlViewModelCheckedState(BarControlKeys.Bold, () => textStyle.Bold);
		BarManager.UpdateControlViewModelCheckedState(BarControlKeys.Italic, () => textStyle.Italic);
		BarManager.UpdateControlViewModelCheckedState(BarControlKeys.Strikethrough, () => textStyle.Strikethrough);

		var underlineKind = textStyle.Underline;
		BarManager.UpdateControlViewModelCheckedState(BarControlKeys.Underline, () => underlineKind != UnderlineKind.None);
		if (BarManager.ControlViewModels[BarControlKeys.UnderlineGallery] is BarGalleryViewModel underlineGalleryViewModel) {
			underlineGalleryViewModel.SelectItemByValueMatch<UnderlineBarGalleryItemViewModel>(i =>
				i.Value == underlineKind
			);
		}

		BarManager.UpdateControlViewModelCheckedState(BarControlKeys.AlignLeft, () => textStyle.TextAlignment == TextAlignment.Left);
		BarManager.UpdateControlViewModelCheckedState(BarControlKeys.AlignCenter, () => textStyle.TextAlignment == TextAlignment.Center);
		BarManager.UpdateControlViewModelCheckedState(BarControlKeys.AlignRight, () => textStyle.TextAlignment == TextAlignment.Right);
		BarManager.UpdateControlViewModelCheckedState(BarControlKeys.AlignJustify, () => textStyle.TextAlignment == TextAlignment.Justify);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="FlowDocument"/> of rich text managed by this view model.
	/// </summary>
	public FlowDocument Document { get; } = document ?? throw new ArgumentNullException(nameof(document));

	/// <summary>
	/// Returns the items to be displayed in a context menu of a view for this document.
	/// </summary>
	public virtual IEnumerable<object> GetContextMenuItems() {
		yield return BarManager.ControlViewModels[BarControlKeys.Cut];
		yield return BarManager.ControlViewModels[BarControlKeys.Copy];
		yield return BarManager.ControlViewModels[BarControlKeys.Paste];
		yield return new BarSeparatorViewModel();
		yield return BarManager.ControlViewModels[BarControlKeys.SelectAll];
	}

	/// <inheritdoc/>
	protected override IEnumerable<KeyValuePair<CompositeCommand, ICommand>> GetCommandMappings(BarManager barManager) {
		return base.GetCommandMappings(barManager)
			.Concat(new Dictionary<CompositeCommand, ICommand>() {
				{ barManager.DecreaseFontSizeCommand, DecreaseFontSizeCommand },
				{ barManager.IncreaseFontSizeCommand, IncreaseFontSizeCommand },
				{ barManager.InsertSymbolCommand, InsertSymbolCommand },
				{ barManager.SetFontColorCommand, SetFontColorCommand },
				{ barManager.SetFontFamilyCommand, SetFontFamilyCommand },
				{ barManager.SetFontSizeCommand, SetFontSizeCommand },
				{ barManager.SetNumberingCommand, SetNumberingCommand },
				{ barManager.SetTextAlignmentCommand, SetTextAlignmentCommand },
				{ barManager.SetTextHighlightColorCommand, SetTextHighlightColorCommand },
				{ barManager.SetTextStyleCommand, SetTextStyleCommand },
				{ barManager.SetUnderlineCommand, SetUnderlineCommand },
				{ barManager.StopHighlightingCommand, StopHighlightingCommand },
				{ barManager.ToggleBoldCommand, ToggleBoldCommand },
				{ barManager.ToggleItalicCommand, ToggleItalicCommand },
				{ barManager.ToggleNumberingCommand, ToggleNumberingCommand },
				{ barManager.ToggleStrikethroughCommand, ToggleStrikethroughCommand},
				{ barManager.ToggleUnderlineCommand, ToggleUnderlineCommand },
				{ barManager.UnknownFontSizeCommand, UnknownFontSizeCommand },
			});
	}

	/// <summary>
	/// Returns the view model which defines the items to be displayed in a mini toolbar with the context menu of a view for this document.
	/// </summary>
	public virtual MiniToolBarViewModel GetMiniToolBar() {
		return new MiniToolBarViewModel() {
			CanUseMultiRowLayout = true,
			Items = {
				BarManager.ControlViewModels[BarControlKeys.Font],
				BarManager.ControlViewModels[BarControlKeys.FontSize],
				new BarSeparatorViewModel(),
				BarManager.ControlViewModels[BarControlKeys.IncreaseFontSize],
				BarManager.ControlViewModels[BarControlKeys.DecreaseFontSize],
				new BarSeparatorViewModel(),
				BarManager.ControlViewModels[BarControlKeys.ClearFormatting],
				new BarSeparatorViewModel(),
				BarManager.ControlViewModels[BarControlKeys.Bold],
				BarManager.ControlViewModels[BarControlKeys.Italic],
				BarManager.ControlViewModels[BarControlKeys.Underline],
				BarManager.ControlViewModels[BarControlKeys.Strikethrough],
				BarManager.ControlViewModels[BarControlKeys.Subscript],
				BarManager.ControlViewModels[BarControlKeys.Superscript],
				new BarSeparatorViewModel(),
				BarManager.ControlViewModels[BarControlKeys.TextHighlightColor],
				BarManager.ControlViewModels[BarControlKeys.FontColor],
			}
		};
	}

	/// <summary>
	/// The current preview preview mode.
	/// </summary>
	public RichTextBoxExtended.PreviewModeState PreviewMode {
		get => _previewMode;
		set => SetProperty(ref _previewMode, value);
	}

	/// <summary>
	/// The template selector to be used for bar controls defined by this view model.
	/// </summary>
	public BarControlTemplateSelector? ItemContainerTemplateSelector { get; set; }

	/// <inheritdoc/>
	protected override void OnCommandsRegistered() {
		base.OnCommandsRegistered();

		// Refresh view models after commands are registered
		UpdateBarControlViewModelsFromSelection();
	}

	/// <summary>
	/// The style of the current selection.
	/// </summary>
	public RichTextStyle SelectionTextStyle {
		get => _selectionTextStyle;
		set {
			if (SetProperty(ref _selectionTextStyle, value))
				UpdateBarControlViewModelsFromSelection(_selectionTextStyle);
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC COMMANDS
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The command to decrease font size.
	/// </summary>
	public ICommand DecreaseFontSizeCommand {
		get => _decreaseFontSizeCommand ??= new DelegateCommand<object>(
			p => {
				UpdateSelectionTextStyle(s => {
					var fontSize = s.FontSize;
					if (fontSize <= FontSizeChangeSmallStepThreshold)
						s.FontSize = Math.Max(1, fontSize - 1);
					else
						s.FontSize = (fontSize - 1).Round(RoundMode.FloorToEven);
				});
			}
		);
	}

	/// <summary>
	/// The command to increase font size.
	/// </summary>
	public ICommand IncreaseFontSizeCommand {
		get => _increaseFontSizeCommand ??= new DelegateCommand<object>(
			p => {
				UpdateSelectionTextStyle(s => {
					var fontSize = s.FontSize;
					if (fontSize < FontSizeChangeSmallStepThreshold)
						s.FontSize++;
					else
						s.FontSize = (fontSize + 1).Round(RoundMode.CeilingToEven);
				});
			}
		);
	}

	/// <summary>
	/// The command to insert a symbol.
	/// </summary>
	public ICommand InsertSymbolCommand
		=> _insertSymbolCommand ??= new DelegateCommand<SymbolBarGalleryItemViewModel>(p => OnRequestInsertText(p?.Value));

	/// <summary>
	/// The command to set a font color.
	/// </summary>
	public ICommand SetFontColorCommand {
		get => _setFontColorCommand ??= new PreviewableDelegateCommand<ColorBarGalleryItemViewModel>(
			executeAction: p => {
				OnRequestSaveAndExitPreviewMode();
				UpdateSelectionTextStyle(s => s.FontColor = p?.Value ?? DefaultFontForegroundPickerColor);

				if (BarManager.ControlViewModels[BarControlKeys.FontColor] is BarSplitButtonViewModel buttonViewModel) {
					buttonViewModel.CommandParameter = p;
					buttonViewModel.SmallImageSource = BarManager.ImageProvider.GetImageSource(BarControlKeys.FontColor, new BarImageOptions(BarImageSize.Small) { ContextualColor = SelectionTextStyle.FontColor });
				}
			},
			canExecuteFunc: _ => true,
			previewAction: p => {
				OnRequestActivatePreviewMode();
				UpdateSelectionTextStyle(s => s.FontColor = p?.Value ?? DefaultFontForegroundPickerColor);
			},
			cancelPreviewAction: _ => OnRequestCancelPreviewMode()
		);
	}

	/// <summary>
	/// The command to set a font family.
	/// </summary>
	public ICommand SetFontFamilyCommand {
		get => _setFontFamilyCommand ??= new PreviewableDelegateCommand<FontFamilyBarGalleryItemViewModel>(
			executeAction: p => {
				OnRequestSaveAndExitPreviewMode();
				UpdateSelectionTextStyle(s => s.FontFamilyName = p?.Value ?? FontSettings.DefaultFontFamilyName);
			},
			canExecuteFunc: _ => true,
			previewAction: p => {
				OnRequestActivatePreviewMode();
				UpdateSelectionTextStyle(s => s.FontFamilyName = p?.Value ?? FontSettings.DefaultFontFamilyName);
			},
			cancelPreviewAction: _ => OnRequestCancelPreviewMode()
		);
	}

	/// <summary>
	/// The command to set a font size.
	/// </summary>
	public ICommand SetFontSizeCommand {
		get => _setFontSizeCommand ??= new PreviewableDelegateCommand<FontSizeBarGalleryItemViewModel>(
			executeAction: p => {
				OnRequestSaveAndExitPreviewMode();
				UpdateSelectionTextStyle(s => s.FontSize = p?.Value ?? FontSettings.DefaultFontSize);
			},
			canExecuteFunc: _ => true,
			previewAction: p => {
				OnRequestActivatePreviewMode();
				UpdateSelectionTextStyle(s => s.FontSize = p?.Value ?? FontSettings.DefaultFontSize);
			},
			cancelPreviewAction: _ => OnRequestCancelPreviewMode()
		);
	}

	/// <summary>
	/// The command to set a numbering style.
	/// </summary>
	public ICommand SetNumberingCommand {
		get => _setNumberingCommand ??= new PreviewableDelegateCommand<NumberingBarGalleryItemViewModel>(
			executeAction: p => {
				OnRequestSaveAndExitPreviewMode();
				if (p is not null)
					ApplyNumbering(p);
			},
			canExecuteFunc: p => true,
			previewAction: p => {
				OnRequestActivatePreviewMode();
				if (p is not null)
					ApplyNumbering(p);
			},
			cancelPreviewAction: p => {
				OnRequestCancelPreviewMode();
			}
		);
	}

	/// <summary>
	/// The command to set text alignment.
	/// </summary>
	/// <value>An <see cref="ICommand"/>.</value>
	public ICommand SetTextAlignmentCommand {
		get => _setTextAlignmentCommand ??= new DelegateCommand<TextAlignment?>(p =>
			UpdateSelectionTextStyle(s => s.TextAlignment = p ?? TextAlignment.Left)
		);
	}

	/// <summary>
	/// The command to set text highlight color.
	/// </summary>
	public ICommand SetTextHighlightColorCommand {
		get => _setTextHighlightColorCommand ??= new PreviewableDelegateCommand<ColorBarGalleryItemViewModel>(
			executeAction: p => {
				OnRequestSaveAndExitPreviewMode();
				UpdateSelectionTextStyle(s => s.TextHighlightColor = p?.Value ?? DefaultFontBackgroundPickerColor);

				if (BarManager.ControlViewModels[BarControlKeys.TextHighlightColor] is BarSplitButtonViewModel buttonViewModel) {
					buttonViewModel.CommandParameter = p;
					buttonViewModel.SmallImageSource = BarManager.ImageProvider.GetImageSource(BarControlKeys.TextHighlightColor, new BarImageOptions(BarImageSize.Small) { ContextualColor = SelectionTextStyle.TextHighlightColor });
				}
			},
			canExecuteFunc: _ => true,
			previewAction: p => {
				OnRequestActivatePreviewMode();
				UpdateSelectionTextStyle(s => s.TextHighlightColor = p?.Value ?? DefaultFontBackgroundPickerColor);
			},
			cancelPreviewAction: _ => OnRequestCancelPreviewMode()
		);
	}

	/// <summary>
	/// The command to set a text style.
	/// </summary>
	public ICommand SetTextStyleCommand {
		get => _setTextStyleCommand ??= new PreviewableDelegateCommand<TextStyleBarGalleryItemViewModel>(
			executeAction: p => {
				OnRequestSaveAndExitPreviewMode();
				if (p?.Value is { } textStyle)
					ApplyTextStyle(textStyle);
			},
			canExecuteFunc: _ => true,
			previewAction: p => {
				OnRequestActivatePreviewMode();
				if (p?.Value is { } textStyle)
					ApplyTextStyle(textStyle);
			},
			cancelPreviewAction: _ => OnRequestCancelPreviewMode()
		);
	}

	/// <summary>
	/// The command to set an underline.
	/// </summary>
	public ICommand SetUnderlineCommand {
		get => _setUnderlineCommand ??= new PreviewableDelegateCommand<UnderlineBarGalleryItemViewModel>(
			executeAction: p => {
				OnRequestSaveAndExitPreviewMode();
				if (p is not null)
					ApplyUnderline(p);
			},
			canExecuteFunc: _ => true,
			previewAction: p => {
				OnRequestActivatePreviewMode();
				if (p is not null)
					ApplyUnderline(p);
			},
			cancelPreviewAction: _ => OnRequestCancelPreviewMode()
		);
	}

	/// <summary>
	/// The command to stop highlighting.
	/// </summary>
	public ICommand StopHighlightingCommand
		=> _stopHighlightingCommand ??= new DelegateCommand<object>(_ => OnRequestClearAllTextHighlights());

	/// <summary>
	/// The command to toggle bold font weight.
	/// </summary>
	public ICommand ToggleBoldCommand {
		get => _toggleBoldCommand ??= new DelegateCommand<object>(_ =>
			BarManager.SetValueFromControlViewModelCheckedState(
				BarControlKeys.Bold,
				isChecked => UpdateSelectionTextStyle(s => s.Bold = isChecked)
			)
		);
	}

	/// <summary>
	/// The command to toggle italic font style.
	/// </summary>
	public ICommand ToggleItalicCommand {
		get => _toggleItalicCommand ??= new DelegateCommand<object>(_ =>
			BarManager.SetValueFromControlViewModelCheckedState(
				BarControlKeys.Italic,
				isChecked => UpdateSelectionTextStyle(s => s.Italic = isChecked)
			)
		);
	}

	/// <summary>
	/// The command to toggle numbering style.
	/// </summary>
	public ICommand ToggleNumberingCommand {
		get => _toggleNumberingCommand ??= new DelegateCommand<object>(_ => {
			// This command has not been implemented
			BarManager.NotImplementedCommand.Execute(parameter: null);

			// Make sure the toggle button does not remain checked
			BarManager.UpdateControlViewModelCheckedState(BarControlKeys.Numbering, () => false);
		});
	}

	/// <summary>
	/// The command to toggle strike-through font style.
	/// </summary>
	public ICommand ToggleStrikethroughCommand {
		get => _toggleStrikethroughCommand ??= new DelegateCommand<object>(_ =>
			BarManager.SetValueFromControlViewModelCheckedState(
				BarControlKeys.Strikethrough,
				isChecked => UpdateSelectionTextStyle(s => s.Strikethrough = isChecked)
			)
		);
	}

	/// <summary>
	/// The command to toggle underline.
	/// </summary>
	public ICommand ToggleUnderlineCommand {
		get => _toggleUnderlineCommand ??= new DelegateCommand<object>(_ =>
			BarManager.SetValueFromControlViewModelCheckedState(
				BarControlKeys.Underline,
				isChecked => UpdateSelectionTextStyle(s => s.Underline = (isChecked ? UnderlineKind.Underline : UnderlineKind.None))
			)
		);
	}

	/// <summary>
	/// The command raised to handle an unknown font size.
	/// </summary>
	public ICommand UnknownFontSizeCommand {
		get => _unknownFontSizeCommand ??= new DelegateCommand<string>(
			executeAction: p => {
				if (int.TryParse(p, out var fontSize))
					UpdateSelectionTextStyle(x => x.FontSize = fontSize);
			},
			canExecuteFunc: p => int.TryParse(p, out var fontSize) && fontSize.IsBetween(byte.MinValue, byte.MaxValue)
		);
	}

}
