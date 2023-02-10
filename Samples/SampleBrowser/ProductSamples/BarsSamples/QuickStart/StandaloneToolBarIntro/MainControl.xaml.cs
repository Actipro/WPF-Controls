using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.StandaloneToolBarIntro {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Bind the view to itself since we do not define an explicit view model
			this.DataContext = this;

			//
			// Configure the primary (top) sample
			//

			var primaryBarManager = new BarManager();
			this.PrimaryStandaloneToolBar = CreateRichTextStandaloneToolBarViewModel(primaryBarManager);

			var primaryDocumentText = "The standalone toolbar above is configured with a customized appearance similar "
				+ "to a Ribbon in Simplified layout mode, while still behaving like a standard toolbar. "
				+ "This appearance may be ideal for an app whose main window has a system backdrop enabled, "
				+ "and there is a single primary toolbar.";
			this.PrimaryDocument = new RichTextEditorDocumentViewModel(primaryBarManager, SampleViewModelFactory.CreateFlowDocument(primaryBarManager, primaryDocumentText)) {
				ItemContainerTemplateSelector = PrimaryStandaloneToolBar.ItemContainerTemplateSelector
			};
			this.PrimaryDocument.RegisterCommands();

			//
			// Configure the secondary (bottom) samples using separate BarManagers since each document doesn't share state
			//

			var secondaryBarManager = new BarManager();
			this.SecondaryStandaloneToolBar = CreateRichTextStandaloneToolBarViewModel(secondaryBarManager);

			var secondaryDocumentText = "The standalone toolbar on the left has a vertical orientation.";
			this.SecondaryDocument = new RichTextEditorDocumentViewModel(secondaryBarManager, SampleViewModelFactory.CreateFlowDocument(secondaryBarManager, secondaryDocumentText)) {
				ItemContainerTemplateSelector = SecondaryStandaloneToolBar.ItemContainerTemplateSelector
			};
			this.SecondaryDocument.RegisterCommands();

			var toolWindowBarManager = new BarManager();
			this.ToolWindowStandaloneToolBar = CreateToolWindowStandaloneToolBarViewModel(toolWindowBarManager);

			var toolWindowDocumentText = "The standalone toolbar in this tool window arranges to the tool window's width.";
			this.ToolWindowDocument = new RichTextEditorDocumentViewModel(toolWindowBarManager, SampleViewModelFactory.CreateFlowDocument(toolWindowBarManager, toolWindowDocumentText)) {
				ItemContainerTemplateSelector = ToolWindowStandaloneToolBar.ItemContainerTemplateSelector
			};
			this.ToolWindowDocument.RegisterCommands();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PRIVATE PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a <see cref="StandaloneToolBarViewModel"/> for a typical rich text editor.
		/// </summary>
		/// <param name="barManager">The associated <see cref="BarManager"/>.</param>
		/// <returns>A new <see cref="StandaloneToolBarViewModel"/>.</returns>
		private static StandaloneToolBarViewModel CreateRichTextStandaloneToolBarViewModel(BarManager barManager) {
			return new StandaloneToolBarViewModel() {
				Items = {
					barManager.ControlViewModels[BarControlKeys.Undo],
					barManager.ControlViewModels[BarControlKeys.Redo],
					new BarSeparatorViewModel(),
					barManager.ControlViewModels[BarControlKeys.PasteMenu],
					barManager.ControlViewModels[BarControlKeys.Cut],
					barManager.ControlViewModels[BarControlKeys.Copy],
					new BarSeparatorViewModel(),
					barManager.ControlViewModels[BarControlKeys.Font],
					barManager.ControlViewModels[BarControlKeys.FontSize],
					new BarSeparatorViewModel(),
					barManager.ControlViewModels[BarControlKeys.Bold],
					barManager.ControlViewModels[BarControlKeys.Italic],
					barManager.ControlViewModels[BarControlKeys.Underline],
					barManager.ControlViewModels[BarControlKeys.LineSpacing],
					barManager.ControlViewModels[BarControlKeys.FontColor],
					barManager.ControlViewModels[BarControlKeys.TextHighlightColor],
				}
			};
		}

		/// <summary>
		/// Creates a <see cref="StandaloneToolBarViewModel"/> for the tool window.
		/// </summary>
		/// <param name="barManager">The associated <see cref="BarManager"/>.</param>
		/// <returns>A new <see cref="StandaloneToolBarViewModel"/>.</returns>
		private static StandaloneToolBarViewModel CreateToolWindowStandaloneToolBarViewModel(BarManager barManager) {
			return new StandaloneToolBarViewModel() {
				Items = {
					barManager.ControlViewModels[BarControlKeys.AlignLeft],
					barManager.ControlViewModels[BarControlKeys.AlignCenter],
					barManager.ControlViewModels[BarControlKeys.AlignRight],
					barManager.ControlViewModels[BarControlKeys.AlignJustify],
					new BarSeparatorViewModel(),
					barManager.ControlViewModels[BarControlKeys.FontColor],
					barManager.ControlViewModels[BarControlKeys.Borders],
					new BarSeparatorViewModel(),
					barManager.ControlViewModels[BarControlKeys.Bullets],
					barManager.ControlViewModels[BarControlKeys.Numbering],
				}
			};
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the view model for the primary document.
		/// </summary>
		public RichTextEditorDocumentViewModel PrimaryDocument { get; private set; }

		/// <summary>
		/// Gets or sets the view model for the primary StandaloneToolBar control.
		/// </summary>
		/// <value>A <see cref="StandaloneToolBarViewModel"/>.</value>
		public StandaloneToolBarViewModel PrimaryStandaloneToolBar { get; private set; }

		/// <summary>
		/// Gets or sets the view model for the secondary document.
		/// </summary>
		public RichTextEditorDocumentViewModel SecondaryDocument { get; private set; }

		/// <summary>
		/// Gets or sets the view model for the secondary StandaloneToolBar control.
		/// </summary>
		/// <value>A <see cref="StandaloneToolBarViewModel"/>.</value>
		public StandaloneToolBarViewModel SecondaryStandaloneToolBar { get; private set; }

		/// <summary>
		/// Gets or sets the view model for the tool window document.
		/// </summary>
		public RichTextEditorDocumentViewModel ToolWindowDocument { get; private set; }

		/// <summary>
		/// Gets or sets the view model for the tool window StandaloneToolBar control.
		/// </summary>
		/// <value>A <see cref="StandaloneToolBarViewModel"/>.</value>
		public StandaloneToolBarViewModel ToolWindowStandaloneToolBar { get; private set; }

	}

}