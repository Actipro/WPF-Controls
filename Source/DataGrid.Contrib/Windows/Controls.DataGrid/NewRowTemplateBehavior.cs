using ActiproSoftware.Windows.Extensions;
using System.Windows.Threading;
using DataGridControl = System.Windows.Controls.DataGrid;

namespace ActiproSoftware.Windows.Controls.DataGrid;

/// <summary>
/// Provides attached behavior for <see cref="DataGridControl"/> controls to customize the new row template.
/// </summary>
public static class NewRowTemplateBehavior {

	#region Dependency Property Keys

	/// <summary>
	/// Defines the <c>DefaultTemplate</c> attached property key.
	/// </summary>
	private static readonly DependencyPropertyKey DefaultTemplatePropertyKey
		= DependencyProperty.RegisterAttachedReadOnly("DefaultTemplate", typeof(ControlTemplate), typeof(NewRowTemplateBehavior), new FrameworkPropertyMetadata(defaultValue: null));

	#endregion

	#region Dependency Properties

	/// <summary>
	/// Defines the <c>DefaultTemplate</c> attached property.
	/// </summary>
	public static readonly DependencyProperty DefaultTemplateProperty
		= DefaultTemplatePropertyKey.DependencyProperty;

	/// <summary>
	/// Defines the <c>Template</c> attached property.
	/// </summary>
	public static readonly DependencyProperty TemplateProperty
		= DependencyProperty.RegisterAttached("Template", typeof(ControlTemplate), typeof(NewRowTemplateBehavior), new FrameworkPropertyMetadata(defaultValue: null, OnTemplatePropertyValueChanged));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private static void OnDataGridLoadingRow(object? sender, DataGridRowEventArgs e) {
		if (sender is not DataGridControl datagrid)
			return;

		if (CollectionView.NewItemPlaceholder == e.Row.Item) {
			// Save the default template, since we need to restore it later
			SetDefaultTemplate(datagrid, e.Row.Template);

			// Assign the custom template
			e.Row.Template = GetTemplate(datagrid);

			e.Row.MouseLeftButtonDown += OnDataGridRowMouseLeftButtonDown;
		}
	}

	private static void OnDataGridRowEditEnding(object? sender, DataGridRowEditEndingEventArgs e) {
		if (sender is not DataGridControl datagrid)
			return;

		if (CollectionViewSource.GetDefaultView(datagrid.ItemsSource) is IEditableCollectionView { IsAddingNew: true }) {
			// Need to wait till after the operation as the NewItemPlaceHolder is added after
			datagrid.Dispatcher.Invoke(DispatcherPriority.ApplicationIdle, new DispatcherOperationCallback(ResetNewItemTemplate), datagrid);
		}
	}

	private static void OnDataGridRowMouseLeftButtonDown(object? sender, MouseButtonEventArgs e) {
		if (sender is not DataGridRow row)
			return;

		var datagrid = row.FindAncestorOfType<DataGridControl>();
		if (datagrid is null)
			return;

		if (CollectionView.NewItemPlaceholder == row.Item) {
			var template = GetTemplate(datagrid);
			if (row.Template == template) {
				row.Template = GetDefaultTemplate(datagrid);
				row.UpdateLayout();

				datagrid.CurrentItem = row.Item;

				// 3/23/2010 - Get the first non-read only column (http://www.actiprosoftware.com/Support/Forums/ViewForumTopic.aspx?ForumTopicID=4710)
				var column = datagrid.Columns.FirstOrDefault(c => !c.IsReadOnly);
				if (column is not null) {
					var cell = column.GetCellContent(row).FindAncestorOfType<DataGridCell>();
					cell?.Focus();
				}

				datagrid.BeginEdit();
			}
		}
	}

	private static void OnDataGridUnloadingRow(object? sender, DataGridRowEventArgs e) {
		if (sender is not DataGridControl datagrid)
			return;

		// Restore default template
		if (CollectionView.NewItemPlaceholder == e.Row.Item) {
			var defaultTemplate = GetDefaultTemplate(datagrid);
			if (defaultTemplate is not null) {
				e.Row.Template = defaultTemplate;
				e.Row.MouseLeftButtonDown -= new MouseButtonEventHandler(OnDataGridRowMouseLeftButtonDown);

				SetDefaultTemplate(datagrid, null);
			}
		}
	}

	private static void OnTemplatePropertyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
		if (d is not DataGridControl datagrid)
			return;

		if (e.NewValue is not null) {
			// Attach to events
			datagrid.LoadingRow += OnDataGridLoadingRow;
			datagrid.RowEditEnding += OnDataGridRowEditEnding;
			datagrid.UnloadingRow += OnDataGridUnloadingRow;
		}
		else {
			// Detach from events
			datagrid.LoadingRow -= OnDataGridLoadingRow;
			datagrid.RowEditEnding -= OnDataGridRowEditEnding;
			datagrid.UnloadingRow -= OnDataGridUnloadingRow;
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
	private static object? ResetNewItemTemplate(object obj) {
		if (obj is not DataGridControl datagrid)
			return null;

		// Get the row for CollectionView.NewItemPlaceholder
		var row = datagrid.GetRow(CollectionView.NewItemPlaceholder);
		if (row is null)
			return null;

		// Ensure it's template is correct
		var template = GetTemplate(datagrid)
			?? GetDefaultTemplate(datagrid);

		if (row.Template != template) {
			row.Template = template;
			row.UpdateLayout();
		}

		return null;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Gets the value of the <see cref="DefaultTemplateProperty"/> attached property for a specified <see cref="DataGridControl"/>.
	/// </summary>
	/// <param name="obj">The object to which the attached property is retrieved.</param>
	public static ControlTemplate? GetDefaultTemplate(DataGridControl obj)
		=> (ControlTemplate)obj.GetValue(DefaultTemplateProperty);

	/// <summary>
	/// Sets the value of the <see cref="DefaultTemplatePropertyKey"/> attached property to a specified <see cref="DataGrid"/>.
	/// </summary>
	/// <param name="obj">The object to which the attached property is written.</param>
	/// <param name="value">
	/// A value indicating the default <see cref="ControlTemplate"/> to use for rows in a <see cref="DataGridControl"/>.
	/// </param>
	private static void SetDefaultTemplate(DataGridControl obj, ControlTemplate? value)
		=> obj.SetValue(DefaultTemplatePropertyKey, value);

	/// <summary>
	/// Gets the value of the <see cref="TemplateProperty"/> attached property for a specified <see cref="DataGridControl"/>.
	/// </summary>
	/// <param name="obj">The object to which the attached property is retrieved.</param>
	public static ControlTemplate? GetTemplate(DataGridControl obj)
		=> (ControlTemplate)obj.GetValue(TemplateProperty);

	/// <summary>
	/// Sets the value of the <see cref="TemplateProperty"/> attached property to a specified <see cref="DataGridControl"/>.
	/// </summary>
	/// <param name="obj">The object to which the attached property is written.</param>
	/// <param name="value">
	/// A value indicating the <see cref="ControlTemplate"/> to use for the "new" row in a <see cref="DataGridControl"/>.
	/// </param>
	public static void SetTemplate(DataGridControl obj, ControlTemplate? value)
		=> obj.SetValue(TemplateProperty, value);

}
