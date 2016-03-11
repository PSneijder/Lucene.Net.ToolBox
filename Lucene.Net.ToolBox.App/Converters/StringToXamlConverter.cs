using System;
using System.Globalization;
using System.Windows.Data;

namespace Lucene.Net.Toolbox.Converters
{
    internal enum ConversionValues
    {
        FromXaml = 0,
        ToXaml = 1
    }

    internal sealed class StringToXamlConverter
        : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var value = values[0] as string;
            var startOffset = values[1] as int?;
            var endOffset = values[2] as int?;

            if (value != null)
            {
                return Convert(value.ToString(), startOffset, endOffset, ConversionValues.ToXaml);
            }

            return value;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string Convert(string value, int? startOffset, int? endOffset, ConversionValues conversion)
        {
            switch (conversion)
            {
                case ConversionValues.FromXaml:
                    // TODO
                    return value;

                case ConversionValues.ToXaml:
                    // TODO
                    if (startOffset.HasValue && endOffset.HasValue)
                    {
                        //var startInsert = "<Run FontWeight='Bold'>";
                        //var endInsert = "</Run>";

                        //value = value.Insert(startOffset.Value, startInsert);
                        //value = value.Insert(endOffset.Value + startInsert.Length, endInsert);
                        
                        return @"<FlowDocument xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>" +
                                "<Paragraph>" + value + "</Paragraph>" +
                                "</FlowDocument>";
                    }
                    
                    return @"<FlowDocument xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>" +
                            "<Paragraph>" + value + "</Paragraph>" +
                            "</FlowDocument>";

                    default:
                    break;
            }

            return string.Empty;
        }
    }
}