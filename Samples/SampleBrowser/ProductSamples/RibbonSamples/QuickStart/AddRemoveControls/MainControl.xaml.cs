using System.Windows.Media.Imaging;
using RibbonControls = ActiproSoftware.Windows.Controls.Ribbon.Controls;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.RibbonSamples.QuickStart.AddRemoveControls;

/// <summary>
/// Provides the main control for this sample.
/// </summary>
public partial class MainControl {

	private int _groupVariantPriority = 100;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a random child control.
	/// </summary>
	/// <param name="seed">The seed for the random generator.</param>
	private static Control CreateRandomChildControl(int seed) {
		var commonLabels = new string[] { "Blank Page", "Bookmark", "Chart", "Cover Page", "Cross Reference", "Drop-Cap", "Equation", "Footer", "Header" };
		var randomIndex = Math.Min(commonLabels.Length - 1, (int)Math.Floor(new Random(seed * DateTime.Now.Millisecond).NextDouble() * commonLabels.Length));

		var label = commonLabels[randomIndex];
		var imageSourceBase = "/Images/Icons/" + commonLabels[randomIndex].Replace(" ", string.Empty).Replace("-", string.Empty);

		var button = new RibbonControls.Button {
			Label = label,
			ImageSourceSmall = new BitmapImage(new Uri(imageSourceBase + "16.png", UriKind.Relative)),
			ImageSourceLarge = new BitmapImage(new Uri(imageSourceBase + "32.png", UriKind.Relative))
		};

		return button;
	}

	private void OnAddButtonClick(object sender, RoutedEventArgs e) {
		switch (controlTypeList.SelectedIndex) {
			case 0: {
				// Add a new QAT control
				ribbon.QuickAccessToolBarItems.Add(CreateRandomChildControl(1));
				break;
			}
			case 1: {
				// Add a new tab panel control
				ribbon.TabPanelItems.Add(CreateRandomChildControl(1));
				break;
			}
			case 2: {
				// Add a new Tab
				var newTab = new RibbonControls.Tab {
					Label = controlLabelTextBox.Text.Trim()
				};
				ribbon.Tabs.Add(newTab);
				ribbon.SelectedTab = newTab;
				break;
			}
			case 3: {
				// Add a new Group
				var newGroup = new RibbonControls.Group {
					Label = controlLabelTextBox.Text.Trim()
				};
				newGroup.Variants.Add(new RibbonControls.GroupVariant(_groupVariantPriority++, RibbonControls.VariantSize.Medium));
				newGroup.Variants.Add(new RibbonControls.GroupVariant(_groupVariantPriority++, RibbonControls.VariantSize.Collapsed));
				ribbon.SelectedTab.Items.Add(newGroup);

				// Add some sample child controls
				var panel = new RibbonControls.StackPanel();
				panel.Children.Add(CreateRandomChildControl(1));
				panel.Children.Add(CreateRandomChildControl(2));
				panel.Children.Add(CreateRandomChildControl(3));
				newGroup.Items.Add(panel);
				break;
			}
			default: {
				// Add a new control to the first group
				if (ribbon.SelectedTab is { Items.Count: > 0 }) {
					var group = ribbon.SelectedTab.Items[0];
					group.Items.Add(CreateRandomChildControl(1));
				}
				else
					MessageBox.Show("There are no groups on the current tab to add a control to.");
				break;
			}
		}
	}

	private void OnRemoveButtonClick(object sender, RoutedEventArgs e) {
		switch (controlTypeList.SelectedIndex) {
			case 0:
				// Remove the last control in the QAT
				if (ribbon.QuickAccessToolBarItems.Count > 0)
					ribbon.QuickAccessToolBarItems.RemoveAt(ribbon.QuickAccessToolBarItems.Count - 1);
				else
					MessageBox.Show("There are no controls remaining on this ribbon's Quick Access ToolBar to remove.");
				break;
			case 1:
				// Remove the last control in the tab panel
				if (ribbon.TabPanelItems.Count > 0)
					ribbon.TabPanelItems.RemoveAt(ribbon.TabPanelItems.Count - 1);
				else
					MessageBox.Show("There are no controls remaining on this ribbon's tab panel to remove.");
				break;
			case 2:
				// Remove the last Tab
				if (ribbon.Tabs.Count > 1)
					ribbon.Tabs.RemoveAt(ribbon.Tabs.Count - 1);
				else
					MessageBox.Show("There must be at least one tab left.");
				break;
			case 3:
				// Remove the last Group
				if (ribbon.SelectedTab.Items.Count > 0)
					ribbon.SelectedTab.Items.RemoveAt(ribbon.SelectedTab.Items.Count - 1);
				else
					MessageBox.Show("There are no groups remaining on this tab to remove.");
				break;
			default: {
				// Remove the last new control in the first group
				if ((ribbon.SelectedTab is not null) && (ribbon.SelectedTab.Items.Count > 0)) {
					var group = ribbon.SelectedTab.Items[0];
					if (group.Items.Count > 0) {
						group.Items.RemoveAt(group.Items.Count - 1);
					}
					else
						MessageBox.Show("There are no controls in the last group to remove a control from.");
				}
				break;
			}
		}
	}

}
