$(document).ready(function() {
    $('#removeImage').click(function() {
        $('#empImage').attr('src', '');
        $('#empImage').css("display", "none");
        $('#removeImage').css("display", "none");
        $('#empimageUpload').val("");
    });
});
function readUrl(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#empImage').css("display", "block");
                $('#removeImage').css("display", "block");
                $('#empImage')
                    .attr('src', e.target.result)
                    .width(150)
                    .height(150);
            };

            reader.readAsDataURL(input.files[0]);
        }
    }