using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Enums
{
    /// <summary>
    /// Enum trạng thái đánh giá theo điểm
    /// </summary>
    public enum EvaluateState : int
    {
        /// <summary>
        /// Không xác định
        /// </summary>
        None = 0,
        /// <summary>
        /// Đạt
        /// </summary>
        Pass = 1,
        /// <summary>
        /// Không đạt
        /// </summary>
        Faile = 2
    }
}
