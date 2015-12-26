using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace OnliStam.Dashboard.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Table: INotifyPropertyChanged
    {
        #region == Fields ==

        string _ime;

        #endregion

        /// <summary>
        /// Ime tabele
        /// </summary>
        public string Ime {
            get { return _ime; }
            set
            {
                if(_ime != value)
                {
                    _ime = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Kolone u tabeli
        /// </summary>
        public ObservableCollection<Kolona> Kolone { get; set; }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

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
    }
}
