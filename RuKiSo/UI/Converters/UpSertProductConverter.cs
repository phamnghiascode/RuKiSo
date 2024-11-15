using System.Globalization;

namespace RuKiSo.UI.Converters
{
    class UpSertProductConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value == null ? "Thêm sản phẩm" : "Cập nhật sản phẩm";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
