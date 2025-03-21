using CoreScanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using STC;
using System.IO;
using System.Windows;

namespace NidecUniform.ConnectZebra
{
    class DiscoverScanner
    {
        // Scanner types
        public const short SCANNER_TYPES_ALL = 1;
        public const short SCANNER_TYPES_SNAPI = 2;
        public const short SCANNER_TYPES_SSI = 3;
        public const short SCANNER_TYPES_RSM = 4;
        public const short SCANNER_TYPES_IMAGING = 5;
        public const short SCANNER_TYPES_IBMHID = 6;
        public const short SCANNER_TYPES_NIXMODB = 7;
        public const short SCANNER_TYPES_HIDKB = 8;
        public const short SCANNER_TYPES_IBMTT = 9;
        public const short SCALE_TYPES_IBM = 10;
        public const short SCALE_TYPES_SSI_BT = 11;
        public const short CAMERA_TYPES_UVC = 14;
        public const short TOTAL_SCANNER_TYPES = CAMERA_TYPES_UVC;
        const int STATUS_SUCCESS = 0;
        const int STATUS_FALSE = 1;
        const int MAX_NUM_DEVICES = 255;
        const int REGISTER_FOR_EVENTS = 1001;
        public const int RSM_ATTR_GETALL = 5000;
        public const int RSM_ATTR_GET = 5001;
        public const int RSM_ATTR_GETNEXT = 5002;
        public const int RSM_ATTR_SET = 5004;
        public const int RSM_ATTR_STORE = 5005;
        bool[] m_arSelectedTypes;


        public static event Action<string> BarcodeScanned;



        CCoreScannerClass coreScanner;
        ScanToConnect scanToConnect;
        bool m_bSuccessOpen;
        short m_nNumberOfTypes;
        short[] m_arScannerTypes;
        Scanner[] m_arScanners;
        int m_nTotalScanners = 0;
        List<string> claimlist = new List<string>();
        string strXml;

        private static DiscoverScanner _discoverScanner = null;
        public DiscoverScanner(CCoreScannerClass m_pCoreScanner)
        {
            coreScanner = m_pCoreScanner;
            coreScanner.BarcodeEvent += new CoreScanner._ICoreScannerEvents_BarcodeEventEventHandler(OnBarcodeEvent);
            m_arScanners = new Scanner[MAX_NUM_DEVICES];
            m_arSelectedTypes = new bool[TOTAL_SCANNER_TYPES];
            scanToConnect = ScanToConnect.GetInstance();
            m_nNumberOfTypes = scanToConnect.GetSelectedScannerTypes();
            m_arScannerTypes = scanToConnect.GetScannerTypes();
            GetScanner();
        }

        public static DiscoverScanner GetInstance()
        {
            if (_discoverScanner == null)
                _discoverScanner = new DiscoverScanner(new CCoreScannerClass());
            return _discoverScanner;

        }


        #region getScan
        private void GetScanner()
        {
            m_arSelectedTypes = scanToConnect.GetSelectedTypes();
            FilterScannerList(ref m_arSelectedTypes, 0);
            MakeConnectCtrl();
            registerForEvents();
            ShowScanners();
            //GetPairingBarcode();
        }
        public void FilterScannerList(ref bool[] m_arSelectedTypes, int indexFilter)
        {
            switch (indexFilter)
            {
                case 0:
                    m_arSelectedTypes[SCANNER_TYPES_ALL - 1] = true;
                    break;
                case 3:
                    m_arSelectedTypes[SCANNER_TYPES_SNAPI - 1] = true;
                    break;
            }
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
                coreScanner.Open(appHandle, m_arScannerTypes, m_nNumberOfTypes, out status);
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
                    coreScanner.ExecCommand(opCode, ref inXml, out outXml, out status);
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
                coreScanner.GetScanners(out numberOfScanners, scannerIdList, out outXML, out status);
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
        #endregion


        private void OnBarcodeEvent(short eventType, ref string scanData)
        {
            try
            {
                strXml = scanData;
                string barcode = ShowBarcodeLabel();
                BarcodeScanned?.Invoke(barcode);
            }
            catch (Exception e)
            {
            }
        }

        public string ShowBarcodeLabel()
        {
            string symbology = "";
            string LabelText = GetBarcodelabel(strXml, out symbology);
            return LabelText;

        }

        public string GetBarcodelabel(string strXml, out string symbology)
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

                return System.Text.Encoding.ASCII.GetString(bytes.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
