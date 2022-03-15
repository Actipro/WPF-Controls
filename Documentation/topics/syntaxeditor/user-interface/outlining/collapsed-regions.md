---
title: "Collapsing Regions Without Outlining"
page-title: "Collapsing Regions Without Outlining - SyntaxEditor Outlining and Collapsing Features"
order: 5
---
# Collapsing Regions Without Outlining

The code outlining feature decribed in earlier topics supports the collapsing of outlining nodes, where contained text becomes hidden.  SyntaxEditor's low-level collapsed region management feature is harnessed to achieve this functionality.  This same collapsed regions mechanism can be used independently of code outlining too.

Various text regions can be collapsed (hidden) independent of the outlining features.  And in these cases you can even optionally replace the collapsed region with an adornment.

## Collapsed Region Manager

The [ICollapsedRegionManager](xref:@ActiproUIRoot.Controls.SyntaxEditor.ICollapsedRegionManager) is the object that tracks all the collapsed regions for a particular view.  A view's collapsed region manager is accessible via the [ITextView](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextView).[CollapsedRegionManager](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextView.CollapsedRegionManager) property.

When the view's text formatting engine builds up layout data for a view line, it consults the collapsed region manager to see if there are any collapsed regions that it needs to be aware of.  When one is found, it ignores the contained text and moves onto the remaining text that should be rendered on the view line.

The [ICollapsedRegionManager](xref:@ActiproUIRoot.Controls.SyntaxEditor.ICollapsedRegionManager) has two main functions:

- Raise an event when the collapsed regions change
- Allow consumers to return collapsed regions that contain an offset or interset a text range

### RegionsChanged Event

The manager watches all document text changes and also changes published by [ICollapsedRegionTag](xref:ActiproSoftware.Text.Tagging.ICollapsedRegionTag) taggers.  When any of those changes occur, the manager updates its collapsed regions accordingly.  If any regions are added or removed, the [RegionsChanged](xref:@ActiproUIRoot.Controls.SyntaxEditor.ICollapsedRegionManager.RegionsChanged) event is raised, passing along the [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) that was changed.

### Returning Intersecting Collapsed Regions

The [ICollapsedRegionManager](xref:@ActiproUIRoot.Controls.SyntaxEditor.ICollapsedRegionManager).[GetCollapsedRange](xref:@ActiproUIRoot.Controls.SyntaxEditor.ICollapsedRegionManager.GetCollapsedRange*) returns the [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange), if any, that contains the specified offset.

The [GetCollapsedRanges](xref:@ActiproUIRoot.Controls.SyntaxEditor.ICollapsedRegionManager.GetCollapsedRanges*) method returns a [NormalizedTextSnapshotRangeCollection](xref:ActiproSoftware.Text.NormalizedTextSnapshotRangeCollection) of collapsed snapshot ranges that intersect the specified range, if any.

## Tagging Collapsed Regions

The [ICollapsedRegionTag](xref:ActiproSoftware.Text.Tagging.ICollapsedRegionTag) is a dedicated tag type that the [ICollapsedRegionManager](xref:@ActiproUIRoot.Controls.SyntaxEditor.ICollapsedRegionManager) watches for and uses to determine which regions are collapsed.  SyntaxEditor's text formatting engine then consults the collapsed region manager and uses its data to determine when text should be rendered as collapsed (hidden).

Therefore to collapse a region of text without using code outlining, all you need to do is create a [tagger](../../text-parsing/tagging/taggers.md) for the [ICollapsedRegionTag](xref:ActiproSoftware.Text.Tagging.ICollapsedRegionTag) type and return appropriate tag ranges when requested.  You can create a custom class that implements [ICollapsedRegionTag](xref:ActiproSoftware.Text.Tagging.ICollapsedRegionTag) and use that in your tagger results.  The tagger can in installed by your language via a [tagger provider](../../text-parsing/tagging/taggers.md) service.

At this point if your tagger is in use by your language, you should notice hidden text wherever your tagger designates.

## Rendering Intra-Text Adornments in Place of Collapsed Regions

Collapsed regions by default will not render anything in their place.  However there is an option to render an adornment in place of the collapsed region.  The code outlining feature uses this option to render the "..." sort of adornments wherever an outlining node is collapsed.

Since the adornments that are inserted in place of collapsed regions should be inserted in between the surrounding text characters, intra-text adornments are used.

> [!NOTE]
> See the [Intra-Text Adornments](../adornment/intra-text-adornments.md) topic for details on how to create intra-text adornments.

What you do is have an [IIntraTextSpacerTag](xref:ActiproSoftware.Text.Tagging.IIntraTextSpacerTag) tagger return the same tag ranges as the [ICollapsedRegionTag](xref:ActiproSoftware.Text.Tagging.ICollapsedRegionTag) ranges that need adornments.  The text formatter will look for intra-text spacers that match the same range as collapsed ranges and will render space in those locations.  Then an intra-text adornment manager can place an adornment in that space.

To sum up, an [ICollapsedRegionTag](xref:ActiproSoftware.Text.Tagging.ICollapsedRegionTag) is returned by a tagger over a certain range.  An [IIntraTextSpacerTag](xref:ActiproSoftware.Text.Tagging.IIntraTextSpacerTag) is also returned by a tagger over the same range.  An [intra-text adornment manager](../adornment/intra-text-adornments.md) places an adornment in the space that has been reserved.

> [!TIP]
> For this mechanism, you can have a single tagger return both the [ICollapsedRegionTag](xref:ActiproSoftware.Text.Tagging.ICollapsedRegionTag) and [IIntraTextSpacerTag](xref:ActiproSoftware.Text.Tagging.IIntraTextSpacerTag) ranges.  Just create the tagger for [ICollapsedRegionTag](xref:ActiproSoftware.Text.Tagging.ICollapsedRegionTag) and explicitly implement the interface of [ITagger<T>](xref:ActiproSoftware.Text.Tagging.ITagger`1) for [IIntraTextSpacerTag](xref:ActiproSoftware.Text.Tagging.IIntraTextSpacerTag).
