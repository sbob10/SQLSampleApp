using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SQLSampleApp
{
    public class MainWindowViewModel : ViewModelBase
    {

        #region Properties

        private Customer _addTabCustomer;

        public Customer AddTabCustomer
        {
            get { return _addTabCustomer; }
            set { SetProperty(ref _addTabCustomer, value, () => AddTabCustomer); }
        }

        #endregion Properties

        #region Data



        #endregion Data

        #region Commands&Services

        public ICommand AddCustomerCommand { get; set; }

        public IMessageBoxService MessageBoxService { get { return ServiceContainer.GetService<IMessageBoxService>(); } set { } }
        public IServiceContainer IServiceContainer { get; set; }

        #endregion Commands&Services

        #region Initialisations

        public MainWindowViewModel()
        {
            InitCommandsAndServices();
            InitValuesAndCollections();
        }

        private void InitValuesAndCollections()
        {
            AddTabCustomer = new Customer();
        }

        private void InitCommandsAndServices()
        {
            IServiceContainer = new ServiceContainer(this);
            ServiceContainer.RegisterService(new DevExpress.Mvvm.UI.MessageBoxService());

            AddCustomerCommand = new DelegateCommand(AddCustomer);
        }

        #endregion Initialisations

        #region Public Methods



        #endregion Public Methods

        #region Private Methods



        #endregion Private Methods

        #region Command Methods

        private void AddCustomer()
        {
            //Here the database stuff...
            MessageBoxService.ShowMessage("Benutzer hinzugefügt: " + AddTabCustomer.ToString(), "Message", MessageButton.OK, MessageIcon.Information);
        }

        #endregion Command Methods

    }
}
