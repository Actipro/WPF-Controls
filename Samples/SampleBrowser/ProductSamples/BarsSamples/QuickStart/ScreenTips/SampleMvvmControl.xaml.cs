using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.ScreenTips {

	/// <summary>
	/// Provides the user control for this sample that uses an MVVM-based ribbon configuration.
	/// </summary>
	public partial class SampleMvvmControl : UserControl {

		private const string ButtonKeyComplexScreenTip	= "MvvmComplexScreenTip";
		private const string ButtonKeyDynamicScreenTip	= "MvvmDynamicScreenTip";
		private const string ButtonKeyFooterHelpContext	= "MvvmFooterHelpContext";

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SampleMvvmControl"/> class.
		/// </summary>
		public SampleMvvmControl()
		{
			InitializeComponent();

			// Bind the view to itself since we do not define an explicit view model
			this.DataContext = this;

			// Listen for ScreenTip opening for controls that notify the ScreenTipService
			ScreenTipService.Current.ScreenTipOpening += this.OnScreenTipOpening;

			// Initialize the Ribbon view models (used by MVVM samples only)
			InitializeRibbonViewModels();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the view models for the ribbon.
		/// </summary>
		private void InitializeRibbonViewModels() {

			Ribbon = new RibbonViewModel() {
				CanChangeLayoutMode = false,
				Footer = new RibbonFooterViewModel() {
					Content = new RibbonFooterSimpleContentViewModel() {
						ImageSource = ImageLoader.GetIcon("InformationClear16.png"),
						Text = "Hover over the buttons in the Ribbon and Quick Access Toolbar to see screen tips."
					},
					ContentTemplateSelector = new RibbonFooterContentTemplateSelector(),
				},
				IsApplicationButtonVisible = false,
				IsCollapsible = false,
				IsMinimizable = false,
				QuickAccessToolBarLocation = RibbonQuickAccessToolBarLocation.Below,
				QuickAccessToolBar = new RibbonQuickAccessToolBarViewModel() {
					Items = {
						new BarButtonViewModel(ApplicationCommands.Cut) { Description = "Removes the selection and puts it on the Clipboard so you can paste it elsewhere.", SmallImageSource = ImageLoader.GetIcon("Cut16.png") },
						new BarButtonViewModel(ApplicationCommands.Copy) { Description = "Puts a copy of the selection on the Clipboard so you can paste it elsewhere.", SmallImageSource = ImageLoader.GetIcon("Copy16.png") },
						new BarButtonViewModel(ApplicationCommands.Paste) { Description = "Adds content from the Clipboard into your document.", SmallImageSource = ImageLoader.GetIcon("Paste16.png") },
						new BarButtonViewModel(ApplicationCommands.Undo) { SmallImageSource = ImageLoader.GetIcon("Undo16.png") },
						new BarButtonViewModel(ApplicationCommands.Redo) { SmallImageSource = ImageLoader.GetIcon("Redo16.png") },
					},
				},
				Tabs = {
					new RibbonTabViewModel("MvvmSamples", "MVVM Samples") {
						Groups = {

							// Simple Group
							new RibbonGroupViewModel("Simple") {
								CanAutoCollapse = false,
								Items = {
									new BarButtonViewModel("SimpleDescription") {
										Description = "The view model Description property is mapped to the content of a ScreenTip.",
										LargeImageSource = ImageLoader.GetIcon("QuickStart32.png"),
										SmallImageSource = ImageLoader.GetIcon("QuickStart16.png"),
									},
								},
							},

							// Headers & Footers Group
							new RibbonGroupViewModel("HeadersAndFooters", "Headers & Footers") {
								CanAutoCollapse = false,
								Items = {
									new BarButtonViewModel("Default", ApplicationCommands.Paste) {
										Description = "The view model automatically generates a header from the Label property and keyboard shortcut (when available) for any associated Command. If Title is specified, it will override the Label when used as the header.",
										LargeImageSource = ImageLoader.GetIcon("Paste32.png"),
										SmallImageSource = ImageLoader.GetIcon("Paste16.png"),
									},
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior=ItemVariantBehavior.AlwaysMedium,
										Items = {
											new BarButtonViewModel("AltHeader", "Alt. Header", ApplicationCommands.Paste) {
												Description = "The view model automatically generates a header from the Title property and keyboard shortcut (when available) for any associated Command. If Title is not specified, the Label will be used instead.",
												LargeImageSource = ImageLoader.GetIcon("Paste32.png"),
												SmallImageSource = ImageLoader.GetIcon("Paste16.png"),
												Title = "Alternate Header",
											},
											new BarButtonViewModel("NoHeaderOrFooter", "No Header/Footer", ApplicationCommands.Paste) {
												Description = "Setting the view model Title to an empty string will hide the default header. Without a header or footer, this ScreenTip looks like a normal ToolTip.",
												LargeImageSource = ImageLoader.GetIcon("Paste32.png"),
												SmallImageSource = ImageLoader.GetIcon("Paste16.png"),
												Title = "",
											},

										}
									},
								},
							},

							// Custom Group
							new RibbonGroupViewModel("Custom") {
								CanAutoCollapse = false,
								Items = {
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior=ItemVariantBehavior.NeverSmall,
										Items = {
											// For more advanced ScreenTip usage scenarios, use ScreenTipService.Current.ScreenTipOpening
											// event to detect when a screen tip is being displayed and alter the content. The following
											// view models rely on this event to customize the ScreenTip based on the view model key.

											new BarButtonViewModel(ButtonKeyFooterHelpContext, "Footer Help") {
												Description = "The footer can be set to any content.",
												LargeImageSource = ImageLoader.GetIcon("QuickStart32.png"),
												SmallImageSource = ImageLoader.GetIcon("QuickStart16.png"),
											},
											new BarButtonViewModel(ButtonKeyComplexScreenTip, "Complex") {
												LargeImageSource = ImageLoader.GetIcon("QuickStart32.png"),
												SmallImageSource = ImageLoader.GetIcon("QuickStart16.png"),
												Title = "ScreenTip Customization",
											},
											new BarButtonViewModel(ButtonKeyDynamicScreenTip, "Dynamic") {
												Description = "The footer of this ScreenTip will show the date/time it was displayed.",
												LargeImageSource = ImageLoader.GetIcon("QuickStart32.png"),
												SmallImageSource = ImageLoader.GetIcon("QuickStart16.png"),
											}
										}
									}
								},
							},

						}
					},

				}
			};

		}

		/// <summary>
		/// Occurs when a <see cref="ScreenTip"/> is opening.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The event data.</param>
		/// <remarks>This event is only raised for controls that notify <see cref="ScreenTipService"/> when their <see cref="ToolTip"/> is opening.</remarks>
		private void OnScreenTipOpening(object sender, ToolTipEventArgs e) {
			if ((sender is DependencyObject element) && (ToolTipService.GetToolTip(element) is ScreenTip screenTip)) {
				var key = BarControlService.GetKey(element);

				// Store the key on the ScreenTip for use with contextual help
				screenTip.Tag = new ScreenTipData() { Key = key };

				// Customize the ScreenTip for specific controls
				if (key == ButtonKeyDynamicScreenTip) {
					// Dynamically include the time stamp in the footer
					screenTip.Footer = $"Displayed at: {DateTime.Now}";
				}
				else if (key == ButtonKeyComplexScreenTip) {
					// Allow the ScreenTip to be wider
					screenTip.ComplexContentWidth = 300;

					// Assign the Content and Footer using XAML resources
					screenTip.Content = this.FindResource("ComplexScreenTipContent");
					screenTip.Footer = this.FindResource("ComplexScreenTipFooterContent");
				}
				else if (key == ButtonKeyFooterHelpContext) {
					// Assign the Footer using a XAML resource
					screenTip.Footer = this.FindResource("FooterHelpContextContent");
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the view model for the Ribbon control.
		/// </summary>
		/// <value>A <see cref="RibbonViewModel"/>.</value>
		public RibbonViewModel Ribbon { get; private set; }

	}

}
