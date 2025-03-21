using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.ViewModels
{
    public partial class ScanVM
    {
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
    }
}
