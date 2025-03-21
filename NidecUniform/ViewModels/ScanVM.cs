using Azure.Core;
using CoreScanner;
using NidecUniform.ConnectZebra;
using NidecUniform.Helpers;
using NidecUniform.Models;
using NidecUniform.Repositories;
using NidecUniform.Repositories.Interface;
using NidecUniform.Utilities;
using STC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;

namespace NidecUniform.ViewModels
{
    public partial class ScanVM : ViewModelBase
    {
        Scanner[] m_arScanners;
        DiscoverScanner discoverScanner;
        bool[] m_arSelectedTypes;
        List<string> claimlist = new List<string>();
        CCoreScannerClass m_pCoreScanner;
        ScanToConnect scanToConnect;
        bool m_bSuccessOpen;//Is open success
        short m_nNumberOfTypes;
        short[] m_arScannerTypes;
        int m_nTotalScanners;
        #region Variable
        public ICommand SearchCommand { get; set; }
        public ICommand IncreaseQuantityCommand { get; private set; }
        public ICommand DecreaseQuantityCommand { get; private set; }

        public ICommand PushCommand { get; set; }

        public ICommand ConnectScanCommand { get; set; }

        private readonly IEmpoloyee _employeeRepository;
        private readonly IDelivery _deliveryRepository;
        private readonly IDeliveryDetail _deliveryDetailReporitory;
        private readonly IRequestDetails _requestDetailReporitory;
        private readonly IUIServices _uiServices;

        #endregion

        #region Binding Data
        private string _bdEmployeeID;
        public string BDEmployeeID
        {
            get => _bdEmployeeID;
            set
            {
                _bdEmployeeID = value;
                OnPropertyChanged(nameof(BDEmployeeID));
            }
        }
        private string _bdFullName;
        public string BDFullName
        {
            get => _bdFullName;
            set
            {
                _bdFullName = value;
                OnPropertyChanged(nameof(BDFullName));
            }
        }

        private string _bdDepartment;
        public string BDDepartment
        {
            get => _bdDepartment;
            set
            {
                _bdDepartment = value;
                OnPropertyChanged(nameof(BDDepartment));
            }
        }
        private string _bdSection;
        public string BDSection
        {
            get => _bdSection;
            set
            {
                _bdSection = value;
                OnPropertyChanged(nameof(BDSection));
            }
        }

        private string _bdSearch;
        public string BDSearch
        {
            get => _bdSearch;
            set
            {
                _bdSearch = value;
                OnPropertyChanged(nameof(BDSearch));
            }
        }

        private int _bdQuantity;
        public int BDQuantity
        {
            get => _bdQuantity;
            set
            {
                _bdQuantity = value;
                OnPropertyChanged(nameof(BDQuantity));
            }
        }

        private ObservableCollection<RequestDetail> _bdListRequest;
        public ObservableCollection<RequestDetail> BDListRequest
        {
            get => _bdListRequest;
            set
            {
                _bdListRequest = value;
                OnPropertyChanged(nameof(BDListRequest));
            }
        }

        #endregion 

        public ScanVM(IUIServices uiServices)
        {
            BDListRequest = new ObservableCollection<RequestDetail>();
            _employeeRepository = AppServices.GetService<IEmpoloyee>();
            _deliveryRepository = AppServices.GetService<IDelivery>();
            _deliveryDetailReporitory = AppServices.GetService<IDeliveryDetail>();
            _requestDetailReporitory = AppServices.GetService<IRequestDetails>();

            _uiServices = uiServices;

            SearchCommand = new RelayCommand(_ => SearchUser());
            IncreaseQuantityCommand = new RelayCommand(IncreaseQuantity);
            DecreaseQuantityCommand = new RelayCommand(DecreaseQuantity);
            PushCommand = new RelayCommand(_ => PushRequest());
            ConnectScanCommand = new RelayCommand(_ => performGetScanner());


            #region
            m_bSuccessOpen = false;
            m_nNumberOfTypes = 0;
            m_arScanners = new Scanner[MAX_NUM_DEVICES];
            m_arScannerTypes = new short[TOTAL_SCANNER_TYPES];
            m_nTotalScanners = 0;
            for (int i = 0; i < MAX_NUM_DEVICES; i++)
            {
                Scanner scanr = new Scanner();
                m_arScanners.SetValue(scanr, i);
            }
            try
            {
                m_pCoreScanner = new CCoreScannerClass();
            }
            catch (Exception)
            {
                Thread.Sleep(1000);
                m_pCoreScanner = new CCoreScannerClass();
            }

            m_pCoreScanner.BarcodeEvent += new CoreScanner._ICoreScannerEvents_BarcodeEventEventHandler(OnBarcodeEvent);
            m_arSelectedTypes = new bool[TOTAL_SCANNER_TYPES];
            discoverScanner = DiscoverScanner.GetInstance(m_pCoreScanner);
            scanToConnect = ScanToConnect.GetInstance();
            #endregion

        }
        void OnBarcodeEvent(short eventType, ref string scanData)
        {
            try
            {
                string tmpScanData = scanData;
                ShowBarcodeLabel(tmpScanData);

            }
            catch (Exception e)
            {
            }
        }

        public string GetBarcodelabel(string strXml,  out string symbology)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Initial XML" + strXml);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strXml);

                string strData = String.Empty;
                string barcode = xmlDoc.DocumentElement.GetElementsByTagName("datalabel").Item(0).InnerText;
                symbology = xmlDoc.DocumentElement.GetElementsByTagName("datatype").Item(0).InnerText;
                string[] numbers = barcode.Split(' ');

                List<byte> bytes = new List<byte>();
                foreach (string number in numbers)
                {
                    if (String.IsNullOrEmpty(number))
                    {
                        break;
                    }

                    bytes.Add(Convert.ToByte(number, 16));
                }

                return ConvertToEncodeString(bytes.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string ConvertToEncodeString(byte[] bytes)
        {
            string encodedString;

            switch ("ASCII")
            {
                case "ASCII":
                    encodedString = System.Text.Encoding.ASCII.GetString(bytes);
                    break;
                case "UTF-8":
                    encodedString = System.Text.Encoding.UTF8.GetString(bytes);
                    break;
                case "UTF-16":
                    encodedString = System.Text.Encoding.Unicode.GetString(bytes);
                    break;
                case "UTF-32":
                    encodedString = System.Text.Encoding.UTF32.GetString(bytes);
                    break;
                default:
                    encodedString = System.Text.Encoding.Default.GetString(bytes);
                    break;
            }
            return encodedString;
        }

        private void ShowBarcodeLabel(string strXml)
        {
            string symbology = "";
            string LabelText = GetBarcodelabel(strXml,  out symbology);

        }

        private void IncreaseQuantity(object parameter)
        {
            Debug.WriteLine("Increase Quantity clicked");
            if (parameter is RequestDetail item)
            {
                var selectedItem = BDListRequest.FirstOrDefault(x => x.ID == item.ID);
                if (selectedItem != null)
                {
                    int maxQuantity = selectedItem.QuantityRequested - selectedItem.QuantityDelivered;
                    selectedItem.BDQuantity = Math.Min(maxQuantity, (selectedItem.BDQuantity ?? 0) + 1);
                    BDListRequest = new ObservableCollection<RequestDetail>(BDListRequest);
                    OnPropertyChanged(nameof(BDListRequest));
                }
            }
            else
            {
                Debug.WriteLine("Parameter is not a RequestDetail");
            }
        }
        private void DecreaseQuantity(object parameter)
        {
            Debug.WriteLine("Increase Quantity clicked");
            if (parameter is RequestDetail item)
            {
                var selectedItem = BDListRequest.FirstOrDefault(x => x.ID == item.ID);
                if (selectedItem != null)
                {
                    selectedItem.BDQuantity = Math.Max(0, (selectedItem.BDQuantity ?? 0) - 1);
                    BDListRequest = new ObservableCollection<RequestDetail>(BDListRequest);
                    OnPropertyChanged(nameof(BDListRequest));
                }
            }
            else
            {
                Debug.WriteLine("Parameter is not a RequestDetail");
            }
        }

        private M_Employee User = new M_Employee();
        private async Task SearchUser()
        {
            BDListRequest.Clear();
            _uiServices.ShowProgressDialog();
            try
            {
                User = await _employeeRepository.GetEmployee(BDSearch.Trim());
                if (User == null)
                {
                    _uiServices.HideProgressDialog();
                    ClearBindingData();
                    MessageBox.Show($"User {BDSearch} Not found", "Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;

                }
                BindingData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            finally
            {
                _uiServices.HideProgressDialog();
            }
        }

        private void BindingData()
        {
            BDEmployeeID = User.EmployeeID;
            BDFullName = User.FullName;
            BDDepartment = User.Department;
            BDSection = User.Position;

            foreach (var i in User.Requests)
            {
                foreach (var j in i.RequestDetails)
                {
                    BDListRequest.Add(new RequestDetail
                    {
                        ID = j.ID,
                        ProductName = j.ProductName,
                        QuantityDelivered = j.QuantityDelivered,
                        QuantityRequested = j.QuantityRequested,
                        StartDate = i.StartDate,
                        EndDate = i.EndDate,
                        RequestID = i.ID,
                        ProductID = j.ProductID,
                        EmployeeID = j.EmployeeID,
                        Unit = j.Unit

                    });
                }
            }
        }
        private void ClearBindingData()
        {
            BDEmployeeID = string.Empty;
            BDFullName = string.Empty;
            BDDepartment = string.Empty;
            BDSection = string.Empty;
        }

        private async Task PushRequest()
        {
            try
            {
                _uiServices.ShowProgressDialog();
                List<RequestDetail> BDListRequestTemp = new List<RequestDetail>();
                List<DeliveryDetail> deliveryDetailsList = new List<DeliveryDetail>();
                List<RequestDetail> updateQuantityDelivered = new List<RequestDetail>();
                foreach (var item in BDListRequest.Where(i => i.BDQuantity > 0))
                {
                    BDListRequestTemp.Add(item);
                }

                foreach (var request in User.Requests)
                {
                    foreach (var item in BDListRequestTemp)
                    {
                        if (request.ID == item.RequestID)
                        {
                            var getDelivery = await _deliveryRepository.GetDelivery(request.ID);
                            int deliveryID;

                            if (getDelivery == null)
                            {
                                deliveryID = await _deliveryRepository.AddDelivery(new M_Delivery
                                {
                                    RequestID = request.ID,
                                    EmployeeID = request.EmployeeID,
                                });
                            }
                            else
                            {
                                deliveryID = getDelivery.ID;
                            }
                            deliveryDetailsList.Add(new DeliveryDetail
                            {
                                EmployeeID = item.EmployeeID,
                                ProductID = item.ProductID,
                                DeliveryID = deliveryID,
                                ProductName = item.ProductName,
                                QuantityDelivered = (int)item.BDQuantity,
                                Unit = item.Unit,
                                RequestID = request.ID,

                            });
                            updateQuantityDelivered.Add(new RequestDetail
                            {
                                ID = item.ID,
                                QuantityDelivered = item.BDQuantity ?? 0,
                            });
                        }
                    }
                }
                foreach (var request in updateQuantityDelivered)
                {
                    await _requestDetailReporitory.UpdateQuantityDelivered(request.ID, request.QuantityDelivered);
                }

                await _deliveryDetailReporitory.AddDeliveryDetails(deliveryDetailsList);
                await SearchUser();
                _uiServices.HideProgressDialog();

                MessageBox.Show("Import Sucess", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void performGetScanner()
        {
            m_arSelectedTypes = scanToConnect.GetSelectedTypes();
            discoverScanner.FilterScannerList(ref m_arSelectedTypes, 0);
            MakeConnectCtrl();
            registerForEvents();
            ShowScanners();
            //GetPairingBarcode();
        }
        private void MakeConnectCtrl()
        {
            Connect();
        }

        private void Connect()
        {
            if (m_bSuccessOpen)
            {
                return;
            }
            int appHandle = 0;
            m_nNumberOfTypes = scanToConnect.GetSelectedScannerTypes();
            m_arScannerTypes = scanToConnect.GetScannerTypes();
            int status = STATUS_FALSE;

            try
            {
                m_pCoreScanner.Open(appHandle, m_arScannerTypes, m_nNumberOfTypes, out status);
                //DisplayResult(status, "OPEN");
                if (STATUS_SUCCESS == status)
                {
                    m_bSuccessOpen = true;
                }
            }
            catch (Exception exp)
            {
                // MessageBox.Show("Error OPEN - " + exp.Message, APP_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //if (STATUS_SUCCESS == status)
                //{
                //    SetControls();
                //}
            }
        }

        private void registerForEvents()
        {

            int nEvents = 0;
            string strEvtIDs = scanToConnect.GetRegUnRegisterIDs(out nEvents);
            string inXml = scanToConnect.GenerateInitXML(nEvents, strEvtIDs);

            int opCode = REGISTER_FOR_EVENTS;
            string outXml = "";
            int status = STATUS_FALSE;
            ExecCmd(opCode, ref inXml, out outXml, out status);
            //DisplayResult(status, "REGISTER_FOR_EVENTS");
        }

        private void ExecCmd(int opCode, ref string inXml, out string outXml, out int status)
        {
            outXml = "";
            status = STATUS_FALSE;
            if (m_bSuccessOpen)
            {
                try
                {
                    m_pCoreScanner.ExecCommand(opCode, ref inXml, out outXml, out status);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void ShowScanners()
        {
            string inXml = String.Empty;
            int status = 1;
            //lstvScanners.Items.Clear();
            //cmbSlcrScnr.Items.Clear();

            m_arScanners.Initialize();
            m_nTotalScanners = 0;
            short numOfScanners = 0;
            string outXML = "";

            try
            {
                m_arScanners = GetScanners(ref numOfScanners, ref outXML, ref status, claimlist);
                //DisplayResult(status, "GET_SCANNERS");

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error GETSCANNERS - " + ex.Message, APP_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public Scanner[] GetScanners(ref short numberOfScanners, ref string outXML, ref int status, List<string> claimlist)
        {
            Scanner[] arScanners = new Scanner[MAX_NUM_DEVICES];
            for (int i = 0; i < MAX_NUM_DEVICES; i++)
            {
                Scanner scanr = new Scanner();
                arScanners.SetValue(scanr, i);
            }
            arScanners.Initialize();
            int[] scannerIdList = new int[MAX_NUM_DEVICES];
            int nScannerCount = 0;
            try
            {
                m_pCoreScanner.GetScanners(out numberOfScanners, scannerIdList, out outXML, out status);
                if (Constants.StatusSuccess == status)
                {
                    ReadXmlString_GetScanners(outXML, arScanners, numberOfScanners, out nScannerCount);

                    for (int index = 0; index < arScanners.Length && claimlist.Count > 0; index++) // Noticed looping 255 times even the claim list = 0
                    {
                        for (int i = 0; i < claimlist.Count; i++)
                        {
                            if (string.Compare(claimlist[i], arScanners[index].SERIALNO) == 0)
                            {
                                Scanner objScanner = (Scanner)arScanners.GetValue(index);
                                objScanner.CLAIMED = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return arScanners;
        }
        public void ReadXmlString_GetScanners(string strXml, Scanner[] arScanner, int nTotal, out int nScannerCount)
        {
            nScannerCount = 0;
            if (1 > nTotal || String.IsNullOrEmpty(strXml))
            {
                return;
            }
            try
            {
                XmlTextReader xmlRead = new XmlTextReader(new StringReader(strXml));
                // Skip non-significant whitespace   
                xmlRead.WhitespaceHandling = WhitespaceHandling.Significant;

                string sElementName = "", sElmValue = "";
                Scanner scanr = null;
                int nIndex = 0;
                bool bScanner = false;
                while (xmlRead.Read())
                {
                    switch (xmlRead.NodeType)
                    {
                        case XmlNodeType.Element:
                            sElementName = xmlRead.Name;
                            if (Scanner.TAG_SCANNER == sElementName)
                            {
                                bScanner = false;
                            }

                            string strScannerType = xmlRead.GetAttribute(Scanner.TAG_SCANNER_TYPE);
                            if (xmlRead.HasAttributes && (
                                (Scanner.TAG_SCANNER_SNAPI == strScannerType) ||
                                (Scanner.TAG_SCANNER_SSI == strScannerType) ||
                                (Scanner.TAG_SCANNER_NIXMODB == strScannerType) ||
                                (Scanner.TAG_SCANNER_IBMHID == strScannerType) ||
                                (Scanner.TAG_SCANNER_OPOS == strScannerType) ||
                                (Scanner.TAG_SCANNER_IMBTT == strScannerType) ||
                                (Scanner.TAG_SCALE_IBM == strScannerType) ||
                                (Scanner.SCANNER_SSI_BT == strScannerType) ||
                                (Scanner.CAMERA_UVC == strScannerType) ||
                                (Scanner.TAG_SCANNER_HIDKB == strScannerType) ||
                                (Scanner.SCANNER_SSI_IP == strScannerType)))//n = xmlRead.AttributeCount;
                            {
                                if (arScanner.GetLength(0) > nIndex)
                                {
                                    bScanner = true;
                                    scanr = (Scanner)arScanner.GetValue(nIndex++);
                                    if (null != scanr)
                                    {
                                        scanr.ClearValues();
                                        nScannerCount++;
                                        scanr.SCANNERTYPE = strScannerType;
                                    }
                                }
                            }
                            break;

                        case XmlNodeType.Text:
                            if (bScanner && (null != scanr))
                            {
                                sElmValue = xmlRead.Value;
                                switch (sElementName)
                                {
                                    case Scanner.TAG_SCANNER_ID:
                                        scanr.SCANNERID = sElmValue;
                                        break;
                                    case Scanner.TAG_SCANNER_SERIALNUMBER:
                                        scanr.SERIALNO = sElmValue;
                                        break;
                                    case Scanner.TAG_SCANNER_MODELNUMBER:
                                        scanr.MODELNO = sElmValue;
                                        break;
                                    case Scanner.TAG_SCANNER_GUID:
                                        scanr.GUID = sElmValue;
                                        break;
                                    case Scanner.TAG_SCANNER_PORT:
                                        scanr.PORT = sElmValue;
                                        break;
                                    case Scanner.TAG_SCANNER_FW:
                                        scanr.SCANNERFIRMWARE = sElmValue;
                                        break;
                                    case Scanner.TAG_SCANNER_CN:
                                        scanr.SCANNERCONFIG = sElmValue;
                                        break;
                                    case Scanner.TAG_SCANNER_DOM:
                                        scanr.SCANNERMNFDATE = sElmValue;
                                        break;
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
