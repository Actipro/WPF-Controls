---
title: "Overview"
page-title: "Tagging - SyntaxEditor Text/Parsing Framework"
order: 1
---
# Overview

Tagging is a generic mechanism that allows any sort of data to be associated with a text range.  This tagged data can then be retrieved on-demand by tag aggregators.  Tagging is the core mechanism behind features such as tokenization, classification (which in turn drives syntax highlighting when used in an editor), and more.

## Tags and Classification Types

Tags are objects that store data and can be applied to a text range.  Classification types are used throughout the product to categorize objects.  Both are key pieces towards understanding tagging.

See the [Tags and Classification Types](basic-concepts.md) topic for more information.

## Taggers and Tagger Providers

Taggers are objects that can return tagged snapshot ranges for any requested snapshot range.  They are generally created by tagger providers, which are language service.  Tagger results are then consumed by tag aggregators.

See the [Taggers and Tagger Providers](taggers.md) topic for more information.

## Tag Aggregators

Tag aggregators are objects that can monitor all the available taggers for a specific tag type and can return a unioned set of tagged snapshot ranges for a requested snapshot range.  They are used for consuming the results of taggers.

See the [Tag Aggregators](tag-aggregators.md) topic for more information.
