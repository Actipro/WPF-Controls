/*

RIBBON GETTING STARTED SERIES - STEP 9

STEP SUMMARY:

	Add view models for "Home" and "New" Backstage Tabs.

CHANGES SINCE LAST STEP:

	Updated the BackstageItems configuration to include new RibbonBackstageTabViewModel
	configurations for "Home" and "New" tabs.

	Prior sample comments were removed/condensed.

*/

using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Input;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GettingStarted.Step09 {

	/// <summary>
	/// Defines the view model for this sample.
	/// </summary>
	public class SampleWindowViewModel : ObservableObjectBase {

		private ICommand							helpCommand;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SampleWindowViewModel"/> class.
		/// </summary>
		public SampleWindowViewModel() {

			var barManager = this.BarManager;

			// Initialize the view model for the Ribbon
			this.Ribbon = new RibbonViewModel() {
				QuickAccessToolBarLocation = RibbonQuickAccessToolBarLocation.Below,

				Backstage = new RibbonBackstageViewModel() {
					Items = {
						//	SAMPLE NOTE 9.1:
						//		Create new view models for the "Home" and "New" backstage tabs. Refer to the
						//		SampleBackstageTabContentTemplateSelector class for details on how the content
						//		for each tab is defined.
						//
						//		This sample uses the same base class for both view models, but some applications
						//		may want to define their own subclass (e.g. RibbonBackstageHomeTabViewModel) that
						//		is specific to each tab. This is particularly necessary when the DataTemplate for
						//		the tab's content needs to be bound to properties on the model.
						new RibbonBackstageTabViewModel(SampleBarControlKeys.BackstageTabHome, "Home") {
							SmallImageSource = barManager.ImageProvider.GetImageSource(SampleBarControlKeys.BackstageTabHome, BarImageSize.Small)
						},
						new RibbonBackstageTabViewModel(SampleBarControlKeys.BackstageTabNew, "New") {
							SmallImageSource = barManager.ImageProvider.GetImageSource(SampleBarControlKeys.BackstageTabNew, BarImageSize.Small)
						},

						new RibbonBackstageHeaderSeparatorViewModel(),
						new RibbonBackstageHeaderButtonViewModel(SampleBarControlKeys.BackstageClose, "Close", SampleBarManager.NotImplementedCommand),
						new RibbonBackstageHeaderButtonViewModel(SampleBarControlKeys.BackstagePrint, "Print", SampleBarManager.NotImplementedCommand),

						new RibbonBackstageHeaderSeparatorViewModel(RibbonBackstageHeaderAlignment.Bottom),
						new RibbonBackstageHeaderButtonViewModel(SampleBarControlKeys.BackstageHelp, "Help", "E", ApplicationCommands.Help) {
							HeaderAlignment = RibbonBackstageHeaderAlignment.Bottom
						},
					}
				},

				QuickAccessToolBar = new RibbonQuickAccessToolBarViewModel() {
					CommonItems = {
						barManager.ControlViewModels[SampleBarControlKeys.Cut],
						barManager.ControlViewModels[SampleBarControlKeys.Copy],
						barManager.ControlViewModels[SampleBarControlKeys.PasteMenu],
					},
					Items = {
						barManager.ControlViewModels[SampleBarControlKeys.Undo],
						barManager.ControlViewModels[SampleBarControlKeys.Redo],
						barManager.ControlViewModels[SampleBarControlKeys.Cut],
						barManager.ControlViewModels[SampleBarControlKeys.Copy],
						barManager.ControlViewModels[SampleBarControlKeys.PasteMenu],
					},
				},

				Tabs = {
					new RibbonTabViewModel("Home") {
						Groups = {
							new RibbonGroupViewModel(SampleBarControlKeys.GroupUndo, "Undo") {
								SmallImageSource = barManager.ImageProvider.GetImageSource(SampleBarControlKeys.GroupUndo, BarImageSize.Small),
								Items = {
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior = ItemVariantBehavior.AlwaysSmall,
										Items = {
											barManager.ControlViewModels[SampleBarControlKeys.Undo],
											barManager.ControlViewModels[SampleBarControlKeys.Redo],
										}
									}
								}
							},
							new RibbonGroupViewModel(SampleBarControlKeys.GroupClipboard, "Clipboard") {
								SmallImageSource = barManager.ImageProvider.GetImageSource(SampleBarControlKeys.GroupClipboard, BarImageSize.Small),
								Items = {
									barManager.ControlViewModels[SampleBarControlKeys.PasteMenu],
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior = ItemVariantBehavior.NeverLarge,
										Items = {
											barManager.ControlViewModels[SampleBarControlKeys.Cut],
											barManager.ControlViewModels[SampleBarControlKeys.Copy],
										}
									}
								}
							},
						}
					},
					new RibbonTabViewModel("Help") {
						KeyTipText = "E",
						Groups = {
							new RibbonGroupViewModel(SampleBarControlKeys.GroupHelpResources, "Resources") {
								Items = {
									barManager.ControlViewModels[SampleBarControlKeys.Help],
								}
							}
						}
					}
				}
			};

		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the manager for working with the objects used by Ribbon and related menus.
		/// </summary>
		private SampleBarManager BarManager { get; } = new SampleBarManager();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the view models of the menu controls to be displayed in the editor's context menu.
		/// </summary>
		/// <value>An <see cref="IEnumerable{T}"/> of type <see cref="object"/> for each view model.</value>
		public IEnumerable<object> EditorContextMenuItems {
			get {
				yield return this.BarManager.ControlViewModels[SampleBarControlKeys.Cut];
				yield return this.BarManager.ControlViewModels[SampleBarControlKeys.Copy];
				yield return this.BarManager.ControlViewModels[SampleBarControlKeys.Paste];
				yield return new BarSeparatorViewModel();
				yield return this.BarManager.ControlViewModels[SampleBarControlKeys.SelectAll];
			}
		}

		/// <summary>
		/// Gets the command for displaying Help.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand HelpCommand {
			get {
				if (helpCommand is null) {
					helpCommand = new DelegateCommand<object>(
						(param) => {
							// Execute
							ThemedMessageBox.Show("This is where contextual Help would be displayed.", "Help", MessageBoxButton.OK, MessageBoxImage.Information);
						});
				}
				return helpCommand;
			}
		}

		/// <summary>
		/// Gets the view model for the ribbon control.
		/// </summary>
		/// <value>A <see cref="RibbonViewModel"/>.</value>
		public RibbonViewModel Ribbon { get; }

	}

}
