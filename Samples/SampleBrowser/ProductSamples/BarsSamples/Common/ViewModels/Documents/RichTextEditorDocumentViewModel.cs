using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Data;
using ActiproSoftware.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Represents a view model for a document of richly-formatted text.
	/// </summary>
	public class RichTextEditorDocumentViewModel : DocumentViewModel {

		private static Color DefaultFontBackgroundPickerColor = Colors.Yellow;
		private static Color DefaultFontForegroundPickerColor = Colors.Red;

		private const double FontSizeChangeSmallStepThreshold = 12.0;

		private ICommand decreaseFontSizeCommand;
		private ICommand increaseFontSizeCommand;
		private ICommand insertSymbolCommand;
		private RichTextBoxExtended.PreviewModeState previewMode;
		private RichTextStyle selectionTextStyle = new RichTextStyle();
		private ICommand setFontColorCommand;
		private ICommand setFontFamilyCommand;
		private ICommand setFontSizeCommand;
		private ICommand setNumberingCommand;
		private ICommand setTextAlignmentCommand;
		private ICommand setTextHighlightColorCommand;
		private ICommand setTextStyleCommand;
		private ICommand setUnderlineCommand;
		private ICommand stopHighlightingCommand;
		private ICommand toggleBoldCommand;
		private ICommand toggleItalicCommand;
		private ICommand toggleNumberingCommand;
		private ICommand toggleStrikethroughCommand;
		private ICommand toggleUnderlineCommand;
		private ICommand unknownFontSizeCommand;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// EVENTS
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Raised to request that the view activates preview mode.
		/// </summary>
		public event EventHandler RequestActivatePreviewMode;

		/// <summary>
		/// Raised to request that the view discards the current state and exits preview model.
		/// </summary>
		public event EventHandler RequestCancelPreviewMode;

		/// <summary>
		/// Raised to request that the view inserts text.
		/// </summary>
		public event EventHandler<string> RequestInsertText;

		/// <summary>
		/// Raised to request that the view saves the current state and exits preview model.
		/// </summary>
		public event EventHandler RequestSaveAndExitPreviewMode;

		/// <summary>
		/// Raised to request that the view clears all text highlights.
		/// </summary>
		public event EventHandler RequestClearAllTextHighlights;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="DocumentViewModel(BarManager)"/>
		/// <param name="document">The <see cref="FlowDocument"/> of rich text to be managed by the view model.</param>
		public RichTextEditorDocumentViewModel(BarManager barManager, FlowDocument document)
			: base(barManager) {

			this.Document = document ?? throw new ArgumentNullException(nameof(document));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Applies numbering to the current selection.
		/// </summary>
		/// <param name="viewModel">The view model which defines the number to apply.</param>
		private void ApplyNumbering(NumberingBarGalleryItemViewModel viewModel) {
			// Numbering is not supported by this application and this method stub is included
			// only for demonstration purposes of how an application might implement
			// applying a gallery item for numbering
			if (PreviewMode == RichTextBoxExtended.PreviewModeState.None) {
				ThemedMessageBox.Show(
					$"The numbering kind '{viewModel.Kind}' is for user interface demonstration purposes only and no application functionality has been implemented for it.", "Numbering Not Implemented",
					MessageBoxButton.OK, MessageBoxImage.Information);

				// Since selecting one of the numbering gallery items will automatically change the selection to that
				// item, make sure the view model selection is reset to reflect that numbering is not active
				this.BarManager.UpdateControlViewModelCheckedState(BarControlKeys.Numbering, () => false);
				if (this.BarManager.ControlViewModels[BarControlKeys.NumberingGallery] is BarGalleryViewModel numberingGalleryViewModel) {
					numberingGalleryViewModel.SelectItemByValueMatch<NumberingBarGalleryItemViewModel>(i =>
						i.Kind == NumberingKind.None
					);
				}
			}
		}

		/// <summary>
		/// Applies a text style to the current selection.
		/// </summary>
		/// <param name="viewModel">The view model which defines the text style to apply.</param>
		private void ApplyTextStyle(TextStyleBarGalleryItemViewModel viewModel) {
			UpdateSelectionTextStyle(x => {
				x.Bold = viewModel.Bold;
				x.FontColor = viewModel.Color;
				x.FontFamilyName = viewModel.FontFamilyName;
				x.FontSize = viewModel.FontSize;
				x.Italic = viewModel.Italic;
				x.Underline = viewModel.Underline ? UnderlineKind.Underline : UnderlineKind.None;
			});
		}

		/// <summary>
		/// Applies an underline to the current selection.
		/// </summary>
		/// <param name="viewModel">The view model which defines the underline style to apply.</param>
		private void ApplyUnderline(UnderlineBarGalleryItemViewModel viewModel) {
			// Only standard underline is supported by this application and the other underline kinds
			// are for demonstration purposes only
			if ((viewModel.Kind == UnderlineKind.None) || (viewModel.Kind == UnderlineKind.Underline)) {
				UpdateSelectionTextStyle(x => x.Underline = viewModel.Kind);
			}
			else if (PreviewMode == RichTextBoxExtended.PreviewModeState.None) {
				// Provide feedback that the selected item is not supported
				ThemedMessageBox.Show(
					$"The underline kind '{viewModel.Kind}' is for user interface demonstration purposes only and no application functionality has been implemented for it.", "Underline Not Implemented",
					MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		/// <summary>
		/// Raises the <see cref="RequestActivatePreviewMode"/> event.
		/// </summary>
		private void OnRequestActivatePreviewMode() {
			this.RequestActivatePreviewMode?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// Raises the <see cref="RequestCancelPreviewMode"/> event.
		/// </summary>
		private void OnRequestCancelPreviewMode() {
			this.RequestCancelPreviewMode?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// Raises the <see cref="RequestClearAllTextHighlights"/> event.
		/// </summary>
		private void OnRequestClearAllTextHighlights() {
			this.RequestClearAllTextHighlights?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// Raises the <see cref="RequestInsertText"/> event.
		/// </summary>
		private void OnRequestInsertText(string text) {
			this.RequestInsertText?.Invoke(this, text);
		}

		/// <summary>
		/// Raises the <see cref="RequestSaveAndExitPreviewMode"/> event.
		/// </summary>
		private void OnRequestSaveAndExitPreviewMode() {
			this.RequestSaveAndExitPreviewMode?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// Updates the style of the current selection.
		/// </summary>
		/// <param name="action">The action to be performed against the current text style./param>
		private void UpdateSelectionTextStyle(Action<RichTextStyle> action) {
			// Ignore changes if text style if preview mode is active and selection is not available because
			// it will not be possible to restore the original text style when preview is canceled
			if (PreviewMode == RichTextBoxExtended.PreviewModeState.ActiveWithoutSelection)
				return;

			if (action != null) {
				var textStyle = this.SelectionTextStyle.Clone();
				action.Invoke(textStyle);
				this.SelectionTextStyle = textStyle;
			}
		}

		/// <summary>
		/// Updates relevant instances within <see cref="BarManager.ControlViewModels"/> based on the current selection's text style.
		/// </summary>
		private void UpdateBarControlViewModelsFromSelection() => UpdateBarControlViewModelsFromSelection(SelectionTextStyle);

		/// <summary>
		/// Updates relevant instances within <see cref="BarManager.ControlViewModels"/> based on the given text style.
		/// </summary>
		/// <param name="textStyle">The current select's text style.</param>
		private void UpdateBarControlViewModelsFromSelection(RichTextStyle textStyle) {
			if (PreviewMode != RichTextBoxExtended.PreviewModeState.None)
				return;

			if (this.BarManager.ControlViewModels[BarControlKeys.FontColorPicker] is BarGalleryViewModel fontColorGalleryViewModel)
				fontColorGalleryViewModel.SelectItemByValueMatch<ColorBarGalleryItemViewModel>(i => i.Color == textStyle.FontColor);

			if ((this.BarManager.ControlViewModels[BarControlKeys.Font] is BarComboBoxViewModel fontFamilyComboBoxViewModel) && !(string.IsNullOrEmpty(textStyle.FontFamilyName)))
				fontFamilyComboBoxViewModel.SelectItemByValueMatch<FontFamilyBarGalleryItemViewModel>(i => i.Name == textStyle.FontFamilyName, textStyle.FontFamilyName);

			if ((this.BarManager.ControlViewModels[BarControlKeys.FontSize] is BarComboBoxViewModel fontSizeComboBoxViewModel) && !(double.IsNaN(textStyle.FontSize)))
				fontSizeComboBoxViewModel.SelectItemByValueMatch<FontSizeBarGalleryItemViewModel>(i => i.Size == textStyle.FontSize, textStyle.FontSize.ToString());

			if (this.BarManager.ControlViewModels[BarControlKeys.TextHighlightColorPicker] is BarGalleryViewModel textHighlightColorGalleryViewModel)
				textHighlightColorGalleryViewModel.SelectItemByValueMatch<ColorBarGalleryItemViewModel>(i => i.Color == textStyle.TextHighlightColor);

			if (this.BarManager.ControlViewModels[BarControlKeys.QuickStylesGallery] is BarGalleryViewModel textStyleGalleryViewModel) {
				textStyleGalleryViewModel.SelectItemByValueMatch<TextStyleBarGalleryItemViewModel>(i =>
					i.Bold == textStyle.Bold
					&& i.Color == textStyle.FontColor
					&& i.FontFamilyName == textStyle.FontFamilyName
					&& i.FontSize == textStyle.FontSize
					&& i.Italic == textStyle.Italic
					&& i.Underline == (textStyle.Underline == UnderlineKind.Underline)
				);
			}

			this.BarManager.UpdateControlViewModelCheckedState(BarControlKeys.Bold, () => textStyle.Bold);
			this.BarManager.UpdateControlViewModelCheckedState(BarControlKeys.Italic, () => textStyle.Italic);
			this.BarManager.UpdateControlViewModelCheckedState(BarControlKeys.Strikethrough, () => textStyle.Strikethrough);

			var underlineKind = textStyle.Underline;
			this.BarManager.UpdateControlViewModelCheckedState(BarControlKeys.Underline, () => underlineKind != UnderlineKind.None);
			if (this.BarManager.ControlViewModels[BarControlKeys.UnderlineGallery] is BarGalleryViewModel underlineGalleryViewModel) {
				underlineGalleryViewModel.SelectItemByValueMatch<UnderlineBarGalleryItemViewModel>(i =>
					i.Kind == underlineKind
				);
			}

			this.BarManager.UpdateControlViewModelCheckedState(BarControlKeys.AlignLeft, () => textStyle.TextAlignment == TextAlignment.Left);
			this.BarManager.UpdateControlViewModelCheckedState(BarControlKeys.AlignCenter, () => textStyle.TextAlignment == TextAlignment.Center);
			this.BarManager.UpdateControlViewModelCheckedState(BarControlKeys.AlignRight, () => textStyle.TextAlignment == TextAlignment.Right);
			this.BarManager.UpdateControlViewModelCheckedState(BarControlKeys.AlignJustify, () => textStyle.TextAlignment == TextAlignment.Justify);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the <see cref="FlowDocument"/> of rich text managed by this view model.
		/// </summary>
		public FlowDocument Document { get; }

		/// <summary>
		/// Gets the items to be displayed in a context menu of a view for this document.
		/// </summary>
		/// <returns>An <see cref="IEnumerable{T}"/> of view models for each object to be displayed in the context menu.</returns>
		public IEnumerable<object> GetContextMenuItems() {
			yield return this.BarManager.ControlViewModels[BarControlKeys.Cut];
			yield return this.BarManager.ControlViewModels[BarControlKeys.Copy];
			yield return this.BarManager.ControlViewModels[BarControlKeys.Paste];
			yield return new BarSeparatorViewModel();
			yield return this.BarManager.ControlViewModels[BarControlKeys.SelectAll];
			yield return new BarSeparatorViewModel();
			yield return this.BarManager.ControlViewModels[BarControlKeys.FontColor];
			yield return this.BarManager.ControlViewModels[BarControlKeys.TextHighlightColor];
		}

		/// <inheritdoc/>
		protected override IEnumerable<KeyValuePair<CompositeCommand, ICommand>> GetCommandMappings(BarManager barManager) {
			return base.GetCommandMappings(barManager).Concat(new Dictionary<CompositeCommand, ICommand>() {
				{ barManager.DecreaseFontSizeCommand, this.DecreaseFontSizeCommand },
				{ barManager.IncreaseFontSizeCommand, this.IncreaseFontSizeCommand },
				{ barManager.InsertSymbolCommand, this.InsertSymbolCommand },
				{ barManager.SetFontColorCommand, this.SetFontColorCommand },
				{ barManager.SetFontFamilyCommand, this.SetFontFamilyCommand },
				{ barManager.SetFontSizeCommand, this.SetFontSizeCommand },
				{ barManager.SetNumberingCommand, this.SetNumberingCommand },
				{ barManager.SetTextAlignmentCommand, this.SetTextAlignmentCommand },
				{ barManager.SetTextHighlightColorCommand, this.SetTextHighlightColorCommand },
				{ barManager.SetTextStyleCommand, this.SetTextStyleCommand },
				{ barManager.SetUnderlineCommand, this.SetUnderlineCommand },
				{ barManager.StopHighlightingCommand, this.StopHighlightingCommand },
				{ barManager.ToggleBoldCommand, this.ToggleBoldCommand },
				{ barManager.ToggleItalicCommand, this.ToggleItalicCommand },
				{ barManager.ToggleNumberingCommand, this.ToggleNumberingCommand },
				{ barManager.ToggleStrikethroughCommand, this.ToggleStrikethroughCommand},
				{ barManager.ToggleUnderlineCommand, this.ToggleUnderlineCommand },
				{ barManager.UnknownFontSizeCommand, this.UnknownFontSizeCommand },
			});
		}

		/// <summary>
		/// Gets or sets the current preview preview mode.
		/// </summary>
		/// <value>
		/// One of the <see cref="RichTextBoxExtended.PreviewModeState"/> values.
		/// </value>
		public RichTextBoxExtended.PreviewModeState PreviewMode {
			get => previewMode;
			set {
				if (previewMode != value) {
					previewMode = value;

					NotifyPropertyChanged(nameof(PreviewMode));
				}
			}
		}

		/// <summary>
		/// Gets or sets the template selector to be used for bar controls defined by this view model.
		/// </summary>
		/// <value>A <see cref="BarControlTemplateSelector"/>.</value>
		public BarControlTemplateSelector ItemContainerTemplateSelector { get; set; }

		/// <inheritdoc/>
		protected override void OnCommandsRegistered() {
			base.OnCommandsRegistered();

			// Refresh view models after commands are registered
			UpdateBarControlViewModelsFromSelection();
		}

		/// <summary>
		/// Gets or sets the style of the current selection.
		/// </summary>
		/// <value>A <see cref="RichTextStyle"/>.</value>
		public RichTextStyle SelectionTextStyle {
			get => selectionTextStyle;
			set {
				if (!selectionTextStyle.Equals(value)) {
					selectionTextStyle = value;
					UpdateBarControlViewModelsFromSelection(selectionTextStyle);

					NotifyPropertyChanged(nameof(SelectionTextStyle));
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC COMMANDS
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the command to decrease font size.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand DecreaseFontSizeCommand {
			get {
				if (decreaseFontSizeCommand is null) {
					decreaseFontSizeCommand = new DelegateCommand<object>(
						p => {
							UpdateSelectionTextStyle(x => {
								var fontSize = x.FontSize;
								if (fontSize <= FontSizeChangeSmallStepThreshold)
									x.FontSize = Math.Max(1, fontSize - 1);
								else
									x.FontSize = MathHelper.Round(RoundMode.FloorToEven, fontSize - 1);
							});
						}
					);
				}
				return decreaseFontSizeCommand;
			}
		}

		/// <summary>
		/// Gets the command to increase font size.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand IncreaseFontSizeCommand {
			get {
				if (increaseFontSizeCommand is null) {
					increaseFontSizeCommand = new DelegateCommand<object>(
						p => {
							UpdateSelectionTextStyle(x => {
								var fontSize = x.FontSize;
								if (fontSize < FontSizeChangeSmallStepThreshold)
									x.FontSize++;
								else
									x.FontSize = MathHelper.Round(RoundMode.CeilingToEven, fontSize + 1);
							});
						}
					);
				}
				return increaseFontSizeCommand;
			}
		}

		/// <summary>
		/// Gets the command to insert a symbol.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand InsertSymbolCommand {
			get {
				if (insertSymbolCommand is null) {
					insertSymbolCommand = new DelegateCommand<SymbolBarGalleryItemViewModel>(
						p => OnRequestInsertText(p.Symbol)
					);
				}
				return insertSymbolCommand;
			}
		}

		/// <summary>
		/// Gets the command to set a font color.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand SetFontColorCommand {
			get {
				if (setFontColorCommand is null) {
					setFontColorCommand = new PreviewableDelegateCommand<ColorBarGalleryItemViewModel>(
						executeAction: p => {
							OnRequestSaveAndExitPreviewMode();
							UpdateSelectionTextStyle(x => x.FontColor = p?.Color ?? DefaultFontForegroundPickerColor);

							if (this.BarManager.ControlViewModels[BarControlKeys.FontColor] is BarSplitButtonViewModel buttonViewModel) {
								buttonViewModel.CommandParameter = p;
								buttonViewModel.SmallImageSource = BarManager.ImageProvider.GetImageSource(BarControlKeys.FontColor, new BarImageOptions(BarImageSize.Small) { ContextualColor = this.SelectionTextStyle.FontColor });
							}
						},
						canExecuteFunc: p => true,
						previewAction: p => {
							OnRequestActivatePreviewMode();
							UpdateSelectionTextStyle(x => x.FontColor = p?.Color ?? DefaultFontForegroundPickerColor);
						},
						cancelPreviewAction: p => {
							OnRequestCancelPreviewMode();
						}
					);
				}
				return setFontColorCommand;
			}
		}

		/// <summary>
		/// Gets the command to set a font family.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand SetFontFamilyCommand {
			get {
				if (setFontFamilyCommand is null) {
					setFontFamilyCommand = new PreviewableDelegateCommand<FontFamilyBarGalleryItemViewModel>(
						executeAction: p => {
							OnRequestSaveAndExitPreviewMode();
							UpdateSelectionTextStyle(x => x.FontFamilyName = p?.Name ?? FontSettings.DefaultFontFamilyName);
						},
						canExecuteFunc: p => true,
						previewAction: p => {
							OnRequestActivatePreviewMode();
							UpdateSelectionTextStyle(x => x.FontFamilyName = p?.Name ?? FontSettings.DefaultFontFamilyName);
						},
						cancelPreviewAction: p => {
							OnRequestCancelPreviewMode();
						}
					);
				}
				return setFontFamilyCommand;
			}
		}

		/// <summary>
		/// Gets the command to set a font size.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand SetFontSizeCommand {
			get {
				if (setFontSizeCommand is null) {
					setFontSizeCommand = new PreviewableDelegateCommand<FontSizeBarGalleryItemViewModel>(
						executeAction: p => {
							OnRequestSaveAndExitPreviewMode();
							UpdateSelectionTextStyle(x => x.FontSize = p?.Size ?? FontSettings.DefaultFontSize);
						},
						canExecuteFunc: p => true,
						previewAction: p => {
							OnRequestActivatePreviewMode();
							UpdateSelectionTextStyle(x => x.FontSize = p?.Size ?? FontSettings.DefaultFontSize);
						},
						cancelPreviewAction: p => {
							OnRequestCancelPreviewMode();
						}
					);
				}
				return setFontSizeCommand;
			}
		}

		/// <summary>
		/// Gets the command to set a numbering style.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand SetNumberingCommand {
			get {
				if (setNumberingCommand is null) {
					setNumberingCommand = new PreviewableDelegateCommand<NumberingBarGalleryItemViewModel>(
						executeAction: p => {
							OnRequestSaveAndExitPreviewMode();
							this.ApplyNumbering(p);
						},
						canExecuteFunc: p => true,
						previewAction: p => {
							OnRequestActivatePreviewMode();
							this.ApplyNumbering(p);
						},
						cancelPreviewAction: p => {
							OnRequestCancelPreviewMode();
						}
					);
				}
				return setNumberingCommand;
			}
		}

		/// <summary>
		/// Gets the command to set text alignment.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand SetTextAlignmentCommand {
			get {
				if (setTextAlignmentCommand is null) {
					setTextAlignmentCommand = new DelegateCommand<TextAlignment?>(
						executeAction: p => {
							UpdateSelectionTextStyle(x => x.TextAlignment = p ?? TextAlignment.Left);
						}
					);
				}
				return setTextAlignmentCommand;
			}
		}

		/// <summary>
		/// Gets the command to set text highlight color.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand SetTextHighlightColorCommand {
			get {
				if (setTextHighlightColorCommand is null) {
					setTextHighlightColorCommand = new PreviewableDelegateCommand<ColorBarGalleryItemViewModel>(
						executeAction: p => {
							OnRequestSaveAndExitPreviewMode();
							UpdateSelectionTextStyle(x => x.TextHighlightColor = p?.Color ?? DefaultFontBackgroundPickerColor);

							if (this.BarManager.ControlViewModels[BarControlKeys.TextHighlightColor] is BarSplitButtonViewModel buttonViewModel) {
								buttonViewModel.CommandParameter = p;
								buttonViewModel.SmallImageSource = BarManager.ImageProvider.GetImageSource(BarControlKeys.TextHighlightColor, new BarImageOptions(BarImageSize.Small) { ContextualColor = this.SelectionTextStyle.TextHighlightColor });
							}
						},
						canExecuteFunc: p => true,
						previewAction: p => {
							OnRequestActivatePreviewMode();
							UpdateSelectionTextStyle(x => x.TextHighlightColor = p?.Color ?? DefaultFontBackgroundPickerColor);
						},
						cancelPreviewAction: p => {
							OnRequestCancelPreviewMode();
						}
					);
				}
				return setTextHighlightColorCommand;
			}
		}

		/// <summary>
		/// Gets the command to set a text style.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand SetTextStyleCommand {
			get {
				if (setTextStyleCommand is null) {
					setTextStyleCommand = new PreviewableDelegateCommand<TextStyleBarGalleryItemViewModel>(
						executeAction: p => {
							OnRequestSaveAndExitPreviewMode();
							this.ApplyTextStyle(p);
						},
						canExecuteFunc: p => true,
						previewAction: p => {
							OnRequestActivatePreviewMode();
							this.ApplyTextStyle(p);
						},
						cancelPreviewAction: p => {
							OnRequestCancelPreviewMode();
						}
					);
				}
				return setTextStyleCommand;
			}
		}

		/// <summary>
		/// Gets the command to set an underline.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand SetUnderlineCommand {
			get {
				if (setUnderlineCommand is null) {
					setUnderlineCommand = new PreviewableDelegateCommand<UnderlineBarGalleryItemViewModel>(
						executeAction: p => {
							OnRequestSaveAndExitPreviewMode();
							this.ApplyUnderline(p);
						},
						canExecuteFunc: p => true,
						previewAction: p => {
							OnRequestActivatePreviewMode();
							this.ApplyUnderline(p);
						},
						cancelPreviewAction: p => {
							OnRequestCancelPreviewMode();
						}
					);
				}
				return setUnderlineCommand;
			}
		}

		/// <summary>
		/// Gets the command to stop highlighting.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand StopHighlightingCommand {
			get {
				if (stopHighlightingCommand is null) {
					stopHighlightingCommand = new DelegateCommand<object>(
						p => OnRequestClearAllTextHighlights()
					);
				}
				return stopHighlightingCommand;
			}
		}

		/// <summary>
		/// Gets the command to toggle bold font weight.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand ToggleBoldCommand {
			get {
				if (toggleBoldCommand is null) {
					toggleBoldCommand = new DelegateCommand<object>(
						p => this.BarManager.SetValueFromControlViewModelCheckedState(BarControlKeys.Bold,
							isChecked => this.UpdateSelectionTextStyle(x => x.Bold = isChecked))
					);
				}
				return toggleBoldCommand;
			}
		}

		/// <summary>
		/// Gets the command to toggle italic font style.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand ToggleItalicCommand {
			get {
				if (toggleItalicCommand is null) {
					toggleItalicCommand = new DelegateCommand<object>(
						p => this.BarManager.SetValueFromControlViewModelCheckedState(BarControlKeys.Italic,
							isChecked => this.UpdateSelectionTextStyle(x => x.Italic = isChecked))
					);
				}
				return toggleItalicCommand;
			}
		}

		/// <summary>
		/// Gets the command to toggle numbering style.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand ToggleNumberingCommand {
			get {
				if (toggleNumberingCommand is null) {
					toggleNumberingCommand = new DelegateCommand<object>(
						p => {
							// This command has not been implemented
							this.BarManager.NotImplementedCommand.Execute(null);

							// Make sure the toggle button does not remain checked
							this.BarManager.UpdateControlViewModelCheckedState(BarControlKeys.Numbering, () => false);
						}
					);
				}
				return toggleNumberingCommand;
			}
		}

		/// <summary>
		/// Gets the command to toggle strike-through font style.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand ToggleStrikethroughCommand {
			get {
				if (toggleStrikethroughCommand is null) {
					toggleStrikethroughCommand = new DelegateCommand<object>(
						p => this.BarManager.SetValueFromControlViewModelCheckedState(BarControlKeys.Strikethrough,
							isChecked => this.UpdateSelectionTextStyle(x => x.Strikethrough = isChecked))
					);
				}
				return toggleStrikethroughCommand;
			}
		}

		/// <summary>
		/// Gets the command to toggle underline.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand ToggleUnderlineCommand {
			get {
				if (toggleUnderlineCommand is null) {
					toggleUnderlineCommand = new DelegateCommand<object>(
						p => this.BarManager.SetValueFromControlViewModelCheckedState(BarControlKeys.Underline,
							isChecked => this.UpdateSelectionTextStyle(x => x.Underline = (isChecked ? UnderlineKind.Underline : UnderlineKind.None)))
					);
				}
				return toggleUnderlineCommand;
			}
		}

		/// <summary>
		/// Gets the command raised to handle an unknown font size.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand UnknownFontSizeCommand {
			get {
				if (unknownFontSizeCommand is null) {
					unknownFontSizeCommand = new DelegateCommand<string>(
						executeAction: p => {
							if (int.TryParse(p, out var fontSize))
								UpdateSelectionTextStyle(x => x.FontSize = fontSize);
						},
						canExecuteFunc: p => int.TryParse(p, out var fontSize) && (byte.MinValue <= fontSize) && (fontSize <= byte.MaxValue)
					);
				}
				return unknownFontSizeCommand;
			}
		}

	}
}
