using System.Windows;

namespace PythonEnv.pages
{
    /// <summary>
    /// Readme.xaml 的交互逻辑
    /// </summary>
    public partial class Readme : Window
    {
        public Readme()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
