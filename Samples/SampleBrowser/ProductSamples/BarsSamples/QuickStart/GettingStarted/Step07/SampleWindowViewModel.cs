/*

RIBBON GETTING STARTED SERIES - STEP 7

STEP SUMMARY:

	Support the Quick Access Toolbar by adding and configuring new properties on the RibbonViewModel.

CHANGES SINCE LAST STEP:

	Updated QuickAccessToolBarMode from None to Visible and configured QuickAccessToolBarLocation.
	Added QuickAccessToolBarViewModel configuration for Items and CommonItems.

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

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GettingStarted.Step07 {

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
				//	SAMPLE NOTE 7.1:
				//		Since the Quick Access Toolbar is finally being populated, it should no
				//		longer be hidden on the Ribbon. It is visible by default, so this property
				//		assignment will be removed in a future step.
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.Visible,

				//	SAMPLE NOTE 7.2:
				//		By default, the Quick Access Toolbar (QAT) is displayed above by the Ribbon and
				//		combined with the window title bar. This sample will configure the QAT
				//		below the Ribbon to illustrate the capability. At run-time, the QAT customize menu
				//		can be used to change the location above or below the Ribbon.
				QuickAccessToolBarLocation = RibbonQuickAccessToolBarLocation.Below,

				QuickAccessToolBar = new RibbonQuickAccessToolBarViewModel() {
					//	SAMPLE NOTE 7.3:
					//		Define the collection of view models that are commonly displayed in the
					//		Quick Access Toolbar. When customizing the Quick Access Toolbar, these
					//		common items are always displayed in the context menu to be easily
					//		toggled on or off.
					//		
					//		These view models DO NOT have to be currently displayed, and including
					//		a view model in the common list will not make the view model actively
					//		visible.
					CommonItems = {
						barManager.ControlViewModels[SampleBarControlKeys.Cut],
						barManager.ControlViewModels[SampleBarControlKeys.Copy],
						barManager.ControlViewModels[SampleBarControlKeys.PasteMenu],
					},

					//	SAMPLE NOTE 7.4:
					//		Define the initial collection of view models that will be displayed in the
					//		Quick Access Toolbar. Note that Undo/Redo are not part of the CommonItems
					//		collection but can still be included in the Quick Access Toolbar.
					Items = {
						barManager.ControlViewModels[SampleBarControlKeys.Undo],
						barManager.ControlViewModels[SampleBarControlKeys.Redo],
						barManager.ControlViewModels[SampleBarControlKeys.Cut],
						barManager.ControlViewModels[SampleBarControlKeys.Copy],
						barManager.ControlViewModels[SampleBarControlKeys.PasteMenu],
					}

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
