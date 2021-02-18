---
title: "Side-by-Side Dock Sites"
page-title: "Side-by-Side Dock Sites - Docking & MDI Layout, Globalization, and Accessibility Features"
order: 4
---
# Side-by-Side Dock Sites

Dock sites are completely self-contained, meaning that you can have two [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite) in the same `Window` and tool windows belonging to one can't be moved into other, and vice versa.

![Screenshot](../images/side-by-side-dock-sites.png)

*Two separate dock sites on the same Window*

Many other competitive docking products don't support this feature.

## Configuration and Features

When placing two separate [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite) controls within a root container, they will automatically be independent of each other.

Tool windows will not be able to be dragged from one dock site into the other, unless you [link the dock sites](linked-dock-sites.md).

## A XAML Example

This sample XAML code shows how to create a side-by-side scenario.

```xaml
<Grid>
	<Grid.RowDefinitions>
		<RowDefinition Height="Auto" />
		<RowDefinition Height="*" />			
	</Grid.RowDefinitions>
	<Grid.ColumnDefinitions>
		<ColumnDefinition Width="*" />
		<ColumnDefinition Width="7" />
		<ColumnDefinition Width="*" />
	</Grid.ColumnDefinitions>
	
	<Border Padding="3">
		<TextBlock Text="DockSite #1" />
	</Border>

	<!-- DockSite #1 -->
	<docking:DockSite Grid.Row="1">
		<docking:DockSite.AutoHideLeftContainers>
			<docking:ToolWindowContainer>
				<docking:ToolWindow Title="DockSite 1-3" />
				<docking:ToolWindow Title="DockSite 1-4" />
			</docking:ToolWindowContainer>
		</docking:DockSite.AutoHideLeftContainers>
		
		<docking:SplitContainer>
			<docking:Workspace/>
			
			<docking:ToolWindowContainer>
				<docking:ToolWindow Title="DockSite 1-1" />
				<docking:ToolWindow Title="DockSite 1-2" />
			</docking:ToolWindowContainer>
		</docking:SplitContainer>
	</docking:DockSite>
	
	<Border Grid.Column="2" Padding="3">
		<TextBlock Text="DockSite #2" />
	</Border>
	
	<!-- DockSite #2 -->
	<docking:DockSite Grid.Row="1" Grid.Column="2">
		<docking:DockSite.AutoHideRightContainers>
			<docking:ToolWindowContainer>
				<docking:ToolWindow Title="DockSite 2-3" />
				<docking:ToolWindow Title="DockSite 2-4" />
			</docking:ToolWindowContainer>
		</docking:DockSite.AutoHideRightContainers>
		
		<docking:SplitContainer>				
			<docking:ToolWindowContainer>
				<docking:ToolWindow Title="DockSite 2-1" />
				<docking:ToolWindow Title="DockSite 2-2" />
			</docking:ToolWindowContainer>
		
			<docking:Workspace/>
		</docking:SplitContainer>
	</docking:DockSite>
	
</Grid>
```
