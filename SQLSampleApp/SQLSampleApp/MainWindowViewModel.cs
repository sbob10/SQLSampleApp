using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private String _searchTabName;

        public String SearchTabName
        {
            get { return _searchTabName; }
            set { SetProperty(ref _searchTabName, value, () => SearchTabName); }
        }

        private ObservableCollection<Customer> _searchTabResultCollection;

        public ObservableCollection<Customer> SearchTabResultCollection
        {
            get { return _searchTabResultCollection; }
            set { SetProperty(ref _searchTabResultCollection, value, () => SearchTabResultCollection); }
        }

        private Customer _searchTabResultSelectedCustomer;

        public Customer SearchTabResultSelectedCustomer
        {
            get { return _searchTabResultSelectedCustomer; }
            set { SetProperty(ref _searchTabResultSelectedCustomer, value, () => SearchTabResultSelectedCustomer); LoadFortuneForCustomer(value); }
        }

        private Fortune _detailsTabFortuneOfCustomer;

        public Fortune DetailsTabFortuneOfCustomer
        {
            get { return _detailsTabFortuneOfCustomer; }
            set { SetProperty(ref _detailsTabFortuneOfCustomer, value, () => DetailsTabFortuneOfCustomer); }
        }

        private int _mainTabControlSelectedIndex;

        public int MainTabControlSelectedIndex
        {
            get { return _mainTabControlSelectedIndex; }
            set { SetProperty(ref _mainTabControlSelectedIndex, value, () => MainTabControlSelectedIndex); }
        }

        #endregion Properties

        #region Data



        #endregion Data

        #region Commands&Services

        public ICommand AddCustomerCommand { get; set; }
        public ICommand SearchNameCommand { get; set; }
        public ICommand ShowDetailsCommand { get; set; }

        public ICommand WriteMailCommand { get; set; }
        public ICommand CallNumberCommand { get; set; }
        public ICommand ExportToPdfCommand { get; set; }

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
            MainTabControlSelectedIndex = 0;

            AddTabCustomer = new Customer();

            SearchTabName = "";
            SearchTabResultSelectedCustomer = new Customer();
            SearchTabResultCollection = new ObservableCollection<Customer>(new List<Customer>());

            DetailsTabFortuneOfCustomer = new Fortune();
        }

        private void InitCommandsAndServices()
        {
            IServiceContainer = new ServiceContainer(this);
            ServiceContainer.RegisterService(new DevExpress.Mvvm.UI.MessageBoxService());

            AddCustomerCommand = new DelegateCommand(AddCustomer);

            SearchNameCommand = new DelegateCommand(SearchName);
            ShowDetailsCommand = new DelegateCommand(ShowDetails);

            WriteMailCommand = new DelegateCommand(WriteMail);
            CallNumberCommand = new DelegateCommand(CallNumber);
            ExportToPdfCommand = new DelegateCommand(ExportToPdf);
        }

        #endregion Initialisations

        #region Public Methods



        #endregion Public Methods

        #region Private Methods

        private void LoadFortuneForCustomer(Customer customer)
        {
            if(customer.ID != 0)
            {

            }
        }

        #endregion Private Methods

        #region Command Methods

        // AddTab Methods

        private void AddCustomer()
        {
            //Here the database stuff...
            MessageBoxService.ShowMessage("Benutzer hinzugefügt:\n" + AddTabCustomer.ToString(), "Message", MessageButton.OK, MessageIcon.Information);
            AddTabCustomer = new Customer();
        }

        // SearchTab Methods

        private void SearchName()
        {

        }

        private void ShowDetails()
        {
            MainTabControlSelectedIndex = 2;
        }

        // DetailsTab Methods

        private void WriteMail()
        {

        }

        private void CallNumber()
        {

        }

        private void ExportToPdf()
        {

        }

        #endregion Command Methods

    }
}
