using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace CPMCAppointmentSystem.ViewModel
{
    public class NotificationViewModel:NavigableViewModelBase
    {
        #region Fields
      
        private GsmHelper _gsmHelper  ;       
        private CpmcContext _dbContext=new CpmcContext();
        private ObservableCollection<RendezVous> _rdvCollectionList;
        private RendezVous _selectedRdv;
        #endregion 
        #region Properties
        public GsmHelper GsmHelper
        {
            get
            {
                return _gsmHelper;
            }

            set
            {
                if (_gsmHelper == value)
                {
                    return;
                }

                _gsmHelper = value;
                RaisePropertyChanged();
            }
        }      
        public RendezVous SelectedRdv
        {
            get
            {
                return _selectedRdv;
            }

            set
            {
                if (_selectedRdv == value)
                {
                    return;
                }

                _selectedRdv = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<RendezVous> RdvCollectionList
        {
            get
            {
                return _rdvCollectionList;
            }

            set
            {
                if (_rdvCollectionList == value)
                {
                    return;
                }

                _rdvCollectionList = value;
                RaisePropertyChanged();
            }
        }
        
        #endregion 
        #region Commands
        private RelayCommand _notificationViewLoadedCommand;
        public RelayCommand NotificationViewLoadedCommand
        {
            get
            {
                return _notificationViewLoadedCommand
                    ?? (_notificationViewLoadedCommand = new RelayCommand(async () =>
                    {                        
                        _dbContext = new CpmcContext();
                        await LoadRdvs();
                        try
                        {
                            await GsmHelper.InitGsmDevice();
                        }
                        catch (Exception ex)
                        {

                            Application.Current.Dispatcher.BeginInvoke(new Action(async () =>
                            {
                                var ctontroller = await ((Application.Current.MainWindow as MetroWindow).ShowMessageAsync(
                                    "Echec de com", "check the gprs device ... "));
                            }));
                        }
                        
                    }));
            }
        }
        private RelayCommand _callPhoneCommand;   
        public RelayCommand CallPhoneCommand
        {
            get
            {
                return _callPhoneCommand
                    ?? (_callPhoneCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedRdv!=null)
                        {
                            GsmHelper.Callphone("+"+SelectedRdv.Patient.TelephoneMobile1);
                        }
                        
                    }));
            }
        }
        private RelayCommand _sendsmsCommand;
        public RelayCommand SendSmsCommand
        {
            get
            {
                return _sendsmsCommand
                    ?? (_sendsmsCommand   = new RelayCommand(
                    () =>
                    {
                        GsmHelper.SendSms("+"+SelectedRdv.Patient.TelephoneMobile1,"Confirmation du rendez vous");

                    }));
            }
        }

        private RelayCommand _callFixCommand;     
        public RelayCommand CallFixCommand
        {
            get
            {
                return _callFixCommand
                    ?? (_callFixCommand = new RelayCommand(
                    () =>
                    {
                        GsmHelper.Callphone("+" + SelectedRdv.Patient.TelephoneFixe);
                    }));
            }
        }

        private async Task LoadRdvs()
        {
            RdvCollectionList=new ObservableCollection<RendezVous>(await Task.Run(()=>_dbContext.RendezVouses));
        }

        #endregion 
        #region Ctors and methods
        public NotificationViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {            
            GsmHelper=new GsmHelper(9600,"+21361000750");                        
            Messenger.Default.Register<NotificationMessage>(this, (m) =>
            {
                try
                {
                    switch (m.Notification)
                    {
                        case "SendSmsToPatient":
                            GsmHelper.SendSms("+" + SelectedRdv.Patient.TelephoneMobile1, "Confirmation du rendez vous");
                            SelectedRdv.NotificationSent = true;
                            break;
                        case "SendSmsToAccom":
                            GsmHelper.SendSms("+" + SelectedRdv.Patient.TelephoneDaccompagnant, "Confirmation du rendez vous");
                            SelectedRdv.NotificationSent = true;
                            break;
                        case "CallPatient":
                            GsmHelper.Callphone(SelectedRdv.Patient.TelephoneMobile1);
                            break;
                        case "CallAccompagnant":
                            GsmHelper.Callphone(SelectedRdv.Patient.TelephoneDaccompagnant);
                            break;
                        case "CallFix":
                            GsmHelper.Callphone(SelectedRdv.Patient.TelephoneFixe);
                            break;

                    }
                }
                catch (Exception ex)
                {

                    Application.Current.Dispatcher.BeginInvoke(new Action(async () =>
                    {
                        var ctontroller = await ((Application.Current.MainWindow as MetroWindow).ShowMessageAsync(
                            "Echec de com", "check the gprs device ... "));
                    }));
                }
            });
        }
        #endregion       
    }
}
