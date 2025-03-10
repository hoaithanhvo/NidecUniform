using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Enums
{
    public enum RequestStatus
    {
        Pending,            // Chờ xử lý
        Approved,           // Đã duyệt
        PartiallyDelivered, // Đã giao một phần
        Delivered,          // Đã giao xong
        Cancelled           // Đã hủy
    }

    public enum RequestDetailStatus
    {
        Pending,            // Chờ giao hàng
        PartiallyDelivered, // Đã giao một phần
        FullyDelivered,     // Đã giao đủ
        OutOfStock          // Hết hàng
    }
}
