---
title: "Tag Aggregators"
page-title: "Tag Aggregators - Tagging - SyntaxEditor Text/Parsing Framework"
order: 4
---
# Tag Aggregators

Tag aggregators are objects that can monitor all the available taggers for a specific tag type and can return a unioned set of tagged snapshot ranges for a requested snapshot range.  They are used for consuming the results of taggers.

## When to Use Tag Aggregators?

Tag aggregators are used when you need to collect all the available tagged ranges for a certain snapshot range.  Most often, tag aggregators are used in conjunction with adornment layers managers in editors.  In this scenario, a tagger is used to mark ranges of text with a custom tag, basically indicating the ranges that need some sort of adornment.  Then an adornment layer manager creates a tag aggregator for this custom tag.  When the adornment layer manager builds adornments for a certain editor view line, it uses the tag aggregator to get the custom tags that fall in that line's text range and makes appropriate adornments based on the data passed in the custom tag.

To sum up, think of tags as data.  Taggers are the data providers.  Tag aggregators are the data consumers.

## Document- or View-Oriented Tag Aggregators

As discussed in the [Taggers and Tagger Providers](taggers.md) topic, tagger providers can be oriented to create taggers either for an [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument) or for an [ITextView](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.ITextView).  Similarly, tag aggregators can be document- or view-oriented.

Document-oriented tag aggregators are created by calling the [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument).[CreateTagAggregator](xref:ActiproSoftware.Text.ICodeDocument.CreateTagAggregator*) method.  The type of tag to collect is indicated as a type parameter to that method.  The tag aggregator collects all the appropriate tags from any taggers associated with the document.

View-oriented tag aggregators are created by calling the [ITextView](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.ITextView).[CreateTagAggregator](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.ITextView.CreateTagAggregator*) method.  The type of tag to collect is indicated as a type parameter to that method.  The tag aggregator collects all the appropriate tags from any taggers associated with the view's document or with that specific view.

## Collecting Tags

The [ITagAggregator<T>](xref:ActiproSoftware.Text.Tagging.ITagAggregator`1) inteface defines two overloads of a [GetTags](xref:ActiproSoftware.Text.Tagging.ITagAggregator`1.GetTags*) method.  Both methods allow you to request the tags that fall into one or more snapshot ranges.

When either of these methods are called, the tag aggregator examines all of the appropriate taggers that provide the tag type associated with the tag aggregator.

## Notification of Tag Changes

Whenever one of the taggers being monitored by a tag aggregator raises an event that notifies its tags have changed, the tag aggregator bubbles the event up as well via the [TagsChanged](xref:ActiproSoftware.Text.Tagging.ITagAggregatorBase.TagsChanged) event.  Thus any listeners to this event can refresh themselves by requesting the updated tags in the affected range.
