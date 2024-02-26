using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.MiniToolBarIntro {

	/// <summary>
	/// Provides the user control for this sample that uses an MVVM-based ribbon configuration.
	/// </summary>
	public partial class SampleMvvmControl : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SampleMvvmControl"/> class.
		/// </summary>
		public SampleMvvmControl() {
			InitializeComponent();

			// Bind the view to itself since we do not define an explicit view model
			this.DataContext = this;
			
			// Initialize the TextBox ContextMenu
			textBox.ContextMenu = CreateTextBoxContextMenu();
			
			// Ensure the textbox is focused when this sample is loaded
			this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, (Action)(() => {
				textBox.Focus();
			}));
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates the context menu that will be assigned to the <see cref="TextBoxControl"/> used
		/// by this sample.
		/// </summary>
		/// <returns>A <see cref="ContextMenu"/> object.</returns>
		private static ContextMenu CreateTextBoxContextMenu() {
			// BarManager is used in this sample only to reuse the NotImplementedCommand
			var barManager = new BarManager();

			// Configure the context menu
			var contextMenu = new BarContextMenu() {

				ItemContainerTemplateSelector = new BarControlTemplateSelector(),

				Items = {
					new BarButtonViewModel("GoToLine", "Go To Line...", barManager.NotImplementedCommand),
					new BarButtonViewModel("InsertFileAsText", "Insert File As Text...", barManager.NotImplementedCommand),
					new BarSeparatorViewModel(),
					new BarButtonViewModel(ApplicationCommands.SelectAll) {
						SmallImageSource = ImageLoader.GetIcon("SelectAll16.png"),
					},
					new BarSeparatorViewModel(),
					new BarButtonViewModel("CommandPalette", "Command Palette...", barManager.NotImplementedCommand),
				},

				MiniToolBarContent = new MiniToolBarViewModel() {
					CanUseMultiRowLayout = true,
					Items = {
						new BarButtonViewModel(ApplicationCommands.Cut) {
							SmallImageSource = ImageLoader.GetIcon("Cut16.png"),
						},
						new BarButtonViewModel(ApplicationCommands.Copy) {
							SmallImageSource = ImageLoader.GetIcon("Copy16.png"),
						},
						new BarButtonViewModel(ApplicationCommands.Paste) {
							SmallImageSource = ImageLoader.GetIcon("Paste16.png"),
						},
						new BarSeparatorViewModel(),
						new BarButtonViewModel(ApplicationCommands.Undo) {
							SmallImageSource = ImageLoader.GetIcon("Undo16.png"),
						},
						new BarButtonViewModel(ApplicationCommands.Redo) {
							SmallImageSource = ImageLoader.GetIcon("Redo16.png"),
						},
						new BarSeparatorViewModel(),
						new BarButtonViewModel("Font", "Font...", barManager.NotImplementedCommand) {
							ToolBarItemVariantBehavior = ItemVariantBehavior.AlwaysMedium,
							SmallImageSource = ImageLoader.GetIcon("WordArt16.png"),
						},
						new BarSeparatorViewModel(),
						new BarTextBoxViewModel("FindWhat", "Find") {
							PlaceholderText = "Find",
							Description = "Search for text",
							RequestedWidth = 175,
						},
						new BarButtonViewModel("Find", "Find", barManager.NotImplementedCommand) {
							SmallImageSource = ImageLoader.GetIcon("Find16.png"),
						},
					}
				}
			};

			return contextMenu;
		}

	}

}
