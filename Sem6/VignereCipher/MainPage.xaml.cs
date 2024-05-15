
namespace VignereCipher
{
    public partial class MainPage : ContentPage
    {
        private string defaultTextSelectedFileText = "Selected text file:" + Environment.NewLine;
        private string defaultTextSelectedFileKey = "Selected text file:" + Environment.NewLine;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public string KeyPath { get; set; } = string.Empty;
        public string TextPath { get; set; } = string.Empty;
        public string OutputPath { get; set; } = "C:\\VignereCipher\\output.txt";

        private void OnCipherClicked(object sender, EventArgs e)
        {
            ErrorText.Text = string.Empty;
            if (TextPath == string.Empty)
            {
                ErrorText.Text = "Text path is empty!";
                return;
            }
            else if (KeyPath == string.Empty)
            {
                ErrorText.Text = "Key path is empty!";
                return;
            }
            else if (OutputPath == string.Empty)
            {
                ErrorText.Text = "Output path is empty!";
                return;
            }
            else if (File.Exists(OutputPath))
            {
                ErrorText.Text = "This file already exists!";
                return;
            }

            var input = File.ReadAllText(TextPath);
            var key = File.ReadAllText(KeyPath);
            var output = string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsLetter(input[i]))
                {
                    output += input[i];
                    continue;
                }
                var x = (input[i] + key[i % key.Length]) % 26;

                x += 'A';

                output += (char)(x);
            }
            File.WriteAllText(OutputPath, output);
        }

        private void OnDecipherClicked(object sender, EventArgs e)
        {
            ErrorText.Text = string.Empty;
            if (TextPath == string.Empty)
            {
                ErrorText.Text = "Text path is empty!";
                return;
            }
            else if (KeyPath == string.Empty)
            {
                ErrorText.Text = "Key path is empty!";
                return;
            }
            else if (OutputPath == string.Empty)
            {
                ErrorText.Text = "Output path is empty!";
                return;
            }
            else if (File.Exists(OutputPath))
            {
                ErrorText.Text = "This file already exists!";
                return;
            }

            var input = File.ReadAllText(TextPath);
            var key = File.ReadAllText(KeyPath);
            var output = string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsLetter(input[i]))
                {
                    output += input[i];
                    continue;
                }
                int x = (input[i] - key[i % key.Length] + 26) % 26;

                x += 'A';
                output += (char)(x);
            }
            File.WriteAllText(OutputPath, output);
        }

        private void OnDecipherWithoutKeyClicked(object sender, EventArgs e)
        {
            ErrorText.Text = string.Empty;
            if (TextPath == string.Empty)
            {
                ErrorText.Text = "Text path is empty!";
                return;
            }
            else if (OutputPath == string.Empty)
            {
                ErrorText.Text = "Output path is empty!";
                return;
            }
            else if (File.Exists(OutputPath))
            {
                ErrorText.Text = "This file already exists!";
                return;
            }

            var input = File.ReadAllText(TextPath);

            var solver = new VignereSolver();
            var output = solver.SolveVignere(input, "polish");
            File.WriteAllText(OutputPath, output);
        }

        private void OnGetKeyLengthClicked(object sender, EventArgs e)
        {
            ErrorText.Text = string.Empty;
            if (TextPath == string.Empty)
            {
                ErrorText.Text = "Text path is empty!";
                return;
            }
            else if (OutputPath == string.Empty)
            {
                ErrorText.Text = "Output path is empty!";
                return;
            }
            else if (File.Exists(OutputPath))
            {
                ErrorText.Text = "This file already exists!";
                return;
            }

            var input = File.ReadAllText(TextPath);

            var solver = new VignereSolver();
            var output = solver.SolveLength(input);
            FoundKeyLengthLabel.Text = "Found key length:" + Environment.NewLine + output;
        }

        private async void OnSelectTextFileClicked(object sender, EventArgs e)
        {
            var pickOptions = GetPickOptions();
            try
            {
                var result = await FilePicker.Default.PickAsync(pickOptions);

                if (result != null)
                {
                    TextPath = result.FullPath;
                    TextFilePathText.Text = defaultTextSelectedFileText + result.FullPath.Split("\\").Last() ?? "No path found!";
                }
            }
            catch (Exception ex)
            {
                TextFilePathText.Text = ex.Message;
            }
        }

        private async void OnSelectKeyFileClicked(object sender, EventArgs e)
        {
            var pickOptions = GetPickOptions();
            try
            {
                var result = await FilePicker.Default.PickAsync(pickOptions);

                if (result != null)
                {
                    KeyPath = result.FullPath;
                    KeyFilePathText.Text = defaultTextSelectedFileKey + result.FullPath.Split("\\").Last() ?? "No path found!";
                }
            }
            catch (Exception ex)
            {
                TextFilePathText.Text = ex.Message;
            }
        }

        private PickOptions GetPickOptions()
        {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.WinUI, new[] { ".txt" } }
                });
            var pickOptions = new PickOptions()
            {
                PickerTitle = "Select a text file",
                FileTypes = customFileType
            };

            return pickOptions;
        }
    }

}
