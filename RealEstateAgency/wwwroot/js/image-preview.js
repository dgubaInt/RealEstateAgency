var DisplayPreviewDialog = function (preview, endelem) {
    var previewbackground = $('<div class="preview_background"></div>');
    var previewclone = preview.clone(true, true).click(function (e) {
        e.stopPropagation();
    });
    var previewdialog = $('<div class="preview_dialog"></div>').append(previewclone);



    var HidePreviewDialog = function () {
        $(document).off('keyup.photo_preview');



        previewbackground.remove();
        endelem?.focus();
    };



    $(document).on('keyup.photo_preview', function (e) {
        if (e.keyCode == 27) {
            HidePreviewDialog();
        }
    });



    previewbackground.append(previewdialog).click(function () {
        HidePreviewDialog();
    });



    $('body').append(previewbackground);
    preview.focus();
};



$("body").on("click", ".image_preview img", function(ev){
    DisplayPreviewDialog($('<img>').attr('src', ev.target.src))
});