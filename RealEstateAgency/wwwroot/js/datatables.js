var language;
var cultureinfo = document.getElementById('cultureinfo');

$(document).ready(function () {
    switch (cultureinfo.value) {
        case 'ro-RO':
            language = '//cdn.datatables.net/plug-ins/1.13.1/i18n/ro.json';
            break;
        case 'ru-RU':
            language = '//cdn.datatables.net/plug-ins/1.13.1/i18n/ru.json';
            break;
        default:
            language = '//cdn.datatables.net/plug-ins/1.13.1/i18n/en-US.json';
            break;
    }
});

$(document).ready(function () {
    $('#estateTable').DataTable({
        "scrollY": "450px",
        "scrollCollapse": true,
        "paging": true,
        "language": {
            "url": language
        }
    });
})