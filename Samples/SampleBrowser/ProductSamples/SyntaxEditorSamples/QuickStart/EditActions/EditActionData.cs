using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.EditActions {

	/// <summary>
	/// Stores data about an edit action.
	/// </summary>
	public class EditActionData {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the <see cref="IEditAction"/> associated with this data.
		/// </summary>
		/// <value>The <see cref="IEditAction"/> associated with this data.</value>
		public IEditAction Action { get; set; }

		/// <summary>
		/// Gets or sets the category.
		/// </summary>
		/// <value>The category.</value>
		public string Category { get; set; }

		/// <summary>
		/// Gets or sets the key that by default executes the edit action.
		/// </summary>
		/// <value>The key that by default executes the edit action.</value>
		public string Key { get; set; }

		/// <summary>
		/// Gets the string key that uniquely identifies the <see cref="Action"/>.
		/// </summary>
		/// <value>The string key that uniquely identifies the <see cref="Action"/>.</value>
		public string Name { 
			get {
				return this.Action.Key;
			}
		}
		
	}
}