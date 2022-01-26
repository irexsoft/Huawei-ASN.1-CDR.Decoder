/*****************************************************************
 * Module: EnumDescConverter.cs
 * Type: C# Source Code
 * Version: 1.0
 * Description: Enum Converter using Description Attributes
 * 
 * Original by Javier Campos (http://www.codeproject.com/KB/cs/enumdescconverter.aspx)
 *****************************************************************/

using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace CDR.Decoder
{
    /// <summary>
    /// EnumConverter supporting System.ComponentModel.DescriptionAttribute
    /// </summary>
    public class EnumDescConverter : EnumConverter
    {
        protected Type _val;

        /// <summary>
        /// Gets Enum Value's Description Attribute
        /// </summary>
        /// <param name="value">The value you want the description attribute for</param>
        /// <returns>The description, if any, else it's .ToString()</returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
              (DescriptionAttribute[])fi.GetCustomAttributes(
              typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        /// <summary>
        /// Gets the description for certaing named value in an Enumeration
        /// </summary>
        /// <param name="value">The type of the Enumeration</param>
        /// <param name="name">The name of the Enumeration value</param>
        /// <returns>The description, if any, else the passed name</returns>
        public static string GetEnumDescription(Type value, string name)
        {
            FieldInfo fi = value.GetField(name);
            DescriptionAttribute[] attributes =
              (DescriptionAttribute[])fi.GetCustomAttributes(
              typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : name;
        }

        /// <summary>
        /// Gets the value of an Enum, based on it's Description Attribute or named value
        /// </summary>
        /// <param name="value">The Enum type</param>
        /// <param name="description">The description or name of the element</param>
        /// <returns>The value, or the passed in description, if it was not found</returns>
        public static object GetEnumValue(Type value, string description)
        {
            FieldInfo[] fis = value.GetFields();
            foreach (FieldInfo fi in fis)
            {
                DescriptionAttribute[] attributes =
                  (DescriptionAttribute[])fi.GetCustomAttributes(
                  typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    if (attributes[0].Description == description)
                    {
                        return fi.GetValue(fi.Name);
                    }
                }
                if (fi.Name == description)
                {
                    return fi.GetValue(fi.Name);
                }
            }
            return description;
        }

        public EnumDescConverter(System.Type type)
            : base(type.GetType())
        {
            _val = type;
        }

        /*
        public static string GetEnumKeyDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        public static DataTable GetEnumAsDataTable(System.Type EnumType)
        {
            DataTable DTEnum = new DataTable();
            DTEnum.Columns.Add(new DataColumn("EnumID", typeof(Int32)));
            DTEnum.Columns.Add(new DataColumn("Enum", typeof(string)));
            DTEnum.Columns.Add(new DataColumn("Description", typeof(string)));
            foreach (int i in Enum.GetValues(EnumType))
            {
                System.Enum fooItem = (System.Enum)Enum.ToObject(EnumType, i);
                DTEnum.Rows.Add(new object[] { i, fooItem.ToString(), GetEnumKeyDescription(fooItem) });
            }
            return DTEnum;
        }
        */

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                String s = value as String;
                if (s != null)
                {
                    return GetEnumDescription(_val, s);
                }
                else
                {
                    Enum e = value as Enum;
                    if (e != null)
                    {
                        return GetEnumDescription(e);
                    }
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            String s = value as String;
            if (s != null)
            {
                return GetEnumValue(_val, s);
            }
            else
            {
                Enum e = value as Enum;
                if (e != null)
                {
                    return GetEnumDescription(e);
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        //public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        //{
        //    return true;
        //}

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            ArrayList values = new ArrayList();
            FieldInfo[] fis = _val.GetFields();
            foreach (FieldInfo fi in fis)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                   typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                    values.Add(fi.GetValue(fi.Name));
            }
            return new TypeConverter.StandardValuesCollection(values);
        }
    }
}