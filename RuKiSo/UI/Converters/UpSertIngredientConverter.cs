using System.Globalization;

namespace RuKiSo.UI.Converters
{
    public class UpSertIngredientConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value == null ? "Thêm nguyên liệu" : "Cập nhật nguyên liệu";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
