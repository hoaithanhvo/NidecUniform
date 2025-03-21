using CoreScanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using STC;

namespace NidecUniform.ConnectZebra
{
    class DiscoverScanner
    {
        CCoreScannerClass coreScanner;
        // Scanner types
        public const short SCANNER_TYPES_ALL = 1;
        public const short SCANNER_TYPES_SNAPI = 2;
        

        private static DiscoverScanner _discoverScanner;

        public DiscoverScanner(CCoreScannerClass m_pCoreScanner)
        {
            coreScanner = m_pCoreScanner;
        }

        public static DiscoverScanner GetInstance(CCoreScannerClass m_pCoreScanner)
        {
            if (_discoverScanner == null)
                _discoverScanner = new DiscoverScanner(m_pCoreScanner);
            return _discoverScanner;

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
    }
}
