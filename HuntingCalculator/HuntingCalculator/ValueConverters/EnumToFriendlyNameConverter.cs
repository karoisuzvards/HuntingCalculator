using System;
using System.Globalization;
using System.Windows.Data;
using HuntingCalculator.Attributes;

namespace HuntingCalculator.ValueConverters
{
    /// <summary>
    /// This class simply takes an enum and uses some reflection to obtain
    /// the friendly name for the enum. Where the friendlier name is
    /// obtained using the LocalizableDescriptionAttribute, which hold the localized
    /// value read from the resource file for the enum
    /// </summary>
    [ValueConversion(typeof(object), typeof(String))]
    public class EnumToFriendlyNameConverter : IValueConverter
    {
        #region IValueConverter implementation

        /// <summary>
        /// Convert value for binding from source object
        /// </summary>
        public object Convert(object value, Type targetType,
                object parameter, CultureInfo culture)
        {
            // To get around the stupid wpf designer bug
            if (value == null) return string.Empty;

            var fi = value.GetType().GetField(value.ToString());

            // To get around the stupid wpf designer bug
            if (fi == null) return string.Empty;

            var attributes =
                (LocalizableDescriptionAttribute[])fi.GetCustomAttributes(typeof(LocalizableDescriptionAttribute), false);

            return ((attributes.Length > 0) &&
                    (!String.IsNullOrEmpty(attributes[0].Description)))
                ?
                attributes[0].Description
                : value.ToString();
        }

        /// <summary>
        /// ConvertBack value from binding back to source object
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("Cant convert back");
        }
        #endregion
    }
}
