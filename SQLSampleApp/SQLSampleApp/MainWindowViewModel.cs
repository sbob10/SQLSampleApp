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
            set { SetDetailsTabFortuneStrings(value); SetProperty(ref _detailsTabFortuneOfCustomer, value, () => DetailsTabFortuneOfCustomer); }
        }

        private int _mainTabControlSelectedIndex;

        public int MainTabControlSelectedIndex
        {
            get { return _mainTabControlSelectedIndex; }
            set { SetProperty(ref _mainTabControlSelectedIndex, value, () => MainTabControlSelectedIndex); }
        }

        private String _detailsTabFortuneOfCustomerShares;

        public String DetailsTabFortuneOfCustomerShares
        {
            get { return _detailsTabFortuneOfCustomerShares; }
            set { SetProperty(ref _detailsTabFortuneOfCustomerShares, value, () => DetailsTabFortuneOfCustomerShares); }
        }

        private String _detailsTabFortuneOfCustomerFonds;

        public String DetailsTabFortuneOfCustomerFonds
        {
            get { return _detailsTabFortuneOfCustomerFonds; }
            set { SetProperty(ref _detailsTabFortuneOfCustomerFonds, value, () => DetailsTabFortuneOfCustomerFonds); }
        }

        private String _detailsTabFortuneOfCustomerPensions;

        public String DetailsTabFortuneOfCustomerPensions
        {
            get { return _detailsTabFortuneOfCustomerPensions; }
            set { SetProperty(ref _detailsTabFortuneOfCustomerPensions, value, () => DetailsTabFortuneOfCustomerPensions); }
        }

        #endregion Properties

        #region Data

        private BusinessLogic _businessLogic;

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
            InitBusinessLogic();
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

        private void InitBusinessLogic()
        {
            _businessLogic = new BusinessLogic();
        }

        #endregion Initialisations

        #region Public Methods


        #endregion Public Methods

        #region Private Methods

        private async void LoadFortuneForCustomer(Customer customer)
        {
            if (customer == null || _businessLogic == null)
                return;

            Result<Fortune> res = await _businessLogic.GetFortuneForCustomerAsync(SearchTabResultSelectedCustomer.ID);

            if (res.Status.Equals(Status.Error))
            {
                switch (res.Message)
                {
                    case Message.DB_ENTRIES_LOAD:
                        MessageBoxService.ShowMessage("Problem mit dem Zugriff auf die Datenbank.", "Vermögen laden gescheitert", MessageButton.OK, MessageIcon.Error);
                        break;
                    default:
                        break;
                }
                DetailsTabFortuneOfCustomer = new Fortune();
            }
            else
            {
                DetailsTabFortuneOfCustomer = res.Data;
            }
        }

        private void SetDetailsTabFortuneStrings(Fortune fortune)
        {
            if(fortune != null)
            {
                DetailsTabFortuneOfCustomerShares = fortune.Shares.ToString("###,###,###.00") + " $";
                DetailsTabFortuneOfCustomerFonds = fortune.Fonds.ToString("###,###,###.00") + " $";
                DetailsTabFortuneOfCustomerPensions = fortune.Pensions.ToString("###,###,###.00") + " $";
            }
        }

        #endregion Private Methods

        #region Command Methods

        // AddTab Methods

        private void AddCustomer()
        {
            Result res = _businessLogic.AddCustomer(AddTabCustomer);

            if (res.Status.Equals(Status.Error))
            {
                switch (res.Message)
                {
                    case Message.NAME_EMPTY_NULL:
                        MessageBoxService.ShowMessage("Das Feld \"Name\" war leer.", "Benutzer hinzufügen gescheitert", MessageButton.OK, MessageIcon.Error);
                        break;
                    case Message.DB_ENTRY_ADD:
                        MessageBoxService.ShowMessage("Problem mit dem Zugriff auf die Datenbank.", "Benutzer hinzufügen gescheitert", MessageButton.OK, MessageIcon.Error);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBoxService.ShowMessage("Benutzer hinzugefügt:\n" + AddTabCustomer.ToString(), "Message", MessageButton.OK, MessageIcon.Information);
                AddTabCustomer = new Customer();
            }
           
        }

        // SearchTab Methods

        private async void SearchName()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            Result<List<Customer>> res = await _businessLogic.GetCustomerAsync(SearchTabName);

            if (res.Status.Equals(Status.Error))
            {
                switch (res.Message)
                {
                    case Message.NAME_EMPTY_NULL:
                        MessageBoxService.ShowMessage("Das Feld \"Name\" war leer.", "Benutzer suchen gescheitert", MessageButton.OK, MessageIcon.Error);
                        break;
                    case Message.DB_ENTRIES_LOAD:
                        MessageBoxService.ShowMessage("Problem mit dem Zugriff auf die Datenbank.", "Benutzer suchen gescheitert", MessageButton.OK, MessageIcon.Error);
                        break;
                    default:
                        break;
                }
                SearchTabResultCollection = new ObservableCollection<Customer>(new List<Customer>());
            }
            else
            {
                //MessageBoxService.ShowMessage("Benutzer geladen.", "Erfolg", MessageButton.OK, MessageIcon.Information);
                SearchTabResultCollection = new ObservableCollection<Customer>(res.Data); 
                SearchTabName = "";               
            }

            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void ShowDetails()
        {
            MainTabControlSelectedIndex = 2;
        }

        // DetailsTab Methods

        private async void WriteMail()
        {
            Result res = await _businessLogic.SendMailToAsync(SearchTabResultSelectedCustomer.Mail);

            if(res.Status.Equals(Status.Success))
            {
                MessageBoxService.ShowMessage("Email versandt.", "Outlook meldet Erfolg", MessageButton.OK, MessageIcon.Information);
            }
            else
            {
                MessageBoxService.ShowMessage("Email nicht versandt.", "Outlook meldet Misserfolg", MessageButton.OK, MessageIcon.Information);
            }

        }

        private void CallNumber()
        {

        }

        private async void ExportToPdf()
        {
            if(SearchTabResultSelectedCustomer != null && DetailsTabFortuneOfCustomer != null)
            { 
                Result res = await _businessLogic.ExportToPdfAsync(SearchTabResultSelectedCustomer, DetailsTabFortuneOfCustomer);

                if (res.Status.Equals(Status.Success))
                {
                    MessageBoxService.ShowMessage("Pdf erstellt.", "System meldet Erfolg", MessageButton.OK, MessageIcon.Information);
                }
                else
                {
                    MessageBoxService.ShowMessage("Pdf nicht erstellt.", "System meldet Misserfolg", MessageButton.OK, MessageIcon.Information);
                }
            }
        }

        #endregion Command Methods
    }
}
