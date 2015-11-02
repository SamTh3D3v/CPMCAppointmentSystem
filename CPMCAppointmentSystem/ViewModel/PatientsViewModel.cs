using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using CPMCAppointmentSystem.Helpers;
using CPMCAppointmentSystem.View;
using CPMCAppointmentSystem.View.PatienstViews;
using DataLayer.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Syncfusion.Windows.Reports;

namespace CPMCAppointmentSystem.ViewModel
{
    public class PatientsViewModel : NavigableViewModelBase
    {
        #region Fields
        
        private Note _selectedNote;
        private AddPatientAppointment _addAppointementWindow;
        private ObservableCollection<Patient> _patientList;
        private Patient _selectedPatient;
        private ObservableCollection<Sexe> _sexeList;
        private readonly CpmcContext _dbContext = new CpmcContext();
        private ObservableCollection<Medecin> _doctorsList;
        private Medecin _selectedDoctor;
        private RendezVous _selectedAppointement;
        private ObservableCollection<Willaya> _willayasList;
        private bool _isFormEnabled;
        private ObservableCollection<PieceJointeType> _pieceJointeTypeListe;
        private PieceJointe _selectedPieceJointe;
        private String _filterText;
        private ObservableCollection<Pathology> _pathologiesList;
        private ObservableCollection<String> _filterByCollection = new ObservableCollection<string>()
        {
            "Nom","Prenom","DateDeNaissance","Telephone","willaya","Piece Jointe" //To be Rereviewed
        };
        private String _filterBySelectedItem;
        private PieceJointeType _selectedTypePieceJointeInFilter;
        private String _reportPath;
        private PreviewReportView _previewReportView;
        #endregion
        #region Properties       
        public ObservableCollection<Pathology> PathologiesList
        {
            get
            {
                return _pathologiesList;
            }

            set
            {
                if (_pathologiesList == value)
                {
                    return;
                }

                _pathologiesList = value;
                RaisePropertyChanged();
            }
        }
        public Note SelectedNote
        {
            get
            {
                return _selectedNote;
            }

            set
            {
                if (_selectedNote == value)
                {
                    return;
                }

                _selectedNote = value;
                RaisePropertyChanged();
            }
        }
        public PieceJointeType SelectedTypePieceJointeInFilter
        {
            get
            {
                return _selectedTypePieceJointeInFilter;
            }

            set
            {
                if (_selectedTypePieceJointeInFilter == value)
                {
                    return;
                }

                _selectedTypePieceJointeInFilter = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<String> FilterByCollection
        {
            get
            {
                return _filterByCollection;
            }

            set
            {
                if (_filterByCollection == value)
                {
                    return;
                }

                _filterByCollection = value;
                RaisePropertyChanged();
            }
        }
        public String FilterText
        {
            get
            {
                return _filterText;
            }

            set
            {
                if (_filterText == value)
                {
                    return;
                }

                _filterText = value;
                RaisePropertyChanged();
                SearchPatients();
            }
        }
        private void SearchPatients()
        {

        }
        public String FilterBySelectedItem
        {
            get
            {
                return _filterBySelectedItem;
            }

            set
            {
                if (_filterBySelectedItem == value)
                {
                    return;
                }

                _filterBySelectedItem = value;
                RaisePropertyChanged();
            }
        }
        public PieceJointe SelectedPieceJointe
        {
            get
            {
                return _selectedPieceJointe;
            }

            set
            {
                if (_selectedPieceJointe == value)
                {
                    return;
                }

                _selectedPieceJointe = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<PieceJointeType> TypePieceJointeList
        {
            get
            {
                return _pieceJointeTypeListe;
            }

            set
            {
                if (_pieceJointeTypeListe == value)
                {
                    return;
                }

                _pieceJointeTypeListe = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Patient> PatientList
        {
            get
            {
                return _patientList;
            }

            set
            {
                if (_patientList == value)
                {
                    return;
                }

                _patientList = value;
                RaisePropertyChanged();
            }
        }
        public Patient SelectedPatient
        {
            get
            {
                return _selectedPatient;
            }

            set
            {
                if (_selectedPatient == value)
                {
                    return;
                }
                IsFormEnabled = value != null;
                _selectedPatient = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Sexe> SexeList
        {
            get
            {
                return _sexeList;
            }

            set
            {
                if (_sexeList == value)
                {
                    return;
                }

                _sexeList = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Medecin> DoctorsList
        {
            get
            {
                return _doctorsList;
            }

            set
            {
                if (_doctorsList == value)
                {
                    return;
                }

                _doctorsList = value;
                RaisePropertyChanged();
            }
        }
        public Medecin SelectedDoctor
        {
            get
            {
                return _selectedDoctor;
            }

            set
            {
                if (_selectedDoctor == value)
                {
                    return;
                }

                _selectedDoctor = value;
                RaisePropertyChanged();
            }
        }
        public RendezVous SelectedAppointement
        {
            get
            {
                return _selectedAppointement;
            }

            set
            {
                if (_selectedAppointement == value)
                {
                    return;
                }

                _selectedAppointement = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Willaya> WillayasList
        {
            get
            {
                return _willayasList;
            }

            set
            {
                if (_willayasList == value)
                {
                    return;
                }

                _willayasList = value;
                RaisePropertyChanged();
            }
        }
        public bool IsFormEnabled
        {
            get
            {
                return _isFormEnabled;
            }

            set
            {
                if (_isFormEnabled == value)
                {
                    return;
                }

                _isFormEnabled = value;
                RaisePropertyChanged();
            }
        }
        public String ReportPath
        {
            get
            {
                return _reportPath;
            }

            set
            {
                if (_reportPath == value)
                {
                    return;
                }

                _reportPath = value;
                RaisePropertyChanged();
            }
        }

        #endregion
        #region Commands
        private RelayCommand _saveNewNoteCommand;
        public RelayCommand SaveNewNoteCommand
        {
            get
            {
                return _saveNewNoteCommand
                    ?? (_saveNewNoteCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedNote.NoteId == Guid.Empty)
                        {
                            //Added by Farouk for Audit purpose
                            SelectedNote.NoteId = Guid.NewGuid();

                            if (SelectedPatient.Notes == null)
                                SelectedPatient.Notes = new ObservableCollection<Note>();
                            SelectedPatient.Notes.Add(SelectedNote);
                            _dbContext.SaveChanges();
                            SelectedNote = null;
                        }
                        else
                        {                                                        
                            _dbContext.SaveChanges();
                            SelectedNote = null;
                        }
                    }));
            }
        }
        private RelayCommand _addNewNoteCommand;
        public RelayCommand AddNewNoteCommand
        {
            get
            {
                return _addNewNoteCommand
                    ?? (_addNewNoteCommand = new RelayCommand(
                    () =>
                    {
                        SelectedNote = new Note();
                    }));
            }
        }
        private RelayCommand _previewRecuDeDepoCommand;
        public RelayCommand PreviewRecuDeDepoCommand
        {
            get
            {
                return _previewRecuDeDepoCommand
                    ?? (_previewRecuDeDepoCommand = new RelayCommand(
                    () =>
                    {
                        ReportPath = App.RecuDeDepotReport;
                        _previewReportView = new PreviewReportView();
                        Messenger.Default.Send<Patient>(SelectedPatient);
                        _previewReportView.ShowDialog();

                    }));
            }
        }
        private RelayCommand _printRecuDeDepotCommand;
        public RelayCommand PrinRecuDeDepotCommand
        {
            get
            {
                return _printRecuDeDepotCommand
                    ?? (_printRecuDeDepotCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }
        private RelayCommand _previewRdvCommand;
        public RelayCommand PreviewRdvCommand
        {
            get
            {
                return _previewRdvCommand
                    ?? (_previewRdvCommand = new RelayCommand(
                    () =>
                    {
                        ReportPath = App.RendezVousReport;
                        _previewReportView = new PreviewReportView();
                        Messenger.Default.Send<RendezVous>(SelectedAppointement);
                        _previewReportView.ShowDialog();

                    }));
            }
        }
        private RelayCommand _printRdvCommand;
        public RelayCommand PrintRdvCommand
        {
            get
            {
                return _printRdvCommand
                    ?? (_printRdvCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }
        private RelayCommand _patientsViewLoadedCommand;
        public RelayCommand PatientsViewLoadedCommand
        {
            get
            {
                return _patientsViewLoadedCommand
                    ?? (_patientsViewLoadedCommand = new RelayCommand(async () =>
                    {
                        SexeList = new ObservableCollection<Sexe>(await Task.Run(() => _dbContext.Sexes));
                        WillayasList = new ObservableCollection<Willaya>(await Task.Run(() => _dbContext.Willayas));
                        await LoadPieceJointeTypeList();
                        await LoadPatienstList();
                        await LoadPathologiseList();
                        await LoadDoctorsList();
                    }));
            }
        }

        private RelayCommand _addPatientCommand;
        public RelayCommand AddPatientCommand
        {
            get
            {
                return _addPatientCommand
                    ?? (_addPatientCommand = new RelayCommand(
                    () =>
                    {
                        SelectedPatient = new Patient()
                        {
                            Adresse = new Adresse(),
                            DateDeDepot = DateTime.Now,
                            DateDeNaissance = DateTime.Now
                        };

                    }));
            }
        }
        private RelayCommand _savePatientCommand;
        public RelayCommand SavePatientCommand
        {
            get
            {
                return _savePatientCommand
                    ?? (_savePatientCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedPatient.PatientId == Guid.Empty)
                        {
                            AddNewPatient();
                        }

                        try
                        {
                            _dbContext.SaveChanges();
                        }
                        catch (DbEntityValidationException e)
                        {
                            foreach (var eve in e.EntityValidationErrors)
                            {
                                Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                        ve.PropertyName, ve.ErrorMessage);
                                }
                            }
                            throw;
                        }
                        LoadPatienstList();
                    }));
            }
        }

        private RelayCommand _deletePatientCommand;
        public RelayCommand DeletePatientCommand
        {
            get
            {
                return _deletePatientCommand
                    ?? (_deletePatientCommand = new RelayCommand(
                    () =>
                    {
                        //todo Logical suppression 
                        if (SelectedPatient != null)
                        {
                            if (SelectedPatient.PatientId != Guid.Empty)
                            {
                                _dbContext.Patients.Remove(SelectedPatient);
                                PatientList.Remove(SelectedPatient);
                                _dbContext.SaveChanges();
                                SelectedPatient = null;
                            }
                        }
                    }));
            }
        }
        private RelayCommand _cancelPatientChangesCommand;
        public RelayCommand CancelPatientChangesCommand
        {
            get
            {
                return _cancelPatientChangesCommand
                    ?? (_cancelPatientChangesCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedPatient != null)
                        {
                            if (SelectedPatient.PatientId != Guid.Empty)
                                _dbContext.Entry(SelectedPatient).Reload();
                        }
                        SelectedPatient = null;
                    }));
            }
        }

        private RelayCommand _addAppointementCommand;
        public RelayCommand AddAppointementCommand
        {
            get
            {
                return _addAppointementCommand
                    ?? (_addAppointementCommand = new RelayCommand(
                    () =>
                    {
                        //If a New Patient, First add him
                        if (SelectedPatient.PatientId == Guid.Empty)
                        {
                            AddNewPatient();
                        }                                           
                        SelectedAppointement = new RendezVous()
                        {
                            Patient = SelectedPatient
                        };
                        _addAppointementWindow = new AddPatientAppointment();
                        _addAppointementWindow.ShowDialog();

                    }));
            }
        }
        private RelayCommand _addAppointementLoadedCommand;
        public RelayCommand AddAppointementLoadedCommand
        {
            get
            {
                return _addAppointementLoadedCommand
                    ?? (_addAppointementLoadedCommand = new RelayCommand(
                    () =>
                    {


                    }));
            }
        }
        private RelayCommand _saveAppointementCommand;
        public RelayCommand SaveAppointementCommand
        {
            get
            {
                return _saveAppointementCommand
                    ?? (_saveAppointementCommand = new RelayCommand(async () =>
                    {

                        if (SelectedAppointement.RendezVousId == Guid.Empty)
                        {
                            AddNewAppointement();
                            NotficationManager.AddNotification(new Notification()
                            {
                                NotificationId = Guid.NewGuid(),
                                NotificationTitle = "New",
                                NotificationMessage = "Rendez vous du patient  " + SelectedPatient.Nom + " " + SelectedPatient.Prenom,
                                NotificationType = TypeNotification.Information
                            });
                        }
                        else
                        {
                            NotficationManager.AddNotification(new Notification()
                            {
                                NotificationId = Guid.NewGuid(),
                                NotificationTitle = "Update",
                                NotificationMessage = "Rendez vous du patient  " + SelectedPatient.Nom + " " + SelectedPatient.Prenom,
                                NotificationType = TypeNotification.Information
                            });
                        }
                        _dbContext.SaveChanges();
                        _addAppointementWindow.Close();
                        await LoadPatientAppointementList();
                        SelectedAppointement = null;

                    }));
            }
        }

        private void AddNewAppointement()
        {
            //Added by Farouk for Audit purpose
            SelectedAppointement.RendezVousId = Guid.NewGuid();
            _dbContext.RendezVouses.Add(SelectedAppointement);
        }

        private RelayCommand _deleteAppointementCommand;
        public RelayCommand DeleteAppointementCommand
        {
            get
            {
                return _deleteAppointementCommand
                    ?? (_deleteAppointementCommand = new RelayCommand(async () =>
                    {
                        //todo Logical suppression 
                        if (SelectedAppointement != null)
                        {
                            if (SelectedAppointement.RendezVousId != Guid.Empty)
                            {
                                _dbContext.RendezVouses.Remove(SelectedAppointement);
                                _dbContext.SaveChanges();
                                SelectedAppointement = null;
                                _addAppointementWindow.Close();
                                await LoadPatientAppointementList();
                            }
                        }

                    }));
            }
        }
        private RelayCommand _cancelAppointementChangesCommand;
        public RelayCommand CancelAppointementChangesCommand
        {
            get
            {
                return _cancelAppointementChangesCommand
                    ?? (_cancelAppointementChangesCommand = new RelayCommand(
                    () =>
                    {
                        _addAppointementWindow.Close();
                    }));
            }
        }
        private RelayCommand _appointementDoubleClickCommand;
        public RelayCommand AppointementDoubleClickCommand
        {
            get
            {
                return _appointementDoubleClickCommand
                    ?? (_appointementDoubleClickCommand = new RelayCommand(
                    () =>
                    {                      
                        _addAppointementWindow = new AddPatientAppointment();
                        _addAppointementWindow.ShowDialog();
                    }));
            }
        }
        private RelayCommand _uploadPieceJointeCommand;
        public RelayCommand UploadPieceJointeCommand
        {
            get
            {
                return _uploadPieceJointeCommand
                    ?? (_uploadPieceJointeCommand = new RelayCommand(
                    () =>
                    {
                        var openFileDialog = new OpenFileDialog
                        {
                            ReadOnlyChecked = true,
                            Filter = "Image Files (*.bmp, *.png, *.jpg)|*.bmp;*.png;*.jpg"
                        };
                        var result = openFileDialog.ShowDialog();
                        if (result != DialogResult.OK) return;
                        var imagePath = openFileDialog.FileName;

                        try
                        {
                            var imageFileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                            var imageStreamReader = new BinaryReader(imageFileStream);
                            byte[] pic = imageStreamReader.ReadBytes((int)imageFileStream.Length);
                            imageStreamReader.Close();
                            imageFileStream.Close();
                            SelectedPieceJointe.PieceJointeImage = pic;

                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message);
                        }

                    }));
            }
        }

        private RelayCommand _loadPatientImageCommand;
        public RelayCommand LoadPatientImageCommand
        {
            get
            {
                return _loadPatientImageCommand
                    ?? (_loadPatientImageCommand = new RelayCommand(
                    () =>
                    {
                        var openFileDialog = new OpenFileDialog
                        {
                            ReadOnlyChecked = true,
                            Filter = "Image Files (*.bmp, *.png, *.jpg)|*.bmp;*.png;*.jpg"
                        };
                        var result = openFileDialog.ShowDialog();
                        if (result != DialogResult.OK) return;
                        var imagePath = openFileDialog.FileName;

                        try
                        {
                            var imageFileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                            var imageStreamReader = new BinaryReader(imageFileStream);
                            byte[] pic = imageStreamReader.ReadBytes((int)imageFileStream.Length);
                            imageStreamReader.Close();
                            imageFileStream.Close();
                            SelectedPatient.ProfilePicture = pic;

                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message);
                        }
                    }));
            }
        }


        private RelayCommand _previewPieceJointeCommand;
        public RelayCommand PreviewPieceJointeCommand
        {
            get
            {
                return _previewPieceJointeCommand
                    ?? (_previewPieceJointeCommand = new RelayCommand(
                    () =>
                    {
                        var preview = new PreviewPieceJointeView();
                        preview.ShowDialog();
                    }));
            }
        }

        private RelayCommand _savePieceJointeCommand;

        public RelayCommand SavePieceJointeCommand
        {
            get
            {
                return _savePieceJointeCommand
                    ?? (_savePieceJointeCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedPieceJointe.PieceJointeId == Guid.Empty)
                        {
                            //Added by Farouk for Audit purpose
                            SelectedPieceJointe.PieceJointeId = Guid.NewGuid();
                            
                            //This is a new PieceJointe
                            if (SelectedPatient.PieceJointes == null)
                                SelectedPatient.PieceJointes = new ObservableCollection<PieceJointe>();
                            SelectedPatient.PieceJointes.Add(SelectedPieceJointe);
                            _dbContext.SaveChanges();
                            SelectedPieceJointe = null;
                        }
                        else
                        {
                            //already added piece jointe
                            SelectedPieceJointe = null;
                            _dbContext.SaveChanges();
                        }

                    }));
            }
        }
        private RelayCommand _newPieceJointeCommand;
        public RelayCommand NewPieceJointeCommand
        {
            get
            {
                return _newPieceJointeCommand
                    ?? (_newPieceJointeCommand = new RelayCommand(
                    () =>
                    {
                        SelectedPieceJointe = new PieceJointe();
                    }));
            }
        }
        private RelayCommand _deletePatientImageCommand;
        public RelayCommand DeletePatientImageCommand
        {
            get
            {
                return _deletePatientImageCommand
                    ?? (_deletePatientImageCommand = new RelayCommand(
                    () =>
                    {
                        SelectedPatient.ProfilePicture = null;
                    }));
            }
        }
        private RelayCommand _deletePieceJointeCommand;
        public RelayCommand DeletePieceJointeCommand
        {
            get
            {
                return _deletePieceJointeCommand
                    ?? (_deletePieceJointeCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedPieceJointe!=null)
                        {
                            _dbContext.PieceJointes.Remove(SelectedPieceJointe);
                            SelectedPatient.PieceJointes.Remove(SelectedPieceJointe);
                            _dbContext.SaveChanges();
                        }
                        
                    }));
            }
        }
        private RelayCommand _deleteNoteCommand;
        public RelayCommand DeleteNoteCommand
        {
            get
            {
                return _deleteNoteCommand
                    ?? (_deleteNoteCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedNote != null)
                        {
                            _dbContext.Notes.Remove(SelectedNote);
                            SelectedPatient.Notes.Remove(SelectedNote);
                            _dbContext.SaveChanges();
                        }
                        
                    }));
            }
        }
        private RelayCommand _deleteSelectedPieceJointsCommand;
        public RelayCommand DeleteSelectedPieceJointsCommand
        {
            get
            {
                return _deleteSelectedPieceJointsCommand
                    ?? (_deleteSelectedPieceJointsCommand = new RelayCommand(
                    () =>
                    {
                        
                    }));
            }
        }
        #endregion
        #region Ctors and Methods
        public PatientsViewModel(IFrameNavigationService mainFrameNavigationService, IInnerFrameNavigationService innerFrameNavigationService)
            : base(mainFrameNavigationService, innerFrameNavigationService)
        {
        }
        private async Task LoadPathologiseList()
        {
            PathologiesList = new ObservableCollection<Pathology>(await Task.Run(() => _dbContext.Pathologies));
        }
        private async Task LoadDoctorsList()
        {
            DoctorsList = new ObservableCollection<Medecin>(await Task.Run(() => _dbContext.Medecins));
        }
        private async Task LoadPatienstList()
        {
            PatientList = new ObservableCollection<Patient>(await Task.Run(() => _dbContext.Patients));
        }
        private async Task LoadPieceJointeTypeList()
        {
            TypePieceJointeList = new ObservableCollection<PieceJointeType>(await Task.Run(() => _dbContext.PieceJointeTypes));
        }
        private void AddNewPatient()
        {
            //Added by Farouk for Audit purpose
            SelectedPatient.PatientId = Guid.NewGuid();

            _dbContext.Patients.Add(SelectedPatient);            
            IsFormEnabled = false;
        }
        private async Task LoadPatientAppointementList()
        {
            SelectedPatient.RendezVouses = new ObservableCollection<RendezVous>(await Task.Run(() =>
                _dbContext.RendezVouses.Where(x => x.PatientId == SelectedPatient.PatientId)
            ));

        }
        #endregion
    }
}
