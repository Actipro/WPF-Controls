/*

RIBBON GETTING STARTED SERIES - STEP 6

STEP SUMMARY:

	Configure the view models to be displayed in the editor context menu.

CHANGES SINCE LAST STEP:

	Added the EditorContextMenuItems property and defined the view models for
	each control to be displayed in the menu.

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

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GettingStarted.Step06 {

	/// <summary>
	/// Defines the view model for this sample.
	/// </summary>
	public class SampleWindowViewModel : ObservableObjectBase {

		private ICommand helpCommand;

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
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.None,

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
				//	SAMPLE NOTE 6.1:
				//		The context menu can use the same control view models as the Ribbon control.
				//
				//		Since this context menu is not expected to change, the property can be based
				//		on IEnumerable<T> instead of ObservableCollection<T>.
				//
				//		The SelectAll view model is new for this step and shows that you can use commands
				//		in a context menu that are not currently displayed in the Ribbon.
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
