using System;
using System.Globalization;
using System.Linq;
using OneNorth.ContentAnonymizer.Areas.ContentAnonymizer.Models;
using OneNorth.ContentAnonymizer.Data.Locales;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace OneNorth.ContentAnonymizer.Data
{
    public class FieldAnonymizer : IFieldAnonymizer
    {
        private static readonly IFieldAnonymizer _instance = new FieldAnonymizer();
        public static IFieldAnonymizer Instance { get { return _instance; } }

        private readonly IAddress _address;
        private readonly IDate _date;
        private readonly IDate _dateTime;
        private readonly IInternet _internet;
        private readonly ILorem _lorem;
        private readonly IMedia _media;
        private readonly IName _name;
        private readonly INumber _number;
        private readonly IPhone _phone;

        private FieldAnonymizer() : this(
            Address.Instance,
            Date.Instance,
            Datetime.Instance,
            Internet.Instance,
            Lorem.Instance,
            Media.Instance,
            Name.Instance,
            Number.Instance,
            Phone.Instance)
        {
            
        }

        internal FieldAnonymizer(
            IAddress address,
            IDate date,
            IDate dateTime,
            IInternet internet,
            ILorem lorem,
            IMedia media,
            IName name,
            INumber number,
            IPhone phone)
        {
            _address = address;
            _date = date;
            _dateTime = dateTime;
            _internet = internet;
            _lorem = lorem;
            _media = media;
            _name = name;
            _number = number;
            _phone = phone;
        }

        public void AnonymizeField(FieldInfo fieldInfo, Item item, AnonymizeOptions options, ILocale locale)
        {
            var field = item.Fields[new ID(fieldInfo.Id)];
            if (field == null)
                return;

            // Only anonymize fields that have an inner value.
            var value = field.GetValue(false, false, false, false);
            if (string.IsNullOrEmpty(value) && !fieldInfo.OverwriteEmptyValues)
                return;

            switch (fieldInfo.Anonymize)
            {
                case AnonymizeType.City:
                    field.Value = _address.City(locale);
                    break;
                case AnonymizeType.Clear:
                    field.Value = "";
                    break;
                case AnonymizeType.Country:
                    field.Value = _address.Country(locale);
                    break;
                case AnonymizeType.Coordinates:
                    field.Value = _address.Coordinates();
                    break;
                case AnonymizeType.Custom:
                    // Perform Custom format last as it is dependent on the non-custom fields
                    break;
                case AnonymizeType.Email:
                    field.Value = _internet.Email(locale);
                    break;
                case AnonymizeType.Fax:
                    field.Value = _phone.FaxNumber(locale);
                    break;
                case AnonymizeType.File:
                    _media.File(field, item.Language, fieldInfo.Source.Path, fieldInfo.OverwriteEmptyValues);
                    break;
                case AnonymizeType.FirstName:
                    field.Value = _name.FirstName(locale, field.Value);
                    break;
                case AnonymizeType.Future:
                    field.Value = DateUtil.ToIsoDate(_date.Future());
                    break;
                case AnonymizeType.FutureDateTime:
                    field.Value = DateUtil.ToIsoDate(_dateTime.Future());
                    break;
                case AnonymizeType.Image:
                    _media.Image(field, item.Language, fieldInfo.Source.Path, fieldInfo.OverwriteEmptyValues);
                    break;
                case AnonymizeType.Integer:
                    field.Value = _number.Integer().ToString(CultureInfo.InvariantCulture);
                    break;
                case AnonymizeType.LastName:
                    field.Value = _name.LastName(locale, field.Value);
                    break;
                case AnonymizeType.Latitude:
                    field.Value = _address.Latitude();
                    break;
                case AnonymizeType.Longitude:
                    field.Value = _address.Longitude();
                    break;
                case AnonymizeType.Mobile:
                    field.Value = _phone.MobileNumber(locale);
                    break;
                case AnonymizeType.Paragraph:
                    field.Value = _lorem.Paragraph(locale);
                    break;
                case AnonymizeType.Paragraphs:
                    field.Value = _lorem.Paragraphs(locale);
                    if (field.Type == "Rich Text")
                        field.Value = field.Value.Replace(Environment.NewLine, "<br /><br />");
                    break;
                case AnonymizeType.Past:
                    field.Value = DateUtil.ToIsoDate(_date.Past());
                    break;
                case AnonymizeType.PastDateTime:
                    field.Value = DateUtil.ToIsoDate(_dateTime.Past());
                    break;
                case AnonymizeType.Phone:
                    field.Value = _phone.PhoneNumber(locale);
                    break;
                case AnonymizeType.PostalCode:
                    field.Value = _address.PostalCode(locale);
                    break;
                case AnonymizeType.Prefix:
                    field.Value = _name.Prefix(locale);
                    break;
                case AnonymizeType.Recent:
                    field.Value = DateUtil.ToIsoDate(_date.Recent());
                    break;
                case AnonymizeType.RecentDateTime:
                    field.Value = DateUtil.ToIsoDate(_dateTime.Recent());
                    break;
                case AnonymizeType.Replace:
                    field.Value = _lorem.Replace(locale, field.Value);
                    break;
                case AnonymizeType.Reset:
                    field.Reset();
                    break;
                case AnonymizeType.Sentence:
                    field.Value = _lorem.Sentence(locale);
                    break;
                case AnonymizeType.Sentences:
                    field.Value = _lorem.Sentences(locale);
                    break;
                case AnonymizeType.State:
                    field.Value = _address.State(locale);
                    break;
                case AnonymizeType.Street:
                    field.Value = _address.StreetAddress(locale);
                    break;
                case AnonymizeType.Suffix:
                    field.Value = _name.Suffix(locale);
                    break;
                case AnonymizeType.Url:
                    field.Value = _internet.Url(locale);
                    break;
                case AnonymizeType.UserName:
                    field.Value = _internet.UserName(locale);
                    break;
                case AnonymizeType.Words:
                    field.Value = _lorem.Words(locale);
                    break;
                default:
                    field.Value = options.Replacements.Aggregate(field.Value, (current, replacement) => current.Replace(replacement.Replace, replacement.With));
                    break;
            }
        }

        public void AnonymizeCustomField(FieldInfo fieldInfo, Item item)
        {
            var field = item.Fields[new ID(fieldInfo.Id)];
            if (field == null)
                return;

            // Only anonymize fields that have an inner value.
            var fieldValue = field.GetValue(false, false, false, false);
            if (string.IsNullOrEmpty(fieldValue))
                return;

            if (fieldInfo.Anonymize != AnonymizeType.Custom)
                return;

            var format = fieldInfo.Format.Format;

            for (var i = 0; i < fieldInfo.Format.Tokens.Count; i++)
            {
                var value = string.Empty;

                var tokenFieldInfo = fieldInfo.Format.Tokens[i].Field;
                if (tokenFieldInfo != null)
                {
                    var tokenField = item.Fields[new ID(tokenFieldInfo.Id)];
                    if (tokenField != null)
                    {
                        value = tokenField.Value;
                        if (ID.IsID(format))
                        {
                            var tokenItem = item.Database.GetItem(new ID(format));
                            value = tokenItem.DisplayName;
                        }
                    }
                }

                var token = string.Format("${0}", i);
                format = format.Replace(token, value);
            }

            field.Value = format;
        }
    }
}