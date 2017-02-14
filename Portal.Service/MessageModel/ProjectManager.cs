using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Service.MessageModel
{
    public class CreateProjectPostRequest
    {
        [DisplayName("Tên dự án")]
        public string Name { get; set; }
        [DisplayName("Khu vực")]
        public int Region { get; set; }
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
        [DisplayName("Chủ đầu tư")]
        public string Investor { get; set; }
        [DisplayName("Gói thầu")]
        public string ProjectPackage { get; set; }
        [DisplayName("Giá trị hợp đồng")]
        public string TotalInvestment { get; set; }
        [DisplayName("Thời gian hoàn thành")]
        public string Duration { get; set; }
        [DisplayName("Tiến độ")]
        public int ProgressStatus { get; set; }
        [DisplayName("Danh mục")]
        public int Type { get; set; }
        [DisplayName("Ưu tiên sắp xếp")]
        public Nullable<int> SortOrder { get; set; }
        [DisplayName("Trạng thái")]
        public Nullable<int> Status { get; set; }
        [DisplayName("Ảnh đại diện")]
        public Nullable<int> CoverImageId { get; set; }
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        [DisplayName("Mô tả chi tiết")]
        public string Description2 { get; set; }
    }

    public class UpdateProjectImage
    {
        public int ImageId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
    public class UpdateProjectImageRequest
    {
        public int projectId { get; set; }
        public int ImageId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsCoverImage { get; set; }
    }

}
