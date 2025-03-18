using System;
using System.Windows;
using ActiproSoftware.Shell;
using ActiproSoftware.Windows.Controls.Grids;

namespace ActiproSoftware.ProductSamples.ShellSamples.QuickStart.ShellListViewColumns {
	
	/// <summary>
	/// Provides an <see cref="IShellService"/> implementation that can inject custom shell objects.
	/// </summary>
	public class CustomShellProperty : IShellProperty {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="IShellProperty.CanonicalName"/>
		public string CanonicalName
			=> "Kind";

		/// <inheritdoc cref="IShellProperty.ColumnDefaultSortDirection"/>
		public ColumnSortDirection ColumnDefaultSortDirection
			=> ColumnSortDirection.Ascending;

		/// <inheritdoc cref="IShellProperty.ColumnHorizontalAlignment"/>
		public HorizontalAlignment ColumnHorizontalAlignment
			=> HorizontalAlignment.Left;

		/// <inheritdoc cref="IShellProperty.DefaultColumnWidth"/>
		public double DefaultColumnWidth
			=> 150;

		/// <inheritdoc cref="IShellProperty.IsNameProperty"/>
		public bool IsNameProperty
			=> false;

		/// <inheritdoc cref="IShellProperty.Key"/>
		public object Key
			=> CanonicalName;

		/// <inheritdoc cref="IShellProperty.Name"/>
		public string Name
			=> CanonicalName;

	}

}