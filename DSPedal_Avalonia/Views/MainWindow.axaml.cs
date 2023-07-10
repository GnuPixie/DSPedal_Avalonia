using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace DSPedal_Avalonia.Views
{
    public partial class MainWindow : Window
    {
        private Dictionary<string, ButtonViewModel> pedalButtons;

        public MainWindow()
        {
            InitializeComponent();
            pedalButtons = new Dictionary<string, ButtonViewModel>();
            LoadPedals();
        }

        private void LoadPedals()
        {
            var jsonString = @"{
              ""distortion"": {
                ""gain"": {
                  ""value"": 5.0,
                  ""display"": ""slider"",
                  ""min"": 0.0,
                  ""max"": 10.0
                },
                ""volume"": {
                  ""value"": 5.0,
                  ""display"": ""slider"",
                  ""min"": 0.0,
                  ""max"": 10.0
                }
              },
              ""freeverb"": {
                ""roomsize"": {
                  ""value"": 0.5,
                  ""display"": ""slider"",
                  ""min"": 0.0,
                  ""max"": 1.0
                },
                ""damp"": {
                  ""value"": 0.5,
                  ""display"": ""slider"",
                  ""min"": 0.0,
                  ""max"": 1.0
                },
                ""wet"": {
                  ""value"": 0.5,
                  ""display"": ""slider"",
                  ""min"": 0.0,
                  ""max"": 1.0
                }
              },
              ""chorus"": {
                ""delay"": {
                  ""value"": 20.0,
                  ""display"": ""slider"",
                  ""min"": 0.0,
                  ""max"": 50.0
                },
                ""depth"": {
                  ""value"": 3.0,
                  ""display"": ""slider"",
                  ""min"": 0.0,
                  ""max"": 10.0
                },
                ""feedback"": {
                  ""value"": 0.5,
                  ""display"": ""slider"",
                  ""min"": -1.0,
                  ""max"": 1.0
                }
              },
              ""flanger"": {
                ""delayTime"": {
                  ""value"": 1.0,
                  ""display"": ""slider"",
                  ""min"": 0.0,
                  ""max"": 10.0
                },
                ""lfoDepth"": {
                  ""value"": 1.0,
                  ""display"": ""slider"",
                  ""min"": -1.0,
                  ""max"": 1.0
                },
                ""feedbackGain"": {
                  ""value"": 0.7,
                  ""display"": ""slider"",
                  ""min"": -1.0,
                  ""max"": 1.0
                }
              }
            }";
            var effects = JsonConvert.DeserializeObject<JObject>(jsonString);
            foreach (var effect in effects)
            {
                var button = new ButtonViewModel { Name = effect.Key };
                pedalButtons[effect.Key] = button;
                PedalsList.Items.Add(button);
            }
        }

        private void AddButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            int index = PedalboardPanel.Children.IndexOf((Button)sender);
            var newButton = new Button { Content = "Nu" };
            PedalboardPanel.Children.Insert(index, newButton);
        }

        private void PedalButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var button = (ToggleButton)sender;
            var buttonViewModel = (ButtonViewModel)button.DataContext;

            // Check if the button is already selected
            if (button.IsChecked == true)
            {
                // Deselect the button
                buttonViewModel.IsSelected = false;
                // Perform the necessary logic when the button is deselected
            }
            else
            {
                // Deselect all buttons
                foreach (var pedalButton in pedalButtons.Values)
                {
                    pedalButton.IsSelected = false;
                }

                // Select the button
                buttonViewModel.IsSelected = true;
                // Perform the necessary logic when the button is selected
            }
        }
    }

    public class ButtonViewModel : INotifyPropertyChanged
    {
        private bool isSelected;

        public string Name { get; set; }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
