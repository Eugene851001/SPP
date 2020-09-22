using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AssemblyBrowser
{
    public class MembersConverter: IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string name = values[0] as string;
            Type type = values[1] as Type;
            Type returnType = values[2] as Type;
            List<DTO.ParameterInfo> parameters = values[3] as List<DTO.ParameterInfo>;
            bool isExtension = false;
            try
            {
                isExtension = (bool)values[4];
            }
            catch
            {
                isExtension = false;
            }
            string result = "";
            if (returnType != null)
                result += returnType.Name + " ";
            if (type != null)
                result += type.Name + " ";
            if (name != null)
                result += name + " ";
            if(parameters != null) 
            {
                result += "(";
                foreach(var parameterInfo in parameters)
                {
                    result += $"{parameterInfo.Type.Name} {parameterInfo.Name}";
                }
                result += ")";
            }
            if(isExtension)
            {
                result += " -Extension method";
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
