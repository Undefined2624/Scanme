
$(document).ready(function () {
    debugger;   
    $('body').on('click', '#CreateDuenoAlternativo', function () {
        debugger;
        var MascotaId = $(this).data('mascota-id');
        $('#exampleModal').modal('show');

        // obtenemos la vista parcial y la ponemos dentro del modal
        $.get(CrearDuenoAlternativo, { MascotaId: MascotaId }, function (data) {
            $('#contenidoModal').html(data);
        });
    });

    $('body').on('click', '#EditDuenoAlternativo', function () {
        debugger;
        var DuenoId = $(this).data('dueno-id');
        $('#exampleModal').modal('show');

        // obtenemos la vista parcial y la ponemos dentro del modal
        $.get(EditarDuenoAlternativo, { DuenoId: DuenoId }, function (data) {
            $('#contenidoModal').html(data);
        });
    });

    $('body').on('click', '#AdditionalInfo', function () {
        var MascotaId = $(this).data('mascota-id');
        $('#exampleModal').modal('show');

        // Obtenemos la vista parcial y la ponemos dentro del modal
        $.get(InformacionAdicional, { mascotaId: MascotaId }, function (data) {
            $('#contenidoModal').html(data);
        });
    });



});
