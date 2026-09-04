using ActiproSoftware.ProductSamples.GridsSamples.Common;

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.MailList;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		GenerateItems();

		if (treeListBox.SelectedItem is { } selectedItem)
			treeListBox.FocusItem(selectedItem);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Generates the sample items.
	/// </summary>
	private void GenerateItems() {
		var rootModel = new TreeNodeModel();

		var todayGroupModel = new TreeNodeModel {
			IsExpanded = true,
			Name = "Today"
		};
		rootModel.Children.Add(todayGroupModel);

		var mailModel = new MailTreeNodeModel {
			IsFlagged = true,
			DateTime = DateTime.Today.Add(TimeSpan.FromMinutes(560)),
			Author = "Actipro Software Sales",
			Name = "TreeListBox Features",
			Text = "The TreeListBox has some amazing features."
		};
		todayGroupModel.Children.Add(mailModel);

		var yesterdayGroupModel = new TreeNodeModel {
			IsExpanded = true,
			Name = "Yesterday"
		};
		rootModel.Children.Add(yesterdayGroupModel);

		mailModel = new MailTreeNodeModel {
			DateTime = DateTime.Today.Subtract(TimeSpan.FromDays(1)).Add(TimeSpan.FromMinutes(734)),
			Author = "Bill Lumbergh",
			Name = "Milton's Stapler",
			Text = "Milton has been looking for his stapler.  It should be downstairs in storage room B."
		};
		yesterdayGroupModel.Children.Add(mailModel);

		mailModel = new MailTreeNodeModel {
			DateTime = DateTime.Today.Subtract(TimeSpan.FromDays(1)).Add(TimeSpan.FromMinutes(644)),
			Author = "Milton Waddams",
			Name = "Stapler",
			Text = "Excuse me, I believe Bill took my stapler.  Have you seen it?"
		};
		yesterdayGroupModel.Children.Add(mailModel);

		var lastWeekGroupModel = new TreeNodeModel {
			IsExpanded = true,
			Name = "Last Week"
		};
		rootModel.Children.Add(lastWeekGroupModel);

		mailModel = new MailTreeNodeModel {
			IsFlagged = true,
			DateTime = DateTime.Today.Subtract(TimeSpan.FromDays(3)).Add(TimeSpan.FromMinutes(841)),
			Author = "Actipro Software Sales",
			Name = "UI Controls Evaluation",
			Text = "How is the evaluation going?  I just wanted to check in."
		};
		lastWeekGroupModel.Children.Add(mailModel);

		mailModel = new MailTreeNodeModel {
			DateTime = DateTime.Today.Subtract(TimeSpan.FromDays(5)).Add(TimeSpan.FromMinutes(724)),
			Author = "Bill Lumbergh",
			Name = "Tree Control",
			Text = "Yeah, I'm going to need you to find a good tree control.  Maybe that Actipro one."
		};
		lastWeekGroupModel.Children.Add(mailModel);

		treeListBox.RootItem = rootModel;
		treeListBox.SelectedItem = mailModel;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnTreeListBoxSelectionChanged(object sender, RoutedEventArgs e) {
		messageData.Visibility = (treeListBox.SelectedItem is MailTreeNodeModel)
			? Visibility.Visible
			: Visibility.Collapsed;
	}

}
