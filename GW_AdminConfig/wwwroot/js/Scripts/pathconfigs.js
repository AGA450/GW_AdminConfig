    //$(document).ready(function () {
    //    $(".pathconfigEdit").click(function () {
    //        //alert("The paragraph was clicked.");
    //    });
    //});

function editItem(Category, Name) {
    $.ajax({
        type: "GET",
        url: "/PathConfig/Edit/",
        data: { Category: Category, Name: Name },
        dataType: 'json',
        success: function (result) {
            alert(result.Value);
            $("#itemCategory").val(result.Category);
        },
        error: function (error) {
            // Handle the error response
            console.error("Error editing item", error);
        }
    });
}

$('#editModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var category = button.data('category');
    var name = button.data('name');

    var modal = $(this);
    modal.find('.modal-body #Category').val(category);
    modal.find('.modal-body #Name').val(name);
});
//function editItem(Category, Name) {
//    //alert(Category);
//    $.ajax({
//        method: "GET",
//        url: "/PathConfig/Edit/",
//        data: { Category: Category, Name: Name },
//        contentType: 'application/json',
//        success: function (res) {
//            var obj = jQuery.parseJSON(res);
//            alert(res);
//        }
//    });
//}