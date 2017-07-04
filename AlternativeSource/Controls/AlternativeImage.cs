using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AlternativeSource.Controls
{
    public class AlternativeImage : Image
    {

        private bool _tryAlternativeSource ;

        public string AlternativeSource
        {
            get { return (string)GetValue(AlternativeSourceProperty); }
            set { SetValue(AlternativeSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlternativeSourceProperty =
            DependencyProperty.Register("AlternativeSource", typeof(string), typeof(AlternativeImage), new PropertyMetadata(null));

        public AlternativeImage()
        {
            Initialized += OnInitialized;
        }

        private void OnInitialized(object sender, EventArgs eventArgs)
        {
            _tryAlternativeSource = !string.IsNullOrEmpty(AlternativeSource);

            //Note , ths need to be unregistered 
            ImageFailed += OnImageFailed;
        }

        private void OnImageFailed(object sender, ExceptionRoutedEventArgs exceptionRoutedEventArgs)
        {
            if (!_tryAlternativeSource)
                return;

            _tryAlternativeSource = false;

            Source = new ImageSourceConverter().ConvertFromString(AlternativeSource) as ImageSource;
        }
    }
}
