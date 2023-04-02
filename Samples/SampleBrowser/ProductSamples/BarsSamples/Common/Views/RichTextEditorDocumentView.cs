using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Controls.Bars.Primitives;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Defines the view of a rich text editor.
	/// </summary>
	public partial class RichTextEditorDocumentView : RichTextBoxExtended {

		private RichTextEditorDocumentViewModel viewModel;

		#region Dependency Properties

		public static readonly DependencyProperty RootBarControlProperty = BarControlService.RootBarControlProperty.AddOwner(typeof(RichTextEditorDocumentView));

		#endregion Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="RichTextEditorDocumentView"/> class.
		/// </summary>
		public RichTextEditorDocumentView() {
			// Listen for changes in the DataContext to update the view model
			this.DataContextChanged += this.OnDataContextChanged;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
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
		/// Gets the text style of the current selection.
		/// </summary>
		/// <returns>A new <see cref="RichTextStyle"/>.</returns>
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
		/// Is called when the DataContext for this element changes.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> data.</param>
		private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
			this.ViewModel = e.NewValue as RichTextEditorDocumentViewModel;
		}

		/// <summary>
		/// Is called when an individual property is changed on the view model that is associated with this view.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> data.</param>
		private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
			if (e.PropertyName == nameof(RichTextEditorDocumentViewModel.SelectionTextStyle)) {
				ApplySelectionTextStyle(ViewModel.SelectionTextStyle);
			}
		}

		/// <summary>
		/// Is called when the associated view model requests to activate preview mode.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> data.</param>
		private void OnViewModelRequestActivatePreviewMode(object sender, EventArgs e) {
			if (!IsPreviewModeActive) {
				ActivatePreviewMode();
				viewModel.PreviewMode = this.PreviewMode;
			}
		}

		/// <summary>
		/// Is called when the associated view model requests to cancel preview mode.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> data.</param>
		private void OnViewModelRequestCancelPreviewMode(object sender, EventArgs e) {
			if (IsPreviewModeActive) {
				DeactivatePreviewMode(restoreOldSettings: true);
				viewModel.PreviewMode = this.PreviewMode;
			}
		}

		/// <summary>
		/// Is called when the associated view model requests to clear all text highlights.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> data.</param>
		private void OnViewModelRequestClearAllTextHighlights(object sender, EventArgs e) {
			ClearAllTextHighlights();
		}

		/// <summary>
		/// Is called when the associated view model requests to insert text.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="text">The text to insert.</param>
		private void OnViewModelRequestInsertText(object sender, string text) {
			ReplaceText(text);
		}

		/// <summary>
		/// Is called when the associated view model requests to save and exit preview mode.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> data.</param>
		private void OnViewModelRequestSaveAndExitPreviewMode(object sender, EventArgs e) {
			if (IsPreviewModeActive) {
				DeactivatePreviewMode(restoreOldSettings: false);
				viewModel.PreviewMode = this.PreviewMode;
			}
		}

		/// <summary>
		/// Replaces the current selection with the given text.
		/// </summary>
		/// <param name="text">The new text.</param>
		private void ReplaceText(String text) {
			this.Selection.Text = text;
			this.CaretPosition = this.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
		}

		/// <summary>
		/// Gets or sets the view model associated with this view.
		/// </summary>
		/// <value>A <see cref="RichTextEditorDocumentViewModel"/>.</value>
		private RichTextEditorDocumentViewModel ViewModel {
			get => viewModel;
			set {
				if (viewModel != value) {
					if (viewModel != null) {
						viewModel.PropertyChanged -= this.OnViewModelPropertyChanged;
						viewModel.RequestActivatePreviewMode -= this.OnViewModelRequestActivatePreviewMode;
						viewModel.RequestCancelPreviewMode -= this.OnViewModelRequestCancelPreviewMode;
						viewModel.RequestClearAllTextHighlights -= this.OnViewModelRequestClearAllTextHighlights;
						viewModel.RequestInsertText -= this.OnViewModelRequestInsertText;
						viewModel.RequestSaveAndExitPreviewMode -= this.OnViewModelRequestSaveAndExitPreviewMode;
					}

					viewModel = value;

					if (viewModel != null) {
						viewModel.PropertyChanged += this.OnViewModelPropertyChanged;
						viewModel.RequestActivatePreviewMode += this.OnViewModelRequestActivatePreviewMode;
						viewModel.RequestCancelPreviewMode += this.OnViewModelRequestCancelPreviewMode;
						viewModel.RequestClearAllTextHighlights += this.OnViewModelRequestClearAllTextHighlights;
						viewModel.RequestInsertText += this.OnViewModelRequestInsertText;
						viewModel.RequestSaveAndExitPreviewMode += this.OnViewModelRequestSaveAndExitPreviewMode;

						// Update the editor with the view model's document and reset selection
						this.Document = viewModel.Document;
						this.ResetSelection();

						// Configure the context menu
						var contextMenu = new BarContextMenu() {
							ItemContainerTemplateSelector = viewModel.ItemContainerTemplateSelector,
						};
						BarControlService.SetRootBarControl(contextMenu, this.RootBarControl);
						foreach (var item in viewModel.GetContextMenuItems())
							contextMenu.Items.Add(item);
						if (contextMenu.Items.Count > 0)
							this.ContextMenu = contextMenu;
					}
				}
			}
		}
		
		/// <inheritdoc/>
		protected override void OnSelectionChanged(RoutedEventArgs e) {
			base.OnSelectionChanged(e);

			// Synchronize the current selection with the view model
			this.ViewModel.SelectionTextStyle = GetSelectionTextStyle();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the <see cref="IRootBarControl"/> associated with the view.
		/// </summary>
		/// <value>A <see cref="IRootBarControl"/>.</value>
		public IRootBarControl RootBarControl {
			get => (IRootBarControl)GetValue(RootBarControlProperty);
			set => SetValue(RootBarControlProperty, value);
		}

	}

}
