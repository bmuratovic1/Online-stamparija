using OnliStam.Dashboard.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OnliStam.Dashboard.Converters
{
    class EnumToStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(parameter == null)
                return null;
            var type = (parameter as Type);
            var input = ((System.Collections.IEnumerable)value); // MySqlTipovi[];
            var response = new List<string>();

            foreach(var inp in input)
            {
                response.Add(dajOpis(inp.ToString(), type));
            }

            return response;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return System.Enum.GetValues(typeof(MySqlTipovi[])); // dajOpis(value.ToString());
        }

        private string dajOpis(string vrijednost, Type type)
        {
            var memInfo = type.GetMember(vrijednost);
            if(memInfo != null && memInfo.Length > 0)
            {
                var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if(attributes != null && attributes.Length > 0)
                    return ((DescriptionAttribute)attributes[0]).Description;
            }
            return vrijednost;
        }
    }
}
