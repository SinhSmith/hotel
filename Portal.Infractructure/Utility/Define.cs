using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infractructure.Utility
{
    public class Define
    {
        public const int DISPLAY_PROJECT_PAGE_SIZE = 9;
        public const int PAGE_SIZE = 10;
        public const int ID_PAGE_INTRODUCTION = 18;
        public enum SystemConfig
        {
            TotalVisitors
        }

        public enum Status
        {
            [Description("Ngưng hoạt động")]
            Deactive = 0,

            [Description("Đang hoạt động")]
            Active = 1,

            [Description("Xóa")]
            Delete = 2
        }

        public enum MenuEnum
        {
            [Description("Admin")]
            Admin = 1,

            [Description("User")]
            User = 2,
        }

        public enum BannerTypes
        {
            [Description("Banner 1 - Slide")]
            Banner1 = 1,

            [Description("Banner 2 - Mùa Xuân")]
            SpringSeason = 2,

            [Description("Banner 2 - Mùa Hạ")]
            SummerSeason = 3,

            [Description("Banner 2 - Mùa Thu")]
            AutumnSeason = 4,

            [Description("Banner 2 - Mùa Đông")]
            WinterSeason = 5,

            [Description("Popup")]
            Popup = 10
        }

        public enum Region
        {
            [Description("Miền bắc")]
            North = 0,
            [Description("Miền trung")]
            Central = 1,
            [Description("Miền nam")]
            South = 2,
            [Description("Nước ngoài")]
            Foreign = 3
        }

        public enum ProgressStatus
        {
            [Description("Đang thi công")]
            InProgress = 0,
            [Description("Hoàn thành")]
            Completed = 1
        }

        public enum ProjectType
        {
            [Description("Thiết kế và thi công")]
            DesignAndConstruction = 0,
            [Description("Dân dụng")]
            CivilEngineering = 1,
            [Description("Công nghiệp")]
            Industry = 2,
            [Description("Hạ tầng")]
            Infrastructure = 3
        }
        public enum CategoryType
        {
            All,
            Region,
            ProjectType,
            ProgressStatus
        }

        public enum SearchItemType
        {
            News,
            Project
        }
    }
}
