---
title: "Headers and Content"
page-title: "Headers and Content - ExplorerBar Features"
order: 3
---
# Headers and Content

ExplorerBar has several configuration options for the display of its expander panes.

## Headers

For pane headers, the `Expander.Header` can be set to either a string or an [ImageTextInfo](xref:@ActiproUIRoot.Controls.ImageTextInfo) object.

If the header is a string, it will be displayed like `File and Folder Tasks` in the screenshot above.  Alternatively, the [ImageTextInfo](xref:@ActiproUIRoot.Controls.ImageTextInfo) class gives you the option of displaying text and an image.

## Image/Text and Alternate Pane Style Example

This sample shows how to create an expander pane with an alternate style (which attracts attention and is sometimes used for the first pane item).  It also has an image in the header, showing the use of [ImageTextInfo](xref:@ActiproUIRoot.Controls.ImageTextInfo).  Note how some extra margin space is defined for the top of the pane since the image will stretch into the margin area.

```xaml
<navigation:ExplorerBar>
    <shared:AnimatedExpander IsExpanded="True" Margin="0,7,0,0"
		Style="{DynamicResource {x:Static themes:ExplorerBarCommonDictionary.ExpanderAlternateStyleKey}}">
		<shared:AnimatedExpander.Header>
			<shared:ImageTextInfo ImageSourceLarge="/Resources/Images/PictureTasks32.png" Text="Picture Tasks" />
		</shared:AnimatedExpander.Header>
		<!-- Content here -->
	</shared:AnimatedExpander>
<navigation:ExplorerBar>
```

## Content

Any sort of content can be placed in the content portion of a pane.  A common practice is to use a `StackPanel` of `Hyperlink` controls in some panes.  But the general rule of thumb for content of a pane is that it should relate in some way to the context outside of the ExplorerBar, such as providing tasks or information related to the outer content.
