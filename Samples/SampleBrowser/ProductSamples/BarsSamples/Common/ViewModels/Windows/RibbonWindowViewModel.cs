using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	public class RibbonWindowViewModel : WindowViewModel {

		private ICommand toggleApplicationButtonCommand;
		private ICommand toggleFooterCommand;
		private ICommand toggleQuickAccessToolBarCommand;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="WindowViewModel(BarManager)"/>
		/// <param name="ribbonViewModel">The view model of the ribbon associated with the window.</param>
		public RibbonWindowViewModel(BarManager barManager, RibbonViewModel ribbonViewModel)
			: base(barManager) {

			this.Ribbon = ribbonViewModel ?? throw new ArgumentNullException(nameof(ribbonViewModel));

			barManager.UpdateControlViewModelCheckedState(BarControlKeys.ShowApplicationButton, () => this.Ribbon?.IsApplicationButtonVisible ?? false);
			barManager.UpdateControlViewModelCheckedState(BarControlKeys.ShowFooter, () => this.Ribbon?.Footer != null);
			barManager.UpdateControlViewModelCheckedState(BarControlKeys.ShowQuickAccessToolBar, () => this.Ribbon?.QuickAccessToolBarMode == RibbonQuickAccessToolBarMode.Visible);

		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		protected override IEnumerable<KeyValuePair<CompositeCommand, ICommand>> GetCommandMappings(BarManager barManager) {
			return base.GetCommandMappings(barManager).Concat(new Dictionary<CompositeCommand, ICommand>() {
				{ barManager.ToggleApplicationButtonCommand, this.ToggleApplicationButtonCommand },
				{ barManager.ToggleFooterCommand, this.ToggleFooterCommand },
				{ barManager.ToggleQuickAccessToolBarCommand, this.ToggleQuickAccessToolBarCommand },
			});
		}

		/// <summary>
		/// Gets or sets the view model of the ribbon associated with the window.
		/// </summary>
		/// <value>A <see cref="RibbonViewModel"/>.</value>
		public RibbonViewModel Ribbon { get; }

		/// <summary>
		/// Gets the command which toggles the visibility of the ribbon application button.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand ToggleApplicationButtonCommand {
			get {
				if (toggleApplicationButtonCommand is null) {
					toggleApplicationButtonCommand = new DelegateCommand<object>(
						executeAction: p => {
							this.BarManager.SetValueFromControlViewModelCheckedState(BarControlKeys.ShowApplicationButton, 
								isChecked => this.Ribbon.IsApplicationButtonVisible = isChecked);
						}
					);
				}
				return toggleApplicationButtonCommand;
			}
		}
		
		/// <summary>
		/// Gets the command which toggles the visibility of the ribbon footer.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand ToggleFooterCommand {
			get {
				if (toggleFooterCommand is null) {
					toggleFooterCommand = new DelegateCommand<object>(
						executeAction: p => {
							this.BarManager.SetValueFromControlViewModelCheckedState(BarControlKeys.ShowFooter,
								isChecked => this.Ribbon.Footer = (isChecked 
									? new RibbonFooterViewModel() {
										Kind = RibbonFooterKind.Warning,
										Content = new RibbonFooterSimpleContentViewModel() { 
											ImageSource = ImageLoader.GetIcon("InformationClear16.png"),
											Text = "Actipro Bars contains everything you need to implement modern ribbon, toolbar, and menu interfaces in your apps.",
										}
									} : null));
						}
					);
				}
				return toggleFooterCommand;
			}
		}

		/// <summary>
		/// Gets the command which toggles the visibility of the ribbon quick access toolbar.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand ToggleQuickAccessToolBarCommand {
			get {
				if (toggleQuickAccessToolBarCommand is null) {
					toggleQuickAccessToolBarCommand = new DelegateCommand<object>(
						executeAction: p => {
							this.BarManager.SetValueFromControlViewModelCheckedState(BarControlKeys.ShowQuickAccessToolBar,
								isChecked => this.Ribbon.QuickAccessToolBarMode = (isChecked ? RibbonQuickAccessToolBarMode.Visible : RibbonQuickAccessToolBarMode.Hidden));
						},
						canExecuteFunc: p => (this.Ribbon?.QuickAccessToolBarMode ?? RibbonQuickAccessToolBarMode.None) != RibbonQuickAccessToolBarMode.None
					);
				}
				return toggleQuickAccessToolBarCommand;
			}
		}

	}

}
