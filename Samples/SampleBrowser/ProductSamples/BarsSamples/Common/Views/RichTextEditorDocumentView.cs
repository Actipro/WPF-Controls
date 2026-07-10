using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Controls.Bars.Primitives;
using System.Windows.Documents;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common;

/// <summary>
/// Defines the view of a rich text editor.
/// </summary>
public partial class RichTextEditorDocumentView : RichTextBoxExtended {

	private RichTextEditorDocumentViewModel? _viewModel;

	#region Dependency Properties

	public static readonly DependencyProperty RootBarControlProperty = BarControlService.RootBarControlProperty.AddOwner(typeof(RichTextEditorDocumentView));

	#endregion Dependency Properties

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public RichTextEditorDocumentView() {
		// Listen for changes in the DataContext to update the view model
		DataContextChanged += OnDataContextChanged;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Applies a text style to the current selection.
	/// </summary>
	/// <param name="textStyle">The text style to apply.</param>
	private void ApplySelectionTextStyle(RichTextStyle textStyle) {
		// Weight, style, alignment
		SelectionBold = textStyle.Bold;
		SelectionItalic = textStyle.Italic;
		SelectionTextAlignment = textStyle.TextAlignment;

		// Font
		SelectionFontFamilyName = textStyle.FontFamilyName;
		SelectionFontSize = FontSizeBarGalleryItemViewModel.ConvertFontSizeToWpfFontSize(textStyle.FontSize);

		// Colors
		SelectionFontColor = textStyle.FontColor;
		SelectionTextHighlightColor = textStyle.TextHighlightColor;

		// Text decorations
		SelectionStrikethrough = textStyle.Strikethrough;
		SelectionUnderline = (textStyle.Underline == UnderlineKind.Underline);
	}

	/// <summary>
	/// Returns the text style of the current selection.
	/// </summary>
	private RichTextStyle GetSelectionTextStyle() {
		return new RichTextStyle() {
			Bold = SelectionBold,
			FontColor = SelectionFontColor,
			FontFamilyName = SelectionFontFamilyName,
			FontSize = FontSizeBarGalleryItemViewModel.ConvertFontSizeFromWpfFontSize(SelectionFontSize),
			Italic = SelectionItalic,
			Strikethrough = SelectionStrikethrough,
			TextAlignment = SelectionTextAlignment,
			TextHighlightColor = SelectionTextHighlightColor,
			Underline = (SelectionUnderline ? UnderlineKind.Underline : UnderlineKind.None), // Sample only supports basic underline
		};
	}

	/// <summary>
	/// Called when the <c>DataContext</c> property value changes.
	/// </summary>
	private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		=> ViewModel = e.NewValue as RichTextEditorDocumentViewModel;

	/// <summary>
	/// Called when the value of a property of the associated view model changes.
	/// </summary>
	private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (e.PropertyName == nameof(RichTextEditorDocumentViewModel.SelectionTextStyle)) {
			if (ViewModel is { } viewModel)
				ApplySelectionTextStyle(viewModel.SelectionTextStyle);
		}
	}

	/// <summary>
	/// Called when the associated view model requests to activate preview mode.
	/// </summary>
	private void OnViewModelRequestActivatePreviewMode(object? sender, EventArgs e) {
		if (!IsPreviewModeActive && (ViewModel is { } viewModel)) {
			ActivatePreviewMode();
			viewModel.PreviewMode = PreviewMode;
		}
	}

	/// <summary>
	/// Called when the associated view model requests to cancel preview mode.
	/// </summary>
	private void OnViewModelRequestCancelPreviewMode(object? sender, EventArgs e) {
		if (IsPreviewModeActive && (ViewModel is { } viewModel)) {
			DeactivatePreviewMode(restoreOldSettings: true);
			viewModel.PreviewMode = PreviewMode;
		}
	}

	/// <summary>
	/// Called when the associated view model requests to clear all text highlights.
	/// </summary>
	private void OnViewModelRequestClearAllTextHighlights(object? sender, EventArgs e)
		=> ClearAllTextHighlights();

	/// <summary>
	/// Called when the associated view model requests to insert text.
	/// </summary>
	private void OnViewModelRequestInsertText(object? sender, string text)
		=> ReplaceText(text);

	/// <summary>
	/// Called when the associated view model requests to save and exit preview mode.
	/// </summary>
	private void OnViewModelRequestSaveAndExitPreviewMode(object? sender, EventArgs e) {
		if (IsPreviewModeActive && (ViewModel is { } viewModel)) {
			DeactivatePreviewMode(restoreOldSettings: false);
			viewModel.PreviewMode = PreviewMode;
		}
	}

	/// <summary>
	/// Replaces the current selection with the given text.
	/// </summary>
	/// <param name="text">The new text.</param>
	private void ReplaceText(string text) {
		Selection.Text = text;
		CaretPosition = CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
	}

	/// <summary>
	/// The view model associated with this view.
	/// </summary>
	private RichTextEditorDocumentViewModel? ViewModel {
		get => _viewModel;
		set {
			if (_viewModel != value) {
				if (_viewModel is not null) {
					_viewModel.PropertyChanged -= OnViewModelPropertyChanged;
					_viewModel.RequestActivatePreviewMode -= OnViewModelRequestActivatePreviewMode;
					_viewModel.RequestCancelPreviewMode -= OnViewModelRequestCancelPreviewMode;
					_viewModel.RequestClearAllTextHighlights -= OnViewModelRequestClearAllTextHighlights;
					_viewModel.RequestInsertText -= OnViewModelRequestInsertText;
					_viewModel.RequestSaveAndExitPreviewMode -= OnViewModelRequestSaveAndExitPreviewMode;
				}

				_viewModel = value;

				if (_viewModel is not null) {
					_viewModel.PropertyChanged += OnViewModelPropertyChanged;
					_viewModel.RequestActivatePreviewMode += OnViewModelRequestActivatePreviewMode;
					_viewModel.RequestCancelPreviewMode += OnViewModelRequestCancelPreviewMode;
					_viewModel.RequestClearAllTextHighlights += OnViewModelRequestClearAllTextHighlights;
					_viewModel.RequestInsertText += OnViewModelRequestInsertText;
					_viewModel.RequestSaveAndExitPreviewMode += OnViewModelRequestSaveAndExitPreviewMode;

					// Update the editor with the view model's document and reset selection
					Document = _viewModel.Document;
					ResetSelection();

					// Configure the context menu
					var contextMenu = new BarContextMenu() {
						ItemContainerTemplateSelector = _viewModel.ItemContainerTemplateSelector,
					};
					BarControlService.SetRootBarControl(contextMenu, RootBarControl);
					foreach (var item in _viewModel.GetContextMenuItems())
						contextMenu.Items.Add(item);
					if (_viewModel.GetMiniToolBar() is { Items.Count: > 0 } miniToolBar)
						contextMenu.MiniToolBarContent = miniToolBar;
					if (contextMenu.Items.Count > 0)
						ContextMenu = contextMenu;
				}
			}
		}
	}

	/// <inheritdoc/>
	protected override void OnSelectionChanged(RoutedEventArgs e) {
		base.OnSelectionChanged(e);

		// Synchronize the current selection with the view model
		if (ViewModel is { } viewModel)
			viewModel.SelectionTextStyle = GetSelectionTextStyle();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="IRootBarControl"/> associated with the view.
	/// </summary>
	public IRootBarControl? RootBarControl {
		get => (IRootBarControl)GetValue(RootBarControlProperty);
		set => SetValue(RootBarControlProperty, value);
	}

}
