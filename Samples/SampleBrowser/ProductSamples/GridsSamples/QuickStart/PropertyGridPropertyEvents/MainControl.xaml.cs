using System;
using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Windows.Controls.Grids;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridPropertyEvents {

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
			
			Person parent = new Person() { FirstName = "Preston", LastName = "Mansfield", Birthday = new DateTime(1940, 1, 30, 0, 0, 0) };

			Person child = new Person() { FirstName = "Bob", LastName = "Mansfield", Birthday = new DateTime(1960, 9, 18, 0, 0, 0) };
			child.Children.Add(new Person() { FirstName = "Bobby", LastName = "Mansfield", Birthday = new DateTime(1980, 4, 19, 0, 0, 0) });
			child.Children.Add(new Person() { FirstName = "Allie", LastName = "Mansfield", Birthday = new DateTime(1984, 7, 10, 0, 0, 0) });
			child.Children.Add(new Person() { FirstName = "Will", LastName = "Mansfield", Birthday = new DateTime(1990, 1, 2, 0, 0, 0) });
			parent.Children.Add(child);

			child = new Person() { FirstName = "Mary", LastName = "Poppings", Birthday = new DateTime(1961, 12, 1, 0, 0, 0) };
			child.Children.Add(new Person() { FirstName = "Zelda", LastName = "Poppings", Birthday = new DateTime(1985, 4, 29, 0, 0, 0) });
			child.Children.Add(new Person() { FirstName = "Link", LastName = "Poppings", Birthday = new DateTime(1987, 3, 31, 0, 0, 0) });
			parent.Children.Add(child);

			child = new Person() { FirstName = "Johnny", LastName = "Mansfield", Birthday = new DateTime(1962, 11, 15, 0, 0, 0) };
			parent.Children.Add(child);

			this.DataContext = parent;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Appends a message to the events <see cref="ListBox"/>.
		/// </summary>
		/// <param name="text">The text to append.</param>
		private void AppendMessage(string text) {
			var item = new ListBoxItem();
			item.Content = text;
			eventsListBox.Items.Add(item);
			eventsListBox.SelectedItem = item;
			eventsListBox.ScrollIntoView(item);
		}

		/// <summary>
		/// Handles the Click event of the clear Button control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnClearButtonClick(object sender, RoutedEventArgs e) {
			eventsListBox.Items.Clear();
		}
		
		/// <summary>
		/// Occurs after a <see cref="IPropertyModel"/> representing a child is added to a parent <see cref="IPropertyModel"/>.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <c>PropertyModelChildChangeEventArgs</c> that contains the event data.</param>
		private void OnPropertyGridChildPropertyAdded(object sender, PropertyModelChildChangeEventArgs e) {
			var person = e.ParentPropertyModel.Target as Person;
			var message = string.Format("Added child to {0}, {1}", person.LastName, person.FirstName);
			this.AppendMessage(message);
			e.Handled = true;
		}
		
		/// <summary>
		/// Occurs before a <see cref="IPropertyModel"/> representing a child is added to a parent <see cref="IPropertyModel"/>.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <c>PropertyModelChildChangeEventArgs</c> that contains the event data.</param>
		private void OnPropertyGridChildPropertyAdding(object sender, PropertyModelChildChangeEventArgs e) {
			var person = e.ParentPropertyModel.Target as Person;
			var message = string.Format("Adding child to {0}, {1}", person.LastName, person.FirstName);
			this.AppendMessage(message);

			var result = MessageBox.Show("Are you sure you want to add a new child?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
			e.Cancel = (MessageBoxResult.No == result);
			e.Handled = true;
		}
		
		/// <summary>
		/// Occurs after a <see cref="IPropertyModel"/> representing a child is removed from a parent <see cref="IPropertyModel"/>.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <c>PropertyModelChildChangeEventArgs</c> that contains the event data.</param>
		private void OnPropertyGridChildPropertyRemoved(object sender, PropertyModelChildChangeEventArgs e) {
			var parent = e.ParentPropertyModel.Target as Person;
			var child = e.ChildPropertyModel.Value as Person;
			var message = string.Format("Removed child {0}, {1} from parent {2}, {3}",child.LastName, child.FirstName, parent.LastName, parent.FirstName);
			this.AppendMessage(message);
			e.Handled = true;
		}
		
		/// <summary>
		/// Occurs before a <see cref="IPropertyModel"/> representing a child is removed from a parent <see cref="IPropertyModel"/>.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <c>PropertyModelChildChangeEventArgs</c> that contains the event data.</param>
		private void OnPropertyGridChildPropertyRemoving(object sender, PropertyModelChildChangeEventArgs e) {
			var parent = e.ParentPropertyModel.Target as Person;
			var child = e.ChildPropertyModel.Value as Person;
			var message = string.Format("Removing child {0}, {1} from parent {2}, {3}", child.LastName, child.FirstName, parent.LastName, parent.FirstName);
			this.AppendMessage(message);

			var result = MessageBox.Show(string.Format("Are you sure you want to remove {0}, {1}?", child.LastName, child.FirstName),
				"Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
			e.Cancel = (MessageBoxResult.No == result);
			e.Handled = true;
		}
		
		/// <summary>
		/// Occurs after the value is set for an <see cref="IPropertyModel"/>.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <c>PropertyModelValueChangeEventArgs</c> that contains the event data.</param>
		private void OnPropertyGridPropertyValueChanged(object sender, PropertyModelValueChangeEventArgs e) {
			var person = e.PropertyModel.Target as Person;
			var message = string.Format("Changed {0} on {1}, {2} (value = {3})", e.PropertyModel.Name, person.LastName, person.FirstName, e.Value);
			this.AppendMessage(message);
			e.Handled = true;
		}
		
		/// <summary>
		/// Occurs before the value is set for an <see cref="IPropertyModel"/>, allowing the value change to be canceled.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <c>PropertyModelValueChangeEventArgs</c> that contains the event data.</param>
		private void OnPropertyGridPropertyValueChanging(object sender, PropertyModelValueChangeEventArgs e) {
			var person = e.PropertyModel.Target as Person;
			var message = string.Format("Changing {0} on {1}, {2} (value = {3})", e.PropertyModel.Name, person.LastName, person.FirstName, e.Value);
			this.AppendMessage(message);
			e.Handled = true;

			if (true.Equals(this.CancelPropertyChanges))
				e.Cancel = true;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets whether property changes should be canceled.
		/// </summary>
		/// <value>
		/// <c>true</c> if property changes should be canceled; otherwise, <c>false</c>.
		/// </value>
		public bool CancelPropertyChanges { get; set; }

	}

}