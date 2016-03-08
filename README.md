# Sitecore Content Tree Data Anonymizer Module Readme

> Note: This module supports Sitecore 8.1+  For prior versions, please submit an Issue and I will try to add support.  You can also submit a pull request.

## Overview

The Sitecore Data Anonymizer allows anonymization of the field values on items. 
Anonymization is performed per template type on a field by field basis.
The Data Anonymizer allows administrators to anonymize data within Sitecore's content tree. 
A few examples of how data can be anonymized are as follows:

1. Paragraph text replaced with Lorem Ipsom text.
1. First and last names replaced with randomly selected names.
1. Email addresses replaced with new addresses based on the randomly generated names.
1. Dates replaced with randomly generated dates.

There may be several reasons to anonymize data.

1. Data from an existing client implmentation needs to be anonymized so it can be used for a demo.
1. Data from live should not be brought down to development due to confidentiality.  The data can be anonymized.

### Features

Here are the key features of the Data Anonymizer.

1. Anonymize the values of fields on items based on the underlying template.
1. Anonmization is applied to selected items of the selected template type.
1. Only the latest version of the items are anonymized. **All prior versions of an item will be removed.**
1. All language versions are anonymized.  Currently english based content will be used for some anonymization types.
1. Only fields with an anonymization type selected will be anonymized.
1. Only fields with inner values are anonymized.  Fields containing the standard value, default value, fallback value, or inherited value are not.
1. 30+ out of the box field value anonymization formats
1. Basic custom field formats are supported.  This allows combining one or more field values into a string for use on another field.
1. Item renaming based on custom fields is supported.  Items names will be updated to follow the configured Sitecore naming conventions.
1. Global search and replace can be performed on all fields that are not flagged to be anonymized.

> Note: Anonymization is a destructive action.  It is recommended to backup the database(s) before anonymizing.

### Supported Fields

The following field types and anonymization options are supported by the Data Anonymizer.

* Date
    * Past
    * Future
    * Recent
* Integer
    * Random
* Single-Line Text | Multi-Line Text | Rich Text
    * Custom Format
    * Name
        * First
        * Last
        * Suffix
    * Phone
        * Phone
        * Fax
        * Mobile
    * Internet
        * Email
        * URL
        * UserName
    * Date
        * Past
        * Future
        * Recent
    * Lorem
        * Replace
        * Words
        * Sentence
        * Sentences
        * Paragraph
        * Paragraphs
    * Address
        * Street
        * City
        * State
        * PostalCode
        * Country
        * Latitude
        * Longitude
        * Coordinates
    * File
        * Random - Randomly chosen media item.
    * Image
        * Random - Randomly chosen image.

> Note: Other field types are not currently supported.  Relationship based fields such as Droplink and Multi-list, should be anonymized by anonymizing the related item.

### Instructions

TODO

### Cautions
1. Only content you chose to be anonymized will be anonymized.  This only includes content in the content tree.  Other content and data will NOT be anonymized.

## Installation

Install the update packages located here: https://github.com/onenorth/data-anonymizer/tree/master/release

## Configuration

There are no configuration files associated with this module.

#License

The associated code is released under the terms of the [MIT license](http://onenorth.mit-license.org).

The Data Anonymizer was inspired by and has used data definitions from:
* https://github.com/marak/Faker.js/ - Copyright (c) 2014-2015 Matthew Bergman & Marak Squires 