using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace View
{
    [ValueConversion(typeof(FieldPositionStatus), typeof(Brush))]
    public class FieldBrushConverter : IMultiValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {


            //var a = (FieldPosition)value;

            //var a = (FieldPosition)value;

            //var cast = a.FieldPositionStatus;
            var cast = (FieldPositionStatus)value;
            Brush tmp = Brushes.DarkSlateBlue;
            switch (cast)
            {
                case FieldPositionStatus.Default:
                    return Brushes.White;
                case FieldPositionStatus.Ship:
                    return Brushes.Black;
                case FieldPositionStatus.ShotHit:
                    return Brushes.DarkOrange;
                case FieldPositionStatus.ShotKill:
                    return Brushes.DarkRed;
                case FieldPositionStatus.ShotMiss:
                    return Brushes.DarkSeaGreen;

            }
            
            return Brushes.DarkSlateBlue;
   
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var cast = (FieldPositionStatus)values[0];
            var b = (bool)values[1];


            
            Brush tmp = Brushes.DarkSlateBlue;
            switch (cast)
            {
                case FieldPositionStatus.Default:
                    tmp = Brushes.LightSkyBlue;
                    break;
                case FieldPositionStatus.Ship:
                    tmp = Brushes.DarkGray;
                    break;
                case FieldPositionStatus.ShotHit:
                    tmp = Brushes.DarkOrange;
                    break;
                case FieldPositionStatus.ShotKill:
                    tmp = Brushes.DarkRed;
                    break;
                case FieldPositionStatus.ShotMiss:
                    tmp = Brushes.DarkSeaGreen;
                    break;

            }
            if (b)
            {
                tmp = Brushes.LightGreen;
            }

            return tmp;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
