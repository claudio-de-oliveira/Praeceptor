//exporte les données sélectionnées
var $table = $('#table');
$(function () {
    $('#toolbar').find('select').change(function () {
        console.log('Cheguei ao módulo table.js');
        $table.bootstrapTable('refreshOptions', {
            exportDataType: $(this).val()
        });
    });
})

var trBoldBlue = $("table");

$(trBoldBlue).on("click", "tr", function () {
    $(this).toggleClass("bold-blue");
});
