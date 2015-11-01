using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Annotations;

namespace DataLayer.Model
{
    [Table("PieceJointe")]
    public class PieceJointe:Auditable,INotifyPropertyChanged
    {       
        #region Fields
        private Guid _pieceJointeId;
        private string _description;
        private string _idPieceJointe;
        private Guid _pieceJointeTypeId;
        private PieceJointeType _typePieceJointe;
        private byte[] _pieceJointeImage;
        #endregion
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PieceJointeId
        {
            get { return _pieceJointeId; }
            set
            {
                if (value.Equals(_pieceJointeId)) return;
                _pieceJointeId = value;
                OnPropertyChanged();
            }
        }
        public String Description
        {
            get { return _description; }
            set
            {
                if (value == _description) return;
                _description = value;
                OnPropertyChanged();
            }
        }
        public String IdPieceJointe
        {
            get { return _idPieceJointe; }
            set
            {
                if (value == _idPieceJointe) return;
                _idPieceJointe = value;
                OnPropertyChanged();
            }
        }
        public Guid PieceJointeTypeId
        {
            get { return _pieceJointeTypeId; }
            set
            {
                if (value.Equals(_pieceJointeTypeId)) return;
                _pieceJointeTypeId = value;
                OnPropertyChanged();
            }
        }
        [ForeignKey("PieceJointeTypeId")]
        public PieceJointeType TypePieceJointe
        {
            get { return _typePieceJointe; }
            set
            {
                if (Equals(value, _typePieceJointe)) return;
                _typePieceJointe = value;
                OnPropertyChanged();
            }
        }
        public byte[] PieceJointeImage
        {
            get { return _pieceJointeImage; }
            set
            {
                if (Equals(value, _pieceJointeImage)) return;
                _pieceJointeImage = value;
                OnPropertyChanged();
            }
        }
        public Guid PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        #endregion
        #region INotifyPropertyChanged related

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
