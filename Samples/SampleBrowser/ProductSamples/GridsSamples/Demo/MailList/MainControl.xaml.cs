using ActiproSoftware.ProductSamples.GridsSamples.Common;
using System;
using System.Windows;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.MailList {

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

			this.GenerateItems();

			treeListBox.FocusItem(treeListBox.SelectedItem);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Generates the sample items.
		/// </summary>
		private void GenerateItems() {
			var rootModel = new TreeNodeModel();

			var todayGroupModel = new TreeNodeModel();
			todayGroupModel.IsExpanded = true;
			todayGroupModel.Name = "Today";
			rootModel.Children.Add(todayGroupModel);

			var mailModel = new MailTreeNodeModel();
			mailModel.IsFlagged = true;
			mailModel.DateTime = DateTime.Today.Add(TimeSpan.FromMinutes(560));
			mailModel.Author = "Actipro Software Sales";
			mailModel.Name = "TreeListBox Features";
			mailModel.Text = "The TreeListBox has some amazing features.";
			todayGroupModel.Children.Add(mailModel);
			
			var yesterdayGroupModel = new TreeNodeModel();
			yesterdayGroupModel.IsExpanded = true;
			yesterdayGroupModel.Name = "Yesterday";
			rootModel.Children.Add(yesterdayGroupModel);
			
			mailModel = new MailTreeNodeModel();
			mailModel.DateTime = DateTime.Today.Subtract(TimeSpan.FromDays(1)).Add(TimeSpan.FromMinutes(734));
			mailModel.Author = "Bill Lumbergh";
			mailModel.Name = "Milton's Stapler";
			mailModel.Text = "Milton has been looking for his stapler.  It should be downstairs in storage room B.";
			yesterdayGroupModel.Children.Add(mailModel);
			
			mailModel = new MailTreeNodeModel();
			mailModel.DateTime = DateTime.Today.Subtract(TimeSpan.FromDays(1)).Add(TimeSpan.FromMinutes(644));
			mailModel.Author = "Milton Waddams";
			mailModel.Name = "Stapler";
			mailModel.Text = "Excuse me, I believe Bill took my stapler.  Have you seen it?";
			yesterdayGroupModel.Children.Add(mailModel);
			
			var lastWeekGroupModel = new TreeNodeModel();
			lastWeekGroupModel.IsExpanded = true;
			lastWeekGroupModel.Name = "Last Week";
			rootModel.Children.Add(lastWeekGroupModel);
			
			mailModel = new MailTreeNodeModel();
			mailModel.IsFlagged = true;
			mailModel.DateTime = DateTime.Today.Subtract(TimeSpan.FromDays(3)).Add(TimeSpan.FromMinutes(841));
			mailModel.Author = "Actipro Software Sales";
			mailModel.Name = "UI Controls Evaluation";
			mailModel.Text = "How is the evaluation going?  I just wanted to check in.";
			lastWeekGroupModel.Children.Add(mailModel);
			
			mailModel = new MailTreeNodeModel();
			mailModel.DateTime = DateTime.Today.Subtract(TimeSpan.FromDays(5)).Add(TimeSpan.FromMinutes(724));
			mailModel.Author = "Bill Lumbergh";
			mailModel.Name = "Tree Control";
			mailModel.Text = "Yeah, I'm going to need you to find a good tree control.  Maybe that Actipro one.";
			lastWeekGroupModel.Children.Add(mailModel);
			
			treeListBox.RootItem = rootModel;
			treeListBox.SelectedItem = mailModel;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the selection has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains data related to this event.</param>
		private void OnTreeListBoxSelectionChanged(object sender, RoutedEventArgs e) {
			messageData.Visibility = (treeListBox.SelectedItem is MailTreeNodeModel ? Visibility.Visible : Visibility.Collapsed);
		}

	}

}
