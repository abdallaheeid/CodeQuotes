namespace CodeQuotes
{
    public partial class MainPage : ContentPage
    {
        List<string> quotes = new List<string>();

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadMauiAsset();
        }

        async Task LoadMauiAsset()
        {
            await using var stream = await FileSystem.OpenAppPackageFileAsync("quotes.txt");
            using var reader = new StreamReader(stream);

            while (reader.Peek() != -1)
            {
                quotes.Add(reader.ReadLine());
            }
        }

        Random random = new Random();

        // TODO: Implement the BtnGenerateQuote_OnClicked method
        private void BtnGenerateQuote_OnClicked(object? sender, EventArgs e)
        {
            var startColor = System.Drawing.Color.FromArgb(
                random.Next(0, 256),
                random.Next(0, 256),
                random.Next(0, 256));

            var endColor = System.Drawing.Color.FromArgb(
                random.Next(0, 256),
                random.Next(0, 256),
                random.Next(0, 256));
            
            var color = ColorUtility.ColorControls.GetColorGradient(startColor, endColor, 6);

            float stopOffset = .0f;
            var stops = new GradientStopCollection();

            foreach (var c in color)
            {
                stops.Add(new GradientStop(Color.FromArgb(c.Name), stopOffset));
                stopOffset += .2f;
            }

            var gradient = new LinearGradientBrush(stops, new Point(0, 0), new Point(1, 1));

            background.Background = gradient;

            var quote = quotes[random.Next(0, quotes.Count - 1)];
            lblQuote.Text = quote;
        }
    }

}
