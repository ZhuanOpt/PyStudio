using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace PythonEnv.pages
{
    /// <summary>
    /// stat.xaml 的交互逻辑
    /// </summary>
    public partial class stat : Page
    {
        public stat()
        {
            InitializeComponent();
        }
        string path_app = AppDomain.CurrentDomain.BaseDirectory;
        private void Grid_Effect_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid sen = sender as Grid;
            DropShadowEffect myDropShadowEffect = new DropShadowEffect();
            // Set the color of the shadow to Black.

            myDropShadowEffect.Color = Color.FromRgb(240, 240, 240);
            myDropShadowEffect.Opacity = 0.5;
            sen.Effect = myDropShadowEffect;
            sen.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

        }
        private void Grid_Effect_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid sen = sender as Grid;

            DropShadowEffect myDropShadowEffect = new DropShadowEffect();
            // Set the color of the shadow to Black.

            myDropShadowEffect.Color = Color.FromRgb(18, 33, 247);
            myDropShadowEffect.Opacity = 1;
            myDropShadowEffect.ShadowDepth = 0;
            myDropShadowEffect.Direction = 300;
            myDropShadowEffect.BlurRadius = 10;
            sen.Effect = myDropShadowEffect;

        }
    }
}
