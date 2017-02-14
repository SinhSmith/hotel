using Portal.Model.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Model.ViewModel
{
    public class ProjectSummaryViewModel
    {
        public int Id { get; set; }
        //[DisplayName("Khu vực")]
        //public int Region { get; set; }
        [DisplayName("Địa chỉ dự án")]
        public string Address { get; set; }
        [DisplayName("Tên dự án")]
        public string Name { get; set; }
        //[DisplayName("Chủ đầu tư")]
        //public string Investor { get; set; }
        //[DisplayName("Gói thầu")]
        //public string ProjectPackage { get; set; }
        //[DisplayName("Giá trị hợp đồng")]
        //public string TotalInvestment { get; set; }
        //[DisplayName("Thời gian hoàn thành")]
        //public string Duration { get; set; }
        //[DisplayName("Tiến độ")]
        //public int ProgressStatus { get; set; }
        [DisplayName("Danh mục")]
        public int Type { get; set; }
        [DisplayName("Ưu tiên sắp xếp")]
        public Nullable<int> SortOrder { get; set; }
        [DisplayName("Trạng thái")]
        public string Status { get; set; }
        [DisplayName("Ảnh đại diện")]
        public share_Images CoverImage { get; set; }
    }

    public class ProjectFullView
    {
        public int Id { get; set; }
        [DisplayName("Tên dự án")]
        public string Name { get; set; }
        [DisplayName("Khu vực")]
        public int Region { get; set; }
        [DisplayName("Địa chỉ dự án")]
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

        public IEnumerable<cms_News> cms_News { get; set; }
        public IEnumerable<ImageProjectViewModel> share_Images { get; set; }
    }

    public class LoadListImageProjectPartialViewModels
    {
        public int ProjectId;
        public IEnumerable<share_Images> Images;
    }

    public class ListImageProjectPartialViewModels
    {
        public int ProjectId;
        public IEnumerable<ImageProjectViewModel> Images;
        public Nullable<int> CoverImageId;
    }

    public class ImageProjectViewModel
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }

    }
}
