var Mode = {
    Display: 0,
    Edit: 1
};
var ProjectManagement = ProjectManagement || {};

ProjectManagement = {
    init: function () {
        // support ajax to upload images
        window.addEventListener("submit", function (e) {
            ProjectManagement.showSpin();
            var form = e.target;
            if (form.getAttribute("enctype") === "multipart/form-data") {
                if (form.dataset.ajax) {
                    e.preventDefault();
                    e.stopImmediatePropagation();
                    var xhr = new XMLHttpRequest();
                    xhr.open(form.method, form.action);
                    xhr.onreadystatechange = function () {
                        if (xhr.readyState == 4 && xhr.status == 200) {
                            if (form.dataset.ajaxUpdate) {
                                var updateTarget = document.querySelector(form.dataset.ajaxUpdate);
                                if (updateTarget) {
                                    updateTarget.innerHTML = xhr.responseText;

                                    ProjectManagement.hideSpin();
                                }
                            }
                        }
                    };
                    xhr.send(new FormData(form));
                }
            }
        }, true);

        // Init spin
        this.controls.spin = new Spinner({
            lines: 13 // The number of lines to draw
            , length: 28 // The length of each line
            , width: 14 // The line thickness
            , radius: 42 // The radius of the inner circle
            , scale: 1 // Scales overall size of the spinner
            , corners: 1 // Corner roundness (0..1)
            , color: '#000' // #rgb or #rrggbb or array of colors
            , opacity: 0.25 // Opacity of the lines
            , rotate: 0 // The rotation offset
            , direction: 1 // 1: clockwise, -1: counterclockwise
            , speed: 1 // Rounds per second
            , trail: 60 // Afterglow percentage
            , fps: 20 // Frames per second when using setTimeout() as a fallback for CSS
            , zIndex: 2e9 // The z-index (defaults to 2000000000)
            , className: 'spinner' // The CSS class to assign to the spinner
            , top: '50%' // Top position relative to parent
            , left: '50%' // Left position relative to parent
            , shadow: true // Whether to render a shadow
            , hwaccel: false // Whether to use hardware acceleration
            , position: 'fixed' // Element positioning
        }).spin();

        // install CKEditor
        CKEDITOR.replace('Description2');
        // install chosen control
        $(".chzn-select").chosen();

        // bind events
        //$("#Btn_UploadImage").unbind("click").bind("click", ProjectManagement.upLoadImage)
    },
    controls: {
        spin: null
    },
    requestDeleteProjectImage: function (projectId, imageId) {
        /// <summary>
        /// Delete a image from list images of project
        /// </summary>
        /// <param>N/A</param>
        /// <returns>N/A</returns>

        $.ajax({
            url: "/Admin/Project/DeleteImage",
            dataType: "html",
            data: { projectId: projectId, imageId: imageId },
            success: function (result) {
                alert("Xóa ảnh thành công!!!");
                $("#EditProject_ListProjectImages").empty();
                $("#EditProject_ListProjectImages").html(result);
            },
            error: function (result) {
                alert("Xóa ảnh thất bại");
            }
        });
    },
    deleteProject: function (id, projectName) {
        var title = "Xóa dự án";
        var message = "Bạn có muốn xóa " + projectName + " ?";
        MessageBox.showMessageBox(title, message, function () {
            $.ajax({
                url: '/Admin/Project/Delete',
                data: { id: id },
                type: 'POST',
                success: function () {
                    window.location.replace("/Admin/Project/Index");
                },
                error: function () {
                    alert("Xóa ảnh thất bại!");
                }
            });
        });
    },
    editProjectImage: function (e, imageId) {
        this.changeMode(imageId, Mode.Edit);
    },
    updateProjectImage: function (e, projectId, imageId) {
        // Call function request to server to update data
        var name = $("#EditProject_ListProjectImages tr.edit-mode[data-id='" + imageId + "'] .txt-imagename").val();
        var isActive = $("#EditProject_ListProjectImages tr.edit-mode[data-id='" + imageId + "'] .ckb-isactive").is(":checked");
        var isCoverImage = $("#EditProject_ListProjectImages tr.edit-mode[data-id='" + imageId + "'] .ckb-iscoverimage").is(":checked");
        var request = {
            ProjectId: projectId,
            ImageId: imageId,
            Name: name,
            IsActive: isActive,
            IsCoverImage: isCoverImage
        }
        $.ajax({
            url: '/Admin/Project/UpdateProjectImage',
            data: { request: request },
            type: 'POST',
            success: function (result) {
                $("#EditProject_ListProjectImages").empty();
                $("#EditProject_ListProjectImages").append(result);
            },
            error: function () {
                alert("Cập nhật ảnh thất bại!");
            }
        })
        // Change to Display mode
        this.changeMode(imageId, Mode.Display);
    },
    cancelUpdateProjectImage: function (e, imageId) {

        // Change to Display mode
        this.changeMode(imageId, Mode.Display);
    },
    changeMode: function (imageId, mode) {
        switch (mode) {
            case Mode.Edit: {
                $("#EditProject_ListProjectImages tr.display-mode[data-id='" + imageId + "']").removeClass("hidden").addClass("hidden");
                $("#EditProject_ListProjectImages tr.edit-mode[data-id='" + imageId + "']").removeClass("hidden");
                break;
            }
            case Mode.Display: {
                $("#EditProject_ListProjectImages tr.display-mode[data-id='" + imageId + "']").removeClass("hidden");
                $("#EditProject_ListProjectImages tr.edit-mode[data-id='" + imageId + "']").removeClass("hidden").addClass("hidden");
                break;
            }
        }
    },
    showSpin: function (target) {
        /// <summary>
        /// Create spin control
        /// </summary>
        /// <param>N/A</param>
        /// <returns>N/A</returns>s

        $("#images").append(ProjectManagement.controls.spin.spin().el);
    },
    hideSpin: function () {
        /// <summary>
        /// Hide spin control
        /// </summary>
        /// <param>N/A</param>
        /// <returns>N/A</returns>

        ProjectManagement.controls.spin.stop();
    }
};