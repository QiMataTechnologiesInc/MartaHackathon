using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Estimotes;
using MartaHackathon.Annotations;
using MartaHackathon.Services;
using MartaHackathon.Views;
using Plugin.TextToSpeech;
using Xamarin.Forms;

namespace MartaHackathon.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private IDisposable _whenRanged;
        private BeaconDataService _beaconDataService;
        private BeaconDataRNG _beaconDataRng;
        private const string userid = "1";
        private DateTime _lastTalked = DateTime.MinValue;
        public INavigation Navigation { get; set; }

        private bool _help;
        public MainViewModel()
        {
            _beaconDataService = new BeaconDataService();
            _beaconDataRng = new BeaconDataRNG();
            CrossTextToSpeech.Current.Init();

            MoreInfoTappedCommand = new Command(async () =>
            {
                await Navigation.PushAsync(new MoreInfoPage());
            });

            HelpTappedCommand = new Command(async () =>
            {
                _help = !_help;
                await _beaconDataService.Help(_help, userid);
            });

            
            EstimoteManager.Instance.Initialize()
                .ContinueWith(x =>
                {
                    if (x.Result != BeaconInitStatus.Success)
                    {
                        throw new Exception("ahhhhh");
                    }

                    EstimoteManager.Instance.Ranged += Instance_Ranged;
                    EstimoteManager.Instance.StartRanging(new BeaconRegion("", "c9407f30-f5f8-466e-aff9-25556b57fe6e"));
                    EstimoteManager.Instance.StartRanging(new BeaconRegion("", "c9407f30-f5f8-466e-aff9-25556b57fe6d"));
                    EstimoteManager.Instance.StartRanging(new BeaconRegion("", "c9407f30-f5f8-466e-aff9-25556b57fe6f"));
                    EstimoteManager.Instance.StartRanging(new BeaconRegion("", "c9407f30-f5f8-466e-aff9-25556b57fe6a"));
                    EstimoteManager.Instance.StartRanging(new BeaconRegion("", "e20a39f4-73f5-4bc4-a12f-17d1ad07a961"));
                    EstimoteManager.Instance.StartRanging(new BeaconRegion("", "2f234454-cf6d-4a0f-adf2-f4911ba9ffab"));
                });
        }

        private async void Instance_Ranged(object sender, IEnumerable<IBeacon> e)
        {
            try
            {
                if (e.Any(x => x.Proximity != Proximity.Unknown))
                {
                    Debug.WriteLine(e.First(x => x.Proximity != Proximity.Unknown).Uuid);
                }

                var speech = await _beaconDataService
                    .SendBeaconData(e
                    .Where(x => x.Proximity != Proximity.Unknown)
                    .Select(x => new BeaconDataDTO
                {
                    Proximity = x.Proximity.ToString(),
                    Major = x.Major,
                    Minor = x.Minor,
                    Uuid = x.Uuid
                }), userid);

                if (!String.IsNullOrWhiteSpace(speech) && DateTime.Now - _lastTalked > TimeSpan.FromSeconds(10))
                {
                    DisplayText = speech;
                    _lastTalked = DateTime.Now;
                    CrossTextToSpeech.Current.Speak(speech);
                }
            }
            catch (Exception exception)
            {
                int i = 0;
            }
        }

        private string _displayText;

        public string DisplayText
        {
            get { return _displayText; }
            set
            {
                if (_displayText == value)
                {
                    return;
                }
                _displayText = value;
                OnPropertyChanged();
            }
        }

        public Command MoreInfoTappedCommand { get; }

        public Command HelpTappedCommand { get; }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
