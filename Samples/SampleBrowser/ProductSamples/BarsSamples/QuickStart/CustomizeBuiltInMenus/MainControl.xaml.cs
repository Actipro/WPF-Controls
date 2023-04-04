using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Input;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.CustomizeBuiltInMenus {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private ICommand				customCommand;
		private readonly ImageSource	customCommandSmallImage;
		private readonly ImageSource	customCommandLargeImage;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Cache images for use with the custom action
			customCommandLargeImage = ImageLoader.GetIcon("QuickStartGreen32.png");
			customCommandSmallImage = ImageLoader.GetIcon("QuickStartGreen16.png");

			// Configure the sample to use a common view model with pre-populated commands
			var windowViewModel = SampleViewModelFactory.CreateDefaultRichTextEditorRibbonWindowViewModel();
			windowViewModel.Ribbon.LayoutMode = RibbonLayoutMode.Classic;
			windowViewModel.Ribbon.IsApplicationButtonVisible = false;

			this.DataContext = windowViewModel;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a new <see cref="BarMenuHeading"/> for use in the menu.
		/// </summary>
		/// <param name="label">The label to be set for the heading.</param>
		/// <returns>A new <see cref="BarMenuHeading"/>.</returns>
		private static BarMenuHeading CreateBarMenuHeading(string label) {
			var barMenuHeading = new BarMenuHeading(label);

			// Block the built-in context menu (with commands like "Add to Quick Access Toolbar") from
			// being displayed for this menu item.
			BarControlService.SetAllowContextMenu(barMenuHeading, false);

			return barMenuHeading;
		}

		/// <summary>
		/// Creates a new <see cref="BarMenuSeparator"/> for use in the menu.
		/// </summary>
		/// <returns>A new <see cref="BarMenuSeparator"/>.</returns>
		private static BarMenuSeparator CreateBarMenuSeparator() {
			var barMenuSeparator = new BarMenuSeparator();

			// Block the built-in context menu (with commands like "Add to Quick Access Toolbar") from
			// being displayed for this menu item.
			BarControlService.SetAllowContextMenu(barMenuSeparator, false);

			return barMenuSeparator;
		}

		/// <summary>
		/// Creates a new <see cref="BarMenuItem"/> for use in the menu that will trigger a custom action when invoked.
		/// </summary>
		/// <returns>A new <see cref="BarMenuItem"/>.</returns>
		private BarMenuItem CreateCustomActionBarMenuItem(object owner) {
			var customActionMenuItem = new BarMenuItem() {
				Command = CustomCommand,
				CommandParameter = owner,
				Description = $"This menu item was added programmatically on {DateTime.Today.ToShortDateString()} at {DateTime.Now.ToLongTimeString()} using the Ribbon.MenuOpening event.",
				KeyTipText = "C",
				Label = $"Custom Action (Created @ {DateTime.Now.ToLongTimeString()})",
				LargeImageSource = customCommandLargeImage,
				ScreenTipHeader = "Custom Action",
				SmallImageSource = customCommandSmallImage,
				UseLargeSize = false,
			};

			// Block the built-in context menu (with commands like "Add to Quick Access Toolbar") from
			// being displayed for this menu item.
			BarControlService.SetAllowContextMenu(customActionMenuItem, false);

			return customActionMenuItem;
		}

		/// <summary>
		/// Gets the command that will be associated with the custom menu item.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		private ICommand CustomCommand {
			get {
				if (customCommand is null) {
					customCommand = new DelegateCommand<object>((param) => {
						var message = (param is null)
							? "You clicked the programmatically-added custom menu item."
							: $"You clicked the programmatically-added custom menu item for:{Environment.NewLine}{Environment.NewLine}{param}";
						ThemedMessageBox.Show(message, "Custom Action", MessageBoxButton.OK, MessageBoxImage.Information);
					});
				}
				return customCommand;
			}
		}

		/// <summary>
		/// Customizes a context menu.
		/// </summary>
		/// <param name="e">A <see cref="BarMenuEventArgs"/> that contains the event data.</param>
		private void CustomizeMenu(BarMenuEventArgs e) {
			var menu = e.Menu;

			// Add custom menu items at the bottom
			menu.Items.Add(CreateBarMenuSeparator());
			menu.Items.Add(CreateBarMenuHeading($"Customization of {GetMenuKindDescription(e.Kind)}"));
			menu.Items.Add(CreateCustomActionBarMenuItem(e.Owner));

			// Customize the pre-defined 'Add to Quick Access Toolbar' and 'Remove from Quick Access Toolbar' menu items
			// by disabling the menu item associated with the corresponding commands
			if (e.Kind == BarMenuKind.ControlContextMenu) {
				foreach (var menuItem in menu.Items.OfType<BarMenuItem>()) {
					if (menuItem.Command == ribbon.AddToQuickAccessToolBarCommand || menuItem.Command == ribbon.RemoveFromQuickAccessToolBarCommand) {

						// Disable the menu item
						menuItem.IsEnabled = false;

						// Update the label to indicate why the commands are disabled
						menuItem.Label += " (Programmatically Disabled)";

						// IMPORTANT NOTE:
						//
						// While this sample demonstrates that built-in command labels can be changed programmatically,
						// this is not the method that would typically be used to customize or localize strings.
						// Refer to "Customizing String Resources" documentation for additional information
						//
						// https://www.actiprosoftware.com/docs/controls/wpf/customizing-string-resources
					}
				}
			}

		}

		/// <summary>
		/// Gets a description for a menu kind.
		/// </summary>
		/// <param name="kind">The menu kind to examine.</param>
		/// <returns>A string describing the menu kind.</returns>
		private static string GetMenuKindDescription(BarMenuKind kind) {
			switch (kind) {
				case BarMenuKind.ControlContextMenu:
					return "Control Context Menu";
				case BarMenuKind.RibbonOptionsButtonMenu:
					return "Options Menu";
				case BarMenuKind.RibbonQuickAccessToolBarCustomizeButtonMenu:
					return "Quick Access Toolbar Customize Menu";
				case BarMenuKind.RibbonTabItemOverflowButtonMenu:
					return "Tab Overflow Menu";
				case BarMenuKind.RibbonGroupOverflowButtonMenu:
					return "Group Overflow Menu";
				default:
					return "Unknown Menu";
			}
		}

		/// <summary>
		/// Logs the details of the menu.
		/// </summary>
		/// <param name="e">A <see cref="BarMenuEventArgs"/> that contains the event data.</param>
		private void LogMenuDetails(BarMenuEventArgs e) {
			var menu = e.Menu;

			// Generate some details about the current menu
			var sb = new StringBuilder()
				.AppendLine("MenuOpening...")
				.AppendLine($"    Kind = {e.Kind.GetType().Name}.{e.Kind}")
				.AppendLine($"    Type = {menu.GetType().FullName}")
				.AppendLine($"    Owner = {(e.Owner?.ToString() ?? "<NULL>")}");
			var menuItemCount = menu.Items.Count;
			for (var i = 0; i < menuItemCount; i++)
				sb.AppendLine($"    Item[{i}] = {menu.Items[i]}");

			// Display the current details in the text box
			textBox.Text = sb.ToString();
			textBox.ScrollToHome();
		}

		/// <summary>
		/// Processes the <see cref="ActiproSoftware.Windows.Controls.Bars.Ribbon.MenuOpening"/> event.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="BarMenuEventArgs"/> that contains the event data.</param>
		private void OnRibbonMenuOpening(object sender, BarMenuEventArgs e) {
			// This demo focuses on the built-in menus, so ignore the menus of standard popup commands and sub menus
			if (e.Kind == BarMenuKind.PopupButtonMenu
				|| e.Kind == BarMenuKind.MenuItemSubmenu) {

				return;
			}

			// Log the details of the menu
			LogMenuDetails(e);

			// Customize the menu
			CustomizeMenu(e);
		}

	}

}