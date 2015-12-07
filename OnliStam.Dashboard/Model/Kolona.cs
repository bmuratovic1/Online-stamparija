using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace OnliStam.Dashboard.Model
{
    /// <summary>
    /// 
    /// </summary>
    class Kolona: INotifyPropertyChanged
    {
        #region == Fields ==

        string _ime;

        string _tip;

        bool _nullable;

        string _key;

        bool _autoIncrement;

        string _default;

        #endregion

        #region == Properties ==

        /// <summary>
        /// Ime kolone
        /// </summary>
        public string Ime {
            get { return _ime; }
            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Ime", "Ime cannot be null or empty!");
                if(_ime != value)
                {
                    _ime = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Tip kolone
        /// </summary>
        public string Tip {
            get { return _tip; }
            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Tip", "Tip cannot be null or empty!");
                if(_tip != value)
                {
                    _tip = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Indicates if column is nulable
        /// </summary>
        public bool Nullable
        {
            get { return _nullable; }
            set { _nullable = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AutoIncrement
        {
            get { return _autoIncrement; }
            set { _autoIncrement = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Default
        {
            get { return _default; }
            set { _default = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region == Constructors ==

        public Kolona()
        {

        }

        #endregion

        #region == Methods ==

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
