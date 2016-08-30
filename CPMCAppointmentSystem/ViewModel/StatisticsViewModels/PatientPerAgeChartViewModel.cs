using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using CPMCAppointmentSystem.SubModel;
using DataLayer.Model;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Syncfusion.Data.Extensions;

namespace CPMCAppointmentSystem.ViewModel.StatisticsViewModels
{
    public class PatientPerAgeChartViewModel : StatisticsChartsViewModelBase
    {
        #region Fields
        private CpmcContext _dbContext;
        private ObservableCollection<BarModel> _patientPerAgeCollection;
        #endregion
        #region Properties
        public ObservableCollection<BarModel> PatientPerAgeCollection
        {
            get
            {
                return _patientPerAgeCollection;
            }

            set
            {
                if (_patientPerAgeCollection == value)
                {
                    return;
                }

                _patientPerAgeCollection = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands      
        private RelayCommand _patientPerAgeLoadedCommand;
        public RelayCommand PatientPerAgeLoadedCommand
        {
            get
            {
                return _patientPerAgeLoadedCommand
                    ?? (_patientPerAgeLoadedCommand = new RelayCommand(async () =>
                    {
                        _dbContext = new CpmcContext();
                        await Task.Run(() =>
                        {
                            PatientPerAgeCollection = new ObservableCollection<BarModel>();
                            var age = 0;
                            var ageRange = 5;
                            while (age <= 120)
                            {
                                var offset = age == 0 ? 0 : 1;
                                PatientPerAgeCollection.Add(new BarModel()
                                {
                                    Item = "[" + (age + offset) + " - " + (age + ageRange) + "]"
                                });
                                ageRange = age < 25 ? 5 : 10;
                                age += ageRange;
                            }
                            _dbContext.Patients.ForEach(p =>
                            {
                                var aG = DateTime.Now.Year - p.DateDeNaissance.Year;
                                var index = aG < 25 ? aG/5 : aG/10;
                                PatientPerAgeCollection[index].ItemsCount++;
                            });
                        });


                    }));
            }
        }
        private RelayCommand _patientPerAgeUnLoadedCommand;
        public RelayCommand PatientPerAgeUnLoadedCommand
        {
            get
            {
                return _patientPerAgeUnLoadedCommand
                    ?? (_patientPerAgeUnLoadedCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }

        #endregion
        #region Ctors Methods
        public PatientPerAgeChartViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        #endregion
    }
}
