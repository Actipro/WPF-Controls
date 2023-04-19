using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DataGridControl = System.Windows.Controls.DataGrid;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.Windows.Controls.DataGrid {

	/// <summary>
	/// Provides attached behavior for <see cref="DataGridControl"/> controls to customize the new row template.
	/// </summary>
	public static class NewRowTemplateBehavior {

		#region Dependency Property Keys

		/// <summary>
		/// Identifies the <c>DefaultTemplate</c> dependency property key.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <c>DefaultTemplate</c> dependency property key.</value>
		private static readonly DependencyPropertyKey DefaultTemplatePropertyKey = DependencyProperty.RegisterAttachedReadOnly("DefaultTemplate",
			typeof(ControlTemplate), typeof(NewRowTemplateBehavior), new FrameworkPropertyMetadata(null));

		#endregion // Dependency Property Keys

		#region Dependency Properties

		/// <summary>
		/// Identifies the read-only <c>DefaultTemplate</c> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <c>DefaultTemplate</c> dependency property.</value>
		public static readonly DependencyProperty DefaultTemplateProperty = DefaultTemplatePropertyKey.DependencyProperty;

		/// <summary>
		/// Identifies the <c>Template</c> attached dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <c>Template</c> attached dependency property.</value>
		public static readonly DependencyProperty TemplateProperty = DependencyProperty.RegisterAttached("Template",
			typeof(ControlTemplate), typeof(NewRowTemplateBehavior), new FrameworkPropertyMetadata(null, OnTemplatePropertyValueChanged));

		#endregion // Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Handles the <c>LoadingRow</c> event of a <see cref="DataGridControl"/>.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DataGridRowEventArgs"/> instance containing the event data.</param>
		[SuppressMessage("Microsoft.Portability", "CA1903:UseOnlyApiFromTargetedFramework", MessageId = "System.Windows.Data.CollectionView.#get_NewItemPlaceholder()")]
		private static void OnDataGridLoadingRow(object sender, DataGridRowEventArgs e) {
			DataGridControl datagrid = sender as DataGridControl;
			if (null == datagrid)
				return;

			if (CollectionView.NewItemPlaceholder == e.Row.Item) {
				// Save the default template, since we need to restore it later
				SetDefaultTemplate(datagrid, e.Row.Template);

				// Assign the custom template
				e.Row.Template = GetTemplate(datagrid);

				e.Row.MouseLeftButtonDown += new MouseButtonEventHandler(OnDataGridRowMouseLeftButtonDown);
			}
		}

		/// <summary>
		/// Handles the <c>RowEditEnding</c> event of a <see cref="DataGrid"/>.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DataGridRowEditEndingEventArgs"/> instance containing the event data.</param>
		[SuppressMessage("Microsoft.Portability", "CA1903:UseOnlyApiFromTargetedFramework", MessageId = "System.ComponentModel.IEditableCollectionView")]
		private static void OnDataGridRowEditEnding(object sender, DataGridRowEditEndingEventArgs e) {
			DataGridControl datagrid = sender as DataGridControl;
			if (null == datagrid)
				return;

			IEditableCollectionView collectionView = CollectionViewSource.GetDefaultView(datagrid.ItemsSource) as IEditableCollectionView;
			if (null != collectionView && collectionView.IsAddingNew) {
				// Need to wait till after the operation as the NewItemPlaceHolder is added after
				datagrid.Dispatcher.Invoke(DispatcherPriority.ApplicationIdle, new DispatcherOperationCallback(ResetNewItemTemplate), datagrid);
			}
		}

		/// <summary>
		/// Handles the <c>MouseLeftButtonDown</c> event of a <see cref="DataGridRow"/>.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
		[SuppressMessage("Microsoft.Portability", "CA1903:UseOnlyApiFromTargetedFramework", MessageId = "System.Windows.Data.CollectionView.#get_NewItemPlaceholder()")]
		private static void OnDataGridRowMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
			DataGridRow row = sender as DataGridRow;
			if (null == row)
				return;

			DataGridControl datagrid = VisualTreeHelperExtended.GetAncestor(row, typeof(DataGridControl)) as DataGridControl;
			if (null == datagrid)
				return;

			if (CollectionView.NewItemPlaceholder == row.Item) {
				ControlTemplate template = GetTemplate(datagrid);
				if (row.Template == template) {
					row.Template = GetDefaultTemplate(datagrid);
					row.UpdateLayout();

					datagrid.CurrentItem = row.Item;

					// 3/23/2010 - Get the first non-read only column (http://www.actiprosoftware.com/Support/Forums/ViewForumTopic.aspx?ForumTopicID=4710)
					DataGridColumn column = datagrid.Columns.FirstOrDefault(col => !col.IsReadOnly);
					if (column != null) {
						DataGridCell cell = VisualTreeHelperExtended.GetAncestor(column.GetCellContent(row), typeof(DataGridCell)) as DataGridCell;

						if (cell != null)
							cell.Focus();
					}

					datagrid.BeginEdit();
				}
			}
		}

		/// <summary>
		/// Handles the <c>UnloadingRow</c> event of a <see cref="DataGridControl"/>.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DataGridRowEventArgs"/> instance containing the event data.</param>
		[SuppressMessage("Microsoft.Portability", "CA1903:UseOnlyApiFromTargetedFramework", MessageId = "System.Windows.Data.CollectionView.#get_NewItemPlaceholder()")]
		private static void OnDataGridUnloadingRow(object sender, DataGridRowEventArgs e) {
			DataGridControl datagrid = sender as DataGridControl;
			if (null == datagrid)
				return;

			// Restore default template
			if (CollectionView.NewItemPlaceholder == e.Row.Item) {
				ControlTemplate defaultTemplate = GetDefaultTemplate(datagrid);
				if (null != defaultTemplate) {
					e.Row.Template = defaultTemplate;
					e.Row.MouseLeftButtonDown -= new MouseButtonEventHandler(OnDataGridRowMouseLeftButtonDown);

					SetDefaultTemplate(datagrid, null);
				}
			}
		}

		/// <summary>
		/// Called when <see cref="TemplateProperty"/> is changed.
		/// </summary>
		/// <param name="d">The dependency object that was changed.</param>
		/// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
		private static void OnTemplatePropertyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			DataGridControl datagrid = d as DataGridControl;
			if (null == datagrid)
				return;

			if (null != e.NewValue) {
				// Attach to events
				datagrid.LoadingRow += new EventHandler<DataGridRowEventArgs>(OnDataGridLoadingRow);
				datagrid.RowEditEnding += new EventHandler<DataGridRowEditEndingEventArgs>(OnDataGridRowEditEnding);
				datagrid.UnloadingRow += new EventHandler<DataGridRowEventArgs>(OnDataGridUnloadingRow);
			}
			else {
				// Detach from events
				datagrid.LoadingRow -= new EventHandler<DataGridRowEventArgs>(OnDataGridLoadingRow);
				datagrid.RowEditEnding -= new EventHandler<DataGridRowEditEndingEventArgs>(OnDataGridRowEditEnding);
				datagrid.UnloadingRow -= new EventHandler<DataGridRowEventArgs>(OnDataGridUnloadingRow);
			}

			// If the DataGrid has already been loaded, then we need to apply changes to the new row manually
			if (datagrid.IsLoaded)
				ResetNewItemTemplate(datagrid);
		}

		/// <summary>
		/// Resets control template for the <c>DataGridRow</c> that presents the <c>CollectionView.NewItemPlaceholder</c> item.
		/// </summary>
		/// <param name="obj">The <c>DataGrid</c>.</param>
		/// <returns><c>null</c>.</returns>
		[SuppressMessage("Microsoft.Portability", "CA1903:UseOnlyApiFromTargetedFramework", MessageId = "System.Windows.Data.CollectionView.#get_NewItemPlaceholder()")]
		private static object ResetNewItemTemplate(object obj) {
			DataGridControl datagrid = obj as DataGridControl;
			if (null == datagrid)
				return null;

			// Get the row for CollectionView.NewItemPlaceholder
			DataGridRow row = datagrid.GetRow(CollectionView.NewItemPlaceholder);
			if (null == row)
				return null;

			// Ensure it's template is correct
			ControlTemplate template = GetTemplate(datagrid);
			if (null == template)
				template = GetDefaultTemplate(datagrid);

			if (row.Template != template) {
				row.Template = template;
				row.UpdateLayout();
			}

			return null;
		}

		/// <summary>
		/// Sets the value of the <see cref="DefaultTemplatePropertyKey"/> attached property to a specified <see cref="DataGrid"/>.
		/// </summary>
		/// <param name="obj">The object to which the attached property is written.</param>
		/// <param name="value">
		/// A value indicating the default <see cref="ControlTemplate"/> to use for rows in a <see cref="DataGridControl"/>.
		/// </param>
		private static void SetDefaultTemplate(DataGridControl obj, ControlTemplate value) {
			if (null == obj) throw new ArgumentNullException("obj");
			obj.SetValue(NewRowTemplateBehavior.DefaultTemplatePropertyKey, value);
		}

		#endregion // NON-PUBLIC PROCEDURES

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the value of the <see cref="DefaultTemplateProperty"/> attached property for a specified <see cref="DataGridControl"/>.
		/// </summary>
		/// <param name="obj">The object to which the attached property is retrieved.</param>
		/// <returns>
		/// The default <see cref="ControlTemplate"/> to use for rows in a <see cref="DataGridControl"/>.
		/// </returns>
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static ControlTemplate GetDefaultTemplate(DataGridControl obj) {
			if (null == obj) throw new ArgumentNullException("obj");
			return (ControlTemplate)obj.GetValue(NewRowTemplateBehavior.DefaultTemplateProperty);
		}

		/// <summary>
		/// Gets the value of the <see cref="TemplateProperty"/> attached property for a specified <see cref="DataGridControl"/>.
		/// </summary>
		/// <param name="obj">The object to which the attached property is retrieved.</param>
		/// <returns>
		/// The <see cref="ControlTemplate"/> to use for the "new" row in a <see cref="DataGridControl"/>.
		/// </returns>
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static ControlTemplate GetTemplate(DataGridControl obj) {
			if (null == obj) throw new ArgumentNullException("obj");
			return (ControlTemplate)obj.GetValue(NewRowTemplateBehavior.TemplateProperty);
		}
		/// <summary>
		/// Sets the value of the <see cref="TemplateProperty"/> attached property to a specified <see cref="DataGridControl"/>.
		/// </summary>
		/// <param name="obj">The object to which the attached property is written.</param>
		/// <param name="value">
		/// A value indicating the <see cref="ControlTemplate"/> to use for the "new" row in a <see cref="DataGridControl"/>.
		/// </param>
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static void SetTemplate(DataGridControl obj, ControlTemplate value) {
			if (null == obj) throw new ArgumentNullException("obj");
			obj.SetValue(NewRowTemplateBehavior.TemplateProperty, value);
		}

		#endregion // PUBLIC PROCEDURES
	}
}
