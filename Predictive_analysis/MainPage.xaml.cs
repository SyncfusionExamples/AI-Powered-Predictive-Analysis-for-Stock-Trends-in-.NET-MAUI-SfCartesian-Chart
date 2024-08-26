namespace Predictive_analysis
{
    public partial class MainPage : ContentPage
    {
       
        public MainPage()
        {
            InitializeComponent();

#if ANDROID
            BaseView.Content = new ForecastAndroidView();

#else
            BaseView.Content = new ForecastWindowsView();
#endif

        }

    }

}
