namespace VignereCipher
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);
            window.MinimumHeight = 600;
            window.MinimumWidth = 800;
            window.Title = "Vignere Cipher";
            return window;
        }
    }
}
